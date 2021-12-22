using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectDeletionTests : AuthTestBase
    {
        [Test]
        public void TestProjectDeletion()
        {
            ProjectData project = new ProjectData("000");

            List<ProjectData> oldProjects = app.Project.GetProjectsFromAPI();

            if(oldProjects.Count == 0)
            {
                app.Project.CreateNewProjectAPI(project);
                oldProjects.Add(project);
            }

            app.Project.DeleteProject(0);

            List<ProjectData> newProjects = app.Project.GetProjectsFromAPI();
            Assert.AreEqual(oldProjects.Count - 1, newProjects.Count);
            oldProjects.RemoveAt(0);
            oldProjects.Sort();
            newProjects.Sort();
            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
