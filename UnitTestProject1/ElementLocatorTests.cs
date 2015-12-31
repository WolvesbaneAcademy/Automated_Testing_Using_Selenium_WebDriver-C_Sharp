using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Firefox;

namespace AcademyTests
{
    [TestClass]
    public class ElementLocatorTests
    {
        IWebDriver driver;

        [TestMethod]
        public void TestElementLocators()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://www.wolvesbaneacademy.xyz");

            // ID
            By menuItemLocator = By.Id("menu-item-193");
            IWebElement articles = driver.FindElement(menuItemLocator);
            articles.Click();
            Assert.AreEqual(driver.Title, "Articles | Wolvesbane Academy | A Fount of Knowledge for the Perpetual Student");

            // Name
            By searchContainer = By.Id("search-2");
            IWebElement searchCont = driver.FindElement(searchContainer);

            By searchBoxLocator = By.Name("s");
            IWebElement searchBox = searchCont.FindElement(searchBoxLocator);
            searchBox.SendKeys("Automate");
            searchBox.Submit();

            // XPath
            By searchBoxLoc = By.XPath("//*[@id='search-2']/form/label/input");

            // CSS Selector
            By searchBoxLocCSS = By.CssSelector("#search-2 > form > label > input");

            // Link Text
            By linktext = By.LinkText("Using Page Objects");

            // Partial Link Text
            By partialLinkText = By.PartialLinkText("Page Objects");

            // Class Name
            By className = By.ClassName("main-content");

            // Tag Name
            By tagName = By.TagName("h2");
        }

        [TestInitialize]
        public void Setup()
        {
            DesiredCapabilities capabilities = new DesiredCapabilities();
            FirefoxProfile ffProfile = new FirefoxProfile();
            capabilities = DesiredCapabilities.Firefox();
            capabilities.SetCapability(FirefoxDriver.ProfileCapabilityName, ffProfile);
            capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Windows));
            capabilities.SetCapability(CapabilityType.BrowserName, "firefox");
            capabilities.SetCapability(CapabilityType.Version, "");

            Uri  driverHub = new Uri("http://127.0.0.1:4444/wd/hub/");
            driver = new RemoteWebDriver(driverHub, capabilities);

        }

        [TestCleanup]
        public void CleanUp()
        {
            driver.Close();
            driver.Quit();
        }

    }
}
