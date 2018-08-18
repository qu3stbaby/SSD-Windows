using Shadowsocks.Controller;
using Shadowsocks.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Shadowsocks.Properties;

namespace Shadowsocks.View {
    public partial class SubscriptionManagementForm : Form {
        private string text_auto = I18N.GetString("(Auto)");

        private ShadowsocksController controller;
        private List<Subscription> subscriptions;

        private bool refresh_switch = true;

        public SubscriptionManagementForm(ShadowsocksController controller_set) {
            InitializeComponent();
            controller = controller_set;
            subscriptions = controller.GetCurrentConfiguration().subscriptions;

            Text = I18N.GetString("Subscription Management");
            Label_url.Text = I18N.GetString("Subscription URL");
            Label_name.Text = I18N.GetString("Airport Name");
            Button_add.Text = I18N.GetString("&Add");
            Button_save.Text = I18N.GetString("&Save");
            Button_delete.Text = I18N.GetString("&Delete");

            Icon = Icon.FromHandle(Resources.ssw128.GetHicon());
        }

        private void NameEntered(object sender, EventArgs e) {
            if (TextBox_name.Text.Trim() == text_auto) {
                TextBox_name.Text = "";
                TextBox_name.ForeColor = SystemColors.WindowText;
            }
        }

        private void SetNameAuto() {
            TextBox_name.ForeColor = Color.Gray;
            TextBox_name.Text = text_auto;
        }

        private void NameLeaved(object sender, EventArgs e) {
            if (TextBox_name.Text.Trim() == "") {
                SetNameAuto();
            }
        }

        private void LoadSubscriptionManage(object sender, EventArgs e) {
            SetNameAuto();
            EnableSwitch();
            RefreshSubscription();
        }

        private void RefreshSubscription() {
            ListBox_subscription.Items.Clear();
            TextBox_url.Text = "";
            SetNameAuto();
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

        private void AddSubscription(object sender, EventArgs e) {
            EnableSwitch();
            //加入重名检验
            subscriptions.Add(GetSubscriptionShowed());
            RefreshSubscription();
        }

        private void SaveSubscription(object sender, EventArgs e) {
            subscriptions[GetSeletedSubscriptionGlobalIndex()] = GetSubscriptionShowed();
            RefreshSubscription();
        }

        private Subscription GetSubscriptionShowed() {
            var subscription = new Subscription();
            var airport = TextBox_name.Text.Trim();
            if (airport != text_auto && airport != "") {
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

        private void DeleteSubscription(object sender, EventArgs e) {
            var delete_index = GetSeletedSubscriptionGlobalIndex();
            if (delete_index <= subscriptions.Count - 1) {
                EnableSwitch();
                subscriptions.RemoveAt(delete_index);
                ListBox_subscription.Items.RemoveAt(delete_index);
                EnableSwitch();
            }
        }

        private void SubscriptionSelected(object sender, EventArgs e) {
            var subscription = subscriptions[GetSeletedSubscriptionGlobalIndex()];
            TextBox_url.Text = subscription.url;
            TextBox_name.Text = subscription.airport;
            TextBox_name.ForeColor = SystemColors.WindowText;
            Button_save.Enabled = true;
        }
    }
}
