using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace AcademyTests
{
    /// <summary>
    /// Summary description for InitWebdriver
    /// </summary>
    [TestClass]
    public class InitWebdriver
    {
        public InitWebdriver()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private IWebDriver driver;
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestInitialize]
        public void Setup()
        {
            // Firefox Driver
            driver = new FirefoxDriver();

            // Chrome Driver
            driver = new ChromeDriver();

            // Remote Web Driver
            DesiredCapabilities capabilities = new DesiredCapabilities();
            FirefoxProfile ffProfile = new FirefoxProfile();
            capabilities = DesiredCapabilities.Firefox();
            capabilities.SetCapability(FirefoxDriver.ProfileCapabilityName, ffProfile);
            capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Windows));
            capabilities.SetCapability(CapabilityType.BrowserName, "firefox");
            capabilities.SetCapability(CapabilityType.Version, "");

            Uri driverHub = new Uri("http://127.0.0.1:4444/wd/hub/");
            driver = new RemoteWebDriver(driverHub, capabilities);

        }

        [TestMethod]
        public void TestMethod1()
        {
            Console.WriteLine(driver.Title);
            //
            // TODO: Add test logic here
            //
        }
    }
}
