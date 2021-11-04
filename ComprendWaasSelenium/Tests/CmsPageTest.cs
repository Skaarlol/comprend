using ComprendWaasSelenium.BaseClass;
using ComprendWaasSelenium.PageObjects;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Globalization;

namespace ComprendWaasSelenium.Tests
{
    [Category("Cms")]
    class CmsPageTest : BaseTest
    {
        private CmsPage _pageObject;
        readonly Random _random = new();

        [SetUp]
        public new void Setup()
        {
            _pageObject = new CmsPage(Driver);
            _pageObject.TryNavigateTo("https://iridium-cms.test.waas.site/");
            _pageObject.LoginCredentials();
        }

        
        [Test]
        public void UpdateHeading()
        {
            var date = DateTime.Now.ToString(CultureInfo.CurrentCulture);

            _pageObject
                .SearchForPage("SeleniumTestPage")
                .ClickSearchedPage()
                .SwitchToEditFrame()
                .UpdateHeading(date)
                .PublishPage()
                .ClickPreview()
                .ReadHeading()
                .Should()
                .ContainEquivalentOf(date);
        }

        [Test]
        public void DiscardChanges()
        {
            var date = DateTime.Now.ToString(CultureInfo.CurrentCulture);

            _pageObject
                .SearchForPage("SeleniumTestPage")
                .ClickSearchedPage()
                .SwitchToEditFrame()
                .UpdateHeading(date)
                .ClickDiscardChanges()
                .SwitchToEditFrame()
                .ReadHeading()
                .Should()
                .NotBeEquivalentTo(date);
        }

        [Test]
        public void BrokenLinksOrImagesShouldShowZero()
        {
            _pageObject
                .SearchForPage("SeleniumTestPage")
                .ClickSearchedPage()
                .ClickPageIssues()
                .ReadBrokenLinksOrImagesIssuesCounter()
                .Should()
                .Be(0);
        }

        //not done
        [Ignore("")]
        [Test]
        public void ChangeLastName()
        {
            DateTime dt = DateTime.Today;
            var day = dt.DayOfWeek.ToString();

            _pageObject
                .ClickAdmin()
                .ClickUsers()
                .ChooseUser("Silas")
                .ChangeLastName(day)
                .ClickSave()
                .ReadLastName()
                .Should()
                .ContainEquivalentOf(day);
        }

        [Test]
        public void BlockStylesDark()
        {
            _pageObject
                .SearchForPage("AddBlockPage")
                .ClickSearchedPage()
                .SwitchToEditFrame()
                .ClickEditBlock()
                .ClickStyle("Dark")
                .ClickOkEditBlock()
                .CheckBlockBackground()
                .Should()
                .ContainEquivalentOf("rgba(63, 42, 72, 1)");
        }

        [Test]
        public void BlockStylesGray()
        {
            _pageObject
                .SearchForPage("AddBlockPage")
                .ClickSearchedPage()
                .SwitchToEditFrame()
                .ClickEditBlock()
                .ClickStyle("Gray")
                .ClickOkEditBlock()
                .CheckBlockBackground()
                .Should()
                .ContainEquivalentOf("rgba(231, 231, 231, 1)");
        }

        [Test]
        public void UpdateUrl()
        {
            var randomUrl = _random.Next(0, 1000).ToString();
            const string page = "SeleniumTestPage";

            _pageObject
                .SearchForPage(page)
                .ClickSearchedPage()
                .ClickSettings()
                .UpdateUrl(randomUrl)
                .PublishPage()
                .RefreshAndGoToPage(page)
                .ClickPreview()
                .ReadUrl()
                .Should()
                .ContainEquivalentOf(randomUrl);
        }

        [Test, Order(1)]
        public void CreateNewStandardPage()
        {
            var randomUrl = _random.Next(0, 1000).ToString();
            const string page = "SeleniumTestPage";

            _pageObject
                .ClickAboutSelenium()
                .ClickMenuActionAboutSelenium("New")
                .ClickNewPageGroup("STANDARD PAGE")
                .EnterNewStandardPageName("CreateNewStandardPage")
                .RefreshAndGoToPage(page)
                .ClickSettings()
                .ReadPageName()
                .Should()
                .ContainEquivalentOf("CreateNewStandardPage");
        }

        [Test, Order(2)]
        public void DeleteStandardPage()
        {
            var randomUrl = _random.Next(0, 1000).ToString();
            const string page = "SeleniumTestPage";

            _pageObject
                .ClickAboutSelenium()
                .ClickMenuActionAboutSelenium("Delete")
                .UpdateUrl(randomUrl)
                .PublishPage()
                .RefreshAndGoToPage(page)
                .ClickPreview()
                .ReadUrl()
                .Should()
                .ContainEquivalentOf(randomUrl);
        }

    }
}