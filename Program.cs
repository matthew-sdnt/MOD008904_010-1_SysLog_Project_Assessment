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
        static void Main(string[] args)
        {
            //MOD008904 010-1 SysLog Project Assessment
            //Variables
            int menu_choice;
            Config config = Config.get_config();

            //Logic
            do
            {
                menu_choice = input_int("Syslog Project" +
                    "\n1. Start UDP Listener" +
                    "\n2. Start TCP Listener" +
                    "\n3. Display UDP Messages" +
                    "\n4. Display TCP Messages" +
                    "\n5. Toggle Real-Time Live Feed (UDP)" +
                    "\n6. Toggle Real-Time Live Feed (TCP)" +
                    "\n7. Configure IP / Port / Colours" +
                    "\n8. Exit", 1, 8);

                switch (menu_choice)
                {
                    case 1: // Start UPD Listener.
                        Console.WriteLine("mainfunc");
                        break;
                    case 2: // Start TCP Listener.
                        break;
                    case 3: // Display UDP Messages.
                        break;
                    case 4: // Display TCP  Messages.
                        break;
                    case 5: // Toggle Real-Time Live Feed (UDP).
                        break;
                    case 6: // Toggle Real-Time Live Feed (TCP).
                        break;
                    case 7: // Configure IP / Port / Colours.
                        break;
                    case 8: // Exit case
                        Console.WriteLine("Exiting...");
                        break;
                    default: //Catchall.
                        Console.WriteLine("Error, invalid input.");
                        break;
                }
            } while (menu_choice != 8);
        }
    }
}
