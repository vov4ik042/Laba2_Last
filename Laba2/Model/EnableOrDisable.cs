using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba2EnableOrDisableModel
{
    static class EnableOrDisable
    {
        public static void EnableAndVisible(bool enable, bool visible, params Control[] control)
        {
            for (int i = 0; i < control.Length; i++)
            {
                control[i].Enabled = enable;
                control[i].Visible = visible;
            }
        }
    }
}