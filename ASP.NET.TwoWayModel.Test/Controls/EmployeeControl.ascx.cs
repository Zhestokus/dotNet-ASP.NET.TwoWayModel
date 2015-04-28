using System;
using ASP.NET.TwoWayModel.UIBases;
using Models;

public partial class Controls_EmployeeControl : UserControlModelBase
{
    public EmployeeModel Model
    {
        get { return GetModel<EmployeeModel>(); }
        set { SetModel(value); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}