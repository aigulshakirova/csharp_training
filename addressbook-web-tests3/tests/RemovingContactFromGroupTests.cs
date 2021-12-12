using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests3
{
    public class RemovingContactFromGroupTests : AuthTestBase
    {
        [Test]
        public void TestRemovingContactFromGroup()
        {
            app.Contacts.CheckContactExists();
            app.Groups.CheckGroupExists();

            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact;
            if (oldList.Count == 0)
            {
                contact = ContactData.GetAll().First();
                app.Contacts.AddContactToGroup(contact, group);
                oldList.Add(contact);
            }
            else
            {
                contact = oldList[0];
            }

            app.Contacts.RemoveContactFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
