using Syslog_Project.Classes;
using Syslog_Project.Classes.Listeners;

namespace Syslog_Project
{
    internal class Program
    {

        static int input_int(string p_prompt)
        {
            int num; //The variable to store the number inputted.

            Console.Write(p_prompt);
            while (!int.TryParse(Console.ReadLine(), out num)) //Attempt to convert input to integer, storing if success.
            {
                Console.Clear(); //Clears the console for neatness.
                Console.WriteLine("Please input an integer."); //If the input cannot be converted to an integer.
                Console.Write(p_prompt);
            }

            Console.Clear();
            return num;
        }
        static int input_int(string p_prompt, int p_min, int p_max) //An overload of the above method that limits to a specific range, functions by calling the above method within this one and recalling the above if not within the given range.
        {
            int num; //The variable to store the number inputted.

            do
            {
                num = input_int(p_prompt);
                if (num < p_min || num > p_max) //Check if in range of p_min - p_max.
                {
                    Console.WriteLine($"Please give an input within the given range {p_min} - {p_max}.");
                }
            } while (num < p_min || num > p_max); //Check if in range of p_min - p_max.

            return num;
        }
        static string input_string(string p_prompt)
        {
            string str;

            do
            {
                Console.Write(p_prompt);
                str = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(str)) //We are not allowing whitespace as inputs.
                {
                    Console.WriteLine("Please do not input whitespace.");
                }
            } while (String.IsNullOrWhiteSpace(str)); //We are not allowing whitespace as inputs.

            return str;
        }
        static async Task Main(string[] args)
        {
            //MOD008904 010-1 SysLog Project Assessment
            //Variables
            int menu_choice;
            int colour_choice;
            ConsoleColor[] colours = Enum.GetValues<ConsoleColor>();
            Config config = Config.get_config();
            UDPListener udp_listener = new UDPListener("UDP", config.Ip_Address, config.Port);
            TCPListener tcp_listener = new TCPListener("TCP", config.Ip_Address, config.Port);

            //Logic
            do
            {
                menu_choice = input_int("Syslog Server Remote Store App" +
                    $"\nCurrent Target: {config.Ip_Address}:{config.Port}" +
                    $"\n1. {udp_listener.Start_Stop} {udp_listener.Listener_Type} Listener" +
                    $"\n2. {tcp_listener.Start_Stop} {tcp_listener.Listener_Type} Listener" +
                    "\n3. Display UDP Messages" +
                    "\n4. Display TCP Messages" +
                    "\n5. Toggle Real-Time Live Feed (UDP)" +
                    "\n6. Toggle Real-Time Live Feed (TCP)" +
                    "\n7. Configure IP / Port / Colours" +
                    "\n8. Exit\nChoice: ", 1, 8);
                Console.Clear();

                switch (menu_choice)
                {
                    case 1: // Start UPD Listener.
                        if (!udp_listener.Running)
                        {
                            udp_listener.Start_Listener();
                            Console.WriteLine($"{udp_listener.Listener_Type} Listener started.");
                            Console.ReadLine();
                        }
                        else
                        {
                            udp_listener.Stop_Listener();
                            Console.WriteLine($"{udp_listener.Listener_Type} Listener stopped.");
                            Console.ReadLine();
                        }
                        break;

                    case 2: // Start TCP Listener.
                        break;

                    case 3: // Display UDP Messages.
                        foreach (var message in udp_listener.Messages) //This is temporary.
                        {
                            Console.WriteLine(message.type);
                        }
                        Console.ReadLine();
                        break;

                    case 4: // Display TCP  Messages.
                        break;

                    case 5: // Toggle Real-Time Live Feed (UDP).
                        break;

                    case 6: // Toggle Real-Time Live Feed (TCP).
                        break;

                    case 7: // Configure IP / Port / Colours.
                        if (tcp_listener.Running || udp_listener.Running) //If any listeners are running.
                        {
                            if (udp_listener.Running)
                            {
                                udp_listener.Stop_Listener(); //Stop the listener.
                                Console.WriteLine($"{udp_listener.Listener_Type} Listener stopped.");
                            }
                            if (tcp_listener.Running)
                            {
                                tcp_listener.Stop_Listener(); //Stop the listener.
                                Console.WriteLine($"{tcp_listener.Listener_Type} Listener stopped.");
                            }
                            Console.Write("This was done to prevent any errors while changing the config, you may restart these afterwards.\nPress enter to continue..."); //Explain why we have done this.
                            Console.ReadLine();
                            Console.Clear();
                        }

                        menu_choice = input_int("1. Configure IP\n2. Port\n3. Colours\nChoice: ", 1, 3);
                        Console.Clear();
                        switch (menu_choice)
                        {
                            case 1: //Configure IP
                                config.Ip_Address = input_string("IP Address: ");
                                Console.Write("Writing...");
                                config.write_config(); //Write the changes made above to the disk.
                                Console.WriteLine($"Your current IP Address is now: {config.Ip_Address} "); //Output the new value of the IP Address to ensure the user knows it has changed.
                                
                                Console.Write("Press enter to continue...");
                                Console.ReadLine();
                                break;

                            case 2: //Port
                                config.Port = input_int("Port Number (1 - 65535): ", 1, 65535);
                                Console.Write("Writing...");
                                config.write_config(); //Write the changes made above to the disk.
                                Console.WriteLine($"Your current port is now: {config.Port}"); //Output the new value of the Port to ensure the user knows it has changed.
                                
                                Console.Write("Press enter to continue...");
                                Console.ReadLine();
                                break;

                            case 3: //Colours.
                                for (int i = 0; i < config.Severity_Name.Length; i++)
                                {
                                    Console.WriteLine($"{i + 1}. {config.Severity_Name[i]} - {(ConsoleColor) config.Severity_Colour[i]}");
                                }

                                menu_choice = input_int("Which severity colour are you changing?: ", 1, config.Severity_Name.Length);
                                Console.Clear();

                                Console.WriteLine($"The {config.Severity_Name[menu_choice - 1]} severity has the colour of {(ConsoleColor)config.Severity_Colour[menu_choice - 1]}, what would you like to change it to?");
                                for (int i = 0; i < colours.Length; i++)
                                {
                                    Console.WriteLine($"{i + 1}. {colours[i]}");
                                }

                                colour_choice = input_int("Choice: ", 1, colours.Length);
                                Console.Clear();
                                config.set_colour(menu_choice - 1, colour_choice - 1);

                                Console.Write("Press enter to continue...");
                                Console.ReadLine();
                                break;

                            default: //Catchall.
                                Console.WriteLine("Error, invalid input.");
                                break;
                        }
                        udp_listener.recheck_config(config);
                        tcp_listener.recheck_config(config);
                        break;

                    case 8: // Exit case.
                        Console.WriteLine("Writing config..."); //Save the config for redundacy. While no changes should be made that aren't yet written by this point, this is here to ensure that all changes are definitely saved.
                        config.write_config();
                        Console.WriteLine("Exiting...");
                        break;

                    default: //Catchall.
                        Console.WriteLine("Error, invalid input.");
                        break;
                }
                Console.Clear();
            } while (menu_choice != 8);
        }
    }
}
