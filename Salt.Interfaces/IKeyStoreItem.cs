using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Interfaces
{
    public interface IKeyStoreItem
    {
        /// <summary>
        /// Gets or sets the name of the key.
        /// </summary>
        string KeyName { get; set; }

        /// <summary>
        /// Gets or sets the full path of the key file.
        /// </summary>
        string FullPath { get; set; }
    }
}
