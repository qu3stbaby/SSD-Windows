using Newtonsoft.Json;
using System.Collections.Generic;

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
    }
}
