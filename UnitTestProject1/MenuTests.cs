using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Framework;
using Framework.PageObjects;
using Framework.Components;



namespace AcademyTests
{
    [TestClass]
    public class MenuTests
    {
        private IWebDriver driver;

        [TestInitialize]
        public void setup()
        {
            driver = Browser.Remote(new Uri("http://localhost:4444/wd/hub/"), "firefox", "");
            driver.Navigate().GoToUrl("http://www.wolvesbaneacademy.xyz/");
        }

        [TestMethod]
        public void testArchiveMenuOption()
        {
            HomePage home = new HomePage(driver);
            Menu main = home.getMenu();
            Page newPage = main.ClickArticles();
            Assert.IsTrue(newPage.isPageTitle("Articles | Wolvesbane Academy | A Fount of Knowledge for the Perpetual Student"), "Incorrect page loaded.");
        }

        [TestMethod]
        public void testVideosMenuOption()
        {
            HomePage home = new HomePage(driver);
            Menu main = home.getMenu();
            Page newPage = main.clickVideos();
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            Assert.IsTrue(newPage.isPageTitle("Wolvesbane Academy - YouTube"), "Incorrect page loaded.");
        }

        [TestMethod]
        public void testGitLabMenuOption()
        {
            HomePage home = new HomePage(driver);
            Menu main = home.getMenu();
            Page newPage = main.clickGitLab();
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            Assert.IsTrue(newPage.isPageTitle("Wolvesbane_Academy · GitLab"), "Incorrect page loaded.");
        }

        [TestCleanup]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
