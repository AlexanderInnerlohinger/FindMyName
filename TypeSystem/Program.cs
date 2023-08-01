using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.InteropServices.ComTypes;
using TypeSystem.Accounting;
using TypeSystem.HR;

namespace BethanysPieShopHRM
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager bethany = new Manager(55156, "Bethany", "Smith", "bethany@snowball.be", new DateTime(1979,1,16), 25 );

            Manager mary = new Manager(748,"Mary", "Jones", "mary@snowball.be", new DateTime(1965, 1,16),30 );

            StoreManager kevin = new StoreManager(11231, "Kevin", "Marks", "kevin@snowball.be", new DateTime(1953,12,12),10 );

            StoreManager kate = new StoreManager(81131,"Kate", "Greggs", "kate@snowball.be", new DateTime(1993, 8,8),10 );

            JuniorResearcher bobJunior = new JuniorResearcher(100,"Bob", "Spencer", "bob@snowball.be", new DateTime(1988, 1, 23), 17);

            List<Employee> employees = new List<Employee>();
            employees.Add(bethany);
            employees.Add(mary);
            employees.Add(kevin);
            employees.Add(kate);
            employees.Add(bobJunior);

            employees.Sort();

            foreach (var employee in employees)
            {
                employee.DisplayEmployeeDetails();
            }


            //Employee[] employees = new Employee[5];
            //employees[0] = bethany;
            //employees[1] = mary;
            //employees[2] = bobJunior;
            //employees[3] = kevin;
            //employees[4] = kate;

            //foreach (Employee employee in employees)
            //{
            //    employee.PerformWork();
            //    employee.ReceiveWage();
            //    employee.DisplayEmployeeDetails();
            //    employee.GiveBonus();
            //    Console.WriteLine(employee.ToString());
            //}

            

            Console.ReadLine();
        }
    }
}

