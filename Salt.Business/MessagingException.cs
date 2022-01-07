using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Business
{
    public class MessagingException : Exception
    {
        public MessagingException()
            :base()
        {
        }

        public MessagingException(string message)
            :base(message)
        {
        }
    }
}
