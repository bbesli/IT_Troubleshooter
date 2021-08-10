using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TroubleshooterUI.Utilities.Log.Concrete;

namespace TroubleshooterUI.Constants
{
    public static class LoggingMessages
    {
        public static string RunApplication = $"Uygulama {GetHelper.GetUserName()} tarafından başlatıldı. ";
        public static string CloseApplication = $"Uygulama {GetHelper.GetUserName()} tarafından kapatıldı. ";
        public static string eWalletFolderPermission = $"eWallet klasörüne kullanıcı için full yetki tanımlandı.";
        public static string IPv6DisabledAllInterfaces = $"IPv6 tüm adaptörlerde kapatıldı.";
        public static string SeperatorsEdited = $"Ayıraç işaretleri eWallet'ın ihtiyaç duyduğu şekilde düzenlendi.";
        public static string eWalletRegistryKeysEdited = $"eWallet regedit keyleri update edildi.";
        public static string HostsFileUpdated = $"hosts dosyası update edildi.";
        public static string HostsFileUpdateError = $"hosts dosyası update edilemedi.[HATA]";
        public static string eWalletUpdaterStarted = $"eWallet update başlatıldı.";
        public static string AnydeskIdCopied = $"AnydeskID kopyalandı.";
        public static string FortiRestarted = $"Forticlient yeniden başlatıldı.";
        public static string FortiRestartedError = $"Forticlient yeniden başlatılmadı.[HATA]";
        public static string SpeedTestFormOpening = $"Speed Test başlatıldı.";
        public static string SpeedTestFormOpened = $"Speed Test formu başarıyla açıldı.";
        public static string SpeedTesFormClosed = $"Speed Test kapatıldı.";
        public static string MobileModemUsageNo = $"Mobil modem kullanılmıyor seçeneği seçildi.";
        public static string MobileModemUsageYes = $"Mobil modem kullanmakta olduğunu belirten seçenek seçildi.";
        public static string ModemRestartChoice = $"Modem'i restart etmesi gerektiği bilgisi mesaj olarak gösterildi.";
        public static string ProxyOpened = $"Proxy pac açıldı.";
        public static string ProxyClosed= $"Proxy pac kapatıldı.";
        public static string SetSIPIdSucceded = $"SIP ID tanımlama başarıyla yapıldı.";
        public static string SetSIPIdError = $"SIP ID tanımlama hatası: ";
        public static string AktifCTiStoppedSucceded = $"CTi servisi başarıyla durduruldu.";
        public static string AktifCTiStartedSucceded = $"CTi servisi başarıyla başlatıldı.";
        public static string AktifCTiStoppedError = $"CTi servisi dururulamadı: ";
        public static string AktifCTiStartedError = $"CTi servisi başlatılamadı: ";
        public static string SIPFormOpened = $"SIP Formu başlatıldı. ";
        public static string CertificateInstalled = $" Sertifika yüklendi. ";
        public static string CertificateInstalledError = $" Sertifika yüklenemedi. ";
        public static string AvayaInfoTextCreated = $"Avaya sıfırlandı, bilgiler masaüstüne alındı. ";
        public static string AddInInstalled = $"webPhone Add-In kuruldu. ";
        public static string AddInPackageInstalled = $"webPhone Add-In Paketi kuruldu. ";
        public static string AddInPackageInstallationError = $"webPhone Add-In Paketi kurulum hatası. ";
        public static string SpeedTestResult = $" Son alınan hız testi sonucu: ";
        public static string AvayaInfoTextCreationError = $"Avaya bilgisi kaydedilemedi. Zaten sıfırlanmış. Uygulamayı tekrar açıp masaüstüne kayıtlı dosyadaki bilgiler ile giriş yapın. ";



    }
}
