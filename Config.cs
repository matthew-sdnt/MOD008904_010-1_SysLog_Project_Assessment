using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO.Enumeration;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Syslog_Project
{
    internal class Config //This class exists to store information about the app config.
    {
        //Fields
        private string file_name = "config.json";
        private string ip_address = "127.0.0.1";
        private int port = 514;
        public string[] severity_name = new string[8] { "Emergency", "Alert", "Critical", "Error", "Warning", "Notice", "Information", "Debug" };
        private int[] severity_colour = new int[8] { 12, 12, 12, 6, 14, 9, 10, 7 };

        //Properties
        public string Ip_Address
        {
            get { return this.ip_address; }
            set
            {
                if (IPAddress.TryParse(value, out IPAddress ip))
                {
                    this.ip_address = ip.ToString();
                }
                else
                {
                    Console.WriteLine($"Invalid address, using current address ({this.ip_address}).");
                }
            }
        }
        public int Port
        {
            get { return this.port; }
            set
            {
                if (value < 1 || value > 65535 )
                {
                    Console.WriteLine($"Invalid port, using current port ({this.port}).");
                }
                else
                {
                    this.port = value;
                }
            }
        }
        public string[] Severity_Name
        {
            get { return this.severity_name; }
        }
        public int[] Severity_Colour
        {
            get { return this.severity_colour; }
            set { this.severity_colour = value; }
        }

        //Constructor
        public Config()
        {

        } //This constructor exists to be used when deserializing the json.
        private Config(string p_file_name)
        {
            this.file_name = p_file_name;
        }

        //Behaviours
        public static Config get_config()
        {
            string file_name = "config.json";

            if (!File.Exists(file_name))
            {
                Console.WriteLine("No config exists, making the config...");
                Config default_config = new Config(file_name);
                try
                {

                    default_config.write_config();
                }
                catch
                {
                    Console.WriteLine("Could not write config, returning a default config.");
                    return default_config;
                }
            }

            string jsonString = File.ReadAllText(file_name);
            return JsonSerializer.Deserialize<Config>(jsonString);
        }

        public void write_config()
        {
            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
            string json_string = JsonSerializer.Serialize(this, options);
            File.WriteAllText(this.file_name, json_string);
        }
    }
}
