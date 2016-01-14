using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.PageObjects;
using OpenQA.Selenium;

namespace Framework.Components
{
    class Menu:Page
    {
        private readonly By mnuArticles = By.LinkText("Articles");
        private readonly By mnuVideos = By.Id("menu-item-197");
        private readonly By mnuCodeRepository = By.Id("menu-item-179");
        private readonly By mnuGitLab = By.LinkText("GitLab (Active)");

        public Menu(IWebDriver driver) : base(driver) { }

        public void ClickArticles()
        {
            IWebElement articles = this.getWhenClickable(mnuArticles);
            articles.Click();
        }

        public void clickVideos()
        {
            this.clickWhenReady(mnuVideos);
        }

        public void clickGitLab()
        {
            this.HoverOver(mnuCodeRepository);
            this.clickWhenReady(mnuGitLab);
        }

    }
}
