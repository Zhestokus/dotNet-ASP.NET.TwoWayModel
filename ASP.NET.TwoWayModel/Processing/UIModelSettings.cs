using System;
using System.Configuration;

namespace ASP.NET.TwoWayModel.Processing
{
    public static class UIModelSettings
    {
        private static bool? _defaultIfNull;
        public static bool DefaultIfNull
        {
            get
            {
                if (_defaultIfNull == null)
                {
                    var value = ConfigurationManager.AppSettings["UIModelProcessor.DefaultIfNull"];

                    bool @bool;
                    if (bool.TryParse(value, out @bool))
                        _defaultIfNull = @bool;
                }

                return _defaultIfNull.GetValueOrDefault();
            }
        }

        private static String _modeProperty;
        public static String ModeProperty
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_modeProperty))
                {
                    _modeProperty = ConfigurationManager.AppSettings["UIModelProcessor.ModeProperty"];

                    if (String.IsNullOrWhiteSpace(_modeProperty))
                        _modeProperty = "Mode";
                }

                return _modeProperty;
            }
        }

        private static String _propertyAttribute;
        public static String PropertyAttribute
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_propertyAttribute))
                {
                    _propertyAttribute = ConfigurationManager.AppSettings["UIModelProcessor.PropertyAttribute"];

                    if (String.IsNullOrWhiteSpace(_propertyAttribute))
                        _propertyAttribute = "Property";
                }

                return _propertyAttribute;
            }
        }
    }
}
