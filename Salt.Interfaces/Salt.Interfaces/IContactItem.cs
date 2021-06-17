using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Interfaces
{
    public interface IContactItem
    {
        Guid Id { get; set; }

        string Name { get; set; }

        string KeyName { get; set; }
    }
}
