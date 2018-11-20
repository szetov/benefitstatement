using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BenefitStatement.Bll
{
    /// <summary>
    /// Record the number of days of annual leave that an employee is entitled to
    /// based on the employee's Title and Length of Service
    /// </summary>
    public class LeaveEntitlement
    {
        /// <summary>
        /// Job title of the employee
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Length of service of the employee in years
        /// </summary>
        public int YearsOfService { get; set; }
        /// <summary>
        /// Number of days of annual leave entitled
        /// </summary>
        public int NumberOfDays { get; set; }
    }

    /// <summary>
    /// An abstract class which implements the strategy to calculate the annual leave entitlement for employee.
    /// Country specific annual leave policy should be implemented by child classes.
    /// </summary>
    public abstract class AnnualLeaveStrategy
    {
        protected ILengthOfServiceCalculator LosCalculator;
        public readonly List<LeaveEntitlement> EntitlementLookup = new List<LeaveEntitlement>();

        /// <summary>
        /// Create an instance of the class with constructor injected dependency on how length of
        /// service should be calculated.
        /// </summary>
        /// <param name="losCalc">a strategy to calculate length of service</param>
        public AnnualLeaveStrategy(ILengthOfServiceCalculator losCalc)
        {           
            this.LosCalculator = losCalc ?? throw new ArgumentNullException("losCalc");
        }

        /// <summary>
        /// Get the number of day of annual leave an employee entitled
        /// </summary>
        /// <param name="emp">target employee</param>
        /// <returns>number of annual leave days</returns>
        public virtual int GetAnnualLeaveEntitlement(Employee emp)
        {
            int yearsOfService = this.LosCalculator.FindLengthOfServiceInYears(emp.HireDate, DateTime.Today);
            // for the given job title, find the entry with years of service smaller than the employee's
            // years of service
            int result = (from e in this.EntitlementLookup
                               where e.Title == emp.Title && e.YearsOfService < yearsOfService
                               orderby e.YearsOfService descending
                               select e.NumberOfDays).First();
            return result;
        }
    }
}
