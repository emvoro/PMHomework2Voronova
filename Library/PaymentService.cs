using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class PaymentService
    {
        public PaymentMethodBase[] AvailablePaymentMethod { get; private set; }
        public static decimal CardsLimit = 0;

        public PaymentService()
        {
            AvailablePaymentMethod = new PaymentMethodBase[] { new CreditCard(), new Privet48(), new Stereobank(), new GiftVoucher() };
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

        public decimal CardLimit(decimal amount, string currency)
        {
            if (currency == "UAH")
                return amount;
            else if (currency == "USD")
                return amount * 28.36m;
            else
                return amount * 33.63m;
        }

            public void GenerateBaseServerError()
        {
            Random random = new Random();
            int probabilityOfCrash = random.Next(1, 101);
            if (probabilityOfCrash < 3)
                throw new Exception("Something went wrong. Try again later...");
        }

        public void StartDeposit(decimal amount, string currency)
        {
            GenerateBaseServerError();
            if (currency != "EUR" && currency != "UAH" && currency != "USD")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new NotSupportedException("Not supported currency. There are only 3 currencies supported : UAH, USD and EUR.");
            }
            List<PaymentMethodBase> paymentMethodBases = new List<PaymentMethodBase>();
            for (int i = 0; i < AvailablePaymentMethod.Length; i++)
            {
                if (AvailablePaymentMethod[i] is ISupportDeposit)
                    paymentMethodBases.Add(AvailablePaymentMethod[i]);     
            }
            for (int j = 0; j < paymentMethodBases.Count; j++)
                Console.WriteLine(j + 1 + ". " + AvailablePaymentMethod[j].Name);
            int command;
            while (!int.TryParse(Console.ReadLine(), out command) || command > paymentMethodBases.Count || command <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid command.");
                Console.ForegroundColor = ConsoleColor.Gray;
                StartDeposit(amount, currency);
            }
            ISupportDeposit paymentMethod = (ISupportDeposit)AvailablePaymentMethod[command-1];

            if (!CheckLimit(amount, currency) && paymentMethodBases.FindIndex(x => x.Name == "Privet48") != command - 1)
                throw new LimitExceededException("Please, try to make a transaction with lower amount");
            else
            {
                if((paymentMethodBases.FindIndex(x => x.Name == "Privet48") != command - 1 && CardsLimit + amount > 10000) ||
                    (paymentMethodBases.FindIndex(x => x.Name == "Stereobank") != command - 1 && CardsLimit + amount > 7000))
                {
                    throw new LimitExceededException("Please, try to make a transaction with lower amount");
                }
                CardsLimit += CardLimit(amount, currency);
                paymentMethod.StartDeposit(amount, currency);
            }    
        }

        public void StartWithdrawal(decimal amount, string currency)
        {
            GenerateBaseServerError();
            if (currency != "USD" && currency != "EUR" && currency != "UAH")
                throw new NotSupportedException("Not supported currency.There are only 3 currencies supported: UAH, USD and EUR.");
            if (!CheckLimit(amount, currency))
                throw new LimitExceededException();
            List<PaymentMethodBase> paymentMethodBases = new List<PaymentMethodBase>();
            for (int i = 0; i < AvailablePaymentMethod.Length; i++)
            {
                if (AvailablePaymentMethod[i] is ISupportWithdrawal)
                    paymentMethodBases.Add(AvailablePaymentMethod[i]);
            }
            for (int j = 0; j < paymentMethodBases.Count; j++)
                Console.WriteLine(j + 1 + ". " + AvailablePaymentMethod[j].Name);
            int command;
            while (!int.TryParse(Console.ReadLine(), out command) || command > paymentMethodBases.Count || command <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid command.");
                Console.ForegroundColor = ConsoleColor.Gray;
                StartWithdrawal(amount, currency);
            }
            ISupportWithdrawal paymentMethod = (ISupportWithdrawal)AvailablePaymentMethod[command-1];
            if (!CheckLimit(amount, currency) && paymentMethodBases.FindIndex(x => x.Name == "Privet48") != command - 1)
                throw new LimitExceededException();
            else
            {
                if ((paymentMethodBases.FindIndex(x => x.Name == "Privet48") != command - 1 && CardsLimit + amount > 10000) ||
                    (paymentMethodBases.FindIndex(x => x.Name == "Stereobank") != command - 1 && CardsLimit + amount > 7000))
                {
                    throw new LimitExceededException("Please, try to make a transaction with lower amount");
                }
                CardsLimit += CardLimit(amount, currency);
                paymentMethod.StartWithdrawal(amount, currency);
            }
                
        }
    }
}
