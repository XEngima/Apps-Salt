using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Interfaces
{
    public interface IMessageHeader
    {
        DateTime Date { get; set; }

        Guid Sender { get; set; }

        List<Guid> Recipients { get; set; }
    }
}
