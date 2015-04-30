using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASP.NET.TwoWayModel.Interfaces;
using ASP.NET.TwoWayModel.Utils;

namespace ASP.NET.TwoWayModel.Common
{
    public class UIModelProcessor
    {
        private readonly bool _allowDefaultIfNull = true;
        private readonly String _propertyAttributeKey = "Property";

        private readonly IDictionary<String, IList<Control>> _propertyControls;
        private readonly Control _containerControl;

        public UIModelProcessor(Control containerControl)
        {
            _propertyAttributeKey = ConfigurationManager.AppSettings["UIModelProcessor.PropertyAttributeKey"];
            _propertyAttributeKey = (String.IsNullOrWhiteSpace(_propertyAttributeKey) ? "Property" : _propertyAttributeKey);

            _containerControl = containerControl;
            _propertyControls = GetPropertyControls(_propertyAttributeKey);

            var allowDefaultIfNull = ConfigurationManager.AppSettings["UIModelProcessor.AllowDefaultIfNull"];
            if (!bool.TryParse(allowDefaultIfNull, out _allowDefaultIfNull))
            {
                _allowDefaultIfNull = true;
            }
        }

        public TModel GetModel<TModel>() where TModel : class, new()
        {
            return (TModel)GetModel(typeof(TModel));
        }
        public TModel GetModel<TModel>(Func<Control, Type, Object> valueGetter) where TModel : class
        {
            return (TModel)GetModel(typeof(TModel), valueGetter);
        }

        public void FillModel<TModel>(TModel model) where TModel : class
        {
            FillModel(model, DefaultControlValueGetter);
        }
        public void FillModel<TModel>(TModel model, Func<Control, Type, Object> valueGetter) where TModel : class
        {
            FillModel(model, typeof(TModel), valueGetter);
        }

        public void SetModel<TModel>(TModel model) where TModel : class
        {
            SetModel(model, DefaultControlValueSetter);
        }
        public void SetModel<TModel>(TModel model, Action<Control, Object> valueSetter) where TModel : class
        {
            SetModel(model, typeof(TModel), valueSetter);
        }

        public Object GetModel(Type type)
        {
            return GetModel(type, DefaultControlValueGetter);
        }
        public Object GetModel(Type type, Func<Control, Type, Object> valueGetter)
        {
            var newModel = Activator.CreateInstance(type);
            FillModel(newModel, type, valueGetter);

            return newModel;
        }

        public void FillModel(Object model, Type type)
        {
            FillModel(model, type, DefaultControlValueGetter);
        }
        public void FillModel(Object model, Type type, Func<Control, Type, Object> valueGetter)
        {
            var modelProperties = type.GetProperties();
            foreach (var property in modelProperties)
            {
                var key = String.Format("{0}.{1}", type.FullName, property.Name);

                IList<Control> controls;
                if (!_propertyControls.TryGetValue(key, out controls))
                {
                    key = String.Format("{0}.{1}", type.Name, property.Name);

                    if (!_propertyControls.TryGetValue(key, out controls))
                    {
                        key = property.Name;

                        if (!_propertyControls.TryGetValue(key, out controls))
                        {
                            continue;
                        }
                    }
                }

                SetModelPropertyValue(controls, property, valueGetter, model);
            }
        }

        public void SetModel(Object model, Type type)
        {
            SetModel(model, type, DefaultControlValueSetter);
        }
        public void SetModel(Object model, Type type, Action<Control, Object> valueSetter)
        {
            var modelProperties = type.GetProperties();
            foreach (var property in modelProperties)
            {
                var key = String.Format("{0}.{1}", type.FullName, property.Name);

                IList<Control> controls;
                if (!_propertyControls.TryGetValue(key, out controls))
                {
                    key = String.Format("{0}.{1}", type.Name, property.Name);

                    if (!_propertyControls.TryGetValue(key, out controls))
                    {
                        key = property.Name;

                        if (!_propertyControls.TryGetValue(key, out controls))
                        {
                            continue;
                        }
                    }
                }

                SetControlPropertyValue(controls, property, valueSetter, model);
            }
        }

        private void SetModelPropertyValue(IList<Control> controls, PropertyInfo propertyInfo, Func<Control, Type, Object> valueGetter, Object modelObject)
        {
            if (controls.Count == 0)
                return;

            if (controls.Count > 1)
                throw new Exception("There are more then one control with same property attribute");

            var control = controls[0];

            var propertyValue = valueGetter(control, propertyInfo.PropertyType);
            propertyInfo.SetValue(modelObject, propertyValue, null);
        }

