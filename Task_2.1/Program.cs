using System;
using Library;

namespace Task_2._1
{
    class Program
    {
        static BettingPlatformEmulator bettingPlatformEmulator = new BettingPlatformEmulator();
        static BetService betService = new BetService();
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                float odd = betService.GetOdds();
                decimal amount = betService.Bet(100);
                Console.WriteLine($"I’ve bet 100 USD with the odd {odd} and I’ve earned {amount}");
            }

            Console.WriteLine("\n");

            int countBets = 0;
            while (countBets < 3)
            {
                float odd = betService.GetOdds();
                if (odd > 12)
                {
                    countBets++;
                    decimal amount = betService.Bet(100);
                    Console.WriteLine($"I’ve bet 100 USD with the odd {odd} and I’ve earned {amount}");
                }
                else continue;
            }
            Console.WriteLine("\n");

            // Uncomment all comments below to see win rate on 20 iterations.


            //for (int i = 0; i < 20; i++)
            //{
            //int count = 0;
                decimal balance = 10000;
                while (balance > 0 && balance < 150000)
                {
                    decimal bet = 400;
                    float odd = betService.GetOdds();
                    if (odd <= 3)
                    {
                        if (bet > balance)
                            bet = balance;
                        balance -= bet;
                        balance += betService.Bet(bet);
                    }
                    else
                    {
                        if (bet/2 > balance)
                            bet = balance;
                        balance -= bet/2;
                        balance += betService.Bet(bet/2);
                    }
                    //count++;
                }
                Console.WriteLine($"Game over. My balance is {Math.Round(balance + 0.001m, 2)}");
            //}
        }
    }
}
