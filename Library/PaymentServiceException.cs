using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class PaymentServiceException : Exception
    {
        private readonly string _innerData;

        public PaymentServiceException() : base()
        {
            _innerData = string.Empty;
        }

        public PaymentServiceException(string innerData) : this("", innerData)
        {
            _innerData = innerData;
        }

        public PaymentServiceException(string message, string innerData) : base(message)
        {
            _innerData = innerData;
        }

        public PaymentServiceException(string message, string innerData, Exception innerException) : base(message, innerException)
        {
            _innerData = innerData;
        }
    }
}
