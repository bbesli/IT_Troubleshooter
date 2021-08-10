using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string SipIdRegistered = "Sip kaydı güncellendi.";
        public static string SipIdCannotRegistered = "Sip kaydı güncellenemedi.";
        public static string KillingSuccess = "FortiClientVPN kapatıldı. Otomatik olarak yeniden başlayacak.";
        public static string KillingError = "FortiClientVPN kapatılamadı. Bilgi İşlem ile iletişime geçiniz.";
        public static string ProgressInfo = "BİLGİ";
        public static string ProgressStarted = "İşlem başladı.";
        public static string ProgressDone = "İşlem tamamlandı.";
        public static string AnydeskIdCopied = "Anydesk ID Kopyalandı.";
        public static string ProxyPacOpened = "Proxy PAC açıldı.";
        public static string ProxyPacClose = "Proxy PAC kapatıldı.";
        public static string HostsCopyDone = "hosts dosyası güncellendi.";
        public static string HostsCopyError = "hosts dosyası güncelleme hatası.";
        public static string HostsCopyErrorMsg = "Vpn bağlantınız olduğundan emin olun.";
        
    }
}
