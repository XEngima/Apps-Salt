using System;
using Salt.Contacts;
using Salt.Messages;

namespace Salt.Business
{
    public static class Factory
    {
        public static IContactStore CreateContactStore()
        {
            return new FakeContactStore();
        }

        public static IMessageStore CreateMessageStore()
        {
            return new FakeMessageStore();
        }
    }
}
