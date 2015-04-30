using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using ASP.NET.TwoWayModel.Utils;

namespace ASP.NET.TwoWayModel.Common
{
    public static class UIHierarchyCache
    {
        private static readonly ConcurrentDictionary<HttpContext, IDictionary<Control, IEnumerable<Control>>> _controls;

        static UIHierarchyCache()
        {
            _controls = new ConcurrentDictionary<HttpContext, IDictionary<Control, IEnumerable<Control>>>();
        }

        public static void InitContextCache()
        {
            var context = HttpContext.Current;
            if (context != null)
            {
                var dictionary = new ConcurrentDictionary<Control, IEnumerable<Control>>();
                _controls.TryAdd(context, dictionary);
            }
        }

        public static void ReleaseContextCache()
        {
            var context = HttpContext.Current;
            if (context != null)
            {
                IDictionary<Control, IEnumerable<Control>> dictionary;
                _controls.TryRemove(context, out dictionary);
            }
        }

        public static IEnumerable<Control> GetChildren(Control parent)
        {
            var context = HttpContext.Current;
            if (context != null)
            {
                IDictionary<Control, IEnumerable<Control>> dictionary;
                if (_controls.TryGetValue(context, out dictionary))
                {
                    IEnumerable<Control> controls;
                    if (dictionary.TryGetValue(parent, out controls))
                    {
                        return controls;
                    }

                    controls = UserInterfaceUtil.EnumerateAllControls(parent).ToList();
                    dictionary.Add(parent, controls);

                    foreach (var control in controls)
                    {
                        if (!dictionary.ContainsKey(control))
                        {
                            var children = UserInterfaceUtil.EnumerateAllControls(parent).ToList();
                            dictionary.Add(control, children);
                        }
                    }

                    return controls;
                }
            }

            return UserInterfaceUtil.EnumerateAllControls(parent);
        }
    }

}
