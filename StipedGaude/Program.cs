using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Xml;

namespace ___Skribbl_Console___
{
    public enum LineDirection
    {
        Undefined,
        RightToLeft,
        LeftToRigth,
        LeftToRight
    }

    public enum StripeDirection
    {
        UpToDown = -1,
        DownToUp = 1
    }
    
    internal class StripedRectangleCommandExtension
    {
        public static List<MarkingJob> GetJobs(StripedRectangleCommand command, double repetitionRate, bool useBidirectionalWriting, double offLength, bool invertX, bool invertY)
        {
            List<MarkingJob> jobs = new List<MarkingJob>();

            double power = command.LaserPowerPercentage;

            double rectStartX = command.Start.X * (invertX ? -1 : 1);
            double rectEndX = command.End.X * (invertX ? -1 : 1);
            double rectStartY = command.Start.Y * (invertY ? -1 : 1);
            double rectEndY = command.End.Y * (invertY ? -1 : 1);

            var spotToSpotDistance = 0.04;
            double directionFactor = rectStartY < rectEndY ? 1 : -1;

            bool invertLine = false;

            double fillDistance = command.FillDistance;
            if (command.FillingOption == StripeFillingOptions.HoneyComb)
            {
                fillDistance = spotToSpotDistance;
            }

            List<double> yLineCoordinates = new List<double>();
            for (double lineY = rectStartY; lineY * directionFactor <= rectEndY * directionFactor; lineY += fillDistance * directionFactor)
            {
                yLineCoordinates.Add(lineY);
            }

            List<double> shotPoints = new List<double>();
            List<double> shotPointsBi = new List<double>();
            for (double i = rectStartX; i <= rectEndX + offLength; i += spotToSpotDistance)
            {
                shotPoints.Add(i);
                if (i + spotToSpotDistance / 2 < rectEndX)
                    shotPointsBi.Add(i + spotToSpotDistance / 2);
            }

            int jobCount = 0;

            for (double stripeStart = rectStartX; stripeStart < rectEndX; stripeStart += command.StripeWidth)
            {
                if (useBidirectionalWriting)
                    yLineCoordinates.Reverse();

                jobCount++;
                MarkingJob job = new MarkingJob();
                job.RepetitionRate = repetitionRate;
                job.UseBidirectionalWriting = useBidirectionalWriting;
                job.ActivateInterventionBeforeStartJob = false;
                job.Id = "StripedCircleCommand_Stripe_" + jobCount;

                //job.PrePositioningPosition = new CartesianCoordinate(stripeStart + command.StripeWidth / 2.0, yLineCoordinates.FirstOrDefault() * (invertY ? -1 : 1), 0);

                var commands = new List<MarkingCommand>();

                bool firstCommandGenerated = false;

                double stripeEnd = stripeStart + command.StripeWidth;
                double lineStart = stripeStart;
                double lineEnd = stripeEnd;

                if (stripeEnd > rectEndX)
                    stripeEnd = rectEndX;

                foreach (var yCoordinate in yLineCoordinates)
                {
                    if (command.FillingOption == StripeFillingOptions.HoneyComb && invertLine)
                    {
                        foreach (var shotPoint in shotPointsBi)
                        {
                            if (stripeStart <= shotPoint)
                            {
                                lineStart = shotPoint;
                                lineEnd = shotPoint;

                                while (lineEnd + spotToSpotDistance < stripeEnd)
                                {
                                    lineEnd += spotToSpotDistance;
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (var shotPoint in shotPoints)
                        {
                            if (stripeStart <= shotPoint + offLength)
                            {
                                lineStart = shotPoint;
                                lineEnd = shotPoint;

                                while (lineEnd + spotToSpotDistance < stripeEnd)
                                {
                                    lineEnd += spotToSpotDistance;
                                }
                                break;
                            }
                        }
                    }

                    CalculateLine(new CartesianCoordinate(lineStart, yCoordinate, 0), new CartesianCoordinate(lineEnd, yCoordinate, 0), useBidirectionalWriting && invertLine, command.ScanSpeed, repetitionRate,
                                  out var startPoint, out var endPoint);

                    if (command.FillingOption == StripeFillingOptions.HoneyComb && invertLine)
                    {
                        startPoint.X += spotToSpotDistance / 2;
                        endPoint.X += spotToSpotDistance / 2;
                    }

                    var distance = CartesianCoordinate.GetDistanceTo(startPoint, endPoint);
                    uint switches = (uint)Math.Ceiling(distance / fillDistance);

                    switches = distance - switches * spotToSpotDistance + offLength > offLength ? switches : switches - 1; //last point can be marked

                    List<double> switchesPosition = new List<double>();

                    var currentValue = 0.0;

                    for (int i = 0; i <= switches; i++)
                    {
                        switchesPosition.Add(currentValue);
                        switchesPosition.Add(currentValue + offLength);

                        currentValue += fillDistance;
                    }

                    int direction = invertLine ? -1 : 1;
                    commands.Add(new DashedLineCommand(startPoint, endPoint + new CartesianCoordinate(offLength * direction, 0, 0), (uint)switchesPosition.Count, switchesPosition.ToArray(), power));

                    if (!firstCommandGenerated)
                    {
                        firstCommandGenerated = true;
                        //job.StartPosition = startPoint;
                    }

                    if (!yLineCoordinates.Last().Equals(yCoordinate))
                        invertLine = !invertLine;
                }

                //commands.Add(new JumpCommand(new CartesianCoordinate(job.PrePositioningPosition.X, yLineCoordinates.LastOrDefault(), 0)));
                //job.MarkingCommands = commands;
                jobs.Add(job);
            }

            return jobs;
        }

        private static void CalculateLine(CartesianCoordinate startPoint, CartesianCoordinate endPoint, bool biDirectional, double scanSpeed, double repetitionRate,
            out CartesianCoordinate calculatedStartPoint, out CartesianCoordinate calculatedEndPoint)
        {
            calculatedStartPoint = startPoint;
            calculatedEndPoint = endPoint;
            double lineLength;

            var spotToSpotDistance = 0.04;

            if (System.Math.Abs(endPoint.X - startPoint.X) > double.Epsilon)
            {
                lineLength = CalculateLineLengthBy10ThOfMicroSecond(endPoint.X - startPoint.X, scanSpeed);
                calculatedEndPoint.X = calculatedStartPoint.X + lineLength;

                if (biDirectional)
                {
                    int numberOfShots = (int)(lineLength / spotToSpotDistance);
                    calculatedStartPoint.X = startPoint.X + spotToSpotDistance * numberOfShots;
                    calculatedEndPoint.X = calculatedStartPoint.X - lineLength;
                }
            }
            else if (System.Math.Abs(endPoint.Y - startPoint.Y) > double.Epsilon)
            {
                lineLength = CalculateLineLengthBy10ThOfMicroSecond(endPoint.Y - startPoint.Y, scanSpeed);
                calculatedEndPoint.Y = calculatedStartPoint.Y + lineLength;

                if (biDirectional)
                {
                    int numberOfShots = (int)(lineLength / spotToSpotDistance);
                    calculatedStartPoint.Y = startPoint.Y + spotToSpotDistance * numberOfShots;
                    calculatedEndPoint.Y = calculatedStartPoint.Y - lineLength;
                }
            }
        }

        private static double CalculateLineLengthBy10ThOfMicroSecond(double length, double scanSpeed)
        {
            //Todo: Find out if it is still necessary to calculate length based on multiple of 10 milliseconds. For now just return the lenght.

            //double lineTimeSec = DataUnitSpecifier.Convert(length, DataUnitSpecifier.Millimeter, DataUnitSpecifier.Meter) / scanSpeed;
            //double lineTimeMicroSec = DataUnitSpecifier.Convert(lineTimeSec, DataUnitSpecifier.Second, DataUnitSpecifier.Microsecond);
            //int lineTimeIn10MicroSecond = (int)(lineTimeMicroSec / 10.0);
            //lineTimeSec = DataUnitSpecifier.Convert((double)lineTimeIn10MicroSecond * 10, DataUnitSpecifier.Microsecond, DataUnitSpecifier.Second);
            //var lineLength = scanSpeed * lineTimeSec;
            //lineLength = DataUnitSpecifier.Convert(lineLength, DataUnitSpecifier.Meter, DataUnitSpecifier.Millimeter);
            //return lineLength;
            return length;
        }
    }

    internal class StripedCircleCommandExtension
    {
        public static List<MarkingJob> GetJobsNewShit(StripedCircleCommand command, double repetitionRate, bool useBidirectionalWriting, double offLength, bool invertX, bool invertY)
        {
            List<MarkingJob> jobs = new List<MarkingJob>();

            double circleCenterX = command.Center.X * (invertX ? -1 : 1);

            bool invertLine = false;

            List<Line> sectionLinesHorizontal = GetHorizontalLines(command);
            double actualHeight = Math.Abs(Math.Abs(sectionLinesHorizontal[1].P0.Y) - Math.Abs(sectionLinesHorizontal[0].P0.Y));

            List<double> shotPoints = new List<double>();
            List<double> shotPointsBi = new List<double>();

            for (double i = circleCenterX - command.Radius; i <= circleCenterX + command.Radius + offLength; i += command.Pitch)
            {
                shotPoints.Add(i);
                if (i + command.Pitch / 2 < circleCenterX + command.Radius)
                    shotPointsBi.Add(i + command.Pitch / 2);
            }

            double power = command.LaserPowerPercentage;

            int jobCount = 0;

            for (double stripeStart = circleCenterX - command.Radius; stripeStart < circleCenterX + command.Radius; stripeStart += command.StripeWidth)
            {
                if (useBidirectionalWriting)
                    sectionLinesHorizontal.Reverse();

                jobCount++;
                MarkingJob job = new MarkingJob();
                job.RepetitionRate = repetitionRate;
                job.UseBidirectionalWriting = useBidirectionalWriting;
                job.ActivateInterventionBeforeStartJob = false;
                job.Id = "StripedCircleCommand_Stripe_" + jobCount;


                double stripeEnd = stripeStart + command.StripeWidth;
                bool lastStripe = Math.Abs(stripeEnd - command.Radius) < offLength;
                bool firstStripe = Math.Abs(stripeStart - -command.Radius) < offLength;

                var commands = new List<MarkingCommand>();

                bool firstCommandGenerated = false;

                foreach (var currentLine in sectionLinesHorizontal)
                {
                    int direction = invertLine ? -1 : 1;

                    var lineStart = currentLine.P0.X * direction;
                    var yCoordinate = currentLine.P0.Y;

                    if (Math.Abs(currentLine.P0.Y - currentLine.P1.Y) > offLength)
                        throw new ArgumentException("Y coordinates from horizontal lines do not match.");

                    //Todo: implement honeycomb pattern

                    var intersectionX = Math.Sqrt(Math.Pow(command.Radius, 2) - Math.Pow(yCoordinate, 2));
                    List<double> viableShotPointsAbsolute = new List<double>();

                    
                    
                    
                    
                    
                    if (command.FillingOption == StripeFillingOptions.HoneyComb && invertLine)
                    {
                        foreach (var pointX in shotPointsBi)
                        {
                            // At first check if point is in range of stripe
                            if (pointX >= stripeStart && pointX <= stripeEnd)
                            {
                                // Then see if point is on the left (negative) or the right side (positive) of the circle.
                                // According to this result see if point is in valid range of intersection in x axis.
                                switch (pointX)
                                {
                                    case var value when value >= 0:  // Point must be on the right (positive) side of the circle

                                        if (pointX <= intersectionX)
                                            viableShotPointsAbsolute.Add(pointX);

                                        break;

                                    case var value when value < 0:   // Point must be on the left (negative) side of the circle

                                        if (pointX >= -intersectionX)
                                            viableShotPointsAbsolute.Add(pointX);

                                        break;

                                    default:
                                        throw new ArgumentOutOfRangeException($"Argument {pointX} is out of expected range.");
                                }
                            }

                        }
                    }
                    else
                    {
                        foreach (var pointX in shotPoints)
                        {
                            // At first check if point is in range of stripe
                            if (pointX >= stripeStart && pointX <= stripeEnd)
                            {
                                // Then see if point is on the left (negative) or the right side (positive) of the circle.
                                // According to this result see if point is in valid range of intersection in x axis.
                                switch (pointX)
                                {
                                    case var value when value >= 0:  // Point must be on the right (positive) side of the circle

                                        if (pointX <= intersectionX)
                                            viableShotPointsAbsolute.Add(pointX);

                                        break;

                                    case var value when value < 0:   // Point must be on the left (negative) side of the circle

                                        if (pointX >= -intersectionX)
                                            viableShotPointsAbsolute.Add(pointX);

                                        break;

                                    default:
                                        throw new ArgumentOutOfRangeException($"Argument {pointX} is out of expected range.");
                                }
                            }

                        }
                    }
                    
                    
                    
                    

                    if (viableShotPointsAbsolute.Count == 0)
                        continue;

                    double currentStripeStart;
                    double currentStripeEnd;
                    LineDirection lineDirection;

                    if (lineStart < stripeEnd)
                    {
                        currentStripeStart = stripeStart;
                        currentStripeEnd = stripeEnd;
                        lineDirection = LineDirection.LeftToRigth;
                    }
                    else
                    {
                        currentStripeStart = stripeEnd;
                        currentStripeEnd = stripeStart;
                        lineDirection = LineDirection.RightToLeft;
                    }


                    List<double> switchesPosition = new List<double>();
                    switch (lineDirection)
                    {
                        case LineDirection.LeftToRigth:

                            foreach (double viableShotPoint in viableShotPointsAbsolute)
                            {
                                var shotPointWithOffset = (viableShotPoint * -1 + currentStripeStart) * -1;

                                switchesPosition.Add(shotPointWithOffset);
                                switchesPosition.Add(shotPointWithOffset + offLength);
                            }

                            commands.Add(new DashedLineCommand(
                                new CartesianCoordinate(currentStripeStart, yCoordinate, 0),
                                new CartesianCoordinate(currentStripeEnd + offLength, yCoordinate, 0),
                                (uint)switchesPosition.Count,
                                switchesPosition.ToArray(),
                                power));

                            break;

                        case LineDirection.RightToLeft:

                            viableShotPointsAbsolute.Reverse();
                            foreach (double viableShotPoint in viableShotPointsAbsolute)
                            {
                                var shotPointWithOffset = viableShotPoint * -1 + currentStripeStart;

                                switchesPosition.Add(shotPointWithOffset);
                                switchesPosition.Add(shotPointWithOffset + offLength);
                            }

                            commands.Add(new DashedLineCommand(
                                new CartesianCoordinate(currentStripeStart, yCoordinate, 0),
                                new CartesianCoordinate(currentStripeEnd - offLength, yCoordinate, 0),
                                (uint)switchesPosition.Count,
                                switchesPosition.ToArray(),
                                power));

                            break;

                        case LineDirection.Undefined:
                            throw new Exception($"Writing direction {lineDirection} is out of expected range.");
                    }

                    if (!firstCommandGenerated)
                    {
                        firstCommandGenerated = true;
                        //job.StartPosition = new CartesianCoordinate(lineStart, yCoordinate, 0.0);
                    }


                    if (!sectionLinesHorizontal.Last().Equals(currentLine))
                        invertLine = !invertLine;
                }

                List<MarkingCommand> preFeedCommands = GetPreFeed(command, commands, actualHeight);
                List<MarkingCommand> postFeedCommands = GetPostFeed(command, commands, actualHeight);

                commands.InsertRange(0, preFeedCommands);
                commands.InsertRange(commands.Count, postFeedCommands);


                // Determine preposition based on first DashedLineCommand
                var yCoordinatePreposition = commands.First() as DashedLineCommand;

                if (yCoordinatePreposition == null)
                    throw new ArgumentNullException($"No dashed line command at first position of command list found. Instead {commands.First()} was found.");

                //job.PrePositioningPosition = new CartesianCoordinate(stripeStart + command.StripeWidth / 2.0, yCoordinatePreposition.Start.Y, 0);


                // Determine final jump based on last DashedLineCommand
                var yCoordinatePostPosition = commands.Last() as DashedLineCommand;

                if (yCoordinatePostPosition == null)
                    throw new ArgumentNullException($"No dashed line command at first position of command list found. Instead {commands.Last()} was found.");

                //commands.Add(new JumpCommand(new CartesianCoordinate(job.PrePositioningPosition.X, yCoordinatePostPosition.End.Y, 0)));


                // At Last add job to list
                job.MarkingCommands = commands;
                jobs.Add(job);
            }

            return jobs;
        }
        
        private static List<MarkingCommand> GetPreFeed(StripedCircleCommand command, List<MarkingCommand> commandList, double actualPitch)
        {
            List<MarkingCommand> preFeedCommands = new List<MarkingCommand>();

            double fillDistance = 0;
            var firstRegularCommand = commandList.First() as DashedLineCommand;
            var lastRegularCommand = commandList.Last() as DashedLineCommand;

            if (firstRegularCommand == null)
                throw new ArgumentNullException($"No dashed line command at first position of command list found. Instead {commandList.First()} was found.");
            if (lastRegularCommand == null)
                throw new ArgumentNullException($"No dashed line command at first position of command list found. Instead {commandList.Last()} was found.");


            StripeDirection stripeDirection;

            if (firstRegularCommand.Start.Y > lastRegularCommand.Start.Y)
                stripeDirection = StripeDirection.UpToDown;
            else if (firstRegularCommand.Start.Y < lastRegularCommand.Start.Y)
                stripeDirection = StripeDirection.DownToUp;
            else
                throw new ArgumentException("Based on y coordinates of first and last dashed line no stripe direction could be determined." +
                                            $"First command: {firstRegularCommand.Start.Y}; Last command: {lastRegularCommand.Start.Y}");


            switch (command.FillingOption)
            {
                case StripeFillingOptions.Fill:
                     fillDistance = command.FillDistance;
                    break;
                case StripeFillingOptions.HoneyComb:
                    fillDistance = actualPitch;
                    break;
                default:
                    break;
            }

            int feedIterations = Convert.ToUInt16(command.PreFeedSize / fillDistance);
            bool alternateDirection = false;

            for (int i = 1; i <= feedIterations; i++)
            {
                if (alternateDirection)
                {
                    // Add dashed line from right to left
                    preFeedCommands.Add(new DashedLineCommand(
                                                              new CartesianCoordinate(firstRegularCommand.Start.X, firstRegularCommand.Start.Y - fillDistance * i * (int)stripeDirection, 0),
                                                              new CartesianCoordinate(firstRegularCommand.End.X, firstRegularCommand.End.Y - fillDistance * i * (int)stripeDirection, 0),
                                                              0,
                                                              Array.Empty<double>(),
                                                              0));
                }
                else
                {
                    // Add dashed line from right to left
                    preFeedCommands.Add(new DashedLineCommand(
                                                              new CartesianCoordinate(firstRegularCommand.End.X, firstRegularCommand.End.Y - fillDistance * i * (int)stripeDirection, 0),
                                                              new CartesianCoordinate(firstRegularCommand.Start.X, firstRegularCommand.Start.Y - fillDistance * i * (int)stripeDirection, 0),
                                                              0,
                                                              Array.Empty<double>(),
                                                              0));
                }

                alternateDirection = !alternateDirection;
            }

            preFeedCommands.Reverse();
            return preFeedCommands;
        }
        
        private static List<MarkingCommand> GetPostFeed(StripedCircleCommand command, List<MarkingCommand> commandList, double actualPitch)
        {
            List<MarkingCommand> postFeedCommands = new List<MarkingCommand>();

            double fillDistance = 0;
            var firstRegularCommand = commandList.First() as DashedLineCommand;
            var lastRegularCommand = commandList.Last() as DashedLineCommand;

            if (firstRegularCommand == null)
                throw new ArgumentNullException($"No dashed line command at first position of command list found. Instead {commandList.First()} was found.");
            if (lastRegularCommand == null)
                throw new ArgumentNullException($"No dashed line command at first position of command list found. Instead {commandList.Last()} was found.");


            StripeDirection stripeDirection;

            if (firstRegularCommand.Start.Y > lastRegularCommand.Start.Y)
                stripeDirection = StripeDirection.UpToDown;
            else if (firstRegularCommand.Start.Y < lastRegularCommand.Start.Y)
                stripeDirection = StripeDirection.DownToUp;
            else
                throw new ArgumentException("Based on y coordinates of first and last dashed line no stripe direction could be determined." +
                                            $"First command: {firstRegularCommand.Start.Y}; Last command: {lastRegularCommand.Start.Y}");


            switch (command.FillingOption)
            {
                case StripeFillingOptions.Fill:
                     fillDistance = command.FillDistance;
                    break;
                case StripeFillingOptions.HoneyComb:
                    fillDistance = actualPitch;
                    break;
                default:
                    break;
            }

            int feedIterations = Convert.ToUInt16(command.PostFeedSize / fillDistance);
            bool alternateDirection = false;

            for (int i = 1; i <= feedIterations; i++)
            {
                if (alternateDirection)
                {
                    // Add dashed line from right to left
                    postFeedCommands.Add(new DashedLineCommand(
                                                              new CartesianCoordinate(lastRegularCommand.Start.X, lastRegularCommand.Start.Y - fillDistance * i * (int)stripeDirection * -1, 0),
                                                              new CartesianCoordinate(lastRegularCommand.End.X, lastRegularCommand.End.Y - fillDistance * i * (int)stripeDirection* -1, 0),
                                                              0,
                                                              Array.Empty<double>(),
                                                              0));
                }
                else
                {
                    // Add dashed line from left to right
                    postFeedCommands.Add(new DashedLineCommand(
                                                              new CartesianCoordinate(lastRegularCommand.End.X, lastRegularCommand.End.Y - fillDistance * i * (int)stripeDirection* -1, 0),
                                                              new CartesianCoordinate(lastRegularCommand.Start.X, lastRegularCommand.Start.Y - fillDistance * i * (int)stripeDirection* -1, 0),
                                                              0,
                                                              Array.Empty<double>(),
                                                              0));
                }

                alternateDirection = !alternateDirection;
            }

            return postFeedCommands;
        }
        
        private static List<Line> GetHorizontalLines(StripedCircleCommand command)
        {
            List<Line> result = new List<Line>();

            var r = command.Radius;
            var d = command.FillDistance;

            if (command.FillingOption == StripeFillingOptions.HoneyComb)
                d = Math.Tan(60 * (Math.PI/180)) * (d / 2);   // calculate height of isosceles triangle based on angular functions
    
            double deltaY = 0;
            while (deltaY <= 2 * r)
            {
                var a = new CartesianCoordinate(-r, -r + deltaY, 0.0d);
                var b = new CartesianCoordinate(+r, -r + deltaY, 0.0d);

                result.Add(new Line(a, b));
                
                deltaY += d;
            }

            return result;
        }
        
        private static double CalculateLineLengthBy10ThOfMicroSecond(double length, double scanSpeed)
        {
            //Todo: Find out if it is still necessary to calculate length based on multiple of 10 milliseconds. For now just return the lenght.

            //double lineTimeSec = DataUnitSpecifier.Convert(length, DataUnitSpecifier.Millimeter, DataUnitSpecifier.Meter) / scanSpeed;
            //double lineTimeMicroSec = DataUnitSpecifier.Convert(lineTimeSec, DataUnitSpecifier.Second, DataUnitSpecifier.Microsecond);
            //int lineTimeIn10MicroSecond = (int)(lineTimeMicroSec / 10.0);
            //lineTimeSec = DataUnitSpecifier.Convert((double)lineTimeIn10MicroSecond * 10, DataUnitSpecifier.Microsecond, DataUnitSpecifier.Second);
            //var lineLength = scanSpeed * lineTimeSec;
            //lineLength = DataUnitSpecifier.Convert(lineLength, DataUnitSpecifier.Meter, DataUnitSpecifier.Millimeter);
            //return lineLength;
            return length;
        }
    }
    
    internal static class RectangleCommandExtensions
    {
        public static List<MarkingJob> GetJobsOriginal(RectangleCommand command, double repetitionRate, bool useBidirectionalWriting, double offLength, bool invertX, bool invertY)
        {
            List<MarkingJob> jobs = new List<MarkingJob>();

            double power = command.LaserPowerPercentage;

            MarkingJob job = new MarkingJob();
            job.RepetitionRate = repetitionRate;
            job.UseBidirectionalWriting = useBidirectionalWriting;
            job.ActivateInterventionBeforeStartJob = false;
            job.Id = "RectangleCommand";

            List<MarkingCommand> commands = new List<MarkingCommand>();

            double rectStartX = command.Start.X * (invertX ? -1 : 1);
            double rectEndX = command.End.X * (invertX ? -1 : 1);
            double rectStartY = command.Start.Y * (invertY ? -1 : 1);
            double rectEndY = command.End.Y * (invertY ? -1 : 1);
            
            //var spotToSpotDistance = CalculateSpotToSpotDistance(command.ScanSpeed, repetitionRate);
            var spotToSpotDistance = command.Pitch;
            bool firstCommandGenerated = false;

            switch (command.FillingOption)
            {
                case RectangleCommand.RectangleFillingOptions.Fill:
                case RectangleCommand.RectangleFillingOptions.HoneyComb:
                    {
                        //Take length between points and add starting point.
                        //job.PrePositioningPosition = new CartesianCoordinate(rectStartX + (rectEndX - rectStartX) /2, rectStartY, command.Start.Z);

                        double directionFactor = rectStartY < rectEndY ? 1 : -1;
                        double currentY = rectStartY;
                        double step = command.FillDistanceY * directionFactor;

                        if (command.FillingOption == RectangleCommand.RectangleFillingOptions.HoneyComb)
                            step = spotToSpotDistance * directionFactor;

                        if (step == 0)
                            step = directionFactor;

                        bool invertLine = false;

                        while (currentY * directionFactor <= rectEndY * directionFactor)
                        {
                            CalculateLine(new CartesianCoordinate(rectStartX, currentY, 0), new CartesianCoordinate(rectEndX, currentY, 0), useBidirectionalWriting && invertLine, command.Pitch, repetitionRate,
                                          out var startPoint, out var endPoint);

                            if (command.FillingOption == RectangleCommand.RectangleFillingOptions.HoneyComb && invertLine)
                            {
                                startPoint.Y += spotToSpotDistance / 2;
                                endPoint.Y += spotToSpotDistance / 2;
                            }

                            var distance = CartesianCoordinate.GetDistanceTo(startPoint, endPoint);
                            uint switches = (uint)System.Math.Floor(distance / spotToSpotDistance);

                            switches = distance - switches * spotToSpotDistance + offLength >= offLength ? switches : switches - 1; //last point can be marked

                            List<double> switchesPosition = new List<double>();

                            var currentValue = 0.0;

                            for (int i = 0; i <= switches; i++)
                            {
                                switchesPosition.Add(currentValue);
                                switchesPosition.Add(currentValue + offLength);

                                currentValue += spotToSpotDistance;
                            }

                            int direction = invertLine ? -1 : 1;
                            commands.Add(new DashedLineCommand(startPoint, endPoint + new CartesianCoordinate(offLength * direction, 0,0), (uint)switchesPosition.Count, switchesPosition.ToArray(), power));

                            invertLine = !invertLine;

                            currentY += step;
                        }

                        break;
                    }
            }

            // At the end of the rectangle command add a JumpCommand.
            // Position is the center of the current rectangle, so the PrePositioningPosition in x direction.
            // In y direction we just decide to move to the upper edge of the rectangle.
            // This whole process is issued so the stage does not move to the last marked point, which would be typically at the edge of the rectangle.
            //commands.Add(new JumpCommand(new CartesianCoordinate(job.PrePositioningPosition.X, rectEndY, 0)));
            job.MarkingCommands = commands;
            jobs.Add(job);

            return jobs;
        }
        
        public static List<MarkingJob> GetJobsNEW(this RectangleCommand command, double repetitionRate, bool useBidirectionalWriting, double offLength, bool invertX, bool invertY)
        {
            List<MarkingJob> jobs = new List<MarkingJob>();
            bool invertLine = false;

            double rectStartX = command.Start.X * (invertX ? -1 : 1);
            double rectEndX = command.End.X * (invertX ? -1 : 1);
            double rectStartY = command.Start.Y * (invertY ? -1 : 1);
            double rectEndY = command.End.Y * (invertY ? -1 : 1);

            List<Line> sectionLinesHorizontal = GetHorizontalLines(command, invertY);
            double actualHeigth = Math.Abs(Math.Abs(sectionLinesHorizontal[1].P0.Y) - Math.Abs(sectionLinesHorizontal[0].P0.Y));
            double lengthRectangle = Math.Abs(Math.Abs(rectStartX) - Math.Abs(rectEndX));

            // Determine all possible shot points
            List<double> shotPoints = new List<double>();
            List<double> shotPointsBi = new List<double>();

            for (double i = 0; i <= lengthRectangle; i += command.Pitch)
            {
                shotPoints.Add(i);
                if (i + command.Pitch / 2 < lengthRectangle)
                    shotPointsBi.Add(i + command.Pitch / 2);
            }

            double power = command.LaserPowerPercentage;

            MarkingJob job = new MarkingJob();
            job.RepetitionRate = repetitionRate;
            job.UseBidirectionalWriting = useBidirectionalWriting;
            job.ActivateInterventionBeforeStartJob = false;
            job.Id = "RectangleCommand";

            List<MarkingCommand> commands = new List<MarkingCommand>();


            bool firstCommandGenerated = false;


            foreach (Line currentLine in sectionLinesHorizontal)
            {
                LineDirection lineDirection = invertLine ? LineDirection.RightToLeft : LineDirection.LeftToRight;
                if (Math.Abs(currentLine.P0.Y - currentLine.P1.Y) > offLength)
                    throw new ArgumentException("Y coordinates from horizontal lines do not match.");

                List<double> viableShotPoints = new List<double>();


                switch (command.FillingOption)
                {
                    case RectangleCommand.RectangleFillingOptions.HoneyComb:
                    case RectangleCommand.RectangleFillingOptions.Fill:

                        if (invertLine)
                            foreach (double pointX in shotPointsBi)
                                viableShotPoints.Add(pointX);

                        else
                            foreach (double pointX in shotPoints)
                                viableShotPoints.Add(pointX);

                        break;
                    default:
                        throw new InvalidEnumArgumentException($"Filling option {command.FillingOption} is not supported.");
                }

                if (viableShotPoints.Count == 0)
                    continue;


                List<double> switchesPosition = new List<double>();
                switch (lineDirection)
                {
                    case LineDirection.LeftToRight:

                        foreach (double viableShotPoint in viableShotPoints)
                        {
                            switchesPosition.Add(viableShotPoint);
                            switchesPosition.Add(viableShotPoint + offLength);
                        }

                        commands.Add(new DashedLineCommand(
                            new CartesianCoordinate(currentLine.P0.X, currentLine.P0.Y, 0),
                            new CartesianCoordinate(currentLine.P1.X + offLength, currentLine.P1.Y, 0),
                            (uint)switchesPosition.Count,
                            switchesPosition.ToArray(),
                            power));

                        break;

                    case LineDirection.RightToLeft:

                        viableShotPoints.Reverse();
                        foreach (double viableShotPoint in viableShotPoints)
                        {
                            var viableShotPointOffset = Math.Abs(viableShotPoint - lengthRectangle);
                            switchesPosition.Add(viableShotPointOffset);
                            switchesPosition.Add(viableShotPointOffset + offLength);
                        }

                        commands.Add(new DashedLineCommand(
                            new CartesianCoordinate(currentLine.P1.X, currentLine.P1.Y, 0),
                            new CartesianCoordinate(currentLine.P0.X - offLength, currentLine.P0.Y, 0),
                            (uint)switchesPosition.Count,
                            switchesPosition.ToArray(),
                            power));

                        break;

                    case LineDirection.Undefined:
                        throw new InvalidEnumArgumentException($"Writing direction {lineDirection} is out of expected range.");
                }

                if (!firstCommandGenerated)
                {
                    firstCommandGenerated = true;
                    //job.StartPosition = new CartesianCoordinate(currentLine.P0.X, currentLine.P0.Y, 0.0);
                }


                if (!sectionLinesHorizontal[sectionLinesHorizontal.Count - 1].Equals(currentLine))
                    invertLine = !invertLine;
            }


            List<MarkingCommand> preFeedCommands = GetPreFeed(command, commands, actualHeigth);
            List<MarkingCommand> postFeedCommands = GetPostFeed(command, commands, actualHeigth);

            commands.InsertRange(0, preFeedCommands);
            commands.InsertRange(commands.Count, postFeedCommands);


            // Determine preposition based on first DashedLineCommand
            var yCoordinatePreposition = commands.First() as DashedLineCommand;

            if (yCoordinatePreposition == null)
                throw new ArgumentNullException($"No dashed line command at first position of command list found. Instead {commands.First()} was found.");

            //job.PrePositioningPosition = new CartesianCoordinate((rectEndX - rectStartX) / 2.0, yCoordinatePreposition.Start.Y, 0);


            // Determine final jump based on last DashedLineCommand
            var yCoordinatePostPosition = commands.Last() as DashedLineCommand;

            if (yCoordinatePostPosition == null)
                throw new ArgumentNullException($"No dashed line command at first position of command list found. Instead {commands.Last()} was found.");


            // At the end of the rectangle command add a JumpCommand.
            // Position is the center of the current rectangle, so the PrePositioningPosition in x direction.
            // In y direction we just decide to move to the upper edge of the rectangle.
            // This whole process is issued so the stage does not move to the last marked point, which would be typically at the edge of the rectangle.
            //commands.Add(new JumpCommand(new CartesianCoordinate(job.PrePositioningPosition.X, rectEndY, 0)));
            job.MarkingCommands = commands;
            jobs.Add(job);

            return jobs;
        }

        private static double CalculateSpotToSpotDistance(double scanSpeed, double repetitionRate)
        {
            return scanSpeed / repetitionRate;
        }

        private static void CalculateLine(CartesianCoordinate startPoint, CartesianCoordinate endPoint, bool biDirectional, double pitch, double repetitionRate,
            out CartesianCoordinate calculatedStartPoint, out CartesianCoordinate calculatedEndPoint)
        {
            calculatedStartPoint = startPoint;
            calculatedEndPoint = endPoint;
            double lineLength;

            var spotToSpotDistance = pitch;

            if (System.Math.Abs(endPoint.X - startPoint.X) > double.Epsilon)
            {
                lineLength = CalculateLineLengthBy10ThOfMicroSecond(endPoint.X - startPoint.X);
                calculatedEndPoint.X = calculatedStartPoint.X + lineLength;

                if (biDirectional)
                {
                    int numberOfShots = (int)(lineLength / spotToSpotDistance);
                    calculatedStartPoint.X = startPoint.X + spotToSpotDistance * numberOfShots;
                    calculatedEndPoint.X = calculatedStartPoint.X - lineLength;
                }
            }
            else if (System.Math.Abs(endPoint.Y - startPoint.Y) > double.Epsilon)
            {
                lineLength = CalculateLineLengthBy10ThOfMicroSecond(endPoint.Y - startPoint.Y);
                calculatedEndPoint.Y = calculatedStartPoint.Y + lineLength;

                if (biDirectional)
                {
                    int numberOfShots = (int)(lineLength / spotToSpotDistance);
                    calculatedStartPoint.Y = startPoint.Y + spotToSpotDistance * numberOfShots;
                    calculatedEndPoint.Y = calculatedStartPoint.Y - lineLength;
                }
            }
        }

        private static double CalculateLineLengthBy10ThOfMicroSecond(double length)
        {
            /*
            double lineTimeSec = DataUnitSpecifier.Convert(length, DataUnitSpecifier.Millimeter, DataUnitSpecifier.Meter) / scanSpeed;
            double lineTimeMicroSec = DataUnitSpecifier.Convert(lineTimeSec, DataUnitSpecifier.Second, DataUnitSpecifier.Microsecond);
            int lineTimeIn10MicroSecond = (int)(lineTimeMicroSec / 10.0);
            lineTimeSec = DataUnitSpecifier.Convert((double)lineTimeIn10MicroSecond * 10, DataUnitSpecifier.Microsecond, DataUnitSpecifier.Second);
            var lineLength = scanSpeed * lineTimeSec;
            lineLength = DataUnitSpecifier.Convert(lineLength, DataUnitSpecifier.Meter, DataUnitSpecifier.Millimeter);
            */
            return length;
        }
        
        private static List<MarkingCommand> GetPreFeed(RectangleCommand command, List<MarkingCommand> commandList, double actualHeight)
        {
            List<MarkingCommand> preFeedCommands = new List<MarkingCommand>();

            double fillDistance;
            var firstRegularCommand = commandList.First() as DashedLineCommand;
            var lastRegularCommand = commandList.Last() as DashedLineCommand;

            if (firstRegularCommand == null)
                throw new ArgumentNullException($"No dashed line command at first position of command list found. Instead {commandList.First()} was found.");
            if (lastRegularCommand == null)
                throw new ArgumentNullException($"No dashed line command at first position of command list found. Instead {commandList.Last()} was found.");


            StripeDirection stripeDirection;

            if (firstRegularCommand.Start.Y > lastRegularCommand.Start.Y)
                stripeDirection = StripeDirection.UpToDown;
            else if (firstRegularCommand.Start.Y < lastRegularCommand.Start.Y)
                stripeDirection = StripeDirection.DownToUp;
            else
                throw new ArgumentException("Based on y coordinates of first and last dashed line no stripe direction could be determined." +
                                            $"First command: {firstRegularCommand.Start.Y}; Last command: {lastRegularCommand.Start.Y}");


            switch (command.FillingOption)
            {
                case RectangleCommand.RectangleFillingOptions.Fill:
                    fillDistance = command.FillDistanceY;
                    break;
                case RectangleCommand.RectangleFillingOptions.HoneyComb:
                    fillDistance = actualHeight;
                    break;
                default:
                    throw new InvalidEnumArgumentException($"Filling option {command.FillingOption} is not supported.");
            }

            int feedIterations = Convert.ToUInt16(command.PreFeedSize / fillDistance);
            bool alternateDirection = false;
            double[] emptyLaserArray = new double[2];   // for laser to function an array with two zeros (LaserON & LaserOFF) must be passed on

            for (int i = 1; i <= feedIterations; i++)
            {
                if (alternateDirection)
                {
                    // Add dashed line from left to right
                    preFeedCommands.Add(new DashedLineCommand(
                                                              new CartesianCoordinate(firstRegularCommand.Start.X, firstRegularCommand.Start.Y - fillDistance * i * (int)stripeDirection, 0),
                                                              new CartesianCoordinate(firstRegularCommand.End.X, firstRegularCommand.End.Y - fillDistance * i * (int)stripeDirection, 0),
                                                              (uint)emptyLaserArray.Length,
                                                              emptyLaserArray,
                                                              command.LaserPowerPercentage));
                }
                else
                {
                    // Add dashed line from right to left
                    preFeedCommands.Add(new DashedLineCommand(
                                                              new CartesianCoordinate(firstRegularCommand.End.X, firstRegularCommand.End.Y - fillDistance * i * (int)stripeDirection, 0),
                                                              new CartesianCoordinate(firstRegularCommand.Start.X, firstRegularCommand.Start.Y - fillDistance * i * (int)stripeDirection, 0),
                                                              (uint)emptyLaserArray.Length,
                                                              emptyLaserArray,
                                                              command.LaserPowerPercentage));
                }

                alternateDirection = !alternateDirection;
            }

            preFeedCommands.Reverse();
            return preFeedCommands;
        }

        private static List<MarkingCommand> GetPostFeed(RectangleCommand command, List<MarkingCommand> commandList, double actualHeight)
        {
            List<MarkingCommand> postFeedCommands = new List<MarkingCommand>();

            double fillDistance;
            var firstRegularCommand = commandList.First() as DashedLineCommand;
            var lastRegularCommand = commandList.Last() as DashedLineCommand;

            if (firstRegularCommand == null)
                throw new ArgumentNullException($"No dashed line command at first position of command list found. Instead {commandList.First()} was found.");
            if (lastRegularCommand == null)
                throw new ArgumentNullException($"No dashed line command at first position of command list found. Instead {commandList.Last()} was found.");


            StripeDirection stripeDirection;

            if (firstRegularCommand.Start.Y > lastRegularCommand.Start.Y)
                stripeDirection = StripeDirection.UpToDown;
            else if (firstRegularCommand.Start.Y < lastRegularCommand.Start.Y)
                stripeDirection = StripeDirection.DownToUp;
            else
                throw new ArgumentException("Based on y coordinates of first and last dashed line no stripe direction could be determined." +
                                            $"First command: {firstRegularCommand.Start.Y}; Last command: {lastRegularCommand.Start.Y}");


            switch (command.FillingOption)
            {
                case RectangleCommand.RectangleFillingOptions.Fill:
                    fillDistance = command.FillDistanceY;
                    break;
                case RectangleCommand.RectangleFillingOptions.HoneyComb:
                    fillDistance = actualHeight;
                    break;
                default:
                    throw new InvalidEnumArgumentException($"Filling option {command.FillingOption} is not supported.");
            }

            int feedIterations = Convert.ToUInt16(command.PostFeedSize / fillDistance);
            bool alternateDirection = false;
            double[] emptyLaserArray = new double[2];   // for laser to function an array with two zeros (LaserON & LaserOFF) must be submitted

            for (int i = 1; i <= feedIterations; i++)
            {
                if (alternateDirection)
                {
                    // Add dashed line from left to right
                    postFeedCommands.Add(new DashedLineCommand(
                                                               new CartesianCoordinate(lastRegularCommand.Start.X, lastRegularCommand.Start.Y - fillDistance * i * (int)stripeDirection * -1, 0),
                                                               new CartesianCoordinate(lastRegularCommand.End.X, lastRegularCommand.End.Y - fillDistance * i * (int)stripeDirection * -1, 0),
                                                               (uint)emptyLaserArray.Length,
                                                               emptyLaserArray,
                                                               command.LaserPowerPercentage));
                }
                else
                {
                    // Add dashed line from right to left
                    postFeedCommands.Add(new DashedLineCommand(
                                                               new CartesianCoordinate(lastRegularCommand.End.X, lastRegularCommand.End.Y - fillDistance * i * (int)stripeDirection * -1, 0),
                                                               new CartesianCoordinate(lastRegularCommand.Start.X, lastRegularCommand.Start.Y - fillDistance * i * (int)stripeDirection * -1, 0),
                                                               (uint)emptyLaserArray.Length,
                                                               emptyLaserArray,
                                                               command.LaserPowerPercentage));
                }

                alternateDirection = !alternateDirection;
            }

            return postFeedCommands;
        }

        private static List<Line> GetHorizontalLines(RectangleCommand command, bool invertY)
        {
            List<Line> result = new List<Line>();

            double rectStartY = command.Start.Y * (invertY ? -1 : 1);
            double rectEndY = command.End.Y * (invertY ? -1 : 1);

            // Determine alignment of rectangle
            StripeDirection verticalDirection;
            if (rectStartY < rectEndY)
                verticalDirection = StripeDirection.DownToUp;
            else
                verticalDirection = StripeDirection.UpToDown;

            var verticalDistance = command.FillDistanceY;
            if (command.FillingOption == RectangleCommand.RectangleFillingOptions.HoneyComb)
                verticalDistance = Math.Tan(60 * (Math.PI / 180)) * verticalDistance / 2;   // calculate height of isosceles triangle based on angular functions

            double deltaY = 0;
            while (Math.Abs(deltaY) <= Math.Abs(Math.Abs(rectStartY) - Math.Abs(rectEndY)))
            {
                var a = new CartesianCoordinate(command.Start.X, rectStartY + deltaY, 0.0d);
                var b = new CartesianCoordinate(command.End.X, rectStartY + deltaY, 0.0d);

                result.Add(new Line(a, b));

                deltaY += verticalDistance * (int)verticalDirection;
            }

            return result;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // StripedRectangle
            StripedRectangleCommand stripedRectangleCommand = new StripedRectangleCommand();
            stripedRectangleCommand.Start = new CartesianCoordinate(0, 0, 0);
            stripedRectangleCommand.End = new CartesianCoordinate(5, 10, 0);
            stripedRectangleCommand.LaserPowerPercentage = 55;
            stripedRectangleCommand.FillDistance = 0.04;
            stripedRectangleCommand.FillingOption = StripeFillingOptions.Fill;
            stripedRectangleCommand.StripeWidth = 2;
            
            double repetitionRate = 500;
            bool useBidirectionalWriting = true;
            double offLenght = 0.001;
            bool invertX = false;
            bool invertY = true;

            List<MarkingJob> stripedRectangleJobs = StripedRectangleCommandExtension.GetJobs(stripedRectangleCommand, repetitionRate, useBidirectionalWriting, offLenght, invertX, invertY);

            // Rectangle
            RectangleCommand rectangleCommand = new RectangleCommand();
            rectangleCommand.Start = new CartesianCoordinate(-11.85, -10.5, 0);
            rectangleCommand.End = new CartesianCoordinate(-20, -21.55, 0);
            rectangleCommand.FillDistanceY = 1;
            rectangleCommand.FillingOption = RectangleCommand.RectangleFillingOptions.Fill;
            rectangleCommand.LaserPowerPercentage = 66;
            rectangleCommand.Pitch = 1;
            rectangleCommand.PreFeedSize = 2;
            rectangleCommand.PostFeedSize = 3;

            List<MarkingJob> rectangleJobs = RectangleCommandExtensions.GetJobsOriginal(rectangleCommand, repetitionRate: 500, useBidirectionalWriting: true,
                offLength: 0.001, invertX: false, invertY: true);
            
            List<MarkingJob> rectangleJobsNEW = RectangleCommandExtensions.GetJobsNEW(rectangleCommand, repetitionRate: 500, useBidirectionalWriting: true,
                offLength: 0.001, invertX: false, invertY: true);
            
            // StripedCircle
            StripedCircleCommand stripedCircleCommand = new StripedCircleCommand();
            stripedCircleCommand.Center = new CartesianCoordinate(0, 0, 0);
            stripedCircleCommand.Radius = 4.5;
            stripedCircleCommand.FillDistance = 0.1;
            stripedCircleCommand.FillingOption = StripeFillingOptions.HoneyComb;
            stripedCircleCommand.StripeWidth = 1.5;
            stripedCircleCommand.Pitch = 0.1;
            stripedCircleCommand.LaserPowerPercentage = 55;
            stripedCircleCommand.PreFeedSize = 0;
            stripedCircleCommand.PostFeedSize = 0;

            List<MarkingJob> stripedCircleJobs = StripedCircleCommandExtension.GetJobsNewShit(stripedCircleCommand, repetitionRate, useBidirectionalWriting, offLenght, invertX, invertY = true);
        }
    }

}