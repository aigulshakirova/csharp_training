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

        public ContactHelper Remove()
        {
            manager.Navigator.GoToHomePage();
            SelectContact();
            RemoveContact();
          //  ReturnToHomePage();
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
            return this;
        }

        public ContactHelper ReturnToHomePage()
        {
            // Return to home page to view the added contact
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }

        public ContactHelper InitContactEditing(int v)
        {
            //  driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[" + v + "]/td[8]/a/img[@title='Edit']")).Click();
            driver.FindElement(By.XPath("//table[@id='maintable']/tbody/tr[@name='entry'][" + v + "]/td[8]/a/img[@title='Edit']")).Click();

            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public ContactHelper SelectContact()
        {
            driver.FindElement(By.XPath("//table[@id='maintable']//tr[@name='entry']//input[@type='checkbox'][1]")).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
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

        public List<ContactData> GetContactList()
        {
            List<ContactData> contacts = new List<ContactData>();
            manager.Navigator.GoToHomePage();
            ICollection<IWebElement> elements = driver.FindElements(By.XPath("//tr[@name='entry']"));
            foreach (IWebElement element in elements)
            {
                ContactData contact = new ContactData(element.Text, element.Text); // как разделить фамилию и имя?
                contacts.Add(contact);
                //groups.Add(new GroupData(element.Text));
            }
            return contacts;
        }


    }
}
