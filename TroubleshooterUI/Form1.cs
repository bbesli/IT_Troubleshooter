using Business.Constants;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using TroubleshooterUI.Business.Abstract;
using TroubleshooterUI.Constants;
using TroubleshooterUI.Entities;
using TroubleshooterUI.Utilities.Log.Concrete;
using System.Globalization;
using System.Threading;

namespace TroubleshooterUI
{
    public partial class Form1 : Form
    {
        string AnydeskId;
        NumberFormatInfo nfi;

        ICommands _cmd;
        public Form1(ICommands cmd)
        {
            _cmd = cmd;/*Ekrana gelmiyor.*/
            InitializeComponent();
        }

        private void btnFixAktifbank_Click(object sender, EventArgs e)
        {

        }

        private void btnFill_Click(object sender, EventArgs e)
        {
            //ListAdapterNames();
            //MessageBox.Show(SetSIPID());

            var result = _cmd.SetSIPId(txtSIP.Text);
            if (result.Success)
            {
                _cmd.ShowNotifyMessage(new Notify { Icon = ToolTipIcon.Info, Interval = 3000, Message = result.Message, Title = Messages.ProgressInfo });
            }
            else
            {
                _cmd.ShowNotifyMessage(new Notify { Icon = ToolTipIcon.Error, Interval = 3000, Message = result.Message, Title = Messages.ProgressInfo });
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.RunApplication + GetHelper.GetDatetimeNow());

            this.Visible = false;
            this.ShowInTaskbar = false;
            lblAnydeskId.Text = _cmd.FindAnydeskId(AnydeskId).ToString();
        }

        private void btnCopyAnydeskId_Click(object sender, EventArgs e)
        {
            _cmd.CopyAnydeskId(lblAnydeskId.Text);
        }

        private void btnSkypeFix_Click(object sender, EventArgs e)
        {

        }

