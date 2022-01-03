using Salt.Cypher;
using Salt.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Business
{
    public static class MessageExtensions
    {
        public static SaltMessage Decrypt(this IMessageStoreItem messageStoreItem, ICryptographer cryptographer, string keyPart)
        {

            var message = cryptographer.Decrypt(messageStoreItem.Message, keyPart);

            return new SaltMessage(messageStoreItem.Id, DateTime.Now, "", "", "", message);
        }
    }
}
