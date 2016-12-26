namespace ASP.NET.TwoWayModel.UIBases.Generic
{
    public abstract class UserControlModelBase<TModel> : UserControlModelBase where TModel : class, new()
    {
        private TModel _model;
        public TModel Model
        {
            get
            {
                _model = (_model ?? GetModel());
                return _model;
            }
            set
            {
                _model = value;
                SetModel(_model);
            }
        }

        public virtual TModel GetModel()
        {
            var model = (TModel)base.GetModel(typeof(TModel));
            return model;
        }

        public virtual void FillModel(TModel model)
        {
            base.FillModel(model);
        }

        public virtual void SetModel(TModel model)
        {
            base.SetModel(model);
        }
    }
}