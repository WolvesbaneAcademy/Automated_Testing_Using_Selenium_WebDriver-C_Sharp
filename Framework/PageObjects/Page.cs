using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using Framework;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO;

namespace Framework.PageObjects
{
    public class Page
    {
        protected IWebDriver driver;
        protected string pageHandle;
        protected string pageTitle;
        protected int timeOut = 10;

        public Page() { }

        /// <summary>
        /// Simple constructor for a page
        /// </summary>
        /// <param name="driver"></param>
        public Page(IWebDriver driver)
        {
            this.driver = driver;
            this.pageHandle = driver.CurrentWindowHandle;
            this.pageTitle = driver.Title;
        }

        public Page(IWebDriver driver, string pgTitle)
        {
            this.driver = driver;
            this.pageHandle = driver.CurrentWindowHandle;
            if (isPageTitle(pgTitle))
            {
                this.pageTitle = pgTitle;
            }
            else {
                throw new InvalidElementStateException("Incorrect page loaded: found " + driver.Title + " expected " + pgTitle);
            }
        }

        public void Initialize(IWebDriver driver)
        {
            this.driver = driver;
            this.pageHandle = driver.CurrentWindowHandle;
        }

        public int Timeout
        {
            get { return timeOut; }
            set { timeOut = value; }
        }

        public string PageTitle
        {
            get { return pageTitle; }
        }

