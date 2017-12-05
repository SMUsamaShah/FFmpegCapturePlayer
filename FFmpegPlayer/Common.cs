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
        public static void InvokeUI(Control ctrl, Action action)
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.Invoke((MethodInvoker)delegate { InvokeUI(ctrl, action); });
                return;
            }
            action();
        }
    }
}
