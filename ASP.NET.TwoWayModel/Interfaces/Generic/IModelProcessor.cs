using System;
using System.Web.UI;

namespace ASP.NET.TwoWayModel.Interfaces.Generic
{
    public interface IModelProcessor : Interfaces.IModelProcessor
    {
        TModel GetModel<TModel>() where TModel : class, new();
        TModel GetModel<TModel>(Func<Control, Type, Object> valueGetter) where TModel : class;

        void FillModel<TModel>(TModel model) where TModel : class;
        void FillModel<TModel>(TModel model, Func<Control, Type, Object> valueGetter) where TModel : class;

        void SetModel<TModel>(TModel model) where TModel : class;
        void SetModel<TModel>(TModel model, Action<Control, Object> valueSetter) where TModel : class;
    }
}