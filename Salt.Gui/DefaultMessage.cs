using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salt.Gui
{
    public static class DefaultMessage
    {
        public static string GetInstructionsMessage()
        {
            return @"Hi and welcome new Salt user!

Salt is - if correctly used - a 100% safe messaging tool for encrypted communications. Here are some instructions to get you started.

============
 1. General
============

To initialize a new contact you will need to first deliver a key in a safe way. A safe way may be personally hand over a locally created key that is copied to a USB memory device, or send it in another safe channel.

You will also need to exchange contact IDs. A contact ID is a simple Guid. The application will create one for you on first use, and if you don't have one already you can go with that. Send it to everyone you will communicate with, because this is your 'phone number' or 'email address'. Once you have installed the key in both ends and have each other's IDs, you will be able to start communicating.

To help you get started, the application has already created your first key in the key store, your first contact (Me) in the contact store, and your first message (this) in the message store.

==================
 2. The Key Store
==================

The key store is by default created as a folder in the application's working directory.

Each key is a file, and the file name (without the .key extension) is the key's name. To be considered safe, a key should contain a totally random set of characters. Allowed characters are the 221 printable characters in the ANSI table - including the non visible tab, linefeed and carriage return.

======================
 3. The Contact Store
======================

The contact store is by default created as a folder in the application's working directory.

Each contact is an XML file, containing the name and the ID of a contact. The key to use in conversations with this contact need also be written in the file. Check the default Me contact for an example. The best and safest practice is to use one unique key per contact.

The name of a contact XML file can be whatever, but best practice would be to name the file after the contact.

======================
 4. The Message Store
======================

The message store is by default created as a folder in the application's working directory.

The messages are stored as XML files. One file per message. These files are encrypted, and while you can read some meta data in the file - e.g. which key is used - the sender, receiver, subject and message body is unreadable unless you have access to the key.

===================================
 5. Sending and Receiving Messages
===================================

To send a message, select New Message from the Messaging menu. Write the name of the contact as it is written in the Contact file. Then write a subject and the message body and press the Send button. The message is now stored in the message store. Find it and send it to the recipient in any way you want. By email, cloud storage, or type it in a snail mail (the last alternative not recommended). No one will be able to decrypt it without the key, not even the military's smartest AI running on the world's fastest super computer.

When you receive a message (let's say by email), download the encrypted XML message file and put it into your message store. When you start up Salt it will appear automatically. However, it will not appear if you do not have the contact in you contact store or if you are missing the necessary key in the key store.

=======================
 6. Why Is It So Safe?
=======================

The algorithm used is a simple Caesar crypto in which each character represents a number. Each character in a message is then 'added' to the corresponding character in the key, which gives a new character - the encrypted character.

This crypto is not safe if you use a short key. However, if you use a key that is of equal length as the message, no one will be able to 'hack' it. The reason is that different keys decrypts to different perfectly readable (or unreadable) messages. A 'hacker' will never be able to decide if your message is 'I will blow it up' or 'Want some coffee?'. One key will give the former, and another will give the latter. Of course, if you reuse any part of a key in another message, the communication can be hacked. The Salt application handles this, but to be safe you can double check the key positions used - they are written in the message file.";
        }
    }
}
