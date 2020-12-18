using System;
using Library;

namespace Task_1._2
{
    class Program
    {
        static Account[] accounts = new Account[1000000];
        static Account[] GetSortedAccounts()
        {
            for (var i = 1; i < accounts.Length; i++)
            {
                for (var j = 0; j < accounts.Length - i; j++)
                {
                    if (accounts[j].Id > accounts[j + 1].Id)
                    {
                        Account temp = accounts[j];
                        accounts[j] = accounts[j + 1];
                        accounts[j + 1] = temp;
                    }
                }
            }
            return accounts;
        }

        static void Main(string[] args)
        {
            for (int i = 0; i < accounts.Length; i++)
                accounts[i] = new Account("UAH");
            accounts = GetSortedAccounts();
            Console.WriteLine("First ten accounts are:");
            for (int i = 0; i < 10; i++)
                Console.WriteLine(accounts[i].Id);
            Console.WriteLine("\nLast ten accounts are:");
            for (int i = accounts.Length-11; i < accounts.Length-1; i++)
                Console.WriteLine(accounts[i].Id);
        }
    }
}
