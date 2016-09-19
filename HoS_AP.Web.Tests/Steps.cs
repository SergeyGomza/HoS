using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.UI.WebControls;
using HoS_AP.BLL.ServiceInterfaces;
using HoS_AP.DAL.Dto;
using HoS_AP.Misc;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using Table = TechTalk.SpecFlow.Table;

namespace HoS_AP.Web.Tests
{
    using HoS_AP.DAL.EF;

    [Binding]
    public class Steps
    {
#region Helpers

        private static InternetExplorerDriver InternetExplorerDriver { get; set; }

        private static string GetCurrentDirectory()
        {
            var uri = new UriBuilder(Assembly.GetExecutingAssembly().CodeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

        private static string GetApplicationPath(string webApplicationRelativePath)
        {
            var path = Path.Combine(GetCurrentDirectory(), webApplicationRelativePath);
            path = Path.GetFullPath(path);
            return path;
        }

        private static void StartIIS()
        {
            var applicationPath = GetApplicationPath(Constants.WebAppRelativePath);
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            Constants.iisProcess = new Process();
            Constants.iisProcess.StartInfo.FileName = programFiles + @"\IIS Express\iisexpress.exe";
            Constants.iisProcess.StartInfo.Arguments = string.Format("/path:\"{0}\" /port:{1}", applicationPath, Constants.IisPort);
            Constants.iisProcess.Start();
        }

        #endregion

#region Init|Quit

        [BeforeScenario]
        public static void BeforeScenario()
        {
            StartIIS();
            InternetExplorerDriver = new InternetExplorerDriver(GetCurrentDirectory());
            InternetExplorerDriver.Manage().Cookies.DeleteAllCookies();
        }

        [AfterScenario]
        public static void AfterScenarion()
        {
            if (Constants.iisProcess.HasExited == false)
            {
                Constants.iisProcess.Kill();
            }

            InternetExplorerDriver.Quit();
        }

        #endregion

        [Given(@"I am logged in as “(.*)”")]
        [When(@"I am logged in as “(.*)”")]
        public void IAmLoggedInAsMegan(string username)
        {
            var userTable = new Table("Name", "Password");
            userTable.AddRow(username, UserManager.GetPassword(username));
            ThereIsAreFollowingUsersInTheSystem(userTable);
            INavigateToPage("Login");
            var table = new Table("UserName", "Password");
            table.AddRow("UserName", username);
            table.AddRow("Password", UserManager.GetPassword(username));
            IFillInControlsAsFollows(table);
            IClickButton("Sign In");
        }

        [Given(@"there are the following characters in system")]
        public void ThereAreTheFollowingCharactersInSystem(Table table)
        {
            using (var context = new DatabaseContext())
                {
                    if (context.Characters.Any())
                    {
                        context.Characters.RemoveRange(context.Characters.AsEnumerable());
                    }

                    context.SaveChanges();

                    foreach (var tr in table.Rows)
                    {
                        context.Characters.Add(new Character
                        {
                            Id = Guid.NewGuid(),
                            Created = DateTime.Now,
                            Name = tr[0],
                            Price = Convert.ToDecimal(tr[1]),
                            Type = (CharacterTypes)Enum.Parse(typeof(CharacterTypes), tr[2], true),
                            Active = Convert.ToBoolean(tr[3]),
                            Deleted = Convert.ToBoolean(tr[4]),
                        });
                    }

                    context.SaveChanges();
                }

            //var path = GetApplicationPath(Constants.WebAppRelativePath);
            //path = Path.Combine(path, "bin") + "/Characters.json";
            //if (File.Exists(path))
            //{
            //    File.Delete(path);
            //}

            //var characters = new List<Character>();
            //foreach (var tr in table.Rows)
            //{
            //    characters.Add(new Character
            //    {
            //        Id= Guid.NewGuid(),
            //        Created = DateTime.Now,
            //        Name = tr[0],
            //        Price = Convert.ToDecimal(tr[1]),
            //        Type = (CharacterTypes)Enum.Parse(typeof(CharacterTypes), tr[2], true),
            //        Active = Convert.ToBoolean(tr[3]),
            //        Deleted = Convert.ToBoolean(tr[4])
            //    });
            //}

            //File.WriteAllText(path, JsonConvert.SerializeObject(characters));
        }

        [When(@"I fill in controls as follows")]
        public void IFillInControlsAsFollows(Table table)
        {
            foreach (var row in table.Rows)
            {
                var element = InternetExplorerDriver.FindElement(By.Name(row[0]));
                var elementType = element.GetAttribute("type");
                if (elementType == "text" || elementType == "password")
                {
                    element.Clear();
                    element.SendKeys(row[1]);
                }
                if (elementType == "select-one")
                {
                    var selectElement = new SelectElement(element);
                    selectElement.SelectByText(row[1]);
                }
                if (elementType == "checkbox")
                {
                    var needToBeChecked = Convert.ToBoolean(row[1]);
                    if ((element.Selected && !needToBeChecked) || (!element.Selected && needToBeChecked))
                    {
                        element.Click();
                    }
                }
            }
        }

        [When(@"I click ""(.*)"" button")]
        public void IClickButton(string buttonName)
        {
            try
            {
                var input =
                    InternetExplorerDriver.FindElementByXPath(string.Format(".//input[@type='submit' and @value='{0}']", buttonName));
                input.Submit();
            }
            catch
            {
                var button = 
                    InternetExplorerDriver.FindElementByXPath(string.Format(".//button[@type='button' and text()='{0}']", buttonName));
                button.Click();
            }
        }

        [Given(@"Given there is are following users in the system")]
        public void ThereIsAreFollowingUsersInTheSystem(Table table)
        {
            using (var context = new DatabaseContext())
                {
                    if (context.Accounts.Any())
                    {
                        context.Accounts.RemoveRange(context.Accounts.AsQueryable());
                    }

                    var service = new EncryptionService();

                    foreach (var tableRow in table.Rows)
                    {
                        context.Accounts.Add(new Account
                        {
                            UserName = tableRow[0],
                            Password = service.CreateHash(tableRow[1])
                        });
                    }

                    context.SaveChanges();
                }

            //var path = GetApplicationPath(Constants.WebAppRelativePath);
            //path = Path.Combine(path, "bin") + "/Accounts.json";
            //if (File.Exists(path))
            //{
            //    File.Delete(path);
            //}

            //var accounts = new List<Account>();
            //var service = new EncryptionService();
            //foreach (var tableRow in table.Rows)
            //{
            //    accounts.Add(new Account
            //    {
            //        UserName = tableRow[0],
            //        Password = service.CreateHash(tableRow[1])
            //    });
            //}

            //File.WriteAllText(path, JsonConvert.SerializeObject(accounts));
        }

        [Then(@"I should see character in list")]
        public void IShouldSeeCharacterInList(Table table)
        {
            var charactersHtmlTable = InternetExplorerDriver.FindElementByTagName("table");
            var tbody = charactersHtmlTable.FindElement(By.TagName("tbody"));
            var charactersHtmlRows = tbody.FindElements(By.TagName("tr"));
            var actualCharacters = new List<string[]>();

            foreach (var row in charactersHtmlRows)
            {
                var character = new string[6];
                var columns = row.FindElements(By.TagName("td"));
                for (var j = 0; j < columns.Count; j++)
                {
                    character[j] = columns[j].Text;
                }

                actualCharacters.Add(character);
            }

            foreach (var row in table.Rows)
            {
                var rowFound = false;
                foreach (var character in actualCharacters)
                {
                    rowFound = row["Name"] == character[0] 
                        && row["Type"] == character[1]
                        && row["Price"] == character[3]
                        && row["Active"] == character[4]
                        && row["Deleted"] == character[5];
                    if (rowFound) break;
                }

                Assert.IsTrue(rowFound);
            }
        }

        [When(@"I navigate to “(.*)” page")]
        public void INavigateToPage(string pageName)
        {
            InternetExplorerDriver.Navigate().GoToUrl(UrlManager.GetPage(pageName));
        }

        [Then(@"I should be on “(.*)” page")]
        public void IShouldBeOnPage(string pageName)
        {
            var url = InternetExplorerDriver.Url;
            if (url.IndexOf("?") > -1)
            {
                url = url.Remove(url.IndexOf("?"));
            }

            Assert.AreEqual(UrlManager.GetPage(pageName), url);
        }
    }
}