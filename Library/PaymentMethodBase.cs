using System;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    public abstract class PaymentMethodBase
    {
        public string Name { get; protected set; }
    }
}
