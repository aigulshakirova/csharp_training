using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_tests_white
{
    [TestFixture]
    public class GroupRemovalTests : TestBase
    {
        [Test]
        public void TestGroupRemoval()
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();

            GroupData newGroup = new GroupData()
            {
                Name = "white1"
            };

            if (oldGroups.Count == 0)
            {
                app.Groups.Add(newGroup);
            }

            GroupData toBeRemoved = oldGroups[0];

            app.Groups.Remove(toBeRemoved);
            //app.Groups.Remove(0);

            List<GroupData> newGroups = app.Groups.GetGroupList();

            Assert.AreEqual(oldGroups.Count - 1, newGroups.Count);

            oldGroups.Remove(toBeRemoved);
            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
