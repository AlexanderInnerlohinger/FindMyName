using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ___Skribbl_Console___
{
    public class MarkingJob
    {
        #region IMarkingJob

        public IEnumerable<MarkingCommand> MarkingCommands { get; set; }

        public string FilePath { get; set; }

        public int JobTimeout { get; set; }

        public bool ActivateInterventionBeforeStartJob { get; set; }

        public bool ActivateInterventionAfterFinishedJob { get; set; } = true;

        public double RepetitionRate { get; set; }

        public bool UseBidirectionalWriting { get; set; }

        public uint InstanceHandle { get; set; }

        public string SimulationFile { get; set; }

        public string Id { get; set; }

        #endregion
    }
}
