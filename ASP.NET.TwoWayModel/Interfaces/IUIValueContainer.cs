using System;

namespace ASP.NET.TwoWayModel.Interfaces
{
    /// <summary>
    ///  This interface can be used for custom controls wich do some logic/calculation and returns single value (wich associated to Model Property) and does not need whole Model class
    /// </summary>
    public interface IUIValueContainer
    {
        Object Value { get; set; }
    }
}