        private void SetControlPropertyValue(IList<Control> controls, PropertyInfo propertyInfo, Action<Control, Object> valueSetter, Object modelObject)
        {
            if (controls.Count == 0)
                return;

            if (controls.Count > 1)
                throw new Exception();

            var control = controls[0];
            var propertyValue = propertyInfo.GetValue(modelObject, null);

            valueSetter(control, propertyValue);
        }

        private IDictionary<String, IList<Control>> GetPropertyControls(String attributeName)
        {
            var dictionary = new Dictionary<String, IList<Control>>();
            var allControls = UIHierarchyCache.GetChildren(_containerControl);

            foreach (var current in allControls)
            {
                var attrValue = GetAttributeValue(current, attributeName);
                if (String.IsNullOrWhiteSpace(attrValue))
                {
                    continue;
                }

                IList<Control> controls;
                if (!dictionary.TryGetValue(attrValue, out controls))
                {
                    controls = new List<Control>();
                    dictionary.Add(attrValue, controls);
                }

                controls.Add(current);
            }

            return dictionary;
        }

        private String GetAttributeValue(Control control, String attributeName)
        {
            var webControl = control as WebControl;
            if (webControl != null)
            {
                return webControl.Attributes[attributeName];
            }

            var userControl = control as UserControl;
            if (userControl != null)
            {
                return userControl.Attributes[attributeName];
            }

            return null;
        }

        private void DefaultControlValueSetter(Control control, Object value)
        {
            if (control is RadioButton)
            {
                var container = (RadioButton)control;
                container.Checked = (bool)value;
            }

            if (control is CheckBox)
            {
                var container = (CheckBox)control;
                container.Checked = (bool)value;
            }

            if (control is TextBox)
            {
                var container = (TextBox)control;
                container.Text = Convert.ToString(value);
            }

            if (control is ListControl)
            {
                var container = (ListControl)control;

                var collection = value as IEnumerable<String>;
                if (collection != null)
                {
                    var @set = collection as ISet<String>;
                    @set = (@set ?? new HashSet<String>(collection));

                    foreach (ListItem item in container.Items)
                    {
                        item.Selected = @set.Contains(item.Value);
                    }
                }
                else
                {
                    var strValue = Convert.ToString(value);
                    foreach (ListItem item in container.Items)
                    {
                        item.Selected = (item.Value == strValue);
                    }
                }
            }

            if (control is IModelProcessor)
            {
                var container = (IModelProcessor)control;
                container.SetModel(value, value.GetType());
            }
        }

        private Object DefaultControlValueGetter(Control control, Type type)
        {
            if (control is IModelProcessor)
            {
                var container = (IModelProcessor)control;
                return container.GetModel(type);
            }

            var value = GetControlValue(control, type);
            if (type.IsInstanceOfType(value))
            {
                return value;
            }

            var converted = GetConvertedValue(value, type);
            return converted;
        }

        private Object GetConvertedValue(Object value, Type type)
        {
            if (ReferenceEquals(value, null))
            {
                if (!type.IsValueType)
                {
                    return null;
                }

                if (IsNullable(type))
                {
                    return null;
                }

                if (_allowDefaultIfNull)
                {
                    return Activator.CreateInstance(type);
                }

                var nullValueErrorText = String.Format("Null is not assignable to type [{0}]", type);
                throw new Exception(nullValueErrorText);
            }

            var converter = TypeDescriptor.GetConverter(type);
            if (converter.CanConvertFrom(value.GetType()))
            {
                return converter.ConvertFrom(value);
            }

            converter = TypeDescriptor.GetConverter(value);
            if (converter.CanConvertTo(type))
            {
                return converter.ConvertTo(value, type);
            }

            var unableConvertErrorText = String.Format("Unable to convert value [{0}] to type [{1}]", value, type);
            throw new Exception(unableConvertErrorText);
        }

        private Object GetControlValue(Control control, Type type)
        {
            if (control is RadioButton)
            {
                var container = (RadioButton)control;
                return container.Checked;
            }

            if (control is CheckBox)
            {
                var container = (CheckBox)control;
                return container.Checked;
            }

            if (control is TextBox)
            {
                var container = (TextBox)control;
                return container.Text;
            }

            if (control is ListControl)
            {
                var container = (ListControl)control;
                var @set = new HashSet<String>();

                foreach (ListItem item in container.Items)
                {
                    if (item.Selected)
                    {
                        @set.Add(item.Value);
                    }
                }

                if (type == typeof(String))
                {
                    foreach (var item in set)
                    {
                        return item;
                    }

                    return null;
                }

                return @set;
            }

            throw new Exception();
        }

        private bool IsNullable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}
