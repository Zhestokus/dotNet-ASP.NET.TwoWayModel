using System;
using ASP.NET.TwoWayModel.UIBases;
using Models;

public partial class Controls_PositionControl : UserControlModelBase
{
    public PositionModel Model
    {
        get { return GetModel<PositionModel>(); }
        set { SetModel(value); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}