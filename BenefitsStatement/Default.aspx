<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BenefitStatement._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .details-panel { margin: 20px 0; padding: 10px 20px; }
    </style>
<h1>Check Benefits Entitlement</h1>
    <div class="form-group">
        <label>Select an employee</label>
        <asp:DropDownList ID="ddlEmployee" CssClass="form-control" runat="server"></asp:DropDownList>
    </div>

    <asp:Button CssClass="btn btn-primary" ID="btnSubmit" runat="server" OnClick="btnSubmit_Clicked" Text="Submit" />

    <asp:Label runat="server" ID="lblMessage"></asp:Label>

    <asp:Panel ID="pnlDetails" CssClass="col-sm-12 bg-info details-panel" runat="server" Visible="false">
        <h3>Benefits Detail for <%=TargetEmp.DisplayName %></h3>
        <dl class="row">        
            <dt class="col-sm-3">Job Title</dt>
            <dd class="col-sm-9"><%=TargetEmp.Title %></dd>

            <dt class="col-sm-3">Work Location</dt>
            <dd class="col-sm-9"><%=TargetEmp.WorkLocation %></dd>

            <dt class="col-sm-3">Hire Date</dt>
            <dd class="col-sm-9"><%=TargetEmp.HireDate.ToString("dd-MMM-yyyy") %></dd>

            <dt class="col-sm-3">Annual Leave</dt>
            <dd class="col-sm-9"><%=$"{EmployeeBenefit.AnnualLeaveDays} days" %></dd>

            <dt class="col-sm-3">Annual Base Salary</dt>
            <dd class="col-sm-9"><%=$"{TargetEmp.Currency} " + TargetEmp.AnnualBaseSalary.ToString("#,###") %></dd>

            <dt class="col-sm-3">Above Base Compensation</dt>
            <dd class="col-sm-9"><%=$"{TargetEmp.Currency} " + TargetEmp.AboveBaseCompensation.ToString("#,###") %></dd>

            <dt class="col-sm-3">Life Insurance</dt>
            <dd class="col-sm-9"><%=$"{TargetEmp.Currency} " + EmployeeBenefit.LifeInsuranceCoverage.ToString("#,###") %></dd>

            <dt class="col-sm-3">Personal Accident Insurance</dt>
            <dd class="col-sm-9"><%=$"{TargetEmp.Currency} " + EmployeeBenefit.PersonalAccidentInsuranceCoverage.ToString("#,###") %></dd>

            <dt class="col-sm-3">Critical Illness Insurance</dt>
            <dd class="col-sm-9"><%=$"{TargetEmp.Currency} " + EmployeeBenefit.CriticalIllnessInsuranceCoverage.ToString("#,###") %></dd>
        </dl>
    </asp:Panel>
</asp:Content>
