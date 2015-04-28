using System.Collections.Generic;
using System.Web.UI;

namespace ASP.NET.TwoWayModel.Utils
{
    public static class UserInterfaceUtil
    {
        public static IEnumerable<Control> EnumerateAllControls(Control control)
        {
            var stack = new Stack<Control>();
            stack.Push(control);

            while (stack.Count > 0)
            {
                var current = stack.Pop();

                if (current.Controls.Count > 0)
                {
                    foreach (Control child in current.Controls)
                    {
                        stack.Push(child);
                    }
                }

                yield return current;
            }
        }

    }
}
