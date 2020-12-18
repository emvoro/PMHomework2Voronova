using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class CreditCard : PaymentMethodBase, ISupportDeposit, ISupportWithdrawal
    {
        public CreditCard() : base()
        {
            Name = "Credit Card";
        }
        public bool CheckLimit(decimal amount, string currency)
        {
            decimal transaction;
            if (currency == "UAH")
                transaction = amount;
            else if (currency == "USD")
                transaction = amount * 28.36m;
            else
                transaction = amount * 33.63m;
            if (transaction <= 3000)
                return true;
            return false;
        }

        public void StartDeposit(decimal amount, string currency)
        {
            if (!CheckLimit(amount, currency))
                throw new LimitExceededException();
            Console.WriteLine("Enter card number :");
            string cardNumber = Console.ReadLine();
            bool isValid = true;
            for (int i = 0; i < cardNumber.Length; i++)
            {
                if (Char.IsDigit(cardNumber[i]))
                    continue;
                else
                {
                    isValid = false;
                    break;
                }
            }
            while ( !isValid || cardNumber.Length != 16 || (cardNumber[0] != '4' 
                && cardNumber[0] != '5'))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid card number. Enter card number :");
                Console.ForegroundColor = ConsoleColor.Gray;
                cardNumber = Console.ReadLine();
            }
            Console.WriteLine("Enter expiry date :");
            string expiryDate = Console.ReadLine();
            while (expiryDate.Length != 5 || !Char.IsDigit(expiryDate[0]) || !Char.IsDigit(expiryDate[1])
                || !Char.IsDigit(expiryDate[3]) || !Char.IsDigit(expiryDate[4])
                || expiryDate[2] != '/'
                || Convert.ToInt32(expiryDate.Substring(0,2)) < 1
                || Convert.ToInt32(expiryDate.Substring(0, 2)) > 12
                || Convert.ToInt32(expiryDate.Substring(3)) < 20
                || Convert.ToInt32(expiryDate.Substring(3)) > 25)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid expiry date. Enter expiry date :");
                Console.ForegroundColor = ConsoleColor.Gray;
                expiryDate = Console.ReadLine();
            }
            Console.WriteLine("Enter cvv :");
            string cvv = Console.ReadLine();
            while (cvv.Length != 3 || !Char.IsDigit(cvv[0]) || !Char.IsDigit(cvv[1]) 
                || !Char.IsDigit(cvv[2]))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid cvv. Enter cvv :");
                Console.ForegroundColor = ConsoleColor.Gray;
                cvv = Console.ReadLine();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"You’ve withdraw {amount} {currency} from your {cardNumber} card successfully\n");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public void StartWithdrawal(decimal amount, string currency)
        {
            if (!CheckLimit(amount, currency))
                throw new LimitExceededException("Limit for one transaction is 3000 UAH.") ;
            Console.WriteLine("Enter card number :");
            string cardNumber = Console.ReadLine();
            bool isValid = true;
            for (int i = 0; i < cardNumber.Length; i++)
            {
                if (Char.IsDigit(cardNumber[i]))
                    continue;
                else
                {
                    isValid = false;
                    break;
                }
            }
            while (!isValid || cardNumber.Length != 16 || (cardNumber[0] != '4'
                && cardNumber[0] != '5'))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid card number. Enter card number :");
                Console.ForegroundColor = ConsoleColor.Gray;
                cardNumber = Console.ReadLine();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"You’ve deposit {amount} {currency} to your {cardNumber} card successfully\n");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
