using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public abstract class Bank : PaymentMethodBase, ISupportDeposit, ISupportWithdrawal
    {
        public string[] AvailableCards { get; protected set; }


        public Bank() : base()
        {

        }

        public void StartDeposit(decimal amount, string currency)
        {
            
            Console.WriteLine($"Welcome, dear client, to the online bank {Name}");
            Console.WriteLine("Please, enter your login : ");
            string login = Console.ReadLine();
            Console.WriteLine("Please, enter your password : ");
            string password = Console.ReadLine();
            Console.WriteLine($"Hello Mr or Mrs {login}. Pick a card to proceed the transaction :");
            for(int i = 0; i < AvailableCards.Length; i++)
                Console.WriteLine($"{i + 1}. " +  AvailableCards[i]);
            int pickedCard;
            while (!int.TryParse(Console.ReadLine(), out pickedCard) || pickedCard > AvailableCards.Length)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Pick a card to proceed the transaction :");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"You’ve withdraw {amount} {currency} from your {AvailableCards[pickedCard-1]} card successfully");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void StartWithdrawal(decimal amount, string currency)
        {
            Console.WriteLine($"Welcome, dear client, to the online bank {Name}");
            Console.WriteLine("Please, enter your login : ");
            string login = Console.ReadLine();
            Console.WriteLine("Please, enter your password : ");
            string password = Console.ReadLine();
            Console.WriteLine($"Hello Mr {login}. Pick a card to proceed the transaction :");
            for (int i = 0; i < AvailableCards.Length; i++)
                Console.WriteLine($"{i + 1}. " + AvailableCards[i]);
            int pickedCard;
            while (!int.TryParse(Console.ReadLine(), out pickedCard) || pickedCard > AvailableCards.Length)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Pick a card to proceed the transaction :");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"You’ve deposit {amount} {currency} to your {AvailableCards[pickedCard - 1]} card successfully");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
