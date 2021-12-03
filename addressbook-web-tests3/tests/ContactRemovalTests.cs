using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests3
{
    [TestFixture]
    public class ContactRemovalTests : ContactTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            app.Contacts.CheckContactExists();

            //   List<ContactData> oldContactList = app.Contacts.GetContactList();
            List<ContactData> oldContactList = ContactData.GetAll();

            ContactData toBeRemoved = oldContactList[0];
            app.Contacts.Remove(toBeRemoved);

            Assert.AreEqual(oldContactList.Count - 1, app.Contacts.GetContactCount());

            //   List<ContactData> newContactList = app.Contacts.GetContactList();
            //   ContactData toBeRemoved = oldContactList[0];
            List<ContactData> newContactList = ContactData.GetAll();
            oldContactList.RemoveAt(0);
            Assert.AreEqual(oldContactList, newContactList);

            foreach (ContactData contact in newContactList)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
        }
    }
}
