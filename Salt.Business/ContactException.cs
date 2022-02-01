using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Business
{
    public class ContactException : ApplicationException
    {
        public ContactException()
            :base()
        {
        }

        public ContactException(string message)
            :base(message)
        {
        }
    }
}
