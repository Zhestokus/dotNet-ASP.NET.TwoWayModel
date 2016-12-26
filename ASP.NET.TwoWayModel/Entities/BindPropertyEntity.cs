using ASP.NET.TwoWayModel.Enums;

namespace ASP.NET.TwoWayModel.Entities
{
    public class BindPropertyEntity : ClassPropertyEntity
    {
        public BindPropertyEntity(BindPropertyEntity entity)
            : this(entity, entity.BindMode)
        {
        }

        public BindPropertyEntity(ClassPropertyEntity entity, BindMode bindMode)
            : base(entity)
        {
            BindMode = bindMode;
        }

        public BindMode BindMode { get; set; }
    }
}