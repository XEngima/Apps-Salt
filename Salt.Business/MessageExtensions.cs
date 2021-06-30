using Salt.Cypher;
using Salt.Interfaces;
using Salt.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Business
{
    public static class MessageExtensions
    {
        public static IMessage Decrypt(this IMessage message, ICryptographer cryptographer, string keyPart)
        {
            string headerKeyPart = "";
            string subjectKeyPart = keyPart.Substring(0, message.Subject.Length);
            string contentKeyPart = keyPart.Substring(message.Subject.Length, message.Content.Length);

            return new Message(message.Id, message.KeyNameHash, message.Header, cryptographer.Decrypt(message.Subject, subjectKeyPart), cryptographer.Decrypt(message.Content, contentKeyPart), message.KeyStartPos);
        }
    }
}
