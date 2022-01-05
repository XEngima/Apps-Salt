using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Messages
{
    public interface IMessageHeaderItem
    {
        Guid MessageId { get; }

        string Content { get; }

        int KeyStartPos { get; }

        int KeyLength { get; }
    }
}
