using System;

namespace Library
{
    public class Account
    {
        public int Id { get; private set; }
        public string Currency { get; private set; }
        public decimal Amount { get; private set; }
        public UniqueIdentifiers UniqueIdentifiers = new UniqueIdentifiers();

        public Account(string currency)
        {
            if(currency != "EUR" && currency != "UAH" && currency != "USD")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new NotSupportedException("Not supported currency. There are only 3 currencies supported : UAH, USD and EUR.");
            }
            Random random = new Random();
            int id = random.Next(100000, 100000000);
            while (!UniqueIdentifiers.AddIdentifier(id))
                id = random.Next(100000, 100000000);
            Id = id;
            Currency = currency;
            Amount = 0;
        }

        public void Deposit(decimal amount, string currency)
        {
            if (currency != "EUR" && currency != "UAH" && currency != "USD")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new NotSupportedException("Not supported currency. There are only 3 currencies supported : UAH, USD and EUR.");
            }
            else
            {
                if (currency == Currency)
                    Amount += amount;
                else if (currency == "USD" && Currency == "UAH")
                    Amount += amount * 28.36m;
                else if (currency == "EUR" && Currency == "UAH")
                    Amount += amount * 33.63m;
                else if (currency == "USD" && Currency == "EUR")
                    Amount += amount / 1.19m;
                else if (currency == "UAH" && Currency == "EUR")
                    Amount += amount / 33.63m;
                else if (currency == "UAH" && Currency == "USD")
                    Amount += amount / 28.36m;
                else
                    Amount += amount * 1.19m;
            }
        }

        public void Withdraw(decimal amount, string currency)
        {
            if (currency != "EUR" && currency != "UAH" && currency != "USD")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new NotSupportedException("Not supported currency. There are only 3 currencies supported : UAH, USD and EUR.");
            }
            else 
            {
                decimal withdrawal;
                if (currency == Currency)
                    withdrawal = amount;
                else if (currency == "USD" && Currency == "UAH")
                    withdrawal = amount * 28.36m;
                else if (currency == "EUR" && Currency == "UAH")
                    withdrawal = amount * 33.63m;
                else if (currency == "USD" && Currency == "EUR")
                    withdrawal = amount / 1.19m;
                else if (currency == "UAH" && Currency == "EUR")
                    withdrawal = amount / 33.63m;
                else if (currency == "UAH" && Currency == "USD")
                    withdrawal = amount / 28.36m;
                else
                    withdrawal = amount * 1.19m;

                if (GetBalance(currency) - amount < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    throw new InvalidOperationException("Please, try to make a transaction with lower amount or change the payment method.");
                }
                else
                    Amount -= withdrawal;
            }       
        }

        public decimal GetBalance(string currency)
        {
            if (currency == Currency)
                return Math.Round(Amount, 2);
            else if (currency == "USD" && Currency == "UAH")
                return Math.Round(Amount / 28.36m, 2);
            else if (currency == "EUR" && Currency == "UAH")
                return Math.Round(Amount / 33.63m, 2);
            else if (currency == "USD" && Currency == "EUR")
                return Math.Round(Amount * 1.19m, 2);
            else if (currency == "UAH" && Currency == "USD")
                return Math.Round(Amount * 28.36m, 2);
            else if (currency == "UAH" && Currency == "EUR")
                return Math.Round(Amount * 33.63m, 2);
            else
                return Math.Round(Amount / 1.19m, 2);
        }
    }
}
