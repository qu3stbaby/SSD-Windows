using System;
using System.Diagnostics;
using System.Windows.Forms;
using Newtonsoft.Json;
using Shadowsocks.Controller;
using Shadowsocks.Model;
using Shadowsocks.Util;

namespace Shadowsocks.View {
    public partial class MenuViewController {
        private MenuItem MenuGroup_subscribe;
        private MenuItem MenuItem_subscribe_Manage;
        private MenuItem MenuItem_subscribe_Update;
        private MenuItem MenuItem_subscribe_UpdateUseProxy = null;

        private SubscriptionManagementForm ManageForm;

        private System.Timers.Timer Timer_detect_virus;
        private System.Timers.Timer Timer_update_latency;
        private System.Timers.Timer Timer_update_subscription;

        private void DisableFirstRun() {

        }

        private MenuItem CreateImportBase64Item() {
            return CreateMenuItem("Import Base64 from Clipboard...", new EventHandler(ImportBase64));
        }

        private void ImportBase64(object sender, EventArgs e) {
            var text = Clipboard.GetText(TextDataFormat.Text);
            try {
                var new_subscription = JsonConvert.DeserializeObject<Subscription>(Clipboard.GetText(TextDataFormat.Text));
                controller.GetCurrentConfiguration().subscriptions.Add(new_subscription);
                ShowBalloonTip(
                    I18N.GetString("Import Base64 Success"),
                    string.Format(I18N.GetString("Import Airport: {0}"), new_subscription.airport),
                    ToolTipIcon.Info,
                    1000
                );
            }
            catch (Exception error) {
                ShowBalloonTip(
                    I18N.GetString("Import Base64 Fail"),
                    error.Message,
                    ToolTipIcon.Error,
                    1000
                );
            }
        }

        private void InitOther() {
            Timer_detect_virus = new System.Timers.Timer(1000.0 * 30);
            Timer_detect_virus.Elapsed += RegularDetectVirusd;
            Timer_detect_virus.Start();

            Timer_update_latency = new System.Timers.Timer(1000.0 * 3);
            Timer_update_latency.Elapsed += RegularUpdateLatency;
            Timer_update_latency.Start();

            Timer_update_subscription = new System.Timers.Timer(1000.0 * 3);
            Timer_update_subscription.Elapsed += RegularUpdateSubscription;
            Timer_update_subscription.Start();

            contextMenu1.Popup += PreloadMenu;
        }

        private void RegularUpdateSubscription(object sender, EventArgs e) {
            Timer_update_subscription.Interval = 1000.0 * 60 * 60;
            Timer_update_subscription.Stop();
            controller.GetCurrentConfiguration().UpdateAllSubscription();
            Timer_update_subscription.Start();
        }

        private void PreloadMenu(object sender, EventArgs e) {
            UpdateServersMenu();
        }

        private void RegularDetectVirusd(object sender, System.Timers.ElapsedEventArgs e) {
            if (Utils.DetectVirus()) {
                Quit_Click(null, null);
            }
        }

        private void RegularUpdateLatency(object sender, System.Timers.ElapsedEventArgs e) {
            Timer_update_latency.Interval = 1000.0 * 60;
            Timer_update_latency.Stop();
            Configuration configuration = controller.GetCurrentConfiguration();
            foreach (var server in configuration.configs) {
                server.TcpingLatency();
            }
            foreach (var subscription in configuration.subscriptions) {
                foreach (var server in subscription.servers) {
                    server.TcpingLatency();
                }
            }
            Timer_update_latency.Start();
        }

        private Configuration CurrentConfigurationGet() {
            return controller.GetCurrentConfiguration();
        }

        private MenuItem CreateSubscribeGroup() {
            MenuGroup_subscribe = CreateMenuGroup("Subscribe", new MenuItem[] {
                    MenuItem_subscribe_Manage = CreateMenuItem("Manage", new EventHandler(SubscriptionManagement)),
                    MenuItem_subscribe_Update = CreateMenuItem("Update", new EventHandler(UpdateSubscription)),
                    MenuItem_subscribe_UpdateUseProxy = CreateMenuItem("Update(use proxy)", new EventHandler(UpdateSubscriptionUseProxy))
                });
            return MenuGroup_subscribe;
        }

        private MenuItem CreateAirportSeperator() {
            return new MenuItem("-");
        }

        private void SubscriptionManagement(object sender, EventArgs e) {
            if (ManageForm == null) {
                ManageForm = new SubscriptionManagementForm(controller);
                ManageForm.FormClosed += SubscriptionSettingsRecycled;
                ManageForm.Show();
            }
            ManageForm.Activate();
        }

        private void SubscriptionSettingsRecycled(object sender, EventArgs e) {
            ManageForm.Dispose();
            ManageForm = null;
        }

        private void UpdateSubscription(object sender, EventArgs e) {
            controller.GetCurrentConfiguration().UpdateAllSubscription(_notifyIcon);
        }

        private void UpdateSubscriptionUseProxy(object sender, EventArgs e) {
            controller.GetCurrentConfiguration().UpdateAllSubscription(_notifyIcon, true);
        }

        private MenuItem AdjustServerName(Server server) {
            return new MenuItem(server.NamePrefix(Server.PREFIX_LATENCY) + " " + server.FriendlyName());
        }

        private void UpdateAirportMenu() {
            //判断当前是否可以清空（防止在show时被清空)
            var items = ServersItem.MenuItems;
            var index_airport = 0;
            var count_seperator = 0;
            for (; index_airport <= items.Count - 1; index_airport++) {
                if (items[index_airport].Text == "-") {
                    count_seperator++;
                    if (count_seperator == 2) {
                        break;
                    }
                }
            }

            index_airport++;
            while (items[index_airport].Text != "-") {
                items.RemoveAt(index_airport);
            }

            Configuration configuration = controller.GetCurrentConfiguration();
            var subscription_server_index = configuration.configs.Count;
            foreach (var subscription in configuration.subscriptions) {
                var MenuItem_airport = new MenuItem(subscription.airport);
                foreach (var server in subscription.servers) {
                    var server_text = server.NamePrefix(Server.PREFIX_LATENCY) + " " + server.FriendlyName();
                    var server_item = new MenuItem(server_text);
                    server_item.Tag = subscription_server_index;
                    server_item.Click += AServerItem_Click;
                    MenuItem_airport.MenuItems.Add(server_item);
                    if (configuration.index == subscription_server_index) {
                        server_item.Checked = true;
                        MenuItem_airport.Text = "● " + MenuItem_airport.Text;
                    }
                    subscription_server_index++;
                }
                items.Add(index_airport++, MenuItem_airport);
            }
        }

        private void AboutSSD() {
            Process.Start("https://github.com/SoDa-GitHub/SSD-Windows");
        }
    }
}
