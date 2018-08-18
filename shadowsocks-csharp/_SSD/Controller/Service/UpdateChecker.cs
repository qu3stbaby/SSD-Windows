using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Shadowsocks.Controller {
    public partial class UpdateChecker {
        private const string VERSION_BASE = "4.1.1";
        private const string VERSION_SSD = "0.0.2";
        private const string UPDATE_URL_SSD = "https://api.github.com/repos/SoDa-GitHub/SSD-Windows/releases/latest";

        public static bool UnderLowerLimit() {
            var version_end = VERSION_SSD[VERSION_SSD.Length - 1];
            var version_end_num = Convert.ToInt32(version_end);
            if (version_end_num % 2 == 0) {
                if (DateTime.Now > DateTime.Parse("2018-08-25")) {
                    MessageBox.Show(I18N.GetString("当前测试版本已超出支持日期"));
                    return true;
                }
            }

            Logging.Debug("Checking low limit...");
            var web_check = new WebClient();
            web_check.Headers.Add("User-Agent", UserAgent);
            try {
                var buffer = web_check.DownloadData(UPDATE_URL_SSD);
                var text = Encoding.GetEncoding("UTF-8").GetString(buffer);
                var match = Regex.Match(text, @"Limit:\s\d+\.\d+\.\d+");
                if (match.Success) {
                    var text_version = match.Value.Substring("Limit: ".Length);
                    var version_current = new Version(VERSION_SSD);
                    var version_web = new Version(text_version);
                    if (version_current < version_web) {
                        MessageBox.Show(I18N.GetString("当前版本过低"));
                        return true;
                    }
                }

            }
            catch (Exception) {
                return false;
            }
            return false;
        }
    }
}
