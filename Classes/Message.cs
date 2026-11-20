using System;
using System.Collections.Generic;
using System.Text;

namespace Syslog_Project.Classes
{
    internal class Message
    {
        //Fields
        public string type = "Unknown"; //UDP or TCP
        public int priority_value;

        //Constructor
        public Message(string p_type, string p_content, List<string> p_valid_types)
        {
            if (p_valid_types.Contains(p_type))
            {
                this.type = p_type;
            }
            this.priority_value = Get_Priority_Value();
        }

        private int Get_Priority_Value()
        {
            //todo
            return 0;
        }
    }
}
