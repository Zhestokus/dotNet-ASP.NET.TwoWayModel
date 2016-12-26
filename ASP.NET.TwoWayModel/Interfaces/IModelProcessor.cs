using System;
using System.Web.UI;

namespace ASP.NET.TwoWayModel.Interfaces
{
    public interface IModelProcessor
    {
        Object GetModel(Type type);

        void FillModel(Object model);

        void SetModel(Object model);
    }
}
