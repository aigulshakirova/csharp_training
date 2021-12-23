using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ProjectHelper : HelperBase
    {
        public ProjectHelper(ApplicationManager manager) : base(manager) { }

        public void CreateNewProject(ProjectData project)
        {
            manager.Navigation.GoToManagementPage();
            manager.Navigation.GoToProjectManagementPage();
            PressNewProjectButton();
            FillNewProjectForm(project);
            SubmitProjectCreation();
            // "project is added" message
            //new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        public void CreateNewProjectAPI(ProjectData project)
        {
            AccountData account = new AccountData("administrator", "root");

            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData projectData = new Mantis.ProjectData();
            projectData.name = project.Name;
            client.mc_project_add(account.Name, account.Password, projectData);
        }

        public void DeleteProject(int index)
        {
            manager.Navigation.GoToManagementPage();
            manager.Navigation.GoToProjectManagementPage();
            SelectToViewProjectInfo(index);
            DeleteProjectInfo();
            ConfirmProjectDeletion();
        }

        private void ConfirmProjectDeletion()
        {
            driver.FindElement(By.XPath("//input[@type='submit' and @value='Удалить проект']")).Click();
        }

        private void DeleteProjectInfo()
        {
            driver.FindElement(By.XPath("//input[@type='submit' and @value='Удалить проект']")).Click();
        }

        private void SelectToViewProjectInfo(int index)
        {
            driver.FindElement(By.XPath("(//div[@class='table-responsive']/table/tbody)[1]/tr["+ (index+1) + "]/td[1]/a")).Click();
        }

        private void SubmitProjectCreation()
        {
            driver.FindElement(By.XPath("//input[@type='submit' and @value='Добавить проект']")).Click();
        }

        private void FillNewProjectForm(ProjectData project)
        {
          //  driver.FindElement(By.Name("name")).SendKeys(project.Name);
            driver.FindElement(By.Id("project-name")).SendKeys(project.Name);
        }

        private void PressNewProjectButton()
        {
            driver.FindElement(By.XPath("//button[contains(@class, 'btn') and normalize-space(text()) = 'Создать новый проект']")).Click();
        }

        public List<ProjectData> GetProjectsFromTable()
        {
            manager.Navigation.GoToManagementPage();
            manager.Navigation.GoToProjectManagementPage();

            List<ProjectData> ProjectList = new List<ProjectData>();

            int count = driver.FindElements(By.XPath("(//div[@class='table-responsive']/table/tbody)[1]/tr")).Count;

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    IList<IWebElement> cells = driver.FindElements(By.XPath("(//div[@class='table-responsive']/table/tbody)[1]/tr"))[i]
                    .FindElements(By.TagName("td"));

                    string projectName = cells[0].Text;
                    ProjectList.Add(new ProjectData(projectName));
                }
            }
            
            return ProjectList;
        }

        public List<ProjectData> GetProjectsFromAPI()
        {
            AccountData account = new AccountData("administrator", "root");
            List<ProjectData> ProjectList = new List<ProjectData>();

            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData[] projectData = client.mc_projects_get_user_accessible(account.Name, account.Password);
            //   List<Mantis.ProjectData> projects = new List<Mantis.ProjectData>();
            //   Mantis.ProjectData[] projectData = new Mantis.ProjectData()[];
            //   projectData = client.mc_projects_get_user_accessible(account.Name, account.Password);
            //   Mantis.ProjectData[] projectData = new Mantis.ProjectData(){client.mc_projects_get_user_accessible(account.Name, account.Password)};


            int count = projectData.Length;

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    ProjectList.Add(new ProjectData(projectData[i].name));
                }
            }
            return ProjectList;
        }
    }
}
