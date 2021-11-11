using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace addressbook_web_tests3
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
       
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("Alex", "Sidorov");

            List<ContactData> oldContactList = app.Contacts.GetContactList();

            app.Contacts.Create(contact);

            List<ContactData> newContactList = app.Contacts.GetContactList();
            Assert.AreEqual(oldContactList.Count + 1, newContactList.Count);
        }

        [Test]
        public void EmptyContactCreationTest()
        {
            ContactData contact = new ContactData("", "");

            app.Contacts.Create(contact);

        }

    }
}
