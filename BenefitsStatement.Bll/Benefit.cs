using BenefitStatement.Common;
using System;

namespace BenefitStatement.Bll
{
    public class BenefitPolicyFactory
    {
        public static BenefitPolicy CreatePolicy(string country)
        {
            BenefitPolicy result = null;
            switch (country)
            {
                case Constants.Country.Japan:
                    result = new BenefitPolicyJpn();
                    break;
                case Constants.Country.Singapore:
                    result = new BenefitPolicySgp();
                    break;
                default:
                    throw new ArgumentException($"{country} is not supported.", country);
            }
            return result;
        }
    }

    /// <summary>
    /// This class represents the detail benefits information of an employee
    /// </summary>
    public class BenefitDetail
    {
        /// <summary>
        /// the number of days of annual leave that the employee is entitled.
        /// </summary>
        public int AnnualLeaveDays { get; set; }
        /// <summary>
        /// Total coverage amount under the life insurance plan
        /// </summary>
        public decimal LifeInsuranceCoverage { get; set; }
        /// <summary>
        /// Total coverage amount under the personal insurance insurance plan
        /// </summary>
        public decimal PersonalAccidentInsuranceCoverage { get; set; }
        /// <summary>
        /// Total coverage amount under the critical illness insurance plan
        /// </summary>
        public decimal CriticalIllnessInsuranceCoverage { get; set; }
    }

    /// <summary>
    /// An abstract class that provides the framework for implementing the benefit
    /// policy for a country.  Each country should implement its own policy in the child class.
    /// </summary>
    public abstract class BenefitPolicy
    {
        protected AnnualLeaveStrategy AnnnualLeaveCalculator;
        protected IInsuranceStrategy InsuranceCalculator;

        /// <summary>
        /// Use constructor dependency injection to inject algorithm for calculating insurnace and annual
        /// leave.  Each country should implement its own child class for country specific annual leave and
        /// insurance policies.
        /// </summary>
        /// <param name="annualLeaveStrategy">implements the logic for annual leave policy</param>
        /// <param name="insuranceStrategy">implements the logic for insurance policy</param>
        public BenefitPolicy(AnnualLeaveStrategy annualLeaveStrategy, IInsuranceStrategy insuranceStrategy)
        {
            this.AnnnualLeaveCalculator = annualLeaveStrategy ?? throw new ArgumentNullException("annualLeaveStrategy");
            this.InsuranceCalculator = insuranceStrategy ?? throw new ArgumentNullException("insuranceStrategy");
        }

        /// <summary>
        /// Gets the detail benefit information for a given employee
        /// </summary>
        /// <param name="emp">target employee</param>
        /// <returns>benefit detail of the employee</returns>
        public virtual BenefitDetail GetBenefitDetail(Employee emp)
        {
            BenefitDetail result = new BenefitDetail();
            result.AnnualLeaveDays = this.AnnnualLeaveCalculator.GetAnnualLeaveEntitlement(emp);
            result.CriticalIllnessInsuranceCoverage = this.InsuranceCalculator.GetCriticalIllnessCoverage(emp);
            result.LifeInsuranceCoverage = this.InsuranceCalculator.GetLifeInsuranceCoverage(emp);
            result.PersonalAccidentInsuranceCoverage = this.InsuranceCalculator.GetPersonalAccidentCoverage(emp);
            return result;
        }
    }
}
