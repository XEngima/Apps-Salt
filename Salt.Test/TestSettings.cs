using Salt.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salt.Test
{
    public class TestSettings : ISettings
    {
        public Guid MyContactId { get { return Guid.Parse("00000002-f760-4cf6-a84d-526397dc8b2a"); } set { } }

        public string KeyStoreFolderPath { get; set; }

        public string MessageStoreFolderPath { get; set; }
    }
}
