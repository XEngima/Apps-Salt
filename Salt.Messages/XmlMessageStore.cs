using System;
using System.Collections.Generic;
using System.Text;

namespace Salt.Messages
{
    public class XmlMessageStore : IMessageStore
    {
        public XmlMessageStore(string folderPath)
        {
            FolderPath = folderPath;
        }

        public string FolderPath { get; set; }

        public int FindNextKeyPos(string keyName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IMessageHeaderItem> GetMessageHeadersByKeyName(string keyName)
        {
            throw new NotImplementedException();
        }

        public IMessageStoreItem GetMessageStoreItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IMessageStoreItem> GetMessageStoreItemsByKeyName(string keyName)
        {
            throw new NotImplementedException();
        }

        public string GetSubjectByMessageId(Guid messageId)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(IMessageStoreItem message)
        {
            throw new NotImplementedException();
        }
    }
}
