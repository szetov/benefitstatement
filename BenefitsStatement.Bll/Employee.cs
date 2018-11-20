using System;
using System.Collections.Generic;
using System.Text;

namespace BenefitStatement.Bll
{
    public class Employee
    {
        /// <summary>
        /// A unique employee ID
        /// </summary>
        public string ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string DisplayName { get { return $"{FirstName} {LastName}"; } }
        public string Title { get; set; }
        public DateTime HireDate { get; set; }
        public string WorkLocation { get; set; }
        public decimal AnnualBaseSalary { get; set; }
        public decimal AboveBaseCompensation { get; set; }
        public string Currency { get; set; }
        public decimal TotalReward { get { return AnnualBaseSalary + AboveBaseCompensation; } }

        public static readonly Employee Null = new Employee()
        {
            ID = "",
            LastName = "",
            FirstName = "",
            Title = "",
            HireDate = new DateTime(1900, 1, 1),
            WorkLocation = "",
            AnnualBaseSalary = 0m,
            AboveBaseCompensation = 0m,
            Currency = ""
        };
    }
}
