using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace mantis_tests 
{
    
    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        string localPath = TestContext.CurrentContext.TestDirectory;
        [OneTimeSetUp]
        public void SetUpConfig()
        {
            
            app.Ftp.BackupFile("/config_inc.php"); // @"D:\xampp\htdocs\mantisbt-2.25.2\config" or "/config_inc.php"
            using (Stream localFile = File.Open(localPath + @"/config_inc.php", FileMode.Open))
            {
                app.Ftp.Upload("/config_inc.php", localFile); // @"D:\xampp\htdocs\mantisbt-2.25.2\config" or /config_inc.php
            };
            
        }

        [Test]
        public void TestAccountRegistration()
        {
            AccountData account = new AccountData() {
                Name = "testuser5",
                Password = "password",
                Email = "testuser5@localhost.localdomain"
            };

            List<AccountData> accounts = app.Admin.GetAllAccounts();
            AccountData existingAccount = accounts.Find(x => x.Name == account.Name);
            if(existingAccount != null)
            {
                app.Admin.DeleteAccount(existingAccount);
            }
           
            app.Registration.Register(account);
        }

        [OneTimeTearDown]
        public void RestoreConfig()
        {
            app.Ftp.RestoreBackupFile(localPath + @"/config_inc.php");
        }
    }
}
