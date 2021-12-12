using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests3
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            app.Contacts.CheckContactExists();
            app.Groups.CheckGroupExists();

            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            List<ContactData> contactsWithoutGroups = ContactData.GetAll().Except(oldList).ToList();
       
            ContactData contact;
            if (contactsWithoutGroups.Count == 0)
            {
                contact = new ContactData("Mila", "Jovovich");
                app.Contacts.Create(contact);
            }

            contact = ContactData.GetAll().Except(oldList).First();

            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }

        
    }
}
