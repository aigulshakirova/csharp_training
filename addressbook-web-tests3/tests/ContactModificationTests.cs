using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests3
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            app.Contacts.CheckContactExists();
            ContactData newContactData = new ContactData("upd", "");

            List<ContactData> oldContactList = app.Contacts.GetContactList();

            app.Contacts.Modify(0, newContactData);

            List<ContactData> newContactList = app.Contacts.GetContactList();
            oldContactList[0].Firstname = newContactData.Firstname;
            oldContactList[0].Lastname = newContactData.Lastname;
            oldContactList.Sort();
            newContactList.Sort();
            Assert.AreEqual(oldContactList, newContactList);

        }
    }
}