        private void anydeskIDKopyalaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _cmd.CopyAnydeskId(lblAnydeskId.Text);
            LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.AnydeskIdCopied + GetHelper.GetDatetimeNow());
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.CloseApplication + GetHelper.GetDatetimeNow() + "\n \n \n");
            notifyIconSetter.Visible = false;
            Application.Exit();
        }

        private void fortiClientVPNiYenidenBaşlatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _cmd.RestartFortiClientVPN();
        }

        private void internetHızTestiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _cmd.ProxyPac(false);
            _cmd.OpenSpeedTestForm();
            LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.SpeedTestFormOpening + GetHelper.GetDatetimeNow());
        }

        private void proxyAçToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogHelper.Log(Utilities.Log.Enums.LogTarget.File, _cmd.BoxesForLog(proxyToolStripMenuItem.Text,
                proxyAçToolStripMenuItem.Text));
            _cmd.ProxyPac(true);
        }

        private void proxyKapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogHelper.Log(Utilities.Log.Enums.LogTarget.File, _cmd.BoxesForLog(proxyToolStripMenuItem.Text,
                proxyKapatToolStripMenuItem.Text));
            _cmd.ProxyPac(false);
        }
        private void btnVolume_Click(object sender, EventArgs e)
        {
            string genesysPath = Path.GetFullPath(@"C:\\Program Files (x86)\\GCTI\\Genesys Softphone\\Softphone.config");
            MessageBox.Show($"\"{genesysPath}\"");
        }

        private void cTiFixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogHelper.Log(Utilities.Log.Enums.LogTarget.File, _cmd.BoxesForLog(aktifbankToolStripMenuItem.Text,
                cTiFixToolStripMenuItem.Text));
            _cmd.ProcessStart("taskkill", @"/IM AktifCTIService.exe /F");
            _cmd.ProcessStart("taskkill", @"/IM GenesysSoftphone.exe /F");
            _cmd.ShowNotifyMessage(new Notify { Icon = ToolTipIcon.Info, Interval = 1000, Message = Messages.ProgressStarted, Title = Messages.ProgressInfo });
            _cmd.AktifCTIService();
            _cmd.DisableIPv6();
            _cmd.ShowNotifyMessage(new Notify { Icon = ToolTipIcon.Info, Interval = 1500, Message = Messages.ProgressDone, Title = Messages.ProgressInfo });
            MessageBox.Show("GenesysSoftphone Torubleshooter uygulaması tarafından kapatıldı. Tekrar açmanız gerekiyor.", "UYARI");

        }

        private void skypeEklentisiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogHelper.Log(Utilities.Log.Enums.LogTarget.File, _cmd.BoxesForLog(aktifbankToolStripMenuItem.Text,
                skypeEklentisiToolStripMenuItem.Text));

            var psi = new ProcessStartInfo
            {
                FileName = @"C:\ProgramData\MayenTroubleshooter\Aktifbank\update.exe",
                UserName = "administrator",
                Domain = ".",
                Password = _cmd.AdminPassword(),
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            Process.Start(psi);
        }

        private void sIPTanımlamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogHelper.Log(Utilities.Log.Enums.LogTarget.File, _cmd.BoxesForLog(aktifbankToolStripMenuItem.Text,
                sIPTanımlamaToolStripMenuItem.Text));

            _cmd.OpenSipIdForm();
        }

        private void güncellemeleriDenetleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //_cmd.ProcessStart("Updater.exe");
            //Process.Start(@"C:\it_troubleshooter\Updater.exe");
            ProcessStartInfo info = new ProcessStartInfo(@"C:\it_troubleshooter\Updater.exe");
            info.UseShellExecute = true;
            info.Verb = "runas";
            Process.Start(info);
        }

        private void sesAyarıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"C:\ProgramData\MayenTroubleshooter\VolumeMixer\VoiceRecorder.exe");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void sertifikalarıYükleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //LogHelper.Log(Utilities.Log.Enums.LogTarget.File, $"{"[" + projelerToolStripMenuItem.Text.ToUpper() + "]" + "[" + hepsiexpressToolStripMenuItem.Text + "]"}");
            LogHelper.Log(Utilities.Log.Enums.LogTarget.File, _cmd.BoxesForLog(hepsiexpressToolStripMenuItem.Text,
                sertifikalarıYükleToolStripMenuItem.Text));

            string root = @"C:\ProgramData\MayenTroubleshooter\Hepsiexpress\4";
            string[] fileEntries = Directory.GetFiles(root);
            foreach (string fileName in fileEntries)
            {
                switch (fileName)
                {
                    case @"C:\ProgramData\MayenTroubleshooter\Hepsiexpress\4\GlobalBilgiIntermediateCA.cer":
                        _cmd.InstallCertificate(fileName, StoreName.CertificateAuthority, StoreLocation.LocalMachine);
                        break;
                    case @"C:\ProgramData\MayenTroubleshooter\Hepsiexpress\4\GlobalBilgiRootCA.cer":
                        _cmd.InstallCertificate(fileName, StoreName.Root, StoreLocation.LocalMachine);
                        break;
                    case @"C:\ProgramData\MayenTroubleshooter\Hepsiexpress\4\ns3_webvoipphone_com.cer":
                        _cmd.InstallCertificate(fileName, StoreName.Root, StoreLocation.LocalMachine);
                        _cmd.InstallCertificate(fileName, StoreName.CertificateAuthority, StoreLocation.LocalMachine);
                        _cmd.InstallCertificate(fileName, StoreName.AuthRoot, StoreLocation.LocalMachine);
                        break;
                    default:
                        break;
                }
            }
        }

        private void projelerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.Projects);
        }

        private void hepsiexpressToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ininalLetgoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void resetAvayaCommunicatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogHelper.Log(Utilities.Log.Enums.LogTarget.File, _cmd.BoxesForLog(alibabaToolStripMenuItem.Text,
                resetAvayaCommunicatorToolStripMenuItem.Text));
            _cmd.ReadConfigFile();
            _cmd.Copy(@"C:\ProgramData\MayenTroubleshooter\AvayaCommConfig\", $"C:\\Users\\{GetHelper.GetUserName()}\\AppData\\Roaming\\Avaya\\Avaya one-X Communicator\\");
        }

        private void resetAvayaCommunicatorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            LogHelper.Log(Utilities.Log.Enums.LogTarget.File, _cmd.BoxesForLog(ininalLetgoToolStripMenuItem.Text,
                resetAvayaCommunicatorToolStripMenuItem.Text));
            _cmd.ReadConfigFile();
            _cmd.Copy(@"C:\ProgramData\MayenTroubleshooter\AvayaCommConfig\", $"C:\\Users\\{GetHelper.GetUserName()}\\AppData\\Roaming\\Avaya\\Avaya one-X Communicator\\");
        }

        private void gbwebphoneAddInYükleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogHelper.Log(Utilities.Log.Enums.LogTarget.File, _cmd.BoxesForLog(hepsiexpressToolStripMenuItem.Text,
                gbwebphoneAddInYükleToolStripMenuItem.Text));
            var result = _cmd.ProcessStart("powershell.exe", $"Start-Process -Wait -FilePath C:\\ProgramData\\MayenTroubleshooter\\Hepsiexpress\\1\\WebPhoneService_Install.exe -PassThru");
            if (result.Success)
            {
                LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.AddInPackageInstalled + GetHelper.GetDatetimeNow());
            }
            else
            {
                LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.AddInPackageInstallationError + GetHelper.GetDatetimeNow());
            }
        }

        private void gbwebphoneAddInPackYükleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogHelper.Log(Utilities.Log.Enums.LogTarget.File, _cmd.BoxesForLog(hepsiexpressToolStripMenuItem.Text, gbwebphoneAddInPackYükleToolStripMenuItem.Text));
            _cmd.ProcessStart("powershell.exe", $"Start-Process -Wait -FilePath C:\\ProgramData\\MayenTroubleshooter\\Hepsiexpress\\2\\WebphoneSSLPatch-TR.exe -PassThru");
            _cmd.Copy("C:\\ProgramData\\MayenTroubleshooter\\Hepsiexpress\\3", "C:\\Program Files (x86)\\GBPhone_Service");
            LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.AddInInstalled + GetHelper.GetDatetimeNow());
        }

        private void vPNSounuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _cmd.RestartFortiClientVPN();
        }

        private void sesSorunuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogHelper.Log(Utilities.Log.Enums.LogTarget.File, _cmd.BoxesForLog(galeriaToolStripMenuItem.Text,
                sesSorunuToolStripMenuItem.Text));
            Process.Start(@"C:\ProgramData\MayenTroubleshooter\VolumeMixer\VoiceRecorder.exe");
        }

        private void eWalletAyarlaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = "\"C:\\Ewallet Client\"";
            string pathToStart = "\"C:\\Ewallet Client\\Update.exe\"";
            _cmd.ProcessStart("icacls.exe", $"{path} /remove:d {GetHelper.GetUserName()} /grant:r {GetHelper.GetUserName()}:(OI)(CI)F /T");
            LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.eWalletFolderPermission + GetHelper.GetDatetimeNow());

            nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";/* . olacak */
            nfi.NumberGroupSeparator = ",";/* , olacak */
            LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.SeperatorsEdited + GetHelper.GetDatetimeNow());

            _cmd.EditEwalletRegistryKeys();
            LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.eWalletRegistryKeysEdited + GetHelper.GetDatetimeNow());

            Process.Start(pathToStart);
            LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.eWalletUpdaterStarted + GetHelper.GetDatetimeNow());
        }

        private void nasılKullanırımToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Process.Start("Chrome.exe", @"C:\ProgramData\MayenTroubleshooter\Rehber\rehber.pdf");

        }

        private void hostDosyasınıGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = @"\\10.94.92.130\service-shares$\TR_IST_Site_IT_SerialNumbers\tsUpdate\Files\hosts\hosts";
            if (MessageBox.Show("Ofiste veya VPN'e bağlı olmanız gerekiyor. Ofiste ya da VPN'e bağlı mısınız?"+ "\n \n \n"+ "BİLGİ: IT Personeli bir sonraki ekranda domain admin girmelidir.","UYARI",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var result = _cmd.ProcessStartRunAs("powershell.exe", $"copy-item {path} C:\\Windows\\System32\\drivers\\etc\\hosts -force -recurse -verbose");
            }
        }

        private void aktifbankToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void aktifbankToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            _cmd.Copy($"{_cmd.ProjectFilesPath("Aktifbank")}", $"C:\\Users\\{_cmd.GetCurrentUsername()}\\Desktop");
        }

        private void galeriaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _cmd.Copy($"{_cmd.ProjectFilesPath("Galeria")}", $"C:\\Users\\{_cmd.GetCurrentUsername()}\\Desktop");

        }

        private void alibabaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _cmd.Copy($"{_cmd.ProjectFilesPath("Alibaba")}", $"C:\\Users\\{_cmd.GetCurrentUsername()}\\Desktop");

        }

        private void letgoIninalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _cmd.Copy($"{_cmd.ProjectFilesPath("ininal_letgo")}", $"C:\\Users\\{_cmd.GetCurrentUsername()}\\Desktop");

        }

        private void multinetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _cmd.Copy($"{_cmd.ProjectFilesPath("Multinet")}", $"C:\\Users\\{_cmd.GetCurrentUsername()}\\Desktop");
        }
    }
}
