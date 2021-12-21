using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests : AuthTestBase
    {
        [Test]
        public void TestProjectCreation()
        {
            ProjectData project = new ProjectData("test1");

            List<ProjectData> oldProjects = app.Project.GetProjectsFromTable();

            app.Project.CreateNewProject(project);

            List<ProjectData> newProjects = app.Project.GetProjectsFromTable();
            Assert.AreEqual(oldProjects.Count + 1, newProjects.Count);
            oldProjects.Add(project);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects); 
        }
    }
}
