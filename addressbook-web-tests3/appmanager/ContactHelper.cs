using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;


namespace addressbook_web_tests3
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager)
            : base(manager)
        {
        }

        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.GoToHomePage();

            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHomePage();
            return this;
        }


        public ContactHelper Modify(int v, ContactData newContactData)
        {
            manager.Navigator.GoToHomePage();

            InitContactEditing(v);
            FillContactForm(newContactData);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }

       
        public ContactHelper Remove(int v)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(v);
            RemoveContact();
            ReturnToHomePage();
            return this;
        }

        
        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
            
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[21]")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper ReturnToHomePage()
        {
            // Return to home page to view the added contact
            driver.FindElement(By.LinkText("home")).Click();
            return this;
        }

        public ContactHelper InitContactEditing(int v)
        {
            //  driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + v + "]/td[8]/a/img[@title='Edit']")).Click();
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[@name='entry'][" + (v+1) + "]/td[8]/a/img[@title='Edit']")).Click();

            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']//tr[@name='entry']//input[@type='checkbox']["+ (index+1) + "]")).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
            contactCache = null;
            return this;
        }

        public void CheckContactExists()
        {
            manager.Navigator.GoToHomePage();

            if (IsElementPresent(By.XPath("//table[@id='maintable']/tbody/tr[@name='entry'][1]/td[8]/a/img[@title='Edit']")))
            {
                return;
            }
            else
            {
                ContactData emptyContact = new ContactData("", "");
                Create(emptyContact);
            }
        }

        private int i;
        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if(contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.GoToHomePage();
                ICollection<IWebElement> elementsLN = driver.FindElements(By.XPath("//tr[@name='entry']/td[2]"));
                ICollection<IWebElement> elementsFN = driver.FindElements(By.XPath("//tr[@name='entry']/td[3]"));

                ICollection<IWebElement> contactLines = driver.FindElements(By.XPath("//tr[@name='entry']"));

                for (i = 0; i < elementsFN.Count; i++)
                {

                    ContactData contact = new ContactData(elementsFN.ElementAt(i).Text, elementsLN.ElementAt(i).Text);
                    contact.Id = contactLines.ElementAt(i).FindElement(By.TagName("input")).GetAttribute("value");

                    contactCache.Add(contact);
                }
            }

            return new List<ContactData>(contactCache);
        }

        public int GetContactCount()
        {
            return driver.FindElements(By.XPath("//tr[@name='entry']")).Count();
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmails
            };
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactEditing(index);
            string firstname = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastname = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string email1 = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstname, lastname)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Email1 = email1,
                Email2 = email2,
                Email3 = email3
            };
        }

        public string GetContactInformationFromDetailsPage(int v)
        {
            manager.Navigator.GoToHomePage();
            ViewContactDetails(v);
            string contentTable = driver.FindElement(By.Id("content")).Text.Trim();

            //    System.Console.Out.Write(contentTable);

            return contentTable;
        }

        public string GetContactInformationFromEditFormAsString(int v)
        {
            ContactData contact = GetContactInformationFromEditForm(v);
            string phones = "";
            string address = "";

            if (contact.HomePhone == "" && contact.MobilePhone == "" && contact.WorkPhone == "")
            {
                phones = "";
            } 
            else if (contact.HomePhone == "" && contact.MobilePhone == "")
            {
                phones = "W: " + contact.WorkPhone + "\r\n" + "\r\n";
            } 
            else if (contact.HomePhone == "" && contact.WorkPhone == "")
            {
                phones = "M: " + contact.MobilePhone + "\r\n" + "\r\n";
            } 
            else if (contact.MobilePhone == "" && contact.WorkPhone == "")
            {
                phones = "H: " + contact.HomePhone + "\r\n" + "\r\n";
            }
            else if (contact.HomePhone == "")
            {
                phones = "M: " + contact.MobilePhone + "\r\n" + "W: " + contact.WorkPhone + "\r\n" + "\r\n";
            }
            else if (contact.MobilePhone == "")
            {
                phones = "H: " + contact.HomePhone + "\r\n" + "W: " + contact.WorkPhone + "\r\n" + "\r\n";
            }
            else if (contact.WorkPhone == "")
            {
                phones = "H: " + contact.HomePhone + "\r\n" + "M: " + contact.MobilePhone + "\r\n" + "\r\n";
            }
            else if (contact.HomePhone != "" && contact.MobilePhone != "" && contact.WorkPhone != "")
            {
                phones = "H: " + contact.HomePhone + "\r\n" + "M: " + contact.MobilePhone + "\r\n" + "W: " + contact.WorkPhone + "\r\n" + "\r\n";
            }

            if (contact.Address == "")
            {
                address = "";
            }
            else
            {
                address = contact.Address + "\r\n";
            }

            string contactInfoAll = contact.Firstname + " " + contact.Lastname + "\r\n" + address + "\r\n"
                + phones
                + contact.AllEmails;

            //    System.Console.Out.Write(contactInfoAll);

            return contactInfoAll.Trim();
        }


        public ContactHelper ViewContactDetails(int v)
        {
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[@name='entry'][" + (v + 1) + "]/td[7]/a/img[@title='Details']")).Click();
            return this;
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }
    }
}
