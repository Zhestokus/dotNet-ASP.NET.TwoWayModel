using System;
using System.Web.UI;
using ASP.NET.TwoWayModel.Common;
using ASP.NET.TwoWayModel.Interfaces;

namespace ASP.NET.TwoWayModel.UIBases
{
    public class MasterPageModelBase : MasterPage, IModelProcessorBasic
    {
        private UIModelProcessor _modelProcessor;
        public UIModelProcessor ModelProcessor
        {
            get
            {
                _modelProcessor = (_modelProcessor ?? new UIModelProcessor(this));
                return _modelProcessor;
            }
        }

        #region IModelProcessorBasic

        public virtual Object GetModel(Type type)
        {
            return ModelProcessor.GetModel(type);
        }

        public virtual Object GetModel(Type type, Func<Control, Type, Object> valueGetter)
        {
            return ModelProcessor.GetModel(type, valueGetter);
        }

        public virtual void FillModel(Object model, Type type)
        {
            ModelProcessor.FillModel(model, type);
        }

        public virtual void FillModel(Object model, Type type, Func<Control, Type, Object> valueGetter)
        {
            ModelProcessor.FillModel(model, type, valueGetter);
        }

        public virtual void SetModel(Object model, Type type)
        {
            ModelProcessor.SetModel(model, type);
        }

        public virtual void SetModel(Object model, Type type, Action<Control, Object> valueSetter)
        {
            ModelProcessor.SetModel(model, type, valueSetter);
        }

        #endregion

        #region IModelProcessorGeneric

        public virtual TModel GetModel<TModel>() where TModel : class, new()
        {
            return ModelProcessor.GetModel<TModel>();
        }

        public virtual TModel GetModel<TModel>(Func<Control, Type, Object> valueGetter) where TModel : class
        {
            return ModelProcessor.GetModel<TModel>(valueGetter);
        }

        public virtual void FillModel<TModel>(TModel model) where TModel : class
        {
            ModelProcessor.FillModel(model);
        }

        public virtual void FillModel<TModel>(TModel model, Func<Control, Type, object> valueGetter) where TModel : class
        {
            ModelProcessor.FillModel(model, valueGetter);
        }

        public virtual void SetModel<TModel>(TModel model) where TModel : class
        {
            ModelProcessor.SetModel(model);
        }

        public virtual void SetModel<TModel>(TModel model, Action<Control, Object> valueSetter) where TModel : class
        {
            ModelProcessor.SetModel(model, valueSetter);
        }

        #endregion
    }
}
