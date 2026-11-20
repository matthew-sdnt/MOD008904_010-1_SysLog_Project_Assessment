using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Syslog_Project.Classes.Listeners
{
    internal class UDPListener : Listener
    {
        //Fields
        UdpClient receivingUdpClient;

        //Constructor
        public UDPListener(string p_type, string p_ip_address, int p_port) : base(p_type, p_ip_address, p_port) { }

        //Behaviours
        public override async Task Start_Listener()
        {
            running = true;
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

            try
            {
                receivingUdpClient = new UdpClient(new IPEndPoint(this.ip_address, this.port));
                Console.WriteLine($"{listener_type} Listener is running at {this.ip_address}:{this.port}...");
            }
            catch
            {
                Console.WriteLine($"Could not set IP to {this.ip_address}, using default.");
                receivingUdpClient = new UdpClient(new IPEndPoint(IPAddress.Any, this.port));
                Console.WriteLine($"{listener_type} Listener is running at {IPAddress.Any}:{this.port}...");
            }
            
            start_stop = "Stop";

            while (running)
            {
                byte[] receiveBytes = (await receivingUdpClient.ReceiveAsync()).Buffer; //Have this task wait for next message.
                string returnData = Encoding.ASCII.GetString(receiveBytes); //Decode the bytes to a string.
                messages.Add(new Message(this.listener_type, returnData, valid_types));
            }

            Console.WriteLine($"Stopping {listener_type} listener.");
        }

        public override void Stop_Client()
        {
            receivingUdpClient.Close();
        }
    }
}
