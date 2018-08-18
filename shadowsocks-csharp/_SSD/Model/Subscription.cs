using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shadowsocks.Model {
    public class Subscription {
        public string url;
        public string airport;
        public int port;
        public string encryption;
        public string password;
        public List<Server> servers;

        public Subscription() {
            servers = new List<Server>();
        }

        public void ParseBase64(string text_base64) {
            var new_subscription = ParseNewBase64(text_base64);
            airport = new_subscription.airport;
            port = new_subscription.port;
            encryption = new_subscription.encryption;
            password = new_subscription.password;
            servers = new_subscription.servers;
            foreach(var server in servers) {
                server.subscription = this;
            }
        }

        public static Subscription ParseNewBase64(string text_base64) {
            text_base64.Replace('-', '+');
            text_base64.Replace('_', '/');
            var mod4 = text_base64.Length % 4;
            if (mod4 > 0) {
                text_base64 += new string('=', 4 - mod4);
            }
            var json_buffer = Convert.FromBase64String(text_base64);
            var json_text = Encoding.UTF8.GetString(json_buffer);
            var new_subscription= JsonConvert.DeserializeObject<Subscription>(json_text);
            foreach (var server in new_subscription.servers) {
                server.server_port = new_subscription.port;
                server.password = new_subscription.password;
                server.method = new_subscription.encryption;
                server.subscription = new_subscription;
            }
            return new_subscription;
        }

    }
}
