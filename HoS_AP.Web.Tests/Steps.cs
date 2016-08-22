using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using HoS_AP.DAL.Dto;
using HoS_AP.Misc;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using TechTalk.SpecFlow;

namespace HoS_AP.Web.Tests
{
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
        public void GivenIAmLoggedInAsMegan(string username)
        {
            InternetExplorerDriver.Navigate().GoToUrl(UrlManager.GetPage(PageTypes.Login));
            var usernameElement = InternetExplorerDriver.FindElementByName("UserName");
            var passwordElement = InternetExplorerDriver.FindElementByName("Password");
            usernameElement.SendKeys(username);
            passwordElement.SendKeys(UserManager.GetPassword(username));
            var signInButton = InternetExplorerDriver.FindElementByXPath(".//input[@type='submit' and @value='Sign In']");
            signInButton.Submit();
        }
        
        [When(@"I navigate to “(.*)” page")]
        public void WhenINavigateToCharacterListingPage(string pageName)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"there are the following characters in system")]
        public void GivenThereAreTheFollowingCharactersInSystem(Table table)
        {
            var path = GetApplicationPath(Constants.WebAppRelativePath);
            path = Path.Combine(path, "bin") + "/Characters.json";
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            var characters = new List<Character>();
            foreach (var tr in table.Rows)
            {
                characters.Add(new Character
                {
                    Id= Guid.NewGuid(),
                    Created = DateTime.Now,
                    Name = tr[0],
                    Price = Convert.ToDecimal(tr[1]),
                    Type = (CharacterTypes)Enum.Parse(typeof(CharacterTypes), tr[2], true),
                    Active = Convert.ToBoolean(tr[3]),
                    Deleted = Convert.ToBoolean(tr[4])
                });
            }

            File.WriteAllText(path, JsonConvert.SerializeObject(characters));
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }


        [Then(@"I should see character in list")]
        public void ThenIShouldSeeCharacterInList(Table table)
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

        [Then(@"I should be on “(.*)” page")]
        public void ThenIShouldBeOnCharacterListingPage(string pageName)
        {
            Assert.AreEqual(InternetExplorerDriver.Url, UrlManager.GetPage(PageTypes.Listing));
        }
    }
}