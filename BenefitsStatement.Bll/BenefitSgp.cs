using BenefitStatement.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BenefitStatement.Bll
{
    public class BenefitPolicySgp : BenefitPolicy
    {
        public BenefitPolicySgp() : base(new AnnualLeaveStrategySgp(), new InsuranceStrategySgp())
        {
            // Singapore specific annual leave policy
            this.AnnnualLeaveCalculator.EntitlementLookup.AddRange(new List<LeaveEntitlement>()
            {
                #region leave entitlement definition
                new LeaveEntitlement() { Title = Constants.JobTitle.AS, YearsOfService = 0, NumberOfDays = 20 },
                new LeaveEntitlement() { Title = Constants.JobTitle.VP, YearsOfService = 0, NumberOfDays = 25 },
                new LeaveEntitlement() { Title = Constants.JobTitle.ED, YearsOfService = 0, NumberOfDays = 25 },
                new LeaveEntitlement() { Title = Constants.JobTitle.MD, YearsOfService = 0, NumberOfDays = 30 },
                #endregion
            });
        }
    }

    /// <summary>
    /// This class implements the logic to determine the annual leave entitlement for
    /// Singaore employees.
    /// </summary>
    public class AnnualLeaveStrategySgp : AnnualLeaveStrategy
    {
        /// <summary>
        /// Instantiate an object that calculate the annual leave for Singapore employees.
        /// Singapore calculates length of service based on calendar year.
        /// </summary>
        /// <param name="entitlementLookup">a lookup table which describes the number of days of annual leave</param>
        public AnnualLeaveStrategySgp()
            : base(new LengthOfServiceByCalendarYearCalculator()) { }
    }

    /// <summary>
    /// This class implements the logic to determine the various insurance coverage amount for
    /// Singaore employees.
    /// </summary>
    public class InsuranceStrategySgp : IInsuranceStrategy
    {
        /// <summary>
        /// Gets the coverage amount of critical illness for Singapore employees
        /// </summary>
        /// <param name="emp">target employee</param>
        /// <returns>the coverage amount</returns>
        public decimal GetCriticalIllnessCoverage(Employee emp)
        {
            if (emp == null)
            {
                throw new ArgumentNullException("emp");
            }
            else if (emp.WorkLocation != Constants.Country.Singapore)
            {
                throw new ArgumentException("Only Singapore employees are supported.", "emp");
            }
            // Critical illness for Singapore is calculated as 1 x total reward
            decimal result = 1.0m * emp.TotalReward;
            return result;
        }

        /// <summary>
        /// Gets the coverage amount of life insurance for Singapore employees
        /// </summary>
        /// <param name="emp">target employee</param>
        /// <returns>the coverage amount</returns>
        public decimal GetLifeInsuranceCoverage(Employee emp)
        {
            if (emp == null)
            {
                throw new ArgumentNullException("emp");
            }
            else if (emp.WorkLocation != Constants.Country.Singapore)
            {
                throw new ArgumentException("Only Singapore employees are supported.", "emp");
            }
            // Life insurance coverage for Singapore is calculated as 2.5 x total reward
            decimal result = 2.5m * emp.TotalReward;
            return result;
        }

        /// <summary>
        /// Gets the coverage amount of personal accident insurance for Singapore employees
        /// </summary>
        /// <param name="emp">target employee</param>
        /// <returns>the coverage amount</returns>
        public decimal GetPersonalAccidentCoverage(Employee emp)
        {
            if (emp == null)
            {
                throw new ArgumentNullException("emp");
            }
            else if (emp.WorkLocation != Constants.Country.Singapore)
            {
                throw new ArgumentException("Only Singapore employees are supported.", "emp");
            }
            // Personal accident coverage for Singapore is calculated as 3 x total reward
            decimal result = 3.0m * emp.TotalReward;
            return result;
        }
    }
}
