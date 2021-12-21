using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace mantis_tests
{
    public class MenuHelper : HelperBase
    {
        public MenuHelper(ApplicationManager manager) : base(manager) { }

        public void GoToManagementPage()
        {
            driver.FindElement(By.XPath("//span[@class='menu-text' and normalize-space(text())='Управление']")).Click();
        }

        public void GoToProjectManagementPage()
        {
            driver.FindElement(By.LinkText("Управление проектами")).Click();
        }
    }
}
