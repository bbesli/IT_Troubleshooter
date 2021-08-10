using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TroubleshooterUI.Entities;

namespace TroubleshooterUI.Business.Abstract
{
    public interface ICommands
    {
        void OpenSpeedTestForm();
        bool CheckForVPNInterface();
        void OpenSipIdForm();
        void ProxyPac(bool active);
        void ProxyBoth(bool active);
        void ProxyServer(bool active);
        void ShowNotifyMessage(Notify notify);
        SecureString AdminPassword();
        void CopyAnydeskId(string anydeskId);
        int RandomSleepTime();
        string FindAnydeskId(string anydesk);
        void DisableIPv6();
        void EditEwalletRegistryKeys();
        //void ProgressBarProgress(int maximumValue);
        void AktifCTIService();
        IResult SetSIPId(string sipId);
        IResult KillProcess(string[] toKill);
        void RestartFortiClientVPN();
        void ReadConfigFile();
        void ProcessStart(string appName);
        IResult ProcessStart(string appName, string argumentString);
        IResult ProcessStartRunAs(string appName, string argumentString);
        //void ProcessStart(string appName, string argumentString);
        void CreateTextFileAvayaInfo(int i);
        void Copy(string sourceDir, string targetDir);
        void InstallCertificate(string fileLocation, StoreName storeName, StoreLocation storeLocation);
        string BoxesForLog(object object_1, object object_2);
        string GetCurrentUsername();

        string ProjectFilesPath(string projectName);
    }
}
