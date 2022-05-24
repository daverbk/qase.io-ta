using Allure.Commons;
using DiplomaProject.Configuration;
using DiplomaProject.Pages;
using DiplomaProject.Services.SeleniumServices;
using FluentAssertions;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;

namespace DiplomaProject.Tests.UI;

[AllureNUnit]
[AllureParentSuite("UI")]
[AllureEpic("Authorization-UI")]
[AllureSeverity(SeverityLevel.blocker)]
[Category("Authorization-UI")]
public class AuthorizationTests
{
    private IWebDriver _webDriver = null!;
    private WelcomingPage _welcomingPage = null!;
    private AuthorizationPage _authorizationPage = null!;
    private ProjectsPage _projectsPage = null!;

    [SetUp]
    public void OpenBrowserAtWelcomingPage()
    {
        DriverFactory.InitBrowser();
        DriverFactory.Driver.Navigate().GoToUrl(Configurator.AppSettings.BaseUiUrl);

        _webDriver = DriverFactory.Driver;
        _welcomingPage = new WelcomingPage(_webDriver);
        _authorizationPage = new AuthorizationPage(_webDriver);
        _projectsPage = new ProjectsPage(_webDriver);
    }

    [Test]
    [Category("Positive")]
    [AllureSuite("Authorization-UI")]
    [AllureStep("Authorize using valid data")]
    [AllureTms("tms", "suite=9&previewMode=modal&case=20")]
    public void PositiveLogIn()
    {
        _welcomingPage
            .ProceedToLoggingIn()
            .PopulateAuthorizationData(Configurator.Admin.Email, Configurator.Admin.Password)
            .SubmitAuthorizationForm();

        _projectsPage.PageOpened.Should().BeTrue();
    }

    [Test]
    [Category("Negative")]
    [AllureSuite("Authorization-UI")]
    [AllureStep("Authorize using invalid data")]
    [AllureTms("tms", "suite=9&previewMode=modal&case=20")]
    public void NegativeLogIn()
    {
        const string invalidEmail = "1111@sma.b";
        const string invalidPassword = "11111";

        _welcomingPage
            .ProceedToLoggingIn()
            .PopulateAuthorizationData(invalidEmail, invalidPassword)
            .SubmitAuthorizationForm();

        _authorizationPage.ErrorMessageDisplayed().Should().BeTrue();
    }

    [Test]
    [Category("Security")]
    [AllureSuite("SQL Injections")]
    [AllureStory("SQL Injections")]
    [AllureStep("Input sql injections into password field")]
    [TestCase("' or \""), TestCase("UNION ALL SELECT USER()--"), TestCase("admin' or 1=1")]
    [AllureTms("tms", "suite=9&previewMode=modal&case=22")]
    public void SqlInjections(string sqlInjections)
    {
        _welcomingPage
            .ProceedToLoggingIn()
            .PopulateAuthorizationData(Configurator.Admin.Email, sqlInjections)
            .SubmitAuthorizationForm();

        _authorizationPage.ErrorMessageDisplayed().Should().BeTrue();
    }

    [TearDown]
    public void QuiteBrowser()
    {
        _webDriver.Quit();
    }
}
