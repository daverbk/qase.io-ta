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
    private ProjectSettingsPage _projectSettingsPage = null!;
    private LoginStep _loginStep = null!;

    private readonly Project _projectToAdd = FakeProject.Generate();
    private readonly Project _projectToUpdateWith = FakeProject.Generate();

    [OneTimeSetUp]
    public void OpenBrowserAtWelcomingPage()
    {
        DriverFactory.InitBrowser();

        _webDriver = DriverFactory.Driver;
        _loginStep = new LoginStep(_webDriver);
        _projectPage = new ProjectPage(_webDriver);
        _projectSettingsPage = new ProjectSettingsPage(_webDriver);

        DriverFactory.Driver.Navigate().GoToUrl(Configurator.AppSettings.BaseUiUrl);
    }

    [Test]
    [Order(1)]
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

    [Test]
    [Order(2)]
    public void UpdateProject()
    {
        _projectPage
            .NavigateToSettings()
            .PopulateUpdatedProjectData(_projectToUpdateWith)
            .SubmitProjectForm();

        _projectSettingsPage.AlertUpdatedSuccessfullyDisplayed().Should().BeTrue();
        _projectSettingsPage.AlertUpdatedSuccessfullyMessage().Should().Be("Project settings were successfully updated!");
        _projectSettingsPage.UpdatedData().Title.Should().Be(_projectToUpdateWith.Title);
        _projectSettingsPage.UpdatedData().Code.Should().Be(_projectToUpdateWith.Code);
        _projectSettingsPage.UpdatedData().Description.Should().Be(_projectToUpdateWith.Description);
    }

    [OneTimeTearDown]
    public void QuiteBrowser()
    {
        _webDriver.Quit();
    }
}
