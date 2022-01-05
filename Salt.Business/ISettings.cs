using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Business
{
    public interface ISettings
    {
        Guid MyContactId { get; set; }

        string KeyStoreFolderPath { get; set; }

        string MessageStoreFolderPath { get; set; }
    }
}
