using Salt.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salt.Test
{
    public class TestHashGenerator : IHashGenerator
    {
        public string CreateHash(string value)
        {
            return value + "123";
        }
    }
}
