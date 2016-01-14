using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Framework.Components;

namespace Framework.PageObjects
{
    public class HomePage : Page
    {
        public HomePage(IWebDriver driver) : base(driver) { }

        public Menu getMenu()
        {
            return new Menu(driver);
        }
    }
}
