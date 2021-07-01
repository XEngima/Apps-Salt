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
            string headerKeyPart = keyPart.Substring(0, message.Header.Length);
            string subjectKeyPart = keyPart.Substring(message.Header.Length, message.Subject.Length);
            string contentKeyPart = keyPart.Substring(message.Header.Length + message.Subject.Length, message.Content.Length);

            return new Message(message.Id, message.KeyNameHash, cryptographer.Decrypt(message.Header, headerKeyPart), cryptographer.Decrypt(message.Subject, subjectKeyPart), cryptographer.Decrypt(message.Content, contentKeyPart), message.KeyStartPos);
        }
    }
}
