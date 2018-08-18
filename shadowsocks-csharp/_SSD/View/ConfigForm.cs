using Shadowsocks.Model;
using System.Collections.Generic;

namespace Shadowsocks.View {
    public partial class ConfigForm {
        private Dictionary<string, Server> SubscriptionServerMap = new Dictionary<string, Server>();
        private void LoadSubscriptionServerNameList(Configuration modifiedConfiguration) {
            SubscriptionServerMap.Clear();
            foreach (var subscription in modifiedConfiguration.subscriptions) {
                foreach (var server in subscription.servers) {
                    var ServerText = server.NamePrefix(Server.PREFIX_AIRPORT) + " " + server.FriendlyName();
                    ServersListBox.Items.Add(ServerText);
                    SubscriptionServerMap.Add(ServerText, server);
                }
            }
        }

        private void DisableMove() {
            MoveUpButton.Visible = false;
            MoveDownButton.Visible = false;
        }

        private void LoadSelectedSubscriptionServerDetails() {
            if (ServersListBox.SelectedIndex >= _modifiedConfiguration.configs.Count) {
                SetServerDetailsToUI(SubscriptionServerMap[(string)ServersListBox.Items[ServersListBox.SelectedIndex]]);
            }
        }
    }
}
