using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ComprendWaasSelenium.PageObjects
{
    public class CmsPage
    {
        private readonly IWebDriver _driver;
        private readonly By _searchTreeElement = By.XPath("//div[@class = 'searchtree']/input");
        private readonly By _searchHitElement = By.ClassName("searchhit");
        private readonly By _seleniumTestPageHeadingElement = By.XPath("//*[@id='_128-1']/div/h1");
        private readonly By _addBlockPageHeadingElement = By.XPath("//*[@id='_130-1']/div/h1");
        private readonly By _publishElement = By.ClassName("primary");
        private readonly By _previewElement = By.XPath("//span[contains(text(), 'Preview')]//ancestor::a");
        private readonly By _settingsElement = By.XPath("//*[@id = 'settings']/button");
        private readonly By _brokenLinksOrImagesElement = By.XPath("//*[@id='edit-tools']/div[2]/div/h2[1]/span/span");
        private readonly By _pageIssuesElement = By.ClassName("fa-exclamation-circle");
        private readonly By _adminElement = By.XPath("//div[@id = 'mainnav']/a[2]");
        private readonly By _usersElement = By.XPath("//*[@data-id='/users']");
        private readonly By _lastNameElement = By.XPath("//*[@data-id='org']");
        private readonly By _saveElement = By.XPath("//button[@class ='primary']/span");
        private readonly By _editBlockElement = By.XPath("//a[@class = 'action-button edit-button']");
        private readonly By _addActionButtonElement = By.ClassName("add-button");
        private readonly By _urlSlugElement = By.XPath("//input[@data-id='_slug']");
        private readonly By _blockStylesElement = By.XPath("//*[@class='textfield checkboxlist autoheight']/label");
        private readonly By _editOkElement = By.XPath("//*[@id='editdialog']//button[@class='primary']");
        private readonly By _styleListAllDataElement = By.XPath("//*[@alldata='[object Object]']");
        private readonly By _blockContentElement = By.Id("mce_0");

        internal void TryNavigateTo(string pageUrl)
        {
            _driver.Navigate().GoToUrl(pageUrl);
        }

        public CmsPage ClickStyle(string style)
        {
            _driver.SwitchTo().ParentFrame();
            IList<string> allText = new List<string>();
            foreach (var element in _driver.FindElements(_blockStylesElement))
            {
                Thread.Sleep(1000);
                allText.Add(element.Text);
                var styleAllData = _driver.FindElement(_styleListAllDataElement).GetAttribute("value");
                styleAllData = styleAllData.Replace("_", " ");
                if (styleAllData.Contains(element.Text))
                {
                    element.Click();
                }
                if (element.Text == style)
                {
                    element.Click();
                }

            }
            return this;
        }

        public CmsPage ClickOnSearchedPage()
        {
            _driver.FindElement(_searchHitElement).Click();
            return this;
        }

        public CmsPage SwitchToEditFrame()
        {
            _driver.SwitchTo().Frame("editframe");
            return this;
        }

        public string ReadHeading()
        {
            return _driver.FindElement(_seleniumTestPageHeadingElement).Text;
        }
        public int ReadBrokenLinksOrImagesIssuesCounter()
        {
            Thread.Sleep(1000);
            var issueCounter = _driver.FindElement(_brokenLinksOrImagesElement).Text;
            var counter = int.Parse(issueCounter);
            return counter;
        }

        public string ReadLastName()
        {
            return _driver.FindElement(_lastNameElement).Text;
        }

        public CmsPage SearchForPage(string page)
        {
            _driver.FindElement(_searchTreeElement).SendKeys(page);
            return this;
        }

        public CmsPage ClickSave()
        {
            _driver.FindElement(_saveElement).Click();
            return this;
        }

        public CmsPage ClickEditBlock()
        {

            var actions = new Actions(_driver);
            var target = _driver.FindElement(_addBlockPageHeadingElement);
            actions.MoveToElement(target).Perform();
            _driver.FindElement(_editBlockElement).Click();
            return this;
        }

        public CmsPage ClickAddAction()
        {
            _driver.FindElement(_addActionButtonElement).Click();
            return this;
        }
        public CmsPage ClickOkEditBlock()
        {
            _driver.FindElement(_editOkElement).Click();
            return this;
        }

        public CmsPage RefreshAndGoToPage(string page)
        {
            _driver.Navigate().Refresh();
            _driver.FindElement(_searchTreeElement).SendKeys(page);
            _driver.FindElement(_searchHitElement).Click();
            return this;
        }

        public CmsPage ChangeLastName(string name)
        {
            _driver.FindElement(_lastNameElement).Clear();
            _driver.FindElement(_lastNameElement).SendKeys(name);
            return this;
        }

        public string ReadUrl()
        {
            return _driver.Url;
        }

        public CmsPage UpdateUrl(string url)
        {
            _driver.FindElement(_urlSlugElement).Clear();
            _driver.FindElement(_urlSlugElement).SendKeys(url);
            _driver.FindElement(_saveElement).Click();
            return this;
        }

        public CmsPage ClickOnSettings()
        {
            Thread.Sleep(1000);
            _driver.FindElement(_settingsElement).Click();
            return this;
        }

        public CmsPage ChooseUser(string user)
        {
            _driver.FindElement(By.XPath("//*[@data-id='/users']/ul/li//span[contains(text(), '" + user + "')]"));
            return this;
        }

        public CmsPage ClickUsers()
        {
            _driver.FindElement(_usersElement).Click();
            return this;
        }

        public CmsPage ClickAdmin()
        {
            _driver.FindElement(_adminElement).Click();
            return this;
        }

        public CmsPage ClickPreview()
        {
            Thread.Sleep(500);
            _driver.SwitchTo().ParentFrame();
            _driver.FindElement(_previewElement).Click();
            _driver.SwitchTo().Window(_driver.WindowHandles.Last());
            return this;
        }

        public CmsPage ClickOnPageIssues()
        {
            _driver.FindElement(_pageIssuesElement).Click();
            return this;
        }

        public string CheckBlockBackground()
        {
            _driver.SwitchTo().Frame("editframe");
            var element = _driver.FindElement(_blockContentElement).GetCssValue("background-color");
            return element;
        }

        public CmsPage PublishPage()
        {
            Thread.Sleep(500);
            _driver.SwitchTo().ParentFrame();
            _driver.FindElement(_publishElement).Click();
            return this;
        }

        public CmsPage UpdateHeading(string header)
        {
            _driver.FindElement(_seleniumTestPageHeadingElement).SendKeys(Keys.LeftControl + "A");
            _driver.FindElement(_seleniumTestPageHeadingElement).SendKeys(header);
            return this;
        }

        public CmsPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public CmsPage LoginCredentials()
        {
            _driver.FindElement(By.Id("username")).SendKeys("services@waas.site");
            _driver.FindElement(By.Id("password")).SendKeys("0Wj1I8EnE0m0UW9qwP7XsI7rH!");
            _driver.FindElement(By.XPath("//button[@type = 'submit']")).Click();
            return this;
        }

    }
}