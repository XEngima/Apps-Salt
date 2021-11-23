using System;
using System.Collections.Generic;

namespace Salt.Interfaces
{
    public interface IKeyStore
    {
        /// <summary>
        /// Checks if a key with a certain name exists in the key store.
        /// </summary>
        /// <param name="keyNameHash">The name to check for.</param>
        /// <returns>true if the key exists, otherwise false.</returns>
        bool HasKey(string keyNameHash);

        /// <summary>
        /// Gets a list of all keys in the keystore.
        /// </summary>
        /// <returns>An enumerable list of key names.</returns>
        IEnumerable<string> GetAllKeyNames();

        /// <summary>
        /// Gets a part of a key.
        /// </summary>
        /// <param name="keyName">The name (hashed) of the key to get.</param>
        /// <param name="pos">The start position within the key.</param>
        /// <param name="length">The length of the part to recieve.</param>
        /// <returns>A key part.</returns>
        string GetKeyPart(string keyName, int pos, int length);

        /// <summary>
        /// Saves a key store item.
        /// </summary>
        /// <param name="keyName">The name of the key.</param>
        /// <param name="fullPath"></param>
        void AddKey(string keyName, string fullPath);
    }
}
