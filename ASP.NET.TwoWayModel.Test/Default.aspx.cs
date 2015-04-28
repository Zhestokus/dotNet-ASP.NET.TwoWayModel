using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnGet_OnClick(object sender, EventArgs e)
    {
        var employeeModel = employeeControl.Model;
    }

    protected void btnSet_OnClick(object sender, EventArgs e)
    {
        var random = new Random();

        var employeeModel = new EmployeeModel();
        employeeModel.Active = true;
        employeeModel.FirstName = "Jon";
        employeeModel.LastName = "Smith";
        employeeModel.Gender = "Male";
        employeeModel.IDNumber = random.Next(100, 999);
        employeeModel.Rate = random.Next(100, 999);

        var positionModel = new PositionModel();
        positionModel.Currency = "GEL";
        positionModel.Salary = random.Next(1000, 9999) / 100M;
        positionModel.ContractNumber = random.Next(1000, 9999);

        employeeModel.Position = positionModel;

        employeeControl.Model = employeeModel;
    }
}