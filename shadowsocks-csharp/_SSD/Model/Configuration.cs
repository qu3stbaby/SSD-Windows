using Newtonsoft.Json;
using Shadowsocks.Controller;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Shadowsocks.Model {
    public partial class Configuration {
        public List<Subscription> subscriptions = new List<Subscription>();

        public static void LoadSubscription(Configuration configuration_subscription) {
            if (configuration_subscription.subscriptions == null) {
                configuration_subscription.subscriptions = new List<Subscription>();
            }
        }

        private static void InitJsonSave() {

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
        }

        public Server CurrentServerEx() {
            if (index >= 0 && index < configs.Count) {
                return configs[index];
            }
            else if (index >= configs.Count) {
                var real_index = index - configs.Count;
                foreach (var subscription in subscriptions) {
                    if (subscription.servers.Count >= real_index - 1) {
                        return subscription.servers[real_index];
                    }
                    real_index -= subscription.servers.Count;
                }
            }
            return GetDefaultServer();
        }

        public void UpdateAllSubscription(NotifyIcon notifyIcon = null) {
            var web_subscribe = new WebClient();
            for (var index = 0; index <= subscriptions.Count - 1; index++) {
                try {
                    var buffer = web_subscribe.DownloadData(subscriptions[index].url);
                    var text = Encoding.GetEncoding("UTF-8").GetString(buffer);
                    text.Replace('-', '+');
                    text.Replace('_', '/');
                    var mod4 = text.Length % 4;
                    if (mod4 > 0) {
                        text += new string('=', 4 - mod4);
                    }
                    var json_buffer = Convert.FromBase64String(text);
                    var json_text = Encoding.UTF8.GetString(json_buffer);
                    var url_backup = subscriptions[index].url;
                    subscriptions[index] = JsonConvert.DeserializeObject<Subscription>(json_text);
                    subscriptions[index].url = url_backup;
                }
                catch (Exception) {
                    if (notifyIcon != null) {
                        notifyIcon.BalloonTipTitle = I18N.GetString("Subscribe Fail");
                        notifyIcon.BalloonTipText = string.Format(I18N.GetString("Fail Link: {0}"), subscriptions[index].url);
                        notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                        notifyIcon.ShowBalloonTip(0);
                    }
                    subscriptions[index].airport = "(error)";
                    continue;
                }
                foreach (var server in subscriptions[index].servers) {
                    server.server_port = subscriptions[index].port;
                    server.password = subscriptions[index].password;
                    server.method = subscriptions[index].encryption;
                    server.subscription = subscriptions[index];
                }
                if (notifyIcon != null) {
                    notifyIcon.BalloonTipTitle = I18N.GetString("Subscribe Success");
                    notifyIcon.BalloonTipText = string.Format(I18N.GetString("Success Airport: {0}"), subscriptions[index].airport);
                    notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                    notifyIcon.ShowBalloonTip(0);
                }
            }
            Save(this);
        }
    }
}
