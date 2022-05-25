using Allure.Commons;
using DiplomaProject.Configuration;
using DiplomaProject.Fakers;
using DiplomaProject.Models;
using DiplomaProject.Pages;
using DiplomaProject.Services.SeleniumServices;
using DiplomaProject.Steps;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;
using OpenQA.Selenium;

namespace DiplomaProject.Tests.UI;

[AllureNUnit]
[AllureParentSuite("UI")]
[AllureSuite("Projects-UI")]
[AllureEpic("Projects-UI")]
[AllureSeverity(SeverityLevel.critical)]
[Category("CRUD-UI")]
public class ProjectsCrudUiTest
{
    private IWebDriver _webDriver = null!;

    private ProjectPage _projectPage = null!;
    private ProjectSettingsPage _projectSettingsPage = null!;
    private ProjectsPage _projectsPage = null!;
    private LoginStep _loginStep = null!;

    private readonly Project _projectToAdd = new ProjectFaker().Generate();
    private readonly Project _projectToUpdateWith = new ProjectFaker().Generate();

    [OneTimeSetUp]
    public void OpenBrowserAtWelcomingPage()
    {
        DriverFactory.InitBrowser();

        _webDriver = DriverFactory.Driver;
        _loginStep = new LoginStep(_webDriver);
        _projectPage = new ProjectPage(_webDriver);
        _projectSettingsPage = new ProjectSettingsPage(_webDriver);
        _projectsPage = new ProjectsPage(_webDriver);

        DriverFactory.Driver.Navigate().GoToUrl(Configurator.AppSettings.BaseUiUrl);
    }

    [Test]
    [Order(1)]
    [AllureName("Create a project with requited fields filled")]
    [AllureTms("tms", "suite=6&previewMode=modal&case=11")]
    public void CreateProject_PopulateProjectForm_ProjectIsCreated()
    {
        _loginStep
            .LogIn(Configurator.Admin.Email, Configurator.Admin.Password)
            .ClickCreateNewProjectButton()
            .PopulateProjectData(_projectToAdd)
            .SubmitProjectForm();

        using (new AssertionScope())
        {
            _projectPage.PageOpened.Should().BeTrue();
            _projectPage.ProjectTitleText().Should().Be(_projectToAdd.Title);
        }
    }

    [Test]
    [Order(2)]
    [AllureName("Update the project with required fields filled")]
    [AllureTms("tms", "suite=6&previewMode=modal&case=12")]
    public void UpdateProject_PopulateUpdateProjectForm_ProjectIsUpdated()
    {
        _projectPage
            .NavigateToSettings()
            .PopulateUpdatedProjectData(_projectToUpdateWith)
            .SubmitProjectForm();

        using (new AssertionScope())
        {
            _projectSettingsPage.AlertDisplayed().Should().BeTrue();
            _projectSettingsPage.AlertMessage().Should().Be("Project settings were successfully updated!");
            _projectSettingsPage.UpdatedData().Title.Should().Be(_projectToUpdateWith.Title);
            _projectSettingsPage.UpdatedData().Code.Should().Be(_projectToUpdateWith.Code);
        }
    }

    [Test]
    [Order(3)]
    [AllureName("Delete the updated project")]
    [AllureTms("tms", "suite=6&previewMode=modal&case=13")]
    public void DeleteProject_ConfirmDeletion_ProjectIsDeleted()
    {
        _projectSettingsPage
            .DeleteProject()
            .ConfirmDeletion();

        _projectsPage.ProjectExistsInTable(_projectToUpdateWith.Title).Should().BeFalse();
    }

    [OneTimeTearDown]
    public void QuitBrowser()
    {
        _webDriver.Quit();
    }
}
