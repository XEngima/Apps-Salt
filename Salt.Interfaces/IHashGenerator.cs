using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Interfaces
{
    public interface IHashGenerator
    {
        string CreateHash(string value);
    }
}