        public ReadOnlyCollection<IWebElement> getAllWhenPresent(By locator)
        {
            ReadOnlyCollection<IWebElement> foundElements = null;

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Timeout));
                foundElements = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
            }
            catch (TimeoutException)
            {
                Browser.CaptureError(driver, "timeout" + new DateTime().ToString("yyyyMMddhhmm") + ".jpg");
                throw;
            }
            return foundElements;

        }

        public ReadOnlyCollection<IWebElement> getAllWhenVisible(By locator)
        {
            ReadOnlyCollection<IWebElement> foundElements = null;

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Timeout));
                foundElements = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));
            }
            catch (TimeoutException)
            {
                Browser.CaptureError(driver, "timeout" + new DateTime().ToString("yyyyMMddhhmm") + ".jpg");
                throw;
            }
            return foundElements;

        }

        public IWebElement getWhenPresent(By locator)
        {
            IWebElement foundElement = null;
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Timeout));
                foundElement = wait.Until(ExpectedConditions.ElementExists(locator));
            }
            catch (TimeoutException)
            {
                Browser.CaptureError(driver, "timeout" + new DateTime().ToString("yyyyMMddhhmm") + ".jpg");
                throw;
            }
            return foundElement;
        }

        public IWebElement getWhenVisible(By locator)
        {
            IWebElement foundElement = null;
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Timeout));
                foundElement = wait.Until(ExpectedConditions.ElementIsVisible(locator));
            }
            catch (TimeoutException)
            {
                Browser.CaptureError(driver, "timeout" + new DateTime().ToString("yyyyMMddhhmm") + ".jpg");
                throw;
            }
            return foundElement;
        }

        public IWebElement getWhenClickable(By locator)
        {
            IWebElement foundElement = null;
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Timeout));
                foundElement = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            }
            catch (TimeoutException)
            {
                Browser.CaptureError(driver, "timeout" + new DateTime().ToString("yyyyMMddhhmm") + ".jpg");
                throw;
            }
            return foundElement;

        }

        /// <summary>
        /// Checks for specified text within the web element specified by locator and returns true if it exists
        /// </summary>
        /// <param name="locator"></param>
        /// <param name="value"></param>
        public bool isTextPresent(By locator, string value)
        {
            bool result = false;

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Timeout));
                result = wait.Until(ExpectedConditions.TextToBePresentInElementLocated(locator, value));
            }
            catch (TimeoutException)
            {
                Browser.CaptureError(driver, "timeout" + new DateTime().ToString("yyyyMMddhhmm") + ".jpg");
                result = false;
            }
            return result;
        }

        public bool isTextPresentInValue(By locator, string value)
        {
            bool result = false;

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Timeout));
                result = wait.Until(ExpectedConditions.TextToBePresentInElementValue(locator, value));
            }
            catch (TimeoutException)
            {
                Browser.CaptureError(driver, "timeout" + new DateTime().ToString("yyyyMMddhhmm") + ".jpg");
                throw;
            }
            return result;
        }

        public bool isElementVisible(By locator)
        {
            bool result = false;

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Timeout));
                if (wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator)))
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }
            catch (TimeoutException)
            {
                Browser.CaptureError(driver, "timeout" + new DateTime().ToString("yyyyMMddhhmm") + ".jpg");
                throw;
            }
            return result;
        }

        public bool isElementAbsent(By locator)
        {
            bool result = false;

            try
            {
                IList<IWebElement> foundElements = driver.FindElements(locator);
                if (foundElements.Count > 0)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
            }
            catch (TimeoutException)
            {
                Browser.CaptureError(driver, "timeout" + new DateTime().ToString("yyyyMMddhhmm") + ".jpg");
                throw;
            }
            return result;
        }

        public bool isElementPresent(By locator)
        {
            bool result = false;

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Timeout));
                ReadOnlyCollection<IWebElement> foundElements = wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
                if (foundElements.Count > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }

            }
            catch (TimeoutException)
            {
                Browser.CaptureError(driver, "timeout" + new DateTime().ToString("yyyyMMddhhmm") + ".jpg");
                throw;
            }
            return result;
        }

        public bool isElementEnabled(By locator)
        {
            bool result = false;

            IWebElement item = getWhenPresent(locator);
            if (null != item)
            {
                result = item.Enabled;
            }
            return result;
        }

        public bool isElementStale(IWebElement item)
        {
            bool result = false;

            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Timeout));
                result = wait.Until(ExpectedConditions.StalenessOf(item));
            }
            catch (TimeoutException)
            {
                Browser.CaptureError(driver, "timeout" + new DateTime().ToString("yyyyMMddhhmm") + ".jpg");
                throw;
            }
            return result;

        }

        public void HoverOver(By locator)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(getWhenVisible(locator)).Build().Perform();
        }

        public void ContextClickOn(By locator)
        {
            Actions action = new Actions(driver);
            action.ContextClick(getWhenClickable(locator)).Build().Perform();
        }

        public void DoubleClickOn(By locator)
        {
            Actions action = new Actions(driver);
            action.DoubleClick(getWhenClickable(locator)).Build().Perform();
        }

        public void clickWhenReady(By locator)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Timeout));
            IWebElement foundElement = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            try
            {
                foundElement.Click();
            }
            catch (StaleElementReferenceException)
            {
                IWebElement retry = wait.Until(ExpectedConditions.ElementToBeClickable(locator));
                retry.Click();
            }

        }

        public bool setElementValue(By locator, Object value)
        {
            bool success = false;

            try
            {
                IWebElement field = this.getWhenPresent(locator);
                if (null != field)
                {
                    switch (field.TagName.ToLower().Trim())
                    {
                        case "input":
                            switch (field.GetAttribute("type").ToLower().Trim())
                            {
                                case "text":
                                case "hidden":
                                case "password":
                                    if (!(field.Text.Equals((string)value, StringComparison.CurrentCultureIgnoreCase) || (field.GetAttribute("value").Equals((string)value, StringComparison.CurrentCultureIgnoreCase))))
                                    {
                                        field.Clear();
                                        field.SendKeys((string)value);
                                        success = true;
                                    }
                                    break;
                                case "checkbox":
                                    if (field.Selected != (bool)value)
                                    {
                                        field.Click();
                                    }
                                    success = true;
                                    break;
                                case "radio":
                                    field.Click();
                                    success = true;
                                    break;
                                case "button":
                                case "submit":
                                case "reset":
                                case "image":
                                    field.Click();
                                    success = true;
                                    break;
                                case "file":
                                    // TODO: add processing code for the file input type
                                    break;
                            }
                            break;
                        case "textarea":
                            field.Clear();
                            field.SendKeys((string)value);
                            success = true;
                            break;
                        case "select":
                            try
                            {
                                SelectElement DDL = new SelectElement(field);
                                if (value.GetType().IsArray)
                                {
                                    // TODO - add multiple selection logic for list boxes
                                    if (DDL.IsMultiple)
                                    {
                                        success = true;
                                    }
                                    else
                                    {
                                        success = false;
                                    }
                                }
                                else
                                {
                                    /* if (System.GetProperty("browserName") == "internet explorer")
                                    {
                                        System.out.println("Using alternate method.");
                                        IWebElement option = field.findElement(By.xpath("./option[text() == " + value));
                                        option.click();
                                    }
                                    else
                                    { */
                                    DDL.SelectByText((string)value);
                                    success = true;
                                    //}
                                }
                            }
                            catch (NoSuchElementException)
                            {
                                string holder = (string)value;
                                if (holder.Equals(""))
                                {
                                    success = true;
                                }
                                else
                                {
                                    success = false;
                                }
                            }
                            break;
                        default:
                            success = false;
                            break;
                    }
                }
                return success;
            }
            catch (StaleElementReferenceException)
            {
                return setElementValue(locator, value);
            }
            catch (TimeoutException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string PageSource()
        {
            return driver.PageSource;
        }

        /**
         * Determines if the provided string is present in the title of the current page. Allows for specifying the amount
         * of time to wait in seconds. If an error occurs and errCapture is true, a screenshot is saved to the Errors
         * folder.
         * 
         * @param pageTitle
         * @param errCapture
         * @param timeOut
         * @return boolean
         */
        public bool isPageTitle(String pageTitle)
        {
            bool matchFound = false;
            try
            {
                matchFound = (new WebDriverWait(driver, TimeSpan.FromSeconds(Timeout))).Until(ExpectedConditions.TitleContains(pageTitle));
            }
            catch (TimeoutException toe)
            {
                    try
                    {
                        Browser.CaptureError(driver, "timeOut" + new DateTime().ToString("yyyyMMddhhmm") + ".jpg");
                    }
                    catch (IOException e)
                    {
                       Console.WriteLine(e.StackTrace);
                    }
                    throw toe;
            }

            return matchFound;
        }

        /// <summary>
        /// Checks for an open Alert box 
        /// </summary>
        public bool isAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns an Alert object if one is present
        /// </summary>
        public IAlert getAlert()
        {
            try
            {
                return driver.SwitchTo().Alert();
            }
            catch (NoAlertPresentException)
            {
                return null;
            }
        }

        /// <summary>
        /// Switches the browser window to the one this page was shown in when it was instantiated.
        /// </summary>
        public void switchToThisPage()
        {
            driver.SwitchTo().Window(pageHandle);
        }

        /// <summary>
        /// Refreshes the current page
        /// </summary>
        public void refreshPage()
        {
            driver.Navigate().Refresh();
        }

    }
}