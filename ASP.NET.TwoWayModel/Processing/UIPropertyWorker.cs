using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Web.UI;
using ASP.NET.TwoWayModel.Entities;

namespace ASP.NET.TwoWayModel.Processing
{
    public static class UIPropertyWorker
    {
        public static void FillModelProperty(ControlMappingEntity entity, Object model)
        {
            FillModelProperty(entity, model, entity.TargetControl);
        }
        public static void FillModelProperty(ControlPropertyEntity entity, Object model, Control control)
        {
            if (IsMetaProperty(entity))
                FillModelPropertyMeta(entity, model, control);
            else
                FillModelPropertySimple(entity, model, control);
        }
        private static void FillModelPropertyMeta(ControlPropertyEntity entity, Object model, Control control)
        {
            var modelType = model.GetType();
            var controlType = control.GetType();

            var modelProperty = modelType.GetProperty(entity.ClassProperty);
            var controlProperty = controlType.GetProperty(entity.ControlProperty);

            ThrowPropertyNotFound(entity, modelType, modelProperty, controlType, controlProperty);

            var dictionary = (IDictionary)modelProperty.GetValue(model);
            if (dictionary == null)
            {
                dictionary = (IDictionary)Activator.CreateInstance(modelProperty.PropertyType);
                modelProperty.SetValue(model, dictionary);
            }

            var destinationType = typeof(Object);
            if (modelProperty.PropertyType.IsGenericType)
            {
                var genericArguments = modelProperty.PropertyType.GetGenericArguments();
                destinationType = genericArguments[genericArguments.Length - 1];
            }

            var objectValue = controlProperty.GetValue(control);
            var convertedValue = ConvertValue(objectValue, destinationType);

            dictionary[entity.ClassPropertyParams] = convertedValue;
        }
        private static void FillModelPropertySimple(ControlPropertyEntity entity, Object model, Control control)
        {
            var modelType = model.GetType();
            var controlType = control.GetType();

            var modelProperty = modelType.GetProperty(entity.ClassProperty);
            var controlProperty = controlType.GetProperty(entity.ControlProperty);

            ThrowPropertyNotFound(entity, modelType, modelProperty, controlType, controlProperty);

            var objectValue = controlProperty.GetValue(control);
            var convertedValue = ConvertValue(objectValue, modelProperty.PropertyType);

            modelProperty.SetValue(model, convertedValue);
        }

        public static void FillControlProperty(ControlMappingEntity entity, Object model)
        {
            FillControlProperty(entity, model, entity.TargetControl);
        }
        public static void FillControlProperty(ControlPropertyEntity entity, Object model, Control control)
        {
            if (IsMetaProperty(entity))
                FillControlPropertyMeta(entity, model, control);
            else
                FillControlPropertySimple(entity, model, control);
        }
        private static void FillControlPropertyMeta(ControlPropertyEntity entity, Object model, Control control)
        {
            var modelType = model.GetType();
            var controlType = control.GetType();

            var modelProperty = modelType.GetProperty(entity.ClassProperty);
            var controlProperty = controlType.GetProperty(entity.ControlProperty);

            ThrowPropertyNotFound(entity, modelType, modelProperty, controlType, controlProperty);

            var dictionary = (IDictionary)modelProperty.GetValue(model);
            if (dictionary == null)
                return;

            var objectValue = (Object)null;

            if (dictionary.Contains(entity.ClassPropertyParams))
                objectValue = dictionary[entity.ClassPropertyParams];

            var convertedValue = ConvertValue(objectValue, controlProperty.PropertyType);

            controlProperty.SetValue(control, convertedValue);
        }
        private static void FillControlPropertySimple(ControlPropertyEntity entity, Object model, Control control)
        {
            var modelType = model.GetType();
            var controlType = control.GetType();

            var modelProperty = modelType.GetProperty(entity.ClassProperty);
            var controlProperty = controlType.GetProperty(entity.ControlProperty);

            ThrowPropertyNotFound(entity, modelType, modelProperty, controlType, controlProperty);

            var objectValue = modelProperty.GetValue(model);
            var convertedValue = ConvertValue(objectValue, controlProperty.PropertyType);

            controlProperty.SetValue(control, convertedValue);
        }

        private static void ThrowPropertyNotFound(ControlPropertyEntity entity, Type modelType, PropertyInfo modelProperty, Type controlType, PropertyInfo controlProperty)
        {
            if (modelProperty == null)
            {
                var message = String.Format("Unable to find property '{0}' of model '{1}'", entity.ClassProperty, modelType.Name);
                throw new Exception(message);
            }

            if (controlProperty == null)
            {
                var message = String.Format("Unable to find property '{0}' of control '{1}'", entity.ControlProperty, controlType.Name);
                throw new Exception(message);
            }
        }

        private static bool IsMetaProperty(ControlPropertyEntity entity)
        {
            return !String.IsNullOrWhiteSpace(entity.ClassPropertyParams);
        }

        private static Object ConvertValue(Object value, Type type)
        {
            if (type.IsInstanceOfType(value))
                return value;

            var strValue = Convert.ToString(value, CultureInfo.CurrentUICulture);
            if (String.IsNullOrEmpty(strValue))
            {
                if (!type.IsValueType || IsNullable(type))
                    return null;

                if (UIModelSettings.DefaultIfNull)
                    return Activator.CreateInstance(type);

                var nullValueErrorText = String.Format("Null is not assignable to type [{0}]", type);
                throw new Exception(nullValueErrorText);
            }

            var sourceType = value.GetType();
            if (IsNullable(sourceType))
                sourceType = Nullable.GetUnderlyingType(sourceType);

            var destinationType = type;
            if (IsNullable(destinationType))
                destinationType = Nullable.GetUnderlyingType(destinationType);

            var converter = TypeDescriptor.GetConverter(destinationType);
            if (converter.CanConvertFrom(sourceType))
                return converter.ConvertFrom(value);

            converter = TypeDescriptor.GetConverter(sourceType);
            if (converter.CanConvertTo(destinationType))
                return converter.ConvertTo(value, destinationType);

            var unableConvertErrorText = String.Format("Unable to convert value [{0} - {1}] to type [{2}]", value, value.GetType(), type);
            throw new Exception(unableConvertErrorText);
        }

        private static bool IsNullable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }

}
