using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class GiftVoucher : PaymentMethodBase, ISupportDeposit
    {
        public SortedSet<string> giftVouchers = new SortedSet<string>();

        public GiftVoucher() : base()
        {
            Name = "GiftVoucher";
        }

        public void StartDeposit(decimal amount, string currency)
        {
            if (amount != 100 && amount != 500 && amount != 1000)
                throw new PaymentServiceException();
            Console.WriteLine("Please, enter voucher denomination :");
            int denomination;
            while (!int.TryParse(Console.ReadLine(), out denomination) || (denomination != 100
                && denomination != 500 && denomination != 1000) || amount != denomination)
            {
                Console.WriteLine("Invalid voucher denomination. Enter valid denomination :");
            }
            Console.WriteLine("Enter voucher number :");
            string voucherNumber = Console.ReadLine();
            ulong numCheck;
            while (!ulong.TryParse(voucherNumber, out numCheck) || voucherNumber.Length != 10)
            {
                Console.WriteLine("Invalid voucher number format. Enter voucher number :");
                voucherNumber = Console.ReadLine();
            }
            if (giftVouchers.Contains(voucherNumber))
                throw new InsufficientFundsException();
            giftVouchers.Add(voucherNumber);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"You’ve used your {voucherNumber} voucher successfully to deposit {amount} {currency}\n");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
