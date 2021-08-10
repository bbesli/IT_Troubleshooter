using Business.Constants;
using Core.Utilities.Results;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using TroubleshooterUI.Business.Abstract;
using TroubleshooterUI.Constants;
using TroubleshooterUI.Entities;
using TroubleshooterUI.Utilities.Log.Concrete;

namespace TroubleshooterUI.Business.Concrete
{
    public class Commands : ICommands
    {
        RegistryKey RegKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);

        NetworkInterface[] allInterfaces = NetworkInterface.GetAllNetworkInterfaces();
        List<Process> processes = new List<Process>();
        List<string> lst = new List<string>();
        string UsersConfig = $"C:\\Users\\{Environment.UserName}\\AppData\\Roaming\\Avaya\\Avaya one-X Communicator\\config.xml";

        public bool CheckForVPNInterface()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface Interface in interfaces)
                {
                    if (Interface.Description.Contains("Fortinet SSL VPN Virtual Ethernet Adapter")
                      && Interface.OperationalStatus == OperationalStatus.Up)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void ShowNotifyMessage(Notify notify)
        {
            using (NotifyIcon ntfIcon = new NotifyIcon())
            {
                ntfIcon.Visible = true;
                //ntfIcon.Icon = new Icon(@"..\..\images\Logo_M.ico");
                ntfIcon.Icon = SystemIcons.Exclamation;
                ntfIcon.BalloonTipIcon = notify.Icon;
                ntfIcon.BalloonTipTitle = notify.Title;
                ntfIcon.BalloonTipText = notify.Message;
                ntfIcon.ShowBalloonTip(notify.Interval);
            }
        }
        public SecureString AdminPassword()
        {
            var pass = new SecureString();
            pass.AppendChar('!');
            

            return pass;
        }

        public void ProxyPac(bool active)
        {
            if (active)
            {
                RegKey.SetValue("AutoConfigURL", "http://10.94.92.130/ProxyPAC/proxy.pac");
                ShowNotifyMessage(new Notify { Icon = ToolTipIcon.Info, Interval = 3000, Message = Messages.ProxyPacOpened, Title = Messages.ProgressInfo });
                LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.ProxyOpened + GetHelper.GetDatetimeNow());
            }
            else
            {
                RegKey.SetValue("AutoConfigURL", "");
                ShowNotifyMessage(new Notify { Icon = ToolTipIcon.Info, Interval = 3000, Message = Messages.ProxyPacClose, Title = Messages.ProgressInfo });
                LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.ProxyClosed + GetHelper.GetDatetimeNow());

            }
        }

        public void ProxyBoth(bool active)
        {
            if (active)
            {
                RegKey.SetValue("AutoConfigURL", "http://10.94.92.130/ProxyPAC/proxy.pac");
                RegKey.SetValue("ProxyEnable", "1");
            }
            else
            {
                RegKey.SetValue("AutoConfigURL", "");
                RegKey.SetValue("ProxyEnable", "");
            }
        }

        public void ProxyServer(bool active)
        {
            if (active)
            {
                RegKey.SetValue("ProxyEnable", "1");
            }
            else
            {
                RegKey.SetValue("ProxyEnable", "");
            }
        }
        public void CopyAnydeskId(string anydeskId)
        {
            Clipboard.SetText(anydeskId);
            ShowNotifyMessage(new Notify { Icon = ToolTipIcon.Info, Interval = 3000, Message = Messages.AnydeskIdCopied, Title = Messages.ProgressInfo });
            ProcessStart(@"C:\Program Files (x86)\AnyDeskMSI\AnyDeskMSI.exe");

            #region Process
            //Process.Start(@"C:\Program Files (x86)\AnyDeskMSI\AnyDeskMSI.exe");

            #endregion
        }

        public int RandomSleepTime()
        {
            Random rnd = new Random();
            int Random = rnd.Next(1000, 4000);


            return Random;
        }

        public string FindAnydeskId(string anydesk)
        {
            string path = @"C:\ProgramData\AnyDesk\ad_msi\system.conf";

            IEnumerable<string> lines = File.ReadAllLines(path);

            string input = "anynet.id=";

            IEnumerable<string> matches = !String.IsNullOrEmpty(input)
                                          ? lines.Where(line => line.IndexOf(input, StringComparison.OrdinalIgnoreCase) >= 0)
                                          : Enumerable.Empty<string>();
            foreach (var match in matches)
            {
                string[] id = Regex.Split(match, @"\W+");
                anydesk = id[3];
            }
            return anydesk;
        }

        public void DisableIPv6()
        {
            //ProgressBar p = Application.OpenForms["Form1"].Controls.Find("prgProgress", true)[0] as ProgressBar;
            //p.Maximum = allInterfaces.Length;

            foreach (var adapter in allInterfaces)
            {
                ProcessStart("powershell.exe", $"Disable-NetAdapterBinding -Name \'{adapter.Name}\' -ComponentID ms_tcpip6");
                #region Process
                //var psi = new ProcessStartInfo
                //{
                //    FileName = "powershell.exe",
                //    Arguments = $"Disable-NetAdapterBinding -Name \'{adapter.Name}\' -ComponentID ms_tcpip6",
                //    UserName = "administrator",
                //    Domain = ".",
                //    Password = AdminPassword(),
                //    UseShellExecute = false,
                //    CreateNoWindow = true,
                //    RedirectStandardOutput = true,
                //    RedirectStandardError = true
                //};
                //Process.Start(psi); 
                #endregion
            }
            LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.IPv6DisabledAllInterfaces + GetHelper.GetDatetimeNow());
            ShowNotifyMessage(new Notify { Icon = ToolTipIcon.Info, Interval = 3000, Message = LoggingMessages.IPv6DisabledAllInterfaces, Title = Messages.ProgressInfo });

        }

        #region ProgressBarProgress
        public void ProgressBarProgress(int maximumValue)
        {
            ProgressBar p = Application.OpenForms["Form1"].Controls.Find("prgProgress", true)[0] as ProgressBar;

            p.Visible = true;
            p.Maximum = maximumValue;
            Thread.Sleep(RandomSleepTime());
            p.Value += 1;
            if (p.Value == maximumValue)
            {
                p.Value = 0;
                p.Visible = false;
                ShowNotifyMessage(new Notify { Icon = ToolTipIcon.Info, Interval = 3000, Message = Messages.ProgressDone, Title = Messages.ProgressInfo });
            }
        }
        #endregion

        public void AktifCTIService()
        {
            ProcessStart("powershell.exe", $"Stop-Process -Name \'AktifCTIService.exe\' -Force");
            try
            {
                ProcessStart("sc", "stop aktifcti");
                LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.AktifCTiStoppedSucceded + GetHelper.GetDatetimeNow());
            }
            catch (Exception ex)
            {
                LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.AktifCTiStoppedError + ex + GetHelper.GetDatetimeNow());
            }

            #region StopProcess
            //var psi = new ProcessStartInfo
            //{
            //    FileName = @"C:\ProgramData\MayenTroubleshooter\Aktifbank\AtkifCTI_Stop.bat",
            //    UserName = "administrator",
            //    Domain = ".",
            //    Password = AdminPassword(),
            //    UseShellExecute = false,
            //    CreateNoWindow = true,
            //    RedirectStandardOutput = true,
            //    RedirectStandardError = true
            //};
            //Process.Start(psi); 
            #endregion

            Thread.Sleep(1500);

            try
            {
                ProcessStart("sc", "start aktifcti");
                LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.AktifCTiStartedSucceded + GetHelper.GetDatetimeNow());
            }
            catch (Exception ex)
            {
                LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.AktifCTiStartedError + ex + GetHelper.GetDatetimeNow());
            }
            #region StartProcess
            //var psi_ = new ProcessStartInfo
            //{
            //    FileName = @"C:\ProgramData\MayenTroubleshooter\Aktifbank\AtkifCTI_Start.bat",
            //    UserName = "administrator",
            //    Domain = ".",
            //    Password = AdminPassword(),
            //    UseShellExecute = false,
            //    CreateNoWindow = true,
            //    RedirectStandardOutput = true,
            //    RedirectStandardError = true
            //};
            //Process.Start(psi_); 
            #endregion
        }

        public IResult SetSIPId(string sipId)
        {
            try
            {
                var path = @"C:\it_troubleshooter\aktifbank\Softphone.config";
                string[] arrLine = File.ReadAllLines(path);
                string SIPID = sipId;
                string SIP = $"\"{SIPID}\"";
                string IP = "\"10.90.27.120:5060\"";
                string protocol = "\"udp\"";
                arrLine[3] = "\t" + "<Connectivity user =" + SIP + " server=" + IP + " protocol=" + protocol + "/>";

                string genesysPath = Path.GetFullPath(@"C:\Program Files (x86)\GCTI\Genesys Softphone\Softphone.config");
                string genesysPathWithQuotes = $"\"{genesysPath}\"";

                string configPath = Path.GetFullPath(@"C:\it_troubleshooter\aktifbank\Softphone.config");
                string configPathWithQuotes = $"\"{configPath}\"";

                ProcessStart("xcopy.exe",
                    $"{configPathWithQuotes} {genesysPathWithQuotes} /K /D /H /Y");

                File.WriteAllLines(path, arrLine);
                LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.SetSIPIdSucceded + GetHelper.GetDatetimeNow());
                return new SuccessResult(Messages.SipIdRegistered);
            }
            catch (Exception ex)
            {
                LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.SetSIPIdError + ex.ToString() + GetHelper.GetDatetimeNow());
                return new ErrorResult(Messages.SipIdCannotRegistered);
            }
        }

        public IResult KillProcess(string[] toKill)
        {
            Process.Start(@"C:\Program Files\Fortinet\FortiClient\FortiClient.exe");
            foreach (var kill in toKill)
            {
                var addTaskList = Process.GetProcesses().Where(f => f.ProcessName == kill).FirstOrDefault();
                processes.Add(addTaskList);
            }
            try
            {
                foreach (var process in processes)
                {
                    ProcessStart("powershell.exe", $"Stop-Process -Name \'{process.ProcessName}\' -Force");
                    #region Process

                    //var psi = new ProcessStartInfo
                    //{
                    //    FileName = "powershell.exe",
                    //    Arguments = $"Stop-Process -Name \'{process.ProcessName}\' -Force",
                    //    UserName = "administrator",
                    //    Domain = ".",
                    //    Password = AdminPassword(),
                    //    UseShellExecute = false,
                    //    CreateNoWindow = true,
                    //    RedirectStandardOutput = true,
                    //    RedirectStandardError = true
                    //};
                    //Process.Start(psi);
                    #endregion

                }
                Thread.Sleep(2000);
                Process.Start(@"C:\Program Files\Fortinet\FortiClient\FortiClient.exe");
                processes.Clear();
                return new SuccessResult();
            }
            catch (Exception)
            {
                processes.Clear();
                return new ErrorResult();
            }
        }

        public void RestartFortiClientVPN()
        {
            var ProcessDone = KillProcess(new string[] { "FortiClient", "FortiSettings", "FortiSSLVPNdaemon", "FortiTray" });
            if (ProcessDone.Success)
            {
                ShowNotifyMessage(new Notify { Icon = ToolTipIcon.Info, Interval = 3000, Message = Messages.KillingSuccess, Title = Messages.ProgressInfo });
                LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.FortiRestarted + GetHelper.GetDatetimeNow());
                Process.Start(@"C:\Program Files\Fortinet\FortiClient\FortiClient.exe");
            }
            else
            {
                ShowNotifyMessage(new Notify { Icon = ToolTipIcon.Error, Interval = 3000, Message = Messages.KillingError, Title = Messages.ProgressInfo });
                LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.FortiRestartedError + GetHelper.GetDatetimeNow());
            }
        }

        public void OpenSpeedTestForm()
        {
            SpeedTest st = new SpeedTest(new Commands());
            st.Show();
        }

        public void OpenSipIdForm()
        {
            SipId si = new SipId(new Commands());
            si.Show();
            LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.SIPFormOpened + GetHelper.GetDatetimeNow());
        }

        public void ReadConfigFile()
        {
            using (XmlReader reader = XmlReader.Create(UsersConfig))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        //return only when you have START tag  
                        switch (reader.Name.ToString())
                        {
                            case "name":
                                lst.Add(reader.ReadString());
                                //rtBox.Text += "Name of the Element is : " + reader.ReadString()+ "\n";
                                break;
                            case "value":
                                lst.Add(reader.ReadString());

                                //rtBox.Text += "Value is : " + reader.ReadString()+"\n";
                                break;
                        }
                    }
                }
            }

            if (lst.Contains("SipUserAccount"))
            {
                CreateTextFileAvayaInfo(lst.IndexOf("SipUserAccount") + 2);
            }
            else
            {
                ShowNotifyMessage(new Notify { Icon = ToolTipIcon.Error, Interval = 3000, Message = LoggingMessages.AvayaInfoTextCreationError, Title = "HATA" });
                LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.AvayaInfoTextCreationError + GetHelper.GetDatetimeNow());
            }
        }

        public void CreateTextFileAvayaInfo(int i)
        {
            try
            {
                string fileName = $"C:\\Users\\{GetHelper.GetUserName()}\\Desktop\\AvayaBilgileri.txt";

                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                // Create a new file     
                using (FileStream fs = File.Create(fileName))
                {
                    // Add some text to file    
                    Byte[] title = new UTF8Encoding(true).GetBytes("Avaya Login ID: " + lst[i + 1] + "\n");
                    fs.Write(title, 0, title.Length);
                    byte[] author = new UTF8Encoding(true).GetBytes("Avaya Şifresi: 1234");
                    fs.Write(author, 0, author.Length);
                }
                ShowNotifyMessage(new Notify { Icon = ToolTipIcon.Info, Interval = 3000, Message = Messages.ProgressDone, Title = "BİLGİ" });
                LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.AvayaInfoTextCreated + GetHelper.GetDatetimeNow());
            }
            catch (Exception Ex)
            {
                ShowNotifyMessage(new Notify { Icon = ToolTipIcon.Error, Interval = 3000, Message = Ex.ToString(), Title = "HATA" });
                LogHelper.Log(Utilities.Log.Enums.LogTarget.File, LoggingMessages.AvayaInfoTextCreationError + Ex + GetHelper.GetDatetimeNow());
            }
        }

        public string ProjectFilesPath(string projectName)
        {
            switch (projectName)
            {
                case "Aktifbank":
                    return @"C:\Users\Public\Proje_Kisayollari\Aktifbank";
                case "Galeria":
                    return @"C:\Users\Public\Proje_Kisayollari\Galeria";
                case "Multinet":
                    return @"C:\Users\Public\Proje_Kisayollari\Multinet";
                case "ininal_letgo":
                    return @"C:\Users\Public\Proje_Kisayollari\Ininal";
                case "Alibaba":
                    return @"C:\Users\Public\Proje_Kisayollari\Alibaba";
                default:
                    return "";
            }
        }

        public void Copy(string sourceDir, string targetDir)
        {
            Directory.CreateDirectory(targetDir);

            foreach (var file in Directory.GetFiles(sourceDir))
                File.Copy(file, Path.Combine(targetDir, Path.GetFileName(file)), true);

            foreach (var directory in Directory.GetDirectories(sourceDir))
                Copy(directory, Path.Combine(targetDir, Path.GetFileName(directory)));
        }

        public void ProcessStart(string appName)
        {
            var psi = new ProcessStartInfo
            {
                FileName = appName,
                UserName = "administrator",
                Domain = ".",
                Password = AdminPassword(),
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };
            Process.Start(psi);
        }

        public string GetCurrentUsername()
        {
            return Environment.UserName;
        }

        public IResult ProcessStart(string appName, string argumentString)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = appName,
                    Arguments = argumentString,
                    UserName = "administrator",
                    Domain = ".",
                    Password = AdminPassword(),
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };
                Process.Start(psi);

                return new SuccessResult();
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.ToString());
            }

        }
        public IResult ProcessStartRunAs(string appName, string argumentString)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    UseShellExecute = true,
                    Verb = "runas",
                    FileName = appName,
                    Arguments = argumentString,
                };
                Process.Start(psi);

                return new SuccessResult();
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.ToString());
            }

        }

        public void InstallCertificate(string fileLocation, StoreName storeName, StoreLocation storeLocation)
        {
            try
            {
                ProcessStart("cmd.exe", $"CertMgr.exe /add {fileLocation} /s /r {storeLocation} {storeName}");
                LogHelper.Log(Utilities.Log.Enums.LogTarget.File, fileLocation + LoggingMessages.CertificateInstalled + GetHelper.GetDatetimeNow());
            }
            catch (Exception ex)
            {
                LogHelper.Log(Utilities.Log.Enums.LogTarget.File, fileLocation + LoggingMessages.CertificateInstalledError + ex + GetHelper.GetDatetimeNow());
            }
            //ProcessStart("powershell.exe", $"Import-Certificate -FilePath \'{fileLocation}\' -CertStoreLocation Cert:\\LocalMachine\\Root");
        }

        public string BoxesForLog(object object_1, object object_2)
        {
            return $"{"[" + object_1.ToString().ToUpper() + "]" + "[" + object_2.ToString().ToUpper() + "]"}";
        }

        public void EditEwalletRegistryKeys()
        {
            ProcessStart("reg.exe", @"ADD HKLM\Software\WOW6432NODE\eWallet /v UpdateURL /t REG_SZ /d \\10.2.235.173\Multinet_Update\Update\update.ini /f");
            ProcessStart("reg.exe", @"ADD HKLM\Software\WOW6432NODE\eWallet /v LastUpdate /t REG_SZ /d 2019-06-26T22:07:23 /f");
        }
    }
}
