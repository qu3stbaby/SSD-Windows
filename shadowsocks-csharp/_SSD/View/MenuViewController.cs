using System;
using System.Windows.Forms;
using Shadowsocks.Model;
using Shadowsocks.Util;

namespace Shadowsocks.View {
    public partial class MenuViewController {
        private MenuItem MenuGroup_subscribe;
        private MenuItem MenuItem_subscribe_Manage;
        private MenuItem MenuItem_subscribe_Update;
        private MenuItem MenuItem_subscribe_UpdateUseProxy = null;

        private SubscriptionSettingsForm ManageForm;

        private System.Timers.Timer Timer_detect_virus;
        private System.Timers.Timer Timer_update_latency;

        private void DisableFirstRun() {

        }

        private void InitOther() {
            Timer_detect_virus = new System.Timers.Timer(1000.0 * 30);
            Timer_detect_virus.Elapsed += Timer_detect_virus_Elapsed;
            Timer_detect_virus.Start();

            Timer_update_latency = new System.Timers.Timer(1000.0 * 3);
            Timer_update_latency.Elapsed += Timer_update_latency_Elapsed;
            Timer_update_latency.Start();

            contextMenu1.Popup += PreloadMenu;
        }

        private void PreloadMenu(object sender, EventArgs e) {
            UpdateServersMenu();
        }

        private void Timer_detect_virus_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
            if (Utils.DetectVirus()) {
                Quit_Click(null, null);
            }
        }

        private void Timer_update_latency_Elapsed(object sender, System.Timers.ElapsedEventArgs e) {
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
                    MenuItem_subscribe_Manage = CreateMenuItem("Settings", new EventHandler(SubscriptionSettings)),
                    MenuItem_subscribe_Update = CreateMenuItem("Update", new EventHandler(Subscription_Update)),
                    MenuItem_subscribe_UpdateUseProxy = CreateMenuItem("Update(use proxy)", new EventHandler(Subscription_UpdateUseProxy))
                });
            return MenuGroup_subscribe;
        }

        private MenuItem CreateAirportSeperator() {
            return new MenuItem("-");
        }

        private void SubscriptionSettings(object sender, EventArgs e) {
            if (ManageForm == null) {
                ManageForm = new SubscriptionSettingsForm(controller);
                ManageForm.FormClosed += SubscriptionSettings_Recycle;
                ManageForm.Show();
            }
            ManageForm.Activate();
        }

        private void SubscriptionSettings_Recycle(object sender, EventArgs e) {
            ManageForm.Dispose();
            ManageForm = null;
        }

        private void Subscription_Update(object sender, EventArgs e) {
            controller.GetCurrentConfiguration().UpdateAllSubscription(_notifyIcon);
            UpdateAirportMenu();
        }

        private void Subscription_UpdateUseProxy(object sender, EventArgs e) {

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
    }
}
