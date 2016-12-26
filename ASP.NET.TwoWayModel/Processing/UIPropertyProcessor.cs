using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASP.NET.TwoWayModel.Entities;
using ASP.NET.TwoWayModel.Enums;
using ASP.NET.TwoWayModel.Interfaces;
using ASP.NET.TwoWayModel.Utils;

namespace ASP.NET.TwoWayModel.Processing
{
    public static class UIPropertyProcessor
    {
        public static IEnumerable<ControlMappingEntity> GetMapping(Control container)
        {
            var controls = UserInterfaceUtil.EnumerateChildren(container, IsModelContainer);
            foreach (var control in controls)
            {
                foreach (var entity in GetProperties(control))
                    yield return entity;
            }
        }

        private static IEnumerable<ControlMappingEntity> GetProperties(Control control)
        {
            var attributeValue = GetAttributeValue(control, UIModelSettings.PropertyAttribute);
            if (!String.IsNullOrWhiteSpace(attributeValue))
            {
                var controlEntities = ParseProperties(attributeValue);
                foreach (var propertyEntity in controlEntities)
                {
                    var mappingEntity = new ControlMappingEntity(propertyEntity, control);
                    yield return mappingEntity;
                }
            }
        }

        private static IEnumerable<ControlPropertyEntity> ParseProperties(String value)
        {
            var bindings = ParseBinding(value);
            foreach (var dict in bindings)
            {
                var mode = GetBindMode(dict);

                foreach (var pair in dict)
                {
                    if (!StringComparer.OrdinalIgnoreCase.Equals(pair.Key, UIModelSettings.ModeProperty))
                    {
                        var source = ParseProperty(pair.Key);

                        var bindEntity = new BindPropertyEntity(source, mode);
                        //var target = ParseProperty(pair.Value);

                        var controlEntity = new ControlPropertyEntity(bindEntity, pair.Value);
                        yield return controlEntity;
                    }
                }
            }
        }

        private static IEnumerable<IDictionary<String, String>> ParseBinding(String exp)
        {
            int brackets = 0;

            var name = String.Empty;
            var value = String.Empty;

            var sb = new StringBuilder();
            var comparer = StringComparer.OrdinalIgnoreCase;

            var dict = new Dictionary<String, String>(comparer);

            for (int i = 0; i < exp.Length; i++)
            {
                switch (exp[i])
                {
                    case '[':
                        {
                            brackets++;
                        }
                        break;
                    case ']':
                        {
                            brackets--;

                            if (brackets < 0)
                                throw new Exception();
                        }
                        break;
                    case '{':
                        {

                            if (brackets == 0)
                                continue;
                        }
                        break;
                    case '}':
                        {
                            if (brackets == 0)
                            {
                                value = sb.ToString();
                                sb.Clear();

                                dict.Add(name, value);

                                var result = new Dictionary<String, String>(dict, comparer);
                                yield return result;

                                dict.Clear();
                                continue;
                            }
                        }
                        break;
                    case '=':
                        {
                            if (brackets == 0)
                            {
                                name = sb.ToString();
                                sb.Clear();
                                continue;
                            }
                        }
                        break;
                    case ',':
                        {
                            if (brackets == 0)
                            {
                                value = sb.ToString();
                                sb.Clear();

                                dict.Add(name, value);
                                continue;
                            }
                        }
                        break;
                    case ' ':
                        {
                            if (brackets == 0)
                                continue;
                        }
                        break;
                }

                sb.Append(exp[i]);
            }

            if (sb.Length > 0)
            {
                value = sb.ToString();
                sb.Clear();

                dict.Add(name, value);
            }

            if (dict.Count > 0)
            {
                var lastOne = new Dictionary<String, String>(dict, comparer);
                yield return lastOne;
            }
        }

        private static ClassPropertyEntity ParseProperty(String exp)
        {
            int brackets = 0;

            var names = new List<String>();
            var @params = String.Empty;

            var sb = new StringBuilder();

            for (int i = 0; i < exp.Length; i++)
            {
                switch (exp[i])
                {
                    case '[':
                        {
                            if (brackets == 0)
                            {
                                names.Add(sb.ToString());
                                sb.Clear();
                            }

                            brackets++;
                            continue;
                        }
                    case ']':
                        {
                            brackets--;

                            if (brackets == 0)
                            {
                                @params = sb.ToString();
                                sb.Clear();
                            }

                            continue;
                        }
                    case '.':
                        {
                            if (brackets == 0)
                            {
                                names.Add(sb.ToString());

                                sb.Clear();
                                continue;
                            }
                        }
                        break;
                }

                sb.Append(exp[i]);
            }

            if (sb.Length > 0)
            {
                names.Add(sb.ToString());
                sb.Clear();
            }

            if (names.Count > 1)
            {
                var array = names.ToArray();

                var @class = String.Join(".", array, 0, array.Length - 1);
                var property = names[names.Count - 1];

                var entity = new ClassPropertyEntity(@class, property, @params);
                return entity;
            }
            else
            {
                var property = names[names.Count - 1];

                var entity = new ClassPropertyEntity(String.Empty, property, @params);
                return entity;
            }

        }

        private static BindMode GetBindMode(IDictionary<String, String> dict)
        {
            String mode;
            if (!dict.TryGetValue(UIModelSettings.ModeProperty, out mode))
                mode = Convert.ToString(BindMode.TwoWay);

            var bindMode = GetBindMode(mode);
            return bindMode;
        }

        private static BindMode GetBindMode(String bindMode)
        {
            if (String.IsNullOrWhiteSpace(bindMode))
                return BindMode.TwoWay;

            if (StringComparer.OrdinalIgnoreCase.Equals(bindMode, Convert.ToString(BindMode.TwoWay)))
                return BindMode.TwoWay;

            if (StringComparer.OrdinalIgnoreCase.Equals(bindMode, Convert.ToString(BindMode.Assigne)))
                return BindMode.Assigne;

            if (StringComparer.OrdinalIgnoreCase.Equals(bindMode, Convert.ToString(BindMode.Receive)))
                return BindMode.Receive;

            var message = String.Format("Unknown bind mode '{0}'", bindMode);
            throw new InvalidOperationException(message);
        }

        private static String GetAttributeValue(Control control, String attributeName)
        {
            var webControl = control as WebControl;
            if (webControl != null)
            {
                var attributeValue = webControl.Attributes[attributeName];
                return attributeValue;
            }

            var userControl = control as UserControl;
            if (userControl != null)
            {
                var attributeValue = userControl.Attributes[attributeName];
                return attributeValue;
            }

            return null;
        }

        private static bool IsModelContainer(Control control)
        {
            if (control is IModelProcessor)
                return true;

            if (control is UserControl)
            {
                var attributeValue = GetAttributeValue(control, UIModelSettings.PropertyAttribute);
                if (!String.IsNullOrWhiteSpace(attributeValue))
                    return true;
            }

            return false;
        }
    }

}
