using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TroubleshooterUI.Entities
{
    public class Notify
    {
        public string Message { get; set; }
        public string Title { get; set; }
        public ToolTipIcon Icon { get; set; }
        public int Interval { get; set; }
    }
}
