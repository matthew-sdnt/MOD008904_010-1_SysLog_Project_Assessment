using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Syslog_Project.Classes.Listeners
{
    internal abstract class Listener
    {
        //Fields
        protected List<string> valid_types = new List<string>() { "UDP", "TCP" };
        protected string listener_type = "Unknown";
        protected IPAddress ip_address;
        protected int port;
        protected List<Message> messages = new List<Message>(); //A list of the messages this listener has recieved
        protected bool running;
        protected string start_stop = "Start";

        //Properties
        public bool Running
        {
            get { return running; }
        }
        public List<Message> Messages
        {
            get { return this.messages; }
        }
        public string Listener_Type
        {
            get { return this.listener_type; }
        }
        public string Start_Stop
        {
            get { return this.start_stop; }
        }
        //Constructor
        protected Listener(string p_type, string p_ip_address, int p_port)
        {
            this.ip_address = IPAddress.Parse(p_ip_address);
            this.port = p_port;
            if (valid_types.Contains(p_type))
            {
                this.listener_type = p_type;
            }
        }

        //Behaviours
        public abstract Task Start_Listener();
        public void Stop_Listener()
        {
            running = false;
            start_stop = "Start";
            Stop_Client();
        }
        public abstract void Stop_Client();
        public void recheck_config(Config p_config)
        {
            this.ip_address = IPAddress.Parse(p_config.Ip_Address);
            this.port = p_config.Port;
        }
    }
}
