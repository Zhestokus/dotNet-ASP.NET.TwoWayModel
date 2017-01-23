using System;
using System.Web.UI;
using ASP.NET.TwoWayModel.Enums;

namespace ASP.NET.TwoWayModel.Processing
{
    public static class UIModelProcessor
    {
        public static Object GetModel(Type type, Control control)
        {
            var model = Activator.CreateInstance(type);
            FillModel(model, control);

            return model;
        }

        public static void FillModel(Object model, Control control)
        {
            if (model == null || control == null)
                return;

            var mappingEntities = UIPropertyProcessor.GetMapping(control);

            foreach (var entity in mappingEntities)
            {
                var bindMode = entity.BindMode;
                if (bindMode == BindMode.TwoWay || bindMode == BindMode.Receive)
                    UIPropertyWorker.FillModelProperty(entity, model);
            }
        }

        public static void FillControl(Object model, Control control)
        {
            if (model == null || control == null)
                return;

            var mappingEntities = UIPropertyProcessor.GetMapping(control);

            foreach (var entity in mappingEntities)
            {
                var bindMode = entity.BindMode;
                if (bindMode == BindMode.TwoWay || bindMode == BindMode.Assigne)
                    UIPropertyWorker.FillControlProperty(entity, model);
            }
        }
    }
}
