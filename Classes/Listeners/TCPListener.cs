using System;
using System.Collections.Generic;
using System.Text;

namespace Syslog_Project.Classes.Listeners
{
    internal class TCPListener : Listener
    {
        //Constructor
        public TCPListener(string p_type, string p_ip_address, int p_port) : base(p_type, p_ip_address, p_port) { }

        public override async Task Start_Listener()
        {

        }
        public override void Stop_Client()
        {

        }
    }
}
