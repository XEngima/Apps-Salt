using Salt.Cypher;
using Salt.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Business
{
    public static class MessageExtensions
    {
        public static MessageViewModel Decrypt(this IMessageStoreItem messageStoreItem, ICryptographer cryptographer, string keyPart)
        {

            var message = cryptographer.Decrypt(messageStoreItem.Message, keyPart);

            return new MessageViewModel(messageStoreItem.Id, DateTime.Now, "", "", "", message);
        }
    }
}
