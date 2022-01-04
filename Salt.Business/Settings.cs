using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Business
{
    public class Settings : ISettings
    {
        public Guid MyId { get { return Guid.Parse("00000002-f760-4cf6-a84d-526397dc8b2a"); ; } }
    }
}
