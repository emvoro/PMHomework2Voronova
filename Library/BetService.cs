using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class BetService
    {
        public decimal Odd { get; private set; }

        public BetService()
        {
            Random random = new Random();
            Odd = Math.Round(random.Next(1, 24) + (decimal)random.NextDouble() + 0.1m, 2);
        }

        public float GetOdds()
        {
            Random random = new Random();
            Odd = Math.Round(random.Next(1, 24) + (decimal)random.NextDouble() + 0.1m, 2);
            return (float)Odd;
        }

        public bool IsWon()
        {
            int probability = Convert.ToInt32(Math.Round(1 / Odd * 100));
            Random random = new Random();
            int win = random.Next(1, 101);
            if (win <= probability)
                return true;
            else return false;
        }

        public decimal Bet(decimal amount)
        {
            if (IsWon())
                return (decimal)Math.Round((double)amount * (double)Odd, 2);
            else return 0;
        }
    }
}
