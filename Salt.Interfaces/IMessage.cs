using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Interfaces
{
    public interface IMessage
    {
        Guid Id { get; set; }

        string KeyName { get; set; }

        string Header { get; set; }

        string Subject { get; set; }

        string Content { get; set; }

        int KeyStartPos { get; set; }
    }
}
