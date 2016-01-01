using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace AcademyTests
{
    [TestClass]
    public class PatienceTests
    {
        IWebDriver driver;

        [TestMethod]
        public void TestPatience()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://www.wolvesbaneacademy.xyz");
            // driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            By searchBoxLocator = By.XPath("//*[@id='search-2']/form/label/input");
            IWebElement searchBox = wait.Until(ExpectedConditions.ElementToBeClickable(searchBoxLocator));
            // WebElement searchBox = driver.findElement(searchBoxLocator);
            searchBox.SendKeys("Automate");
            searchBox.Submit();
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
