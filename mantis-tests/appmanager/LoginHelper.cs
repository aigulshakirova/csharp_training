using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace mantis_tests
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager) : base(manager) { }

        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account))
                {
                    return;
                }

                Logout();
            }

            OpenMainPage();
            EnterUsername(account);
            PressSubmitButton();
            EnterPassword(account);
            PressSubmitButton();
         //   driver.FindElement(By.XPath("//span[@class='user-info' and @value=" + account.Name + "]"));
        }



        private void EnterPassword(AccountData account)
        {
            driver.FindElement(By.Name("password")).SendKeys(account.Password);
        }

        private void PressSubmitButton()
        {
           // driver.FindElement(By.CssSelector("input.btn")).Click();
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();
        }

        private void EnterUsername(AccountData account)
        {
            driver.FindElement(By.Name("username")).SendKeys(account.Name);
        }

        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.LinkText("Выход")).Click();
                driver.FindElement(By.Name("username"));
            }

        }


        public bool IsLoggedIn()
        {
            return IsElementPresent(By.XPath("//span[@class='user-info']"));
        }

        public bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn()
                && GetLoggedUserName() == account.Name;
        }

        private string GetLoggedUserName()
        {
            return driver.FindElement(By.XPath("//span[@class='user-info']")).Text;
        }
    }
}
