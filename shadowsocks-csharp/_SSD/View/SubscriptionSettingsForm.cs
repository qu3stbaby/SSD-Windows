using Shadowsocks.Controller;
using Shadowsocks.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Shadowsocks.View {
    public partial class SubscriptionSettingsForm : Form {
        private const string NAME_AUTO = "(Auto)";

        private ShadowsocksController controller;
        private List<Subscription> subscriptions;

        private bool refresh_switch = true;

        public SubscriptionSettingsForm(ShadowsocksController controller_set) {
            InitializeComponent();
            controller = controller_set;
            subscriptions = controller.GetCurrentConfiguration().subscriptions;
        }

        private void TextBox_name_Enter(object sender, EventArgs e) {
            if (TextBox_name.Text.Trim() == NAME_AUTO) {
                TextBox_name.Text = "";
                TextBox_name.ForeColor = SystemColors.WindowText;
            }
        }

        private void NameAuto() {
            TextBox_name.ForeColor = Color.Gray;
            TextBox_name.Text = NAME_AUTO;
        }

        private void TextBox_name_Leave(object sender, EventArgs e) {
            if (TextBox_name.Text.Trim() == "") {
                NameAuto();
            }
        }

        private void SubscriptionManageForm_Load(object sender, EventArgs e) {
            NameAuto();
            EnableSwitch();
            Subscription_Refresh();
        }

        private void Subscription_Refresh() {
            ListBox_subscription.Items.Clear();
            TextBox_url.Text = "";
            NameAuto();
            controller.GetCurrentConfiguration().UpdateAllSubscription();
            foreach (var subscription in subscriptions) {
                ListBox_subscription.Items.Add(subscription.airport);
            }
            EnableSwitch();
        }

        private void EnableSwitch() {
            ListBox_subscription.Enabled = !refresh_switch;
            TextBox_url.Enabled = !refresh_switch;
            TextBox_name.Enabled = !refresh_switch;
            Button_add.Enabled = !refresh_switch;
            Button_save.Enabled = false;
            Button_delete.Enabled = !refresh_switch;
            refresh_switch = !refresh_switch;
        }

        private void Button_add_Click(object sender, EventArgs e) {
            EnableSwitch();
            //加入重名检验
            subscriptions.Add(GetSubscriptionShowed());
            Subscription_Refresh();
        }

        private void Button_save_Click(object sender, EventArgs e) {
            subscriptions[GetSeletedSubscriptionGlobalIndex()] = GetSubscriptionShowed();
            Subscription_Refresh();
        }

        private Subscription GetSubscriptionShowed() {
            var subscription = new Subscription();
            var airport = TextBox_name.Text.Trim();
            if (airport != NAME_AUTO && airport != "") {
                subscription.airport = airport;
            }
            subscription.url = TextBox_url.Text;
            return subscription;
        }

        private int GetSeletedSubscriptionGlobalIndex() {
            var airport_selected = (string)ListBox_subscription.SelectedItem;
            var subscription_index = 0;
            for (; subscription_index <= subscriptions.Count - 1; subscription_index++) {
                if (subscriptions[subscription_index].airport == airport_selected) {
                    break;
                }
            }
            return subscription_index;
        }

        private void Button_delete_Click(object sender, EventArgs e) {
            var delete_index = GetSeletedSubscriptionGlobalIndex();
            if (delete_index <= subscriptions.Count - 1) {
                EnableSwitch();
                subscriptions.RemoveAt(delete_index);
                ListBox_subscription.Items.RemoveAt(delete_index);
                EnableSwitch();
            }
        }

        private void ListBox_subscription_SelectedIndexChanged(object sender, EventArgs e) {
            var subscription = subscriptions[GetSeletedSubscriptionGlobalIndex()];
            TextBox_url.Text = subscription.url;
            TextBox_name.Text = subscription.airport;
            TextBox_name.ForeColor = SystemColors.WindowText;
            Button_save.Enabled = true;
        }
    }
}
