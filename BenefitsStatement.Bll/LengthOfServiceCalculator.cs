using System;
using System.Collections.Generic;
using System.Text;

namespace BenefitStatement.Bll
{
    public interface ILengthOfServiceCalculator
    {
        /// <summary>
        /// Calculate the length of service in years for a give date of hire and date. This will be used to
        /// calculate the annual leave entitlement
        /// </summary>
        /// <param name="hireDate">The hire date of the employee</param>
        /// <param name="asOfDate">the target date to which the length of service should be calcualted</param>
        /// <returns>the length of service in years</returns>
        int FindLengthOfServiceInYears(DateTime hireDate, DateTime asOfDate);
    }

    /// <summary>
    /// This class implements the calculation logic so that years of service is incremented only after
    /// employee's service has passed the anniversary day of the year.
    /// </summary>
    public class LengthOfServiceByAnniversaryCalculator : ILengthOfServiceCalculator
    {
        public int FindLengthOfServiceInYears(DateTime hireDate, DateTime asOfDate)
        {
            if (asOfDate < hireDate)
            {
                throw new ArgumentException("The  value of 'asOfDate' should not be earlier than the 'hireDate'.");
            }
            int result = 0;
            if (hireDate.Year == asOfDate.Year)
            {
                result = 0;
            }
            else
            {
                DateTime anniversaryDateInAsOfYear = new DateTime(hireDate.Year, hireDate.Month, asOfDate.Day);
                bool hasPassedAnniversaryDay = asOfDate >= anniversaryDateInAsOfYear;
                result = hasPassedAnniversaryDay ? asOfDate.Year - hireDate.Year: asOfDate.Year - hireDate.Year - 1;
            }
            return result;
        }
    }

    /// <summary>
    /// This class implements the calculation logic so that years of service is incremented when
    /// the calendar year changes.
    /// </summary>
    public class LengthOfServiceByCalendarYearCalculator : ILengthOfServiceCalculator
    {
        public int FindLengthOfServiceInYears(DateTime hireDate, DateTime asOfDate)
        {
            if (asOfDate < hireDate)
            {
                throw new ArgumentException("The  value of 'asOfDate' should not be earlier than the 'hireDate'.");
            }
            int result = asOfDate.Year - hireDate.Year;
            return result;
        }
    }
}
