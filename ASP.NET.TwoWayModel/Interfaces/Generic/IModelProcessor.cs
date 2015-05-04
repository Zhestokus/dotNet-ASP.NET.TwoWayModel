using System;
using System.Web.UI;

namespace ASP.NET.TwoWayModel.Interfaces.Generic
{
    public interface IModelProcessor : Interfaces.IModelProcessor
    {
        TModel GetModel<TModel>() where TModel : class, new();

        void FillModel<TModel>(TModel model) where TModel : class;

        void SetModel<TModel>(TModel model) where TModel : class;
    }
}