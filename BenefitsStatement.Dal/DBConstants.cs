using System;
using System.Collections.Generic;
using System.Text;

namespace BenefitStatement.Dal
{
    public class DBConstants
    {
        public static class ColName
        {
            // columns related to employees
            public const string EmpID = "ID";
            public const string EmpFirstName = "FirstName";
            public const string EmpLastName = "LastName";
            public const string EmpJobTitleCode = "JobTitleCode";
            public const string EmpHireDate = "HireDate";
            public const string EmpCountryCode = "CountryCode";
            public const string EmpAnnualBase = "AnnualBase";
            public const string EmpAboveBaseComp = "AbboveBaseComp";
            public const string EmpCurrency = "Currency";
        }
    }
}
