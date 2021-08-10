using Business.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TroubleshooterUI.Business.Abstract;
using TroubleshooterUI.Entities;

namespace TroubleshooterUI
{
    public partial class SipId : Form
    {
        ICommands _cmd;
        public SipId(ICommands cmd)
        {
            _cmd = cmd;
            InitializeComponent();
        }

        private void btnSetSipId_Click(object sender, EventArgs e)
        {

            var result = _cmd.SetSIPId(txtSIPIdBox.Text);
            if (result.Success)
            {
                _cmd.ShowNotifyMessage(new Notify { Icon = ToolTipIcon.Info, Interval = 3000, Message = result.Message, Title = Messages.ProgressInfo });
            }
            else
            {
                _cmd.ShowNotifyMessage(new Notify { Icon = ToolTipIcon.Error, Interval = 3000, Message = result.Message, Title = Messages.ProgressInfo });
            }
            txtSIPIdBox.Text = string.Empty;
            this.ShowInTaskbar = false;
            this.Hide();
        }

        private void txtSIPIdBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                var result = _cmd.SetSIPId(txtSIPIdBox.Text);
                if (result.Success)
                {
                    _cmd.ShowNotifyMessage(new Notify { Icon = ToolTipIcon.Info, Interval = 3000, Message = result.Message, Title = Messages.ProgressInfo });
                }
                else
                {
                    _cmd.ShowNotifyMessage(new Notify { Icon = ToolTipIcon.Error, Interval = 3000, Message = result.Message, Title = Messages.ProgressInfo });
                }
                txtSIPIdBox.Text = string.Empty;
                this.ShowInTaskbar = false;
                this.Hide();
            }
        }
    }
}
