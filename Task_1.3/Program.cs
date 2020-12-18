using System;
using Library;

namespace Task_1._3
{
    class Program
    {
        static int Count = 0;
        static Account[] GetSortedAccounts(Account[] accounts)
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

        static void GetAccount(int id)
        {
            Account[] accounts = new Account[1000000];
            for (int i = 0; i < accounts.Length; i++)
                accounts[i] = new Account("UAH");
            accounts = GetSortedAccounts(accounts);
            int index = BinarySearch(accounts, id, 0, accounts.Length - 1);
            if (index < 0)
                Console.WriteLine($"There is no account {id} in the list.");
            else
                Console.WriteLine($"{id} was found at index {index} by {Count} tries");
        }

        static int BinarySearch(Account[] array, int searchedValue, int first, int last)
        {
            Count++;
            if (first > last)
                return -1;
            var middle = (first + last) / 2;
            var middleValue = array[middle];
            if (middleValue.Id == searchedValue)
                return middle;
            else
            {
                if (middleValue.Id > searchedValue)
                    return BinarySearch(array, searchedValue, first, middle - 1);
                else
                    return BinarySearch(array, searchedValue, middle + 1, last);
            }
        }
        

        static void Main(string[] args)
        {
            Console.WriteLine("Enter index to search:");
            int id = int.Parse(Console.ReadLine());
            GetAccount(id);
        }
    }
}
