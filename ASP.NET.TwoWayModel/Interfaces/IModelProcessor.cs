using System;
using System.Web.UI;

namespace ASP.NET.TwoWayModel.Interfaces
{
    public interface IModelProcessor
    {
        Object GetModel(Type type);
        Object GetModel(Type type, Func<Control, Type, Object> valueGetter);

        void FillModel(Object model, Type type);
        void FillModel(Object model, Type type, Func<Control, Type, Object> valueGetter);

        void SetModel(Object model, Type type);
        void SetModel(Object model, Type type, Action<Control, Object> valueSetter);
    }
}
