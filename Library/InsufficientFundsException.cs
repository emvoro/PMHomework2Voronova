using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class InsufficientFundsException : PaymentServiceException
    {
        private readonly string _innerData;

        public InsufficientFundsException() : base()
        {
            _innerData = string.Empty;
        }
        
        public InsufficientFundsException(string innerData) : this("Insufficient funds.", innerData)
        {
            _innerData = innerData;
        }

        public InsufficientFundsException(string message, string innerData) : base(message)
        {
            _innerData = innerData;
        }

        public InsufficientFundsException(string message, string innerData, Exception innerException) : base(message, innerData, innerException)
        {
            _innerData = innerData;
        }
    }
}
