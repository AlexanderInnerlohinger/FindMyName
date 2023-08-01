using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeSystem.HR
{
    public interface IEmployee
    {
        double ReceiveWage();
        void GiveBonus();
        void PerformWork();
        void StopWorking();
        void DisplayEmployeeDetails();
        void GiveCompliment();
    }
}
