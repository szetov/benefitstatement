using BenefitStatement.Dal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BenefitStatement.Bll
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Gets all employees
        /// </summary>
        /// <returns></returns>
        List<Employee> GetAll();
        /// <summary>
        /// Gets an employee by the employee's unique ID
        /// </summary>
        /// <param name="id">unique ID of the employee</param>
        /// <returns>an employee object.  A Employee.Null object will be returned if no record is found</returns>
        Employee GetByEmployeeID(string id);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private IEmployeeDataServcie EmpDataService;

        public EmployeeRepository(IEmployeeDataServcie dataService) 
            => this.EmpDataService = dataService ?? throw new ArgumentNullException("dataService");

        public List<Employee> GetAll()
        {
            DataTable data = this.EmpDataService.GetAllEmployees();
            List<Employee> result = ConvertEmpDataTableToList(data);
            return result;
        }

        public Employee GetByEmployeeID(string id)
        {
            Employee result = Employee.Null;
            DataRow data = this.EmpDataService.GetEmployee(id);
            if (data != null)
            {
                result = ConvertDataRowToEmployee(data);
            }
            return result;
        }

        /// <summary>
        /// Perform ORM to convert a DataRow to an Employee object
        /// </summary>
        /// <param name="row">A DataRow representing an employee</param>
        /// <returns>an Employee object</returns>
        private Employee ConvertDataRowToEmployee(DataRow row)
        {
            if (row == null)
            {
                throw new ArgumentNullException("r");
            }
            Employee result = new Employee()
            {
                ID = row.Field<string>(DBConstants.ColName.EmpID),
                FirstName = row.Field<string>(DBConstants.ColName.EmpFirstName),
                LastName = row.Field<string>(DBConstants.ColName.EmpLastName),
                Title = row.Field<string>(DBConstants.ColName.EmpJobTitleCode),
                HireDate = row.Field<DateTime>(DBConstants.ColName.EmpHireDate),
                WorkLocation = row.Field<string>(DBConstants.ColName.EmpCountryCode),
                AnnualBaseSalary = row.Field<decimal>(DBConstants.ColName.EmpAnnualBase),
                AboveBaseCompensation = row.Field<decimal>(DBConstants.ColName.EmpAboveBaseComp),
                Currency = row.Field<string>(DBConstants.ColName.EmpCurrency)
            };
            return result;
        }

        /// <summary>
        /// Convert a DataTable containing employees into a generic List of Employee object
        /// </summary>
        /// <param name="empTable">a DataTable containing employee information</param>
        /// <returns>a generic List of Employee objects</returns>
        private List<Employee> ConvertEmpDataTableToList(DataTable empTable)
        {
            List<Employee> result = (from e in empTable.AsEnumerable()
                                     select new Employee()
                                     {
                                         ID = e.Field<string>(DBConstants.ColName.EmpID),
                                         FirstName = e.Field<string>(DBConstants.ColName.EmpFirstName),
                                         LastName = e.Field<string>(DBConstants.ColName.EmpLastName),
                                         Title = e.Field<string>(DBConstants.ColName.EmpJobTitleCode),
                                         HireDate = e.Field<DateTime>(DBConstants.ColName.EmpHireDate),
                                         WorkLocation = e.Field<string>(DBConstants.ColName.EmpCountryCode),
                                         AnnualBaseSalary = e.Field<decimal>(DBConstants.ColName.EmpAnnualBase),
                                         AboveBaseCompensation = e.Field<decimal>(DBConstants.ColName.EmpAboveBaseComp),
                                         Currency = e.Field<string>(DBConstants.ColName.EmpCurrency)
                                     }).ToList();
            return result;
        }
    }
}
