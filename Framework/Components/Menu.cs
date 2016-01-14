using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.PageObjects;
using OpenQA.Selenium;

namespace Framework.Components
{
    public class Menu:Page
    {
        private readonly By mnuArticles = By.LinkText("ARTICLES");
        private readonly By mnuVideos = By.Id("menu-item-197");
        private readonly By mnuCodeRepository = By.Id("menu-item-179");
        private readonly By mnuGitLab = By.LinkText("GITLAB (ACTIVE)");

        public Menu(IWebDriver driver) : base(driver) { }

        public Page ClickArticles()
        {
            IWebElement articles = this.getWhenClickable(mnuArticles);
            articles.Click();
            return new Page(driver);
        }

        public Page clickVideos()
        {
            this.clickWhenReady(mnuVideos);
            return new Page(driver);
        }

        public Page clickGitLab()
        {
            this.HoverOver(mnuCodeRepository);
            this.clickWhenReady(mnuGitLab);
            return new Page(driver);
        }
    }
}
