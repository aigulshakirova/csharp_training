using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace addressbook_web_tests3
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {
        
        [Test]
        public void GroupCreationTest() 
        {
            
            GroupData group = new GroupData("test2");
            group.Header = "test2Header";
            group.Footer = "test2Footer";

            app.Groups.Create(group);
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
           
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";

            app.Groups.Create(group);
        }

    }
}
