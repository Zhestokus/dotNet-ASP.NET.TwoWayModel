# dotNet-ASP.NET.TwoWayModel
dotNet-ASP.NET.TwoWayModel (Two way model binding)

little library whitch give you model binding features (like in MVC)
you need just to add Property attribute with model class property name

e.g
```asp
<asp:TextBox runat="server" ID="tbxEmail" Property="ASP.NET.TwoWayModel.Test.WebApp.Models.SubscriberModel.Email" />
OR
<asp:TextBox runat="server" ID="tbxEmail" Property="SubscriberModel.Email" />
OR
<asp:TextBox runat="server" ID="tbxEmail" Property="Email" />
```

and then you can use GetModel method

e.g

Your own controls

LocationControl
```asp
<asp:TextBox runat="server" ID="tbxLatitude" Property="LocationModel.Latitude" />
<asp:TextBox runat="server" ID="tbxLongitude" Property="LocationModel.Longitude" />
<asp:TextBox runat="server" ID="tbxAltitude" Property="LocationModel.Altitude" />
<asp:TextBox runat="server" ID="tbxSpeed" Property="LocationModel.Speed" />
```
```csharp
public partial class VehicleControl : UserControlModelBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
}
```
VehicleControl
```asp
<%@ Register Src="~/LocationControl.ascx" TagPrefix="local" TagName="LocationControl" %>

<div>
  <asp:DropDownList runat="server" ID="ddlMake" Property="VehicleModel.Make">
    <Items>
      <asp:ListItem Text="Ford" Value="Ford" />
      <asp:ListItem Text="Toyota" Value="Toyota" />
      <asp:ListItem Text="Nissan" Value="Nissan" />
      <asp:ListItem Text="Other" Value="Other" />
    </Items>
  </asp:DropDownList>
  <asp:TextBox runat="server" ID="tbxModel" Property="VehicleModel.Model" />
  <asp:TextBox runat="server" ID="tbxYear" Property="VehicleModel.Year" />
  <asp:TextBox runat="server" ID="tbxMonth" Property="VehicleModel.Month" />
  <asp:TextBox runat="server" ID="tbxVIN" Property="VehicleModel.VIN" />
  <asp:TextBox runat="server" ID="tbxEngine" Property="VehicleModel.Engine" />
</div>
<div>
  <local:LocationControl runat="server" ID="locationControl" Property="VehicleModel.Location"/>
</div>
```
```csharp
public partial class LocationControl : UserControlModelBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
}
```

Model class
```csharp
public class VehicleModel
{
  public String Make { get; set; }
  public String Model { get; set; }
  
  public int Year { get; set; }
  public int Month { get; set; }
  
  public String VIN { get; set; }
  public String Engine { get; set; }
  
  public LocationModel Location { get; set; }
}

public class LocationModel
{
  public double Latitude { get; set; }
  public double Longitude { get; set; }
  public double Altitude { get; set; }
  public double Speed { get; set; }
}
```

Page 
```asp
<%@ Register Src="~/VehicleControl.ascx" TagPrefix="local" TagName="VehicleControl" %>

<local:VehicleControl runat="server" ID="vehicleControl" />
```

```csharp
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var vehicleModel = vehicleControl.GetModel<VehicleModel>();
        
        //change subscriberModel instance data
        
        vehicleControl.SetModel<VehicleModel>(vehicleModel);
    }
}
```

NOTE: controls or pages which represents Model should be inherited from UserControlModelBase class and/or PageModelBase class respectively

for more information see ASP.NET.TwoWayMode.Test application
