using System;
using System.Collections.Generic;
using Library;

namespace Task_1._1
{
    class Program
    {
        static void Main(string[] args)
        {
            Account EURAccount = new Account("EUR");
            Account USDAccount = new Account("USD");
            Account UAHAccount = new Account("UAH");
            try
            {
                EURAccount.Deposit(10, "EUR");
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            try
            {
                EURAccount.Withdraw(3, "UAH");
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            try
            {
                UAHAccount.Deposit(121, "USD");
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            try
            {
                USDAccount.Withdraw(5, "USD");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            try
            {
                Account PLNAccount = new Account("PLN");
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.WriteLine($"\nAccount with currency {EURAccount.Currency} has {EURAccount.GetBalance("EUR")} balance");
            Console.WriteLine($"Account with currency {USDAccount.Currency} has {USDAccount.GetBalance("USD")} balance");
            Console.WriteLine($"Account with currency {UAHAccount.Currency} has {UAHAccount.GetBalance("UAH")} balance");
        }
    }
}
