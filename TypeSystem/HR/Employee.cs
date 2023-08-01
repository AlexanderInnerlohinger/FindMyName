using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeSystem.HR
{
    public abstract class Employee : IEmployee, IComparable
    {

        private int id;
        private string firstName;
        private string lastName;
        private string email;

        private int numberOfHoursWorked;
        private double wage;
        private double hourlyRate;
        private DateTime birthday;

        public static double taxRate = 0.15;
        private const double maxAmountHoursWorked = 1000;

        public int Id { get; set; }
        public string FirstName
        {
            get { return firstName;}
            set { firstName = value; }
        }

        public string LastName { get; set; }
        public string Email { get; set; }
        public int NumberOfHoursWorked { get; set; }
        public double Wage
        {
            get {return wage;}
            set {
                if (value < 0)
                {
                    wage = 0;
                }
                else
                {
                    wage = value;
                }
            }
        }
        public double HourlyRate { get; set; }
        public DateTime Birthday { get; set; }

        public Employee(int id, string first, string last, string em, DateTime bd, double rate)
        {
            Id = id;
            FirstName = first;
            LastName = last;
            Email = em;
            Birthday = bd;
            HourlyRate = rate;
        }

        public Employee(int id, string first, string last, string em, DateTime bd) : this(id, first, last, em, bd, 0)
        {
            
        }

        public void PerformWork()
        {
            NumberOfHoursWorked++;

            Console.WriteLine($"{FirstName} {LastName} is now working!");
        }

        public void StopWorking()
        {
            Console.WriteLine($"{FirstName} {LastName} has stopped working!");
        }

        public double ReceiveWage()
        {
            double wageBeforeTax = NumberOfHoursWorked * HourlyRate;
            double taxAmount = wageBeforeTax * taxRate;

            Wage = wageBeforeTax - taxAmount;

            Console.WriteLine($"The wage for {NumberOfHoursWorked} hours of work is {Wage}.");
            NumberOfHoursWorked = 0;

            return Wage;
        }

        public void DisplayEmployeeDetails()
        {
            Console.WriteLine($"\nEmployee ID: {Id}\nFirst name: {FirstName}\nLast name: {LastName}\nEmail: {Email}\nBirthday: {Birthday.ToShortDateString()}\nTax rate: {taxRate}");
        }

        public static void DisplayTaxRate()
        {
            Console.WriteLine($"The current tax rate is {taxRate}");
        }

        public virtual void GiveBonus()
        {
            Console.WriteLine($"{FirstName} {LastName} received a generic bonus of 100!");
        }

        public void GiveCompliment()
        {
            Console.WriteLine($"You've done a great job, {FirstName}.");
        }

        public int CompareTo(object obj)
        {
            var otherEmployee = (Employee) obj;
            if (Id > otherEmployee.Id)
            {
                return 1;
            }
            else if (Id < otherEmployee.Id)
            {
                return -1;

            }
            else
            {
                return 0;
            }
        }
    }
}
