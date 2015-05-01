using System;
using ASP.NET.TwoWayModel.Interfaces;

/*
 * we can't add Property attribute to asp:HiddenField and because we should create simple UserControl
 * wich implements IUIValueContainer interface.
 * also that interface can be used for custom controls wich do some logic/calculation and returns single value and does not need whole Model class
 */

public partial class Controls_HiddenFieldControl : System.Web.UI.UserControl, IUIValueContainer
{
    public Object Value
    {
        get { return hdValue.Value; }
        set { hdValue.Value = Convert.ToString(value); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}