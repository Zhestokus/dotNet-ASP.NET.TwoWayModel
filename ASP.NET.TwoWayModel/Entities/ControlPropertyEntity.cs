using System;

namespace ASP.NET.TwoWayModel.Entities
{
    public class ControlPropertyEntity : BindPropertyEntity
    {
        public ControlPropertyEntity(ControlPropertyEntity entity)
            : this(entity, entity.ControlProperty)
        {
        }

        public ControlPropertyEntity(BindPropertyEntity entity, String controlProperty)
            : base(entity)
        {
            ControlProperty = controlProperty;
        }

        public String ControlProperty { get; set; }
    }
}
