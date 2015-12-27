using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace Framework
{
    public static class Browser
    {
        private static string screenshotPath = "." + Path.DirectorySeparatorChar + "Screenshots" + Path.DirectorySeparatorChar;
        private static string errorScreenshotPath = "." + Path.DirectorySeparatorChar + "ErrorScreenshots" + Path.DirectorySeparatorChar;

        public static IWebDriver Firefox() 
        {
            return new FirefoxDriver();
        }
        public static IWebDriver Chrome()
        {
            return new ChromeDriver();
        }
        public static IWebDriver IE()
        {
            return new InternetExplorerDriver();
        }
        public static IWebDriver Driver(Uri driverHub, string browserName, string browserVersion)
        {
            DesiredCapabilities capabilities = new DesiredCapabilities();
            switch (browserName)
            {
                case "chrome":
                    ChromeOptions ChrOpt = new ChromeOptions();
                    ChrOpt.AddArguments("test-type");
                    capabilities = DesiredCapabilities.Chrome();
                    capabilities.SetCapability(ChromeOptions.Capability, ChrOpt);
                    capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Windows));
                    break;
                case "internet explorer":
                    capabilities = DesiredCapabilities.InternetExplorer();
                    capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Windows));
                    break;
                default:
                    FirefoxProfile ffProfile = new FirefoxProfile();
                    capabilities = DesiredCapabilities.Firefox();
                    capabilities.SetCapability(FirefoxDriver.ProfileCapabilityName, ffProfile);
                    capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.Windows));
                    break;
            }

            capabilities.SetCapability(CapabilityType.BrowserName, browserName);
            capabilities.SetCapability(CapabilityType.Version, browserVersion);
            return new RemoteWebDriver(driverHub, capabilities);
        }

        public static string ScreenshotPath
        {
            get { return screenshotPath; }
            set
            {
                if (value.EndsWith(Path.DirectorySeparatorChar.ToString()))
                {
                    screenshotPath = value;
                }
                else
                {
                    screenshotPath = value + Path.DirectorySeparatorChar;
                }
            }
                
        }

        public static string ErrorScreenshotPath
        {
            get { return errorScreenshotPath; }
            set
            {
                if (value.EndsWith(Path.DirectorySeparatorChar.ToString()))
                {
                    errorScreenshotPath = value;
                }
                else
                {
                    errorScreenshotPath = value + Path.DirectorySeparatorChar;
                }
            }
        }

        public static void TakeScreenshot(IWebDriver driver, string fileName)
        {
            Screenshot scrFile = ((ITakesScreenshot)driver).GetScreenshot();
            scrFile.SaveAsFile(ScreenshotPath + fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        public static void CaptureError(IWebDriver driver, string fileName)
        {
            Screenshot scrFile = ((ITakesScreenshot)driver).GetScreenshot();
            scrFile.SaveAsFile(ErrorScreenshotPath + fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        public static Object executeJSCommand(IWebDriver driver, String jsCMD)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            Object obj = jse.ExecuteScript(jsCMD);
            return obj;
        }

        public static void Close(IWebDriver driver)
        {
            driver.Close();
        }
    }
}
