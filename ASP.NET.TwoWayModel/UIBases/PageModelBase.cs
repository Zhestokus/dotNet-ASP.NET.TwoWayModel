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
            var model = ModelProcessor.GetModel(type);
            OnGetModel(model, type);

            return model;
        }

        public virtual Object GetModel(Type type, Func<Control, Type, Object> valueGetter)
        {
            var model = ModelProcessor.GetModel(type, valueGetter);
            OnGetModel(model, type);

            return model;
        }

        public virtual void FillModel(Object model, Type type)
        {
            ModelProcessor.FillModel(model, type);
            OnFillModel(model, type);
        }

        public virtual void FillModel(Object model, Type type, Func<Control, Type, Object> valueGetter)
        {
            ModelProcessor.FillModel(model, type, valueGetter);
            OnFillModel(model, type);
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
            var model = ModelProcessor.GetModel<TModel>();
            OnGetModel(model, typeof(TModel));

            return model;
        }

        public virtual TModel GetModel<TModel>(Func<Control, Type, Object> valueGetter) where TModel : class
        {
            var model = ModelProcessor.GetModel<TModel>(valueGetter);
            OnGetModel(model, typeof(TModel));

            return model;
        }

        public virtual void FillModel<TModel>(TModel model) where TModel : class
        {
            ModelProcessor.FillModel(model);
            OnFillModel(model, typeof(TModel));
        }

        public virtual void FillModel<TModel>(TModel model, Func<Control, Type, object> valueGetter) where TModel : class
        {
            ModelProcessor.FillModel(model, valueGetter);
            OnFillModel(model, typeof(TModel));
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

        protected virtual void OnGetModel(Object model, Type type)
        {
        }

        protected virtual void OnFillModel(Object model, Type type)
        {
        }

        protected virtual void OnSetModel(Object model, Type type)
        {
        }

        #endregion
    }
}
