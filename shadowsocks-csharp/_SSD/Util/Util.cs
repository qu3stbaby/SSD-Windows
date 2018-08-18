using Microsoft.Win32;
using Shadowsocks.Controller;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Shadowsocks.Util {
    public static partial class Utils {
        public static bool DetectVirus() {
            int offense_count = 0;
            string[] virus_process =
            {
                "360Safe","ZhuDongFangYu",
                "2345SoftSvc","2345RTProtect",
                "BaiduAnSvc","BaiduHips"
            };
            foreach (string process_name in virus_process) {
                offense_count+=Process.GetProcessesByName(process_name).Length;
            }

            string registry_prefix = IntPtr.Size==4 ? @"Software\Microsoft\Windows\CurrentVersion\App Paths\" : @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\App Paths\";
            string[] virus_registry =
            {
                "360safe.exe","360se6.exe",
                "2345MPCSafe.exe","2345Explorer.exe",
                "baidubrowser.exe"
            };
            foreach (string registry_name in virus_registry) {
                RegistryKey registry_virus = Registry.LocalMachine.OpenSubKey(registry_prefix+registry_name);
                if (registry_virus!=null) {
                    offense_count++;
                    registry_virus.Close();
                }
            }
            if (offense_count!=0) {
                //因为只有国行小白才会看到本消息，所以用中文就行了
                MessageBox.Show(I18N.GetString("SSRR无法运行于安装有[360/2345/百度]产品的电脑，告辞！"));
                return true;
            }
            return false;
        }
    }
}
