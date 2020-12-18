using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public class LimitExceededException : PaymentServiceException
    {
        private readonly string _innerData;

        public LimitExceededException() : base("Limit Exceeded.")
        {
            _innerData = string.Empty;
        }

        public LimitExceededException(string innerData) : this("Limit Exceeded.", innerData)
        {
            _innerData = innerData;
        }

        public LimitExceededException(string message, string innerData) : base(message)
        {
            _innerData = innerData;
        }

        public LimitExceededException(string message, string innerData, Exception innerException) : base(message, innerData, innerException)
        {
            _innerData = innerData;
        }
    }
}
