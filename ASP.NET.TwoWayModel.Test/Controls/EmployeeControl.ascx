<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmployeeControl.ascx.cs" Inherits="Controls_EmployeeControl" %>

<%@ Register Src="~/Controls/PositionControl.ascx" TagPrefix="local" TagName="PositionControl" %>

<table>
    <tr>
        <td>ID Number</td>
        <td>
            <asp:TextBox runat="server" ID="tbxIDNumber" Property="{EmployeeModel.IDNumber=Text}" /></td>
    </tr>
    <tr>
        <td>First Name</td>
        <td>
            <asp:TextBox runat="server" ID="tbxFirstName" Property="{EmployeeModel.FirstName=Text}" /></td>
    </tr>
    <tr>
        <td>Last Name</td>
        <td>
            <asp:TextBox runat="server" ID="tbxLastName" Property="{EmployeeModel.LastName=Text}" /></td>
    </tr>
    <tr>
        <td>Gender</td>
        <td>
            <asp:DropDownList runat="server" ID="ddlGender" Property="{EmployeeModel.Gender=SelectedValue}">
                <Items>
                    <asp:ListItem Text="Male" Value="Male" />
                    <asp:ListItem Text="Female" Value="Female" />
                </Items>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Rate</td>
        <td>
            <asp:TextBox runat="server" ID="tbxRate" Property="{EmployeeModel.Rate=Text}" /></td>
    </tr>
    <tr>
        <td>Active</td>
        <td>
            <asp:CheckBox runat="server" ID="chkActive" Property="{EmployeeModel.Active=Text}" /></td>
    </tr>
    <tr>
        <td colspan="2">
            <local:PositionControl runat="server" ID="positionControl" Property="{EmployeeModel.Position=Model}" />
        </td>
    </tr>
</table>
