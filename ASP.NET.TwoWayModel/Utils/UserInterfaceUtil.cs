using System;
using System.Collections.Generic;
using System.Web.UI;

namespace ASP.NET.TwoWayModel.Utils
{
    public static class UserInterfaceUtil
    {
        public static IEnumerable<Control> EnumerateChildren(Control control)
        {
            return EnumerateChildren(control, null);
        }

        public static IEnumerable<Control> EnumerateChildren(Control control, Predicate<Control> skipPredicate)
        {
            var stack = new Stack<Control>();

            foreach (Control child in control.Controls)
                stack.Push(child);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (current.Controls.Count > 0)
                {
                    if (skipPredicate == null || !skipPredicate(current))
                    {
                        foreach (Control child in current.Controls)
                            stack.Push(child);
                    }
                }

                yield return current;
            }
        }
    }
}
