using ComprendWaasSelenium.BaseClass;
using ComprendWaasSelenium.PageObjects;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace ComprendWaasSelenium.Tests
{
    [Category("Cms")]
    class CmsPageTest : BaseTest
    {
        private CmsPage _pageObject;
        readonly Random _random = new();

        [SetUp]
        public void Setup()
        {
            _pageObject = new CmsPage(Driver);
            _pageObject.TryNavigateTo("https://iridium-cms.test.waas.site/");
            _pageObject.loginCredentials();
        }

        
        [Test]
        public void UpdateHeading()
        {
            string date = DateTime.Now.ToString();

            _pageObject
                .SearchForPage("SeleniumTestPage")
                .ClickOnSearchedPage()
                .SwitchToEditFrame()
                .UpdateHeading(date)
                .PublishPage()
                .ClickPreview()
                .ReadHeading()
                .Should()
                .ContainEquivalentOf(date);
        }

        [Test]
        public void BrokenLinksOrImagesShouldShowZero()
        {
            _pageObject
                .SearchForPage("SeleniumTestPage")
                .ClickOnSearchedPage()
                .ClickOnPageIssues()
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
                .ClickOnSearchedPage()
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
                .ClickOnSearchedPage()
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
                .ClickOnSearchedPage()
                .ClickOnSettings()
                .UpdateUrl(randomUrl)
                .PublishPage()
                .RefreshandGoToPage(page)
                .ClickPreview()
                .ReadUrl()
                .Should()
                .ContainEquivalentOf(randomUrl);
        }

    }
}