using System;
using Library;

namespace Task_1._4
{
    class Program
    {
        static Account[] accounts = new Account[1000000];
        static Account[] GetSortedAccountsByQuickSort()
        {
            return Quick_Sort(accounts, 0, accounts.Length-1);
        }

        private static Account[] Quick_Sort(Account[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right);
                if (pivot > 1)
                    Quick_Sort(arr, left, pivot - 1);
                if (pivot + 1 < right)
                    Quick_Sort(arr, pivot + 1, right);
            }
            return arr;
        }

        private static int Partition(Account[] arr, int left, int right)
        {
            int pivot = arr[left].Id;
            while (true)
            {
                while (arr[left].Id < pivot)
                    left++;
                while (arr[right].Id > pivot)
                    right--;
                if (left < right)
                {
                    if (arr[left].Id == arr[right].Id) 
                        return right;
                    Account temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;
                }
                else
                    return right;
            }
        }
        static void Main(string[] args)
        {
            for (int i = 0; i < accounts.Length; i++)
                accounts[i] = new Account("UAH");
            accounts = GetSortedAccountsByQuickSort();
            Console.WriteLine("First ten accounts are:");
            for (int i = 0; i < 10; i++)
                Console.WriteLine(accounts[i].Id);
            Console.WriteLine("\nLast ten accounts are:");
            for (int i = accounts.Length - 11; i < accounts.Length - 1; i++)
                Console.WriteLine(accounts[i].Id);
        }
    }
}
