using System;
using System.Web.UI;

namespace ASP.NET.TwoWayModel.Interfaces
{
    public interface IModelProcessor
    {
        Object GetModel(Type type);

        void FillModel(Object model, Type type);

        void SetModel(Object model, Type type);
    }
}
