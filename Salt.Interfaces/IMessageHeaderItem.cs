using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Interfaces
{
    public interface IMessageHeaderItem
    {
        Guid MessageId { get; }

        string Content { get; }
    }
}
