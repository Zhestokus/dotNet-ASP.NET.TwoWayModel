using System;
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
        private const String UIHierarchyCacheKey = "$[UIHierarchyCache]";

        public static IEnumerable<Control> GetChildren(Control parent)
        {
            var context = HttpContext.Current;
            if (context != null)
            {
                var cacheItem = context.Items[UIHierarchyCacheKey];

                var dictionary = cacheItem as IDictionary<Control, IEnumerable<Control>>;
                if (dictionary == null)
                {
                    dictionary = new Dictionary<Control, IEnumerable<Control>>();
                    context.Items[UIHierarchyCacheKey] = dictionary;
                }

                IEnumerable<Control> controls;
                if (dictionary.TryGetValue(parent, out controls))
                {
                    return controls;
                }

                controls = GetAllChildren(parent);
                dictionary.Add(parent, controls);

                foreach (var control in controls)
                {
                    if (!dictionary.ContainsKey(control))
                    {
                        var children = GetAllChildren(control);
                        dictionary.Add(control, children);
                    }
                }

                return controls;
            }
            else
            {
                var collection = UserInterfaceUtil.EnumerateAllControls(parent);
                var children = new List<Control>(collection);

                return children;
            }
        }

        private static IList<Control> GetAllChildren(Control parent)
        {
            var collection = UserInterfaceUtil.EnumerateAllControls(parent);
            var children = new List<Control>(collection);

            return children;
        }
    }
}
