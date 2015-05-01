using System;
using System.Collections.Generic;
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
            var cache = InitializeCache();
            if (cache != null)
            {
                IEnumerable<Control> controls;
                if (cache.TryGetValue(parent, out controls))
                {
                    return controls;
                }

                controls = GetAllChildren(parent);
                cache.Add(parent, controls);

                foreach (var control in controls)
                {
                    if (!cache.ContainsKey(control))
                    {
                        var children = GetAllChildren(control);
                        cache.Add(control, children);
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

        private static IDictionary<Control, IEnumerable<Control>> InitializeCache()
        {
            var context = HttpContext.Current;
            if (context == null)
            {
                return null;
            }

            if (!context.Items.Contains(UIHierarchyCacheKey))
            {
                var dictionary = new Dictionary<Control, IEnumerable<Control>>();
                context.Items[UIHierarchyCacheKey] = dictionary;

                return dictionary;
            }
            else
            {
                var cacheItem = context.Items[UIHierarchyCacheKey];

                var dictionary = cacheItem as IDictionary<Control, IEnumerable<Control>>;
                if (dictionary == null)
                {
                    dictionary = new Dictionary<Control, IEnumerable<Control>>();
                    context.Items[UIHierarchyCacheKey] = dictionary;
                }

                return dictionary;
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
