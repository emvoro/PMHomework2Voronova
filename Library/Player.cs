using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class Player
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public Account Account { get; private set; }
        public UniqueIdentifiers UniqueIdentifiers = new UniqueIdentifiers();

        public Player(string firstName, string lastName, string email, string password, string currency)
        {
            Random random = new Random();
            int id = random.Next(100000, 100000000);
            while (!UniqueIdentifiers.AddIdentifier(id))
                id = random.Next(100000, 100000000);
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Account = new Account(currency);
        }

        public bool IsPassportValid(string password)
        {
            return Password == password;
        }

        public void Deposit(decimal amount, string currency)
        {
            Account.Deposit(amount, currency);
        }

        public void Withdraw(decimal amount, string currency)
        {
            Account.Withdraw(amount, currency);
        }
    }
}
