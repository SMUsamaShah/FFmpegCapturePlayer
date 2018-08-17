using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdDetectVideoPlayer
{
    static class Common
    {
        /// <summary>
        /// Extension method to update UI element on the UI thread.
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="action"></param>
        public static void InvokeUI(this Control ctrl, Action action)
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.Invoke(action);
                return;
            }
            action.Invoke();
        }

        public static void RunOnUI(this Control ctrl, Action action)
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.BeginInvoke(action);
                return;
            }
            action.Invoke();
        }

        public static void AddControlToPanel(Panel panel, Control ctrl)
        {
            InvokeUI(panel, () => { panel.Controls.Add(ctrl); });
        }

        public static void RemoveControlFromPanel(Panel panel, int index)
        {
            InvokeUI(panel, () => { panel.Controls.RemoveAt(index); });
        }

        public static void RemoveControlFromPanel(Panel panel, Control ctrl)
        {
            InvokeUI(panel, () => { panel.Controls.Remove(ctrl); });
        }
    }
}
