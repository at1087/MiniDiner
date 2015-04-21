using System;

namespace MiniDinerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();

            var orderManager = new OrderManager("DinerAbc");

            p.Run(orderManager);
        }

        private void Run(OrderManager orderManager_)
        {
            Console.WriteLine("Welcome to {0}, and this the Menus we offer:", orderManager_.DinerName);
            Console.WriteLine();

            string allMenus = orderManager_.GetAllMenus();
            Console.WriteLine(allMenus);

            while (true)
            {
                Console.WriteLine("Please enter your order; (e.g. morning, 1,2,3) or <ENTER> quit;");

                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    break;
                }

                try
                {
                    string output = orderManager_.Process(input);
                    Console.WriteLine();
                    Console.WriteLine("You ordered: '{0}'", output);
                    Console.WriteLine();
                }
                catch (Exception)
                {
                    Console.WriteLine("The order you entered '{0}' is invalid, please enter again", input);
                }
            }
        }
    }
}
