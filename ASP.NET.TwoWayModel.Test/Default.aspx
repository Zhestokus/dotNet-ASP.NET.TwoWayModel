<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="~/Controls/EmployeeControl.ascx" TagPrefix="local" TagName="EmployeeControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <asp:Button runat="server" ID="btnSet" Text="Fill page using model with random data" OnClick="btnSet_OnClick" />
    </div>
    <div>
        <local:EmployeeControl runat="server" ID="employeeControl" />
    </div>
    <div>
        <asp:Button runat="server" ID="btnGet" Text="Set break point on end of click event (to see filled model) and click here" OnClick="btnGet_OnClick" />
    </div>
</asp:Content>

