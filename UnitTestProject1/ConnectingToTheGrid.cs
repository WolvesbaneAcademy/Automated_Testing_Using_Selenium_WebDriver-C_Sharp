using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Firefox;

namespace AcademyTests
{
    [TestClass]
    public class ConnectingToTheGrid
    {
        IWebDriver driver;

        [TestMethod]
        public void NavigateToPage()
        {
            driver.Navigate().GoToUrl("http://www.wolvesbaneacademy.xyz/");
            Assert.IsTrue(driver.Title.Contains("Wolvesbane Academy"), "Incorrect page loaded: found " + driver.Title);
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
