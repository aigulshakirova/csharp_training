using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


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
    }
}
