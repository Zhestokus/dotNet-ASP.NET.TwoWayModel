using System.Web.UI;

namespace ASP.NET.TwoWayModel.Entities
{
    public class ControlMappingEntity : ControlPropertyEntity
    {
        public ControlMappingEntity(ControlMappingEntity entity)
            : this(entity, entity.TargetControl)
        {
        }

        public ControlMappingEntity(ControlPropertyEntity entity, Control targetControl)
            : base(entity)
        {
            TargetControl = targetControl;
        }

        public Control TargetControl { get; set; }
    }
}