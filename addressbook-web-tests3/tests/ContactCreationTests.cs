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
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(15), GenerateRandomString(30)));
            }

            return contacts;
        }

        [Test, TestCaseSource("RandomContactDataProvider")]
        public void ContactCreationTest(ContactData contact)
        {
            //ContactData contact = new ContactData("Alex", "Sidorov");

            List<ContactData> oldContactList = app.Contacts.GetContactList();

            app.Contacts.Create(contact);

            Assert.AreEqual(oldContactList.Count + 1, app.Contacts.GetContactCount());

            List<ContactData> newContactList = app.Contacts.GetContactList();
            oldContactList.Add(contact);
            oldContactList.Sort();
            newContactList.Sort();
            Assert.AreEqual(oldContactList, newContactList);
            //Assert.AreEqual(oldContactList.Count + 1, newContactList.Count);
        }

        

    }
}
