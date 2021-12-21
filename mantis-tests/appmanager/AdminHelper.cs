using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace mantis_tests
{
    public class AdminHelper : HelperBase
    {
        private string baseUrl;

        public AdminHelper(ApplicationManager manager, String baseUrl) : base(manager) 
        {
            this.baseUrl = baseUrl;
        }

        public List<AccountData> GetAllAccounts()
        {
            List<AccountData> accounts = new List<AccountData>();
            OpenAppAndLogin();
            driver.FindElement(By.XPath("//span[@class='menu-text' and normalize-space(text())='Управление']")).Click();
            driver.FindElement(By.LinkText("Управление пользователями")).Click();
            IList<IWebElement> rows= driver.FindElements(By.XPath("//table//tr"));
            int rowsCount = rows.Count;
            for (int i=0; i< rowsCount; i++)
            {
                if (i != 0)
                {
                    IWebElement link = rows[i].FindElement(By.TagName("a"));
                    string name = link.Text;
                    string href = link.GetAttribute("href");
                    Match m = Regex.Match(href, @"\d+$");
                    string id = m.Value;

                    accounts.Add(new AccountData()
                    {
                        Name = name, Id=id
                    });
                }  
            }

            return accounts;

            /*    foreach(IWebElement row in rows)
                {
                    IWebElement link = row.FindElement(By.TagName("a"));
                    string name = link.Text;
                    string href = link.GetAttribute("href");
                    Match m = Regex.Match(href, @"\d+$");
                    string id = m.Value;
                } */
        }

        public void DeleteAccount(AccountData account)
        {
            //  IWebDriver driver = OpenAppAndLogin();
            OpenAppAndLogin();
            OpenMainPage();
            driver.FindElement(By.XPath("//span[@class='menu-text' and normalize-space(text())='Управление']")).Click();
            driver.FindElement(By.LinkText("Управление пользователями")).Click();
            driver.Url = baseUrl + "/manage_user_edit_page.php?user_id=" + account.Id;
            driver.FindElement(By.CssSelector("input[value='Удалить учётную запись']")).Click();
            driver.FindElement(By.CssSelector("input[value='Удалить учётную запись']")).Click();
        }

        private void OpenAppAndLogin()
        {
            //IWebDriver driver = ApplicationManager.GetInstance();
            //app.Url = baseUrl + "";
            manager.Auth.Logout();
            OpenMainPage();
            driver.FindElement(By.Name("username")).SendKeys("administrator");
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();
            driver.FindElement(By.Name("password")).SendKeys("root");
            driver.FindElement(By.XPath("//input[@type='submit']")).Click();
        }
    }
}
