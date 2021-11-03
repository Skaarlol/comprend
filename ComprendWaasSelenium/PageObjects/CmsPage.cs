using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.VisualBasic;

namespace ComprendWaasSelenium.PageObjects
{
    public class CmsPage
    {
        private readonly IWebDriver Driver;
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
            Driver.Navigate().GoToUrl(pageUrl);
        }

        public CmsPage ClickStyle(string style)
        {
            Driver.SwitchTo().ParentFrame();
            IList<string> allText = new List<string>();
            foreach (var element in Driver.FindElements(_blockStylesElement))
            {
                Thread.Sleep(1000);
                allText.Add(element.Text);
                var styleAllData = Driver.FindElement(_styleListAllDataElement).GetAttribute("value");
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
            Driver.FindElement(_searchHitElement).Click();
            return this;
        }

        public CmsPage SwitchToEditFrame()
        {
            Driver.SwitchTo().Frame("editframe");
            return this;
        }

        public string ReadHeading()
        {
            return Driver.FindElement(_seleniumTestPageHeadingElement).Text;
        }
        public int ReadBrokenLinksOrImagesIssuesCounter()
        {
            Thread.Sleep(1000);
            var issueCounter = Driver.FindElement(_brokenLinksOrImagesElement).Text;
            var counter = int.Parse(issueCounter);
            return counter;
        }

        public string ReadLastName()
        {
            return Driver.FindElement(_lastNameElement).Text;
        }

        public CmsPage SearchForPage(string page)
        {
            Driver.FindElement(_searchTreeElement).SendKeys(page);
            return this;
        }

        public CmsPage ClickSave()
        {
            Driver.FindElement(_saveElement).Click();
            return this;
        }

        public CmsPage ClickEditBlock()
        {

            var actions = new Actions(Driver);
            var target = Driver.FindElement(_addBlockPageHeadingElement);
            actions.MoveToElement(target).Perform();
            Driver.FindElement(_editBlockElement).Click();
            return this;
        }

        public CmsPage ClickaddAction()
        {
            Driver.FindElement(_addActionButtonElement).Click();
            return this;
        }
        public CmsPage ClickOkEditBlock()
        {
            Driver.FindElement(_editOkElement).Click();
            return this;
        }

        public CmsPage RefreshandGoToPage(string page)
        {
            Driver.Navigate().Refresh();
            Driver.FindElement(_searchTreeElement).SendKeys(page);
            Driver.FindElement(_searchHitElement).Click();
            return this;
        }

        public CmsPage ChangeLastName(string name)
        {
            Driver.FindElement(_lastNameElement).Clear();
            Driver.FindElement(_lastNameElement).SendKeys(name);
            return this;
        }

        public string ReadUrl()
        {
            return Driver.Url;
        }

        public CmsPage UpdateUrl(string url)
        {
            Driver.FindElement(_urlSlugElement).Clear();
            Driver.FindElement(_urlSlugElement).SendKeys(url);
            Driver.FindElement(_saveElement).Click();
            return this;
        }

        public CmsPage ClickOnSettings()
        {
            Thread.Sleep(1000);
            Driver.FindElement(_settingsElement).Click();
            return this;
        }

        public CmsPage ChooseUser(string user)
        {
            Driver.FindElement(By.XPath("//*[@data-id='/users']/ul/li//span[contains(text(), '" + user + "')]"));
            return this;
        }

        public CmsPage ClickUsers()
        {
            Driver.FindElement(_usersElement).Click();
            return this;
        }

        public CmsPage ClickAdmin()
        {
            Driver.FindElement(_adminElement).Click();
            return this;
        }

        public CmsPage ClickPreview()
        {
            Thread.Sleep(500);
            Driver.SwitchTo().ParentFrame();
            Driver.FindElement(_previewElement).Click();
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
            return this;
        }

        public CmsPage ClickOnPageIssues()
        {
            Driver.FindElement(_pageIssuesElement).Click();
            return this;
        }

        public string CheckBlockBackground()
        {
            Driver.SwitchTo().Frame("editframe");
            var element = Driver.FindElement(_blockContentElement).GetCssValue("background-color");
            return element;
        }

        public CmsPage PublishPage()
        {
            Thread.Sleep(500);
            Driver.SwitchTo().ParentFrame();
            Driver.FindElement(_publishElement).Click();
            return this;
        }

        public CmsPage UpdateHeading(string header)
        {
            Driver.FindElement(_seleniumTestPageHeadingElement).SendKeys(Keys.LeftControl + "A");
            Driver.FindElement(_seleniumTestPageHeadingElement).SendKeys(header);
            return this;
        }

        public CmsPage(IWebDriver driver)
        {
            Driver = driver;
        }
        public CmsPage loginCredentials()
        {
            Driver.FindElement(By.Id("username")).SendKeys("services@waas.site");
            Driver.FindElement(By.Id("password")).SendKeys("0Wj1I8EnE0m0UW9qwP7XsI7rH!");
            Driver.FindElement(By.XPath("//button[@type = 'submit']")).Click();
            return this;
        }

    }
}