using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using TroubleshooterUI.Business.Abstract;
using TroubleshooterUI.Constants;
using TroubleshooterUI.Utilities.Log.Concrete;

namespace TroubleshooterUI
{
    public partial class SpeedTest : Form
    {
        public ChromiumWebBrowser browser;
        ICommands _cmd;
        public SpeedTest(ICommands cmd)
        {
            _cmd = cmd;
            InitializeComponent();
            InitBrowser();
        }

        public void InitBrowser()
        {

            if (!Cef.IsInitialized) // Check before init
            {
                CefSettings settings = new CefSettings();
                Cef.Initialize(settings);
            }
            browser = new ChromiumWebBrowser("www.fast.com");
            this.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;
        }
        private void SpeedTest_Load(object sender, EventArgs e)
        {
            LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.SpeedTestFormOpened+GetHelper.GetDatetimeNow());
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

        }

        private void SpeedTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            _cmd.ProxyPac(true);
            LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.SpeedTesFormClosed + GetHelper.GetDatetimeNow());
        }


        private void SetTitle()
        {
            string script = @"document.getElementById('speed-value').innerHTML;";
            browser.EvaluateScriptAsync(script).ContinueWith(x =>
            {
                var response = x.Result;
                    LogHelper.Log(Utilities.Log.Enums.LogTarget.File,LoggingMessages.SpeedTestResult+ response.Result.ToString() +"mbps'dir."+ GetHelper.GetDatetimeNow());
                if (response.Result.ToString() == "0" || Convert.ToDecimal(response.Result.ToString()) < 6)
                {
                    if (MessageBox.Show("İnternet hızınız çalışılabilir değer olan 6 mbps'in altındadır. Şirket modemi mi kullanıyorsunuz?", "", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.MobileModemUsageNo);

                        if (MessageBox.Show("Modeminizi 1 dakika kapalı tuttuktan sonra, bilgisayarınızı da yeniden başlatarak sistemlere erişmeyi " +
                            "tekrar deneyiniz.", "UYARI!") == DialogResult.OK)
                        {
                            LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.ModemRestartChoice);
                        }
                    }
                    else
                    {
                        LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.MobileModemUsageYes);
                        MessageBox.Show("Takım Liderinizden IT ile iletişime geçmesini isteyiniz.", "BİLGİ");
                    }
                }
            });
        }

        private void SpeedTest_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            _cmd.ProxyPac(true);
            SetTitle();
        }
    }
}
