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
                .SearchForPage("SeleniumTestStandardPage")
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
                .SearchForPage("SeleniumTestStandardPage")
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
                .SearchForPage("SeleniumTestStandardPage")
                .ClickSearchedPage()
                .ClickPageIssues()
                .ReadBrokenLinksOrImagesIssuesCounter()
                .Should()
                .Be(0);
        }

        [Test]
        public void BlockStylesDark()
        {
            _pageObject
                .SearchForPage("SeleniumTestStandardPage")
                .ClickSearchedPage()
                .SwitchToEditFrame()
                .ClickEditBlock()
                .ClickStyle("Dark")
                .ClickOkEditBlock()
                .CheckBlockBackground()
                .Should()
                .Be("rgba(63, 42, 72, 1)");
        }

        [Test]
        public void BlockStylesGray()
        {
            _pageObject
                .SearchForPage("SeleniumTestStandardPage")
                .ClickSearchedPage()
                .SwitchToEditFrame()
                .ClickEditBlock()
                .ClickStyle("Gray")
                .ClickOkEditBlock()
                .CheckBlockBackground()
                .Should()
                .Be("rgba(231, 231, 231, 1)");
        }

        [Test]
        public void UpdateUrl()
        {
            var randomUrl = _random.Next(0, 1000).ToString();

            _pageObject
                .SearchForPage("SeleniumTestStandardPage")
                .ClickSearchedPage()
                .ClickSettings()
                .UpdateUrl(randomUrl)
                .PublishPage()
                .RefreshAndGoToPage("SeleniumTestStandardPage")
                .ClickPreview()
                .ReadUrl()
                .Should()
                .ContainEquivalentOf(randomUrl);
        }

        [Test, Order(1)]
        public void CreateNewStandardPage()
        {
            _pageObject
                .ClickExpandAboutSelenium()
                .ClickTrashSelenium()
                .ClickMenuActionTrashSelenium("New")
                .ClickNewPageGroup("STANDARD PAGE")
                .EnterNewStandardPageName("NewStandardPageSelenium")
                .ClickSettings()
                .ReadUrl()
                .Should()
                .ContainEquivalentOf("NewStandardPageSelenium");
        }

        [Test, Order(2)]
        public void DeleteStandardPage()
        {
            _pageObject
                .ClickExpandAboutSelenium()
                .ClickExpandTrashSelenium()
                .ClickPageToDelete()
                .ClickMenuActionNewStandardPageSelenium("Delete")
                .ClickOkToDelete()
                .ReadUrl()
                .Should()
                .Be("https://iridium-cms.test.waas.site/");
        }

        [Test]
        public void AddNewBlock()
        {
            _pageObject
                .SearchForPage("AddBlockStandardPage")
                .ClickSearchedPage()
                .SwitchToEditFrame()
                .ClickAddBlock()
                .ClickStyle("Dark")
                .ClickOkEditBlock()
                .CheckBlockBackground()
                .Should()
                .Be("rgba(63, 42, 72, 1)");
        }

    }
}