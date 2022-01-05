﻿using System;
using System.Collections.Generic;

namespace Salt.Keys
{
    public interface IKeyStore
    {
        /// <summary>
        /// Gets all key names in the key store.
        /// </summary>
        /// <returns>A list of key names.</returns>
        IEnumerable<string> GetAllKeyNames();

        /// <summary>
        /// Gets a part of a key.
        /// </summary>
        /// <param name="keyName">The name (hashed) of the key to get.</param>
        /// <param name="pos">The start position within the key.</param>
        /// <param name="length">The length of the part to recieve.</param>
        /// <returns>A key part.</returns>
        string GetKeyPart(string keyName, int pos, int length);
    }
}
