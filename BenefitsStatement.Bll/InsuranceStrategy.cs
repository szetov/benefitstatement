using System;
using System.Collections.Generic;
using System.Text;

namespace BenefitStatement.Bll
{
    public interface IInsuranceStrategy
    {
        /// <summary>
        /// Gets the coverage amount for life insurannce
        /// </summary>
        /// <param name="emp">target employee</param>
        /// <returns>the coverage amount</returns>
        decimal GetLifeInsuranceCoverage(Employee emp);

        /// <summary>
        /// Gets the coverage amount for personal accident insurance
        /// </summary>
        /// <param name="emp">target employee</param>
        /// <returns>coverage amount for personal accident</returns>
        decimal GetPersonalAccidentCoverage(Employee emp);

        /// <summary>
        /// Gets the coverage amount for critical illness insurance
        /// </summary>
        /// <param name="emp">target employee</param>
        /// <returns>the coverage amount for critical illness</returns>
        decimal GetCriticalIllnessCoverage(Employee emp);
    }
}
