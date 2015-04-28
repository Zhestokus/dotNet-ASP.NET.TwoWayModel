<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PositionControl.ascx.cs" Inherits="Controls_PositionControl" %>
<table>
    <tr>
        <td>ContractNumber</td>
        <td>
            <asp:TextBox runat="server" ID="tbxContractNumber" Property="PositionModel.ContractNumber" /></td>
    </tr>
    <tr>
        <td>Salary</td>
        <td>
            <asp:TextBox runat="server" ID="tbxSalary" Property="PositionModel.Salary" /></td>
    </tr>
    <tr>
        <td>Currency</td>
        <td>
            <asp:DropDownList runat="server" ID="ddlCurrency" Property="PositionModel.Currency">
                <Items>
                    <asp:ListItem Text="USD" Value="USD" />
                    <asp:ListItem Text="AUD" Value="AUD" />
                    <asp:ListItem Text="CAD" Value="CAD" />
                    <asp:ListItem Text="GEL" Value="GEL" />
                </Items>
            </asp:DropDownList>
        </td>
    </tr>
</table>
