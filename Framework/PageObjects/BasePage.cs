using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public class BasePage
    {
        protected IWebDriver webDriver;

        /**
         * Object constructor
         */
        public BasePage(IWebDriver driver)
        {
            webDriver = driver;
        }

        /**
         * Checks for the specified text within the web element found using the provided locator Allows for specifying the
         * timeout delay in seconds.
         * 
         * @param locator
         * @param value
         * @param timeout
         * @return boolean
         */
        public bool isTextPresent(By locator, String value, int timeout)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(timeout));
            bool result = false;

            try
            {
                result = wait.Until(ExpectedConditions.TextToBePresentInElementLocated(locator, value));
            }
            catch (TimeoutException toe)
            {
                result = false;
            }

            return result;
        }

    }
}
