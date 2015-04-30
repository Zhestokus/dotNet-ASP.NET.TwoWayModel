using System;
using System.Web.UI;
using ASP.NET.TwoWayModel.Common;
using ASP.NET.TwoWayModel.Interfaces.Generic;

namespace ASP.NET.TwoWayModel.UIBases
{
    public abstract class PageModelBase : Page, IModelProcessor
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
            OnSetModel(model, type);
        }

        public virtual void SetModel(Object model, Type type, Action<Control, Object> valueSetter)
        {
            ModelProcessor.SetModel(model, type, valueSetter);
            OnSetModel(model, type);
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
            OnSetModel(model, typeof(TModel));
        }

        public virtual void SetModel<TModel>(TModel model, Action<Control, Object> valueSetter) where TModel : class
        {
            ModelProcessor.SetModel(model, valueSetter);
            OnSetModel(model, typeof(TModel));
        }

        #endregion

        #region Overridables

        protected virtual void OnSetModel(Object model, Type type)
        {
        }

        #endregion
    }
}
