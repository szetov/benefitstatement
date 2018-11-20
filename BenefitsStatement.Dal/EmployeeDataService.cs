using BenefitStatement.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BenefitStatement.Dal
{
    public interface IEmployeeDataServcie
    {
        /// <summary>
        /// Get an employee's data by the unique employee ID
        /// </summary>
        /// <param name="empID">a unique Employee ID</param>
        /// <returns>A DataRow representing an eployee</returns>
        DataRow GetEmployee(string empID);
        DataTable GetAllEmployees();
    }

    public class DummyEmployeeDataService : IEmployeeDataServcie
    {
        private DataTable AllEmployees;

        public DummyEmployeeDataService()
        {
            //Initialize with dummy data
            this.AllEmployees = new DataTable();
            this.AllEmployees.Columns.Add(DBConstants.ColName.EmpID, typeof(string));
            this.AllEmployees.Columns.Add(DBConstants.ColName.EmpFirstName, typeof(string));
            this.AllEmployees.Columns.Add(DBConstants.ColName.EmpLastName, typeof(string));
            this.AllEmployees.Columns.Add(DBConstants.ColName.EmpJobTitleCode, typeof(string));
            this.AllEmployees.Columns.Add(DBConstants.ColName.EmpCountryCode, typeof(string));
            this.AllEmployees.Columns.Add(DBConstants.ColName.EmpHireDate, typeof(DateTime));
            this.AllEmployees.Columns.Add(DBConstants.ColName.EmpAnnualBase, typeof(decimal));
            this.AllEmployees.Columns.Add(DBConstants.ColName.EmpAboveBaseComp, typeof(decimal));
            this.AllEmployees.Columns.Add(DBConstants.ColName.EmpCurrency, typeof(string));

            this.AllEmployees.Rows.Add(new object[]
            {
                "98228",
                "Ikuna",
                "Kodou",
                Constants.JobTitle.AS,
                Constants.Country.Japan,
                new DateTime(2017,1,4),
                2010000m,
                78000m,
                "JPY"
            });
            this.AllEmployees.Rows.Add(new object[]
            {
                "72CB9",
                "Patsy",
                "Kertzmann",
                Constants.JobTitle.ED,
                Constants.Country.Singapore,
                new DateTime(1998,8,12),
                999999m,
                1m,
                "SGD"
            });
        }

        public DataTable GetAllEmployees()
        {
            DataTable result = this.AllEmployees.Copy();
            return result;
        }

        public DataRow GetEmployee(string empID)
        {
            string whereClause = $"{DBConstants.ColName.EmpID} = '{empID}'";
            DataRow[] tmp = GetAllEmployees().Select(whereClause);
            DataRow result = tmp.Length == 1 ? tmp[0] : null;
            return result;
        }
    }
}
