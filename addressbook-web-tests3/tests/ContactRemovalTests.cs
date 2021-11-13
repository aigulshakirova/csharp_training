using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests3
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            app.Contacts.CheckContactExists();

            List<ContactData> oldContactList = app.Contacts.GetContactList();
            app.Contacts.Remove(0);

            List<ContactData> newContactList = app.Contacts.GetContactList();
            oldContactList.RemoveAt(0);
            Assert.AreEqual(oldContactList, newContactList);
        }
    }
}
