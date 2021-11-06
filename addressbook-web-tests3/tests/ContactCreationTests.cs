using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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

            app.Contacts.Create(contact);

        }

        [Test]
        public void EmptyContactCreationTest()
        {
            ContactData contact = new ContactData("", "");

            app.Contacts.Create(contact);

        }

    }
}
