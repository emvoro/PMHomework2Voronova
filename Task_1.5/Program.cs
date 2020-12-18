using System;
using Library;

namespace Task_1._5
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player("John Doe", "Betman", "john777@gmail.com", "TheP@$$w0rd", "USD");
            bool loggedIn = false;
            while (!loggedIn)
            {
                Console.WriteLine("Enter login :");
                string login = Console.ReadLine();
                Console.WriteLine("Enter password :");
                string password = Console.ReadLine();
                if (login == player.Email && password == player.Password)
                {
                    loggedIn = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nLogin with login {login} and password {password} is successfull.\n");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    player.Deposit(100, "USD");
                    player.Withdraw(50, "EUR");
                    try
                    {
                        player.Withdraw(1000, "USD");
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine("Invalid operation.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nLogin with login {login} and password {password} is failed.\n");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                    
            }
            try
            {
                Player invalidCurrencyPlayer = new Player("John Doe", "Betman", "john777@gmail.com", "TheP@$$w0rd", "PLN");
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }
}
