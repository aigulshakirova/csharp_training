using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests3
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void TestContactInformation()
        {
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(2);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(2);

            // verification
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones.Trim());
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
        }

        [Test]
        public void TestDetailedContactInformation()
        {
            string infoFromDetailsPage = app.Contacts.GetContactInformationFromDetailsPage(2);
            string infoFromEditForm = app.Contacts.GetContactInformationFromEditFormAsString(2);

            //verification
            Assert.AreEqual(infoFromDetailsPage, infoFromEditForm);

        }
    }
}
