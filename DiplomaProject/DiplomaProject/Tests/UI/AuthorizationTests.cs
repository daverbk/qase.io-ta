using DiplomaProject.Configuration;
using DiplomaProject.Pages;
using DiplomaProject.Services.SeleniumServices;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;

namespace DiplomaProject.Tests.UI;

[Category("Authorization-UI")]
[Description("This test suite should be run as a whole, don't run the tests one by one.")]
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
    [TestCase("' or \"")]
    [TestCase("UNION ALL SELECT USER()--")]
    [TestCase("admin' or 1=1")]
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
