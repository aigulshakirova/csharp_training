using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using NUnit.Framework;

namespace addressbook_web_tests3
{
    [TestFixture]
    public class ContactCreationTests : ContactTestBase
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

        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            return (List<ContactData>)new XmlSerializer(typeof(List<ContactData>)).Deserialize(new StreamReader(@"contacts.xml"));
        }

        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(File.ReadAllText(@"contacts.json"));
        }

        [Test, TestCaseSource("ContactDataFromXmlFile")]
        public void ContactCreationTest(ContactData contact)
        {
            //   List<ContactData> oldContactList = app.Contacts.GetContactList();
            List<ContactData> oldContactList = ContactData.GetAll();

            app.Contacts.Create(contact);

            Assert.AreEqual(oldContactList.Count + 1, app.Contacts.GetContactCount());

         //   List<ContactData> newContactList = app.Contacts.GetContactList();
            List<ContactData> newContactList = ContactData.GetAll();
            oldContactList.Add(contact);
            oldContactList.Sort();
            newContactList.Sort();
            Assert.AreEqual(oldContactList, newContactList);
            //Assert.AreEqual(oldContactList.Count + 1, newContactList.Count);
        }

        

    }
}
