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
            ContactData oldContactData = oldContactList[0];

            app.Contacts.Modify(0, newContactData);

            Assert.AreEqual(oldContactList.Count, app.Contacts.GetContactCount());

            List<ContactData> newContactList = app.Contacts.GetContactList();
            oldContactList[0].Firstname = newContactData.Firstname;
            oldContactList[0].Lastname = newContactData.Lastname;
            oldContactList.Sort();
            newContactList.Sort();
            Assert.AreEqual(oldContactList, newContactList);

            foreach (ContactData contact in newContactList)
            {
                if (contact.Id == oldContactData.Id)
                {
                    Assert.AreEqual(newContactData.Firstname, contact.Firstname);
                    Assert.AreEqual(newContactData.Lastname, contact.Lastname);
                }
            }

        }
    }
}
