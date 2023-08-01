using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeSystem.HR
{
    public class Researcher : Employee
    {
        public Researcher(int id, string first, string last, string em, DateTime bd, double rate) : base(id, first, last, em, bd, rate)
        {
        }


        public void ResearchNewPieTastes(int researchHours)
        {
            NumberOfHoursWorked += researchHours;
            Console.WriteLine($"Research {FirstName} {LastName} has invented a new pie taste!");
        }
    }

    public class JuniorResearcher : Researcher
    {
        public JuniorResearcher(int id, string first, string last, string em, DateTime bd, double rate) : base(id, first, last, em, bd, rate)
        {
        }


    }
}
