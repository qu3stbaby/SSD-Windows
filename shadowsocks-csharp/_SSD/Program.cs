using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadowsocks
{
    public partial class Program
    {
        public static Process[] OldProcess()
        {
            return Process.GetProcessesByName("ShadowsocksD");
        }
    }
}
