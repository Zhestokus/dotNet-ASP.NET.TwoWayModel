# dotNet-ASP.NET.TwoWayModel
dotNet-ASP.NET.TwoWayModel (Two way model binding)

ASP.NET WebForms MVVM/MVC without JavaScript frameworks


little library whitch give you model binding features (like in MVC/MVVM)
you need just to add Property attribute with model class property name.
you can map any property of Control to any property of Model,
format of mapping is {[Name class].[Name of property]=[Name of property of Control]}


e.g
```asp
<asp:TextBox runat="server" ID="tbxEmail" Property="{SubscriberModel.Email=Text}" />
```
If you need map more then one property
```asp
<asp:FileUpload runat="server" ID="fuDocument" Property="{DocumentModel.FileName=FileName}{DocumentModel.FileBytes=FileBytes}" />
```

BUT FileUpload control can be used only for upload, you can't assige file data to FileUpload control, so it need to specify direction of assignement 

```asp
<asp:FileUpload runat="server" ID="fuDocument" Property="{DocumentModel.FileName=FileName, Mode=Receive}{DocumentModel.FileBytes=FileBytes, Mode=Receive}" />

<asp:GridView runat="server" ID="gvDocuments" Property="{DocumentsModel.List=DataSource, Mode=Assigne}">
</asp:GridView>
```
possible values of assignement mode is 
1. TwoWay (is Default),
2. Assigne (only assigne value of property of Model to property of Control), 
3. Receive (only assigne value of property of Control to property of Model)

e.g

Your own controls

LocationControl
```asp
<asp:TextBox runat="server" ID="tbxLatitude" Property="{LocationModel.Latitude=Text}" />
<asp:TextBox runat="server" ID="tbxLongitude" Property="{LocationModel.Longitude=Text}" />
<asp:TextBox runat="server" ID="tbxAltitude" Property="{LocationModel.Altitude=Text}" />
<asp:TextBox runat="server" ID="tbxSpeed" Property="{LocationModel.Speed=Text}" />
```
```csharp
public partial class LocationControl : UserControlModelBase<LocationModel>
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
  <asp:DropDownList runat="server" ID="ddlMake" Property="{VehicleModel.Make=SelectedValue}">
    <Items>
      <asp:ListItem Text="Ford" Value="Ford" />
      <asp:ListItem Text="Toyota" Value="Toyota" />
      <asp:ListItem Text="Nissan" Value="Nissan" />
      <asp:ListItem Text="Other" Value="Other" />
    </Items>
  </asp:DropDownList>
  <asp:TextBox runat="server" ID="tbxModel" Property="{VehicleModel.Model=Text}" />
  <asp:TextBox runat="server" ID="tbxYear" Property="{VehicleModel.Year=Text}" />
  <asp:TextBox runat="server" ID="tbxMonth" Property="{VehicleModel.Month=Text}" />
  <asp:TextBox runat="server" ID="tbxVIN" Property="{VehicleModel.VIN=Text}" />
  <asp:TextBox runat="server" ID="tbxEngine" Property="{VehicleModel.Engine=Text}" />
</div>
<div>
  <local:LocationControl runat="server" ID="locationControl" Property="{VehicleModel.Location=Model}"/>
</div>
```
```csharp
public partial class VehicleControl : UserControlModelBase<VehicleModel>
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
        var vehicleModel = vehicleControl.Model;
        
        //change Model instance data
        
        vehicleControl.Model = vehicleModel;
    }
}
```

NOTE: controls which represents Model should be inherited from UserControlModelBase class

for more information see ASP.NET.TwoWayMode.Test application
