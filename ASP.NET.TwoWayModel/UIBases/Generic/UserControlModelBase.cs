namespace ASP.NET.TwoWayModel.UIBases.Generic
{
    public abstract class UserControlModelBase<TModel> : UserControlModelBase where TModel : class, new()
    {
        private TModel _model;
        public TModel Model
        {
            get
            {
                _model = (_model ?? GetModel<TModel>());
                return _model;
            }
            set
            {
                //if (ReferenceEquals(_model, value))
                //    return;

                _model = value;
                SetModel(_model);
            }
        }
    }
}