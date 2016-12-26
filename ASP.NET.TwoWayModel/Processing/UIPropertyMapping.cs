using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET.TwoWayModel.Processing
{
    public static class UIPropertyMapping
    {
        private static readonly IDictionary<String, Func<Object, Object>> _getters;
        private static readonly IDictionary<String, Func<Object, Object>> _setters;

        static UIPropertyMapping()
        {
            _getters = new Dictionary<String, Func<Object, Object>>();
            _setters = new Dictionary<String, Func<Object, Object>>();
        }

        public static void RegisterGetter(Type type, Func<Object, Object> func)
        {
            _getters[type.FullName] = func;
        }
        public static Func<Object, Object> GetGetter(Type type)
        {
            Func<Object, Object> func;
            if (_getters.TryGetValue(type.FullName, out func))
                return func;

            return null;
        }
        public static void RemoveGetter(Type type)
        {
            _getters.Remove(type.FullName);
        }

        public static void RegisterSetter(Type type, Func<Object, Object> func)
        {
            _setters[type.FullName] = func;
        }
        public static Func<Object, Object> GetSetter(Type type)
        {
            Func<Object, Object> func;
            if (_setters.TryGetValue(type.FullName, out func))
                return func;

            return null;
        }
        public static void RemoveSetter(Type type)
        {
            _setters.Remove(type.FullName);
        }
    }
}
