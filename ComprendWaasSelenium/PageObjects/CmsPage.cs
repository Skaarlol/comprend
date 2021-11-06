using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Linq;
using System.Threading;

namespace ComprendWaasSelenium.PageObjects
{
    public class CmsPage
    {
        private readonly IWebDriver _driver;
        private readonly By _blockContentElement = By.Id("mce_0");
        private readonly By _publishElement = By.ClassName("primary");
        private readonly By _searchHitElement = By.ClassName("searchhit");
        private readonly By _lastNameElement = By.XPath("//*[@data-id='org']");
        private readonly By _urlSlugElement = By.XPath("//input[@data-id='_slug']");
        private readonly By _pageIssuesElement = By.ClassName("fa-exclamation-circle");
        private readonly By _settingsElement = By.XPath("//*[@id = 'settings']/button");
        private readonly By _saveElement = By.XPath("//button[@class ='primary']/span");
        private readonly By _mewPageListElement = By.XPath("//*[@class='blocklinks']/a");
        private readonly By _expandAboutSeleniumElement = By.XPath("//li[@data-id='152']/i");
        private readonly By _expandTrashSeleniumElementt = By.XPath("//li[@data-id='153']/i");
        private readonly By _newPageNameInputElement = By.XPath("//input[@data-id='_name']");
        private readonly By _searchTreeElement = By.XPath("//div[@class = 'searchtree']/input");
        private readonly By _styleListAllDataElement = By.XPath("//*[@alldata='[object Object]']");
        private readonly By _seleniumTestPageHeadingElement = By.XPath("//*[@id='_154-1']/div/h1");
        private readonly By _PageHeadingElement = By.XPath("//h1[contains(text(), 'Heading')]");
        private readonly By _menuActionAboutSeleniumElement = By.XPath("//li[@data-id='152']/a/i[2]");
        private readonly By _menuActionTrashSeleniumElement = By.XPath("//li[@data-id='153']/a/i[2]");
        private readonly By _editBlockElement = By.XPath("//a[@class = 'action-button edit-button']");
        private readonly By _addBlockElement = By.XPath("//*[@id='_162 - 1']/a[2]");
        private readonly By _menuActionOptionsElement = By.XPath("//*[@class='contextmenu popup']/a");
        private readonly By _editOkElement = By.XPath("//*[@id='editdialog']//button[@class='primary']");
        private readonly By _newPageSave = By.XPath("//*[@id='newPagePopup']//button[@class='primary']");
        private readonly By _previewElement = By.XPath("//span[contains(text(), 'Preview')]//ancestor::a");
        private readonly By _trashSeleniumPageElement = By.XPath("//span[contains(text(), 'TrashSelenium')]");
        private readonly By _blockStylesElement = By.XPath("//*[@class='textfield checkboxlist autoheight']/label");
        private readonly By _brokenLinksOrImagesElement = By.XPath("//*[@id='edit-tools']/div[2]/div/h2[1]/span/span");
        private readonly By _discardChangesElement = By.XPath("//span[contains(text(), 'Discard changes')]//parent::button");
        private readonly By _newStandardPageSeleniumElement = By.XPath("//span[contains(text(), 'NewStandardPageSelenium')]");
        private readonly By _menuActionNewStandardPageElement = By.XPath("//span[contains(text(), 'NewStandardPageSelenium')]//following-sibling::i");


        internal void TryNavigateTo(string pageUrl)
        {
            _driver.Navigate().GoToUrl(pageUrl);
        }
        public CmsPage ClickDiscardChanges()
        {
            _driver.SwitchTo().ParentFrame();
            //Had to add another click and a sleep to manage to click the button.
            _driver.FindElement(_discardChangesElement).Click();
            Thread.Sleep(1000);
            _driver.FindElement(_discardChangesElement).Click();
            return this;
        }

        public CmsPage ClickStyle(string style)
        {
            _driver.SwitchTo().ParentFrame();
            foreach (var element in _driver.FindElements(_blockStylesElement))
            {
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

        public CmsPage ClickSearchedPage()
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

        public CmsPage SearchForPage(string page)
        {
            _driver.FindElement(_searchTreeElement).SendKeys(page);
            return this;
        }

        public CmsPage ClickEditBlock()
        {
            var actions = new Actions(_driver);
            actions.MoveToElement(_driver.FindElement(_seleniumTestPageHeadingElement)).Perform();
            _driver.FindElement(_editBlockElement).Click();
            return this;
        }

        public CmsPage ClickAddBlock()
        {
            var actions = new Actions(_driver);
            actions.MoveToElement(_driver.FindElement(_PageHeadingElement)).Perform();
            _driver.FindElement(_addBlockElement).Click();
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

        public CmsPage ClickSettings()
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

        public CmsPage ClickTrashSelenium()
        {
            _driver.FindElement(_trashSeleniumPageElement).Click();
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

        public CmsPage ClickPageIssues()
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

        public CmsPage ClickMenuActionAboutSelenium(string action)
        {
            _driver.FindElement(_menuActionAboutSeleniumElement).Click();
            foreach (var element in _driver.FindElements(_menuActionOptionsElement))
            {
                if (element.Text == action)
                {
                    element.Click();
                    break;
                }

            }
            return this;
        }

        public CmsPage ClickMenuActionTrashSelenium(string action)
        {
            _driver.FindElement(_menuActionTrashSeleniumElement).Click();
            foreach (var element in _driver.FindElements(_menuActionOptionsElement))
            {
                if (element.Text == action)
                {
                    element.Click();
                    break;
                }

            }
            return this;
        }

        public CmsPage ClickMenuActionNewStandardPageSelenium(string action)
        {
            _driver.FindElement(_menuActionNewStandardPageElement).Click();
            foreach (var element in _driver.FindElements(_menuActionOptionsElement))
            {
                if (element.Text == action)
                {
                    element.Click();
                    break;
                }

            }
            return this;
        }

        public CmsPage ClickNewPageGroup(string newPage)
        {
            foreach (var element in _driver.FindElements(_mewPageListElement))
            {
                if (element.Text == newPage)
                {
                    element.Click();
                    break;
                }

            }
            return this;
        }

        public CmsPage EnterNewStandardPageName(string pageName)
        {
            _driver.FindElement(_newPageNameInputElement).SendKeys(pageName);
            _driver.FindElement(_newPageSave).Click();
            return this;
        }

        public CmsPage ClickExpandAboutSelenium()
        {
            _driver.FindElement(_expandAboutSeleniumElement).Click();
            return this;
        }

        public CmsPage ClickExpandTrashSelenium()
        {
            _driver.FindElement(_expandTrashSeleniumElementt).Click();
            return this;
        }

        public CmsPage ClickPageToDelete()
        {
            _driver.FindElement(_newStandardPageSeleniumElement).Click();
            return this;
        }

        public CmsPage ClickOkToDelete()
        {
            _driver.SwitchTo().Alert().Accept();
            Thread.Sleep(1000);
            return this;
        }
    }
}