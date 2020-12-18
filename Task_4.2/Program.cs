using System;
using Library;

namespace Task_4._2
{
    class Program
    {
        static void Main(string[] args)
        {
            PaymentService paymentService = new PaymentService();
            string command  = "";
            while (command != "3")
            {
                Console.WriteLine("Commands list :");
                Console.WriteLine("1. Deposit");
                Console.WriteLine("2. Withdrawal");
                Console.WriteLine("3. Exit");
                Console.WriteLine("\nEnter command number :");
                command = Console.ReadLine();
                if (command != "1" && command != "2" && command != "3")
                {
                    Console.WriteLine("Invalid command.");
                    continue;
                }
                if (command == "3") 
                    break;
                Console.WriteLine("Enter amount :");
                decimal amount;
                while (!decimal.TryParse(Console.ReadLine(), out amount) || amount < 0)
                {
                    Console.WriteLine("Invalid amount.");
                    Console.WriteLine("Enter amount :");
                }
                Console.WriteLine("Enter currency :");
                string currency = Console.ReadLine();
                while (currency != "UAH" && currency != "USD" && currency != "EUR")
                {
                    Console.WriteLine("Supported currencies: UAH, USD, EUR.");
                    Console.WriteLine("Enter currency :");
                    currency = Console.ReadLine();
                }
                if (command == "1")
                    paymentService.StartDeposit(amount, currency);
                if (command == "2")
                    paymentService.StartWithdrawal(amount, currency);
            }

        }
    }
}
