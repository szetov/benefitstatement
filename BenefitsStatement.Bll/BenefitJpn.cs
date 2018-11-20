using BenefitStatement.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BenefitStatement.Bll
{
    public class BenefitPolicyJpn : BenefitPolicy
    {
        public BenefitPolicyJpn() : base(new AnnualLeaveStrategyJpn(), new InsuranceStrategyJpn())
        {
            // Japan specific annual leave policy
            this.AnnnualLeaveCalculator.EntitlementLookup.AddRange(new List<LeaveEntitlement>()
            {
                #region leave entitlement definition
                new LeaveEntitlement() { Title = Constants.JobTitle.AS, YearsOfService = 0, NumberOfDays = 20 },
                new LeaveEntitlement() { Title = Constants.JobTitle.AS, YearsOfService = 10, NumberOfDays = 25 },
                new LeaveEntitlement() { Title = Constants.JobTitle.AS, YearsOfService = 15, NumberOfDays = 30 },
                new LeaveEntitlement() { Title = Constants.JobTitle.VP, YearsOfService = 0, NumberOfDays = 20 },
                new LeaveEntitlement() { Title = Constants.JobTitle.VP, YearsOfService = 10, NumberOfDays = 25 },
                new LeaveEntitlement() { Title = Constants.JobTitle.VP, YearsOfService = 15, NumberOfDays = 30 },
                new LeaveEntitlement() { Title = Constants.JobTitle.ED, YearsOfService = 0, NumberOfDays = 20 },
                new LeaveEntitlement() { Title = Constants.JobTitle.ED, YearsOfService = 10, NumberOfDays = 25 },
                new LeaveEntitlement() { Title = Constants.JobTitle.ED, YearsOfService = 15, NumberOfDays = 30 },
                new LeaveEntitlement() { Title = Constants.JobTitle.MD, YearsOfService = 0, NumberOfDays = 20 },
                new LeaveEntitlement() { Title = Constants.JobTitle.MD, YearsOfService = 10, NumberOfDays = 25 },
                new LeaveEntitlement() { Title = Constants.JobTitle.MD, YearsOfService = 15, NumberOfDays = 30 }
                #endregion
            });
        }
    }

    /// <summary>
    /// This class implements the logic to determine the annual leave entitlement for
    /// Japan employees.
    /// </summary>
    public class AnnualLeaveStrategyJpn : AnnualLeaveStrategy
    {
        public AnnualLeaveStrategyJpn() 
            : base(new LengthOfServiceByAnniversaryCalculator()) { }
    }

    /// <summary>
    /// Gets the coverage amount of life insurance for Japan employees
    /// </summary>
    /// <param name="emp">target employee</param>
    /// <returns>the coverage amount</returns>
    public class InsuranceStrategyJpn : IInsuranceStrategy
    {
        /// <summary>
        /// Maximum coverage amount for critical illness
        /// </summary>
        protected readonly decimal? MaxCriticalIllnessCoverage = 20000000m;
        /// <summary>
        /// Maximum coverage amount for life insurance.  A null value means that
        /// there is no maximum.
        /// </summary>
        protected readonly decimal? MaxLifeCoverage = null;
        /// <summary>
        /// Maximum coverage amount for personal accident
        /// </summary>
        protected readonly decimal? MaxPersonalAccidentCoverage = 10000000m;

        /// <summary>
        /// Gets the coverage amount of critical illness for Japan employees
        /// </summary>
        /// <param name="emp">target employee</param>
        /// <returns>the coverage amount</returns>
        public decimal GetCriticalIllnessCoverage(Employee emp)
        {
            if (emp == null)
            {
                throw new ArgumentNullException("emp");
            }
            else if (emp.WorkLocation != Constants.Country.Japan)
            {
                throw new ArgumentException("Only Japan employees are supported.", "emp");
            }
            // Critical illness for Japan is calculated as 1.5 x annual base salary, with a maxinum
            // coverage amount defined in MaxCriticalIllnessCoveage
            decimal result = MaxCriticalIllnessCoverage == null ? 1.5m * emp.AnnualBaseSalary :
                Math.Min(MaxCriticalIllnessCoverage.Value, 1.5m * emp.AnnualBaseSalary);
            return result;
        }

        /// <summary>
        /// Gets the coverage amount of life insurance for Japan employees
        /// </summary>
        /// <param name="emp">target employee</param>
        /// <returns>the coverage amount</returns>
        public decimal GetLifeInsuranceCoverage(Employee emp)
        {
            if (emp == null)
            {
                throw new ArgumentNullException("emp");
            }
            else if (emp.WorkLocation != Constants.Country.Japan)
            {
                throw new ArgumentException("Only Japan employees are supported.", "emp");
            }
            // Life insurance coverage for Japan is calculated as 3 x annual base salary, with a maxinum
            // coverage amount defined in MaxLifeCoverage
            decimal result = MaxLifeCoverage == null ? 3m * emp.AnnualBaseSalary :
                Math.Min(MaxLifeCoverage.Value, 3m * emp.AnnualBaseSalary);
            return result;
        }

        /// <summary>
        /// Gets the coverage amount of personal accident insurance for Japan employees
        /// </summary>
        /// <param name="emp">target employee</param>
        /// <returns>the coverage amount</returns>
        public decimal GetPersonalAccidentCoverage(Employee emp)
        {
            if (emp == null)
            {
                throw new ArgumentNullException("emp");
            }
            else if (emp.WorkLocation != Constants.Country.Japan)
            {
                throw new ArgumentException("Only Japan employees are supported.", "emp");
            }
            // Personal accident coverage for Japan is calculated as 3 x annual base salary, with a maxinum
            // coverage amount defined in MaxLifeCoverage
            decimal result = MaxPersonalAccidentCoverage == null ? 3m * emp.AnnualBaseSalary :
                Math.Min(MaxPersonalAccidentCoverage.Value, 3m * emp.AnnualBaseSalary);
            return result;
        }
    }
}
