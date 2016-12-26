using System;
using System.Web.UI;
using ASP.NET.TwoWayModel.Interfaces;
using ASP.NET.TwoWayModel.Processing;

namespace ASP.NET.TwoWayModel.UIBases
{
    public abstract class UserControlModelBase : UserControl, IModelProcessor
    {
        public virtual Object GetModel(Type type)
        {
            var model = UIModelProcessor.GetModel(type, this);
            OnGetModel(model);

            return model;
        }

        public virtual void FillModel(object model)
        {
            UIModelProcessor.FillModel(model, this);
            OnFillModel(model);
        }

        public virtual void SetModel(object model)
        {
            UIModelProcessor.FillControl(model, this);
            OnSetModel(model);
        }

        protected virtual void OnGetModel(Object model)
        {
        }

        protected virtual void OnFillModel(Object model)
        {
        }

        protected virtual void OnSetModel(Object model)
        {
        }
    }
}
