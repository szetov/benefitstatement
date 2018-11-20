using BenefitStatement.Bll;
using BenefitStatement.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BenefitStatement
{
    public partial class _Default : Page
    {
        private IEmployeeRepository EmpRepo = new EmployeeRepository(new DummyEmployeeDataService());
        protected BenefitDetail EmployeeBenefit = new BenefitDetail();
        protected Employee TargetEmp = Employee.Null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitControls();
            }
        }

        protected void btnSubmit_Clicked(object sender, EventArgs e)
        {
            string empID = ddlEmployee.SelectedValue;
            this.TargetEmp = this.EmpRepo.GetByEmployeeID(empID);
            var policy = BenefitPolicyFactory.CreatePolicy(this.TargetEmp.WorkLocation);
            this.EmployeeBenefit = policy.GetBenefitDetail(this.TargetEmp);
            pnlDetails.Visible = true;
        }

        private void InitControls()
        {
            List<Employee> employees = this.EmpRepo.GetAll();
            ddlEmployee.DataSource = employees;
            ddlEmployee.DataValueField = nameof(Employee.Null.ID);
            ddlEmployee.DataTextField = nameof(Employee.Null.DisplayName);
            ddlEmployee.DataBind();            
        }
    }
}