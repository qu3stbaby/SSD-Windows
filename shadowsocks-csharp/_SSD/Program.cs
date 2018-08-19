using Shadowsocks.Controller;
using System;
using System.Windows.Forms;

namespace Shadowsocks {
    public partial class Program {

        private static void UnexpectedError(bool UI, string message) {
            string text_ui = UI ? "UI" : "non-UI";
            MessageBox.Show(
                $"{I18N.GetString("Unexpected error, shadowsocks will exit. Please report to")} https://github.com/SoDa-GitHub/SSD-Windows/issues {Environment.NewLine}{message}",
                "Shadowsocks " + text_ui + " Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
