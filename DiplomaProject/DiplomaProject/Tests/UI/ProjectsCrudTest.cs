using DiplomaProject.Configuration;
using DiplomaProject.Models;
using DiplomaProject.Pages;
using DiplomaProject.Services.SeleniumServices;
using DiplomaProject.Steps;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;

namespace DiplomaProject.Tests.UI;

[Category("CRUD-UI")]
[Description("This test suite should be run as a whole, don't run the tests one by one.")]
public class ProjectsCrudTest : BaseTest
{
    private IWebDriver _webDriver = null!;
    private ProjectPage _projectPage = null!;
    private LoginStep _loginStep = null!;

    private readonly Project _projectToAdd = FakeProject.Generate();

    [OneTimeSetUp]
    public void OpenBrowserAtWelcomingPage()
    {
        DriverFactory.InitBrowser();

        _webDriver = DriverFactory.Driver;
        _loginStep = new LoginStep(_webDriver);
        _projectPage = new ProjectPage(_webDriver);

        DriverFactory.Driver.Navigate().GoToUrl(Configurator.AppSettings.BaseUiUrl);
    }

    [Test]
    public void CreateProject()
    {
        _loginStep
            .LogIn(Configurator.Admin.Email, Configurator.Admin.Password)
            .ClickCreateNewProjectButton()
            .PopulateProjectData(_projectToAdd)
            .SubmitProjectForm();

        _projectPage.PageOpened.Should().BeTrue();
        _projectPage.ProjectTitleText().Should().Be(_projectToAdd.Title);
    }

    [OneTimeTearDown]
    public void QuiteBrowser()
    {
        _webDriver.Quit();
    }
}
