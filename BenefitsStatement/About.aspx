<%@ Page Title="About this Application" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="BenefitStatement.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3>Applicaiton Overview</h3>
    <p>This application enables Human Resources admnistrators to query employees current benefits.
        It covers employees from various countires in Asia; and therefore some of the important design
        considerations are listed below.
    </p>
    <ul>
        <li>the applicaiton must support different regulatory requiremments (e.g. annual leave) from 
            different countries.
        </li>
        <li>the application should be designed in a way so that the impact of adding support for new countries
            is minimal
        </li>
    </ul>
    <h3>Key Design Decisions</h3>
    <p>
        The <em>Strategy Pattern</em> is used so that country specific 
        logic is implemented by a dedicated strategy subclasse, to avoid use of if-else or switch-case statements. 
        The addition of new countries requires creating 
        new sub-classes instead of modifying existing classes (the Open-and-Close Principle).
    </p>
    <p>
        Use of dependency injection (mainly via constructor injection) makes mocking and unit testing
        easier.
    </p>
    <p>
        The applicaiton is logically separated into multiple layers / components:
    </p>
    <ul>
        <li>The presentation layer (web applicaiton project <em>BenefitStatement</em>)</li>
        <li>The business logic layer (namespace <em>BenefitStatement.Bll</em>) contains classes 
            that repsent the domain model and implement domain logic
        </li>
        <li>
            The data access layer (namespace <em>BenefitStatement.Dal</em>) contains classes that 
            interact with data persistence service (e.g. the database).  The current implementation 
            does not use any databases and therefore it is only a provides a mock implementation.
        </li>
        <li>
            The common library (namespace <em>BenefitStatement.Common</em>) contains classes that 
            are shared across all layers.
        </li>
    </ul>
    <p>Some other design / coding considerations are:</p>
    <ul>
        <li>Instead of one file per class, I have, in certain cases, defined multiple closely related
            small classes in one .cs file.  The intention is to reduce the number of files
            scattered in the proejct, and also to shorten the list of files in the Solution Explorer 
            of Visual Studio.
        </li>
        <li>
           Functions will only return in the last statement; there are no 'return' statement
            in the middle of the function body.  The intention is to make debugging / putting breakpoints
            easier.
        </li>
    </ul>
</asp:Content>
