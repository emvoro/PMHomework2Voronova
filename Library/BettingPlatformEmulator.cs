using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class BettingPlatformEmulator
    {
        public List<Player> Players { get; private set; }
        public Player ActivePlayer { get; private set; }
        public Account Account { get; private set; }
        private BetService BetService = new BetService();
        private PaymentService PaymentService = new PaymentService();

        public BettingPlatformEmulator()
        {
            Players = new List<Player>();
            Account = new Account("USD");
        }

        public void Start()
        {
            if (ActivePlayer == null)
            {
                Console.WriteLine("\n1. Register\n2. Login\n3. Stop");
                int command;
                while (!int.TryParse(Console.ReadLine(), out command) || command >= 4 || command <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid command.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Start();
                }
                switch (command)
                {
                    case 1:
                        Register();
                        break;
                    case 2:
                        Login();
                        break;
                    case 3:
                        Exit();
                        break;
                }
            }
            else 
            {
                Console.WriteLine("\n1. Deposit\n2. Withdraw\n3. Get Odds\n4. Bet\n5. Balance\n6. Logout");
                int command;
                while (!int.TryParse(Console.ReadLine(), out command) || command >= 7 || command <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid command.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Start();
                }
                switch (command)
                {
                    case 1:
                        Deposit();
                        break;
                    case 2:
                        Withdraw();
                        break;
                    case 3:
                        Console.WriteLine("Current odds : " + BetService.GetOdds());
                        Start();
                        break;
                    case 4:
                        Console.WriteLine("Enter your bet :");
                        decimal amount;
                        while (!decimal.TryParse(Console.ReadLine(), out amount))
                            Console.WriteLine("Invalid bet. Enter correct amount.");
                        Console.WriteLine("Enter currency :");
                        string currency = "";
                        while (currency != "USD" && currency != "EUR" && currency != "UAH")
                            currency = Console.ReadLine();
                        try
                        {
                            Bet(amount, currency);
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        Start();
                        break;
                    case 5:
                        Console.WriteLine("Balance : " + ActivePlayer.Account.GetBalance(ActivePlayer.Account.Currency)
                            + " " + ActivePlayer.Account.Currency);
                        Start();
                        break;
                    case 6:
                        Logout();
                        break;
                }
            }
        }

        public void Bet(decimal amount, string currency)
        {
            ActivePlayer.Withdraw(amount, currency);
            decimal bet = amount;
            decimal result = BetService.Bet(bet);
            if (result > 0)
            {
                ActivePlayer.Deposit(result, currency);
                Console.WriteLine($"You won {result} {currency}");
            }
            else
                Console.WriteLine($"You lost"); 
        }

        public void Exit()
        {
            Environment.Exit(0);
        }

        public void Register()
        {
            Console.WriteLine("Enter your name, please :");
            string name = Console.ReadLine();
            Console.WriteLine("Enter your last name, please :");
            string lastName = Console.ReadLine();
            Console.WriteLine("Enter your email, please :");
            string email = Console.ReadLine();
            while (Players.FindIndex(x => x.Email == email) > -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("User with such email already exists. Enter another email, please :");
                Console.ForegroundColor = ConsoleColor.Gray;
                email = Console.ReadLine();
            }
            Console.WriteLine("Enter your password, please :");
            string password = Console.ReadLine();
            string currency = "";
            while (currency != "USD" &&  currency != "EUR" && currency != "UAH")
            {
                Console.WriteLine("Enter your basic currency, please (USD/EUR/UAH supported) :");
                currency = Console.ReadLine();
            }
            Player player = new Player(name, lastName, email, password, currency);
            Players.Add(player);
            Start();
        }

        public void Login()
        {
            Console.WriteLine("Enter email :");
            string email = Console.ReadLine();
            Console.WriteLine("Enter password :");
            string password = Console.ReadLine();
            if (Players.FindIndex(x => x.Email == email && x.Password == password) > -1)
                ActivePlayer = Players.Find(x => x.Email == email && x.Password == password);
            else 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No such user.");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Start();
        }

        public void Logout()
        {
            ActivePlayer = null;
            Start();
        }

        public void Deposit()
        {
            bool problems = false;
            Console.WriteLine("Enter amount :");
            decimal amount;
            while (!decimal.TryParse(Console.ReadLine(), out amount))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid amount. Enter amount :");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            Console.WriteLine("Enter currency :");
            string currency = Console.ReadLine();
            try
            {
                PaymentService.StartDeposit(amount, currency);
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                problems = true;
                Deposit();
            }
            catch (InsufficientFundsException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You have already used this voucher.");
                Console.ForegroundColor = ConsoleColor.Gray;
                problems = true;
            }
            catch (LimitExceededException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please, try to make a transaction with lower amount.");
                Console.ForegroundColor = ConsoleColor.Gray;
                problems = true;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                problems = true;
            }
            finally
            {
                if (!problems)
                {
                    ActivePlayer.Deposit(amount, currency);
                    Account.Deposit(amount, currency);
                }
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Start();
        }

        public void Withdraw()
        {
            Console.WriteLine("Enter amount :");
            decimal amount = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter currency :");
            string currency = Console.ReadLine();
            try
            {
                ActivePlayer.Withdraw(amount, currency);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                Start();
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                Start();
            }
            try
            {
                Account.Withdraw(amount, currency);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("There is some problem on the platform side. Please try it later");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            finally
            {
                try
                {
                    PaymentService.StartWithdrawal(amount, currency);
                }
                catch(LimitExceededException ex)
                {
                    Console.WriteLine(ex.Message/*"Please, try to make a transaction with lower amount."*/);
                }
                catch (PaymentServiceException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Start();
        }
    }
}
