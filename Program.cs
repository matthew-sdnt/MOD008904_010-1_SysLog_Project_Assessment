namespace Syslog_Project
{
    internal class Program
    {

        static int input_int(string p_prompt)
        {
            int num; //The variable to store the number inputted.

            Console.WriteLine(p_prompt);
            while (!int.TryParse(Console.ReadLine(), out num)) //Attempt to convert input to integer, storing if success.
            {
                Console.Clear(); //Clears the console for neatness.
                Console.WriteLine("Please input an integer."); //If the input cannot be converted to an integer.
                Console.WriteLine(p_prompt);
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
                if (num < p_min || num > p_max)
                {
                    Console.WriteLine($"Please give an input within the given range {p_min} - {p_max}.");
                }
            } while (num < p_min || num > p_max);

            return num;
        }
        static void Main(string[] args)
        {
            //MOD008904 010-1 SysLog Project Assessment
            //Variables
            Config config = Config.get_config();

            //Logic

        }
    }
}
