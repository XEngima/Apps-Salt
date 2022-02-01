﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Business
{
    public class MessagingException : ApplicationException
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
