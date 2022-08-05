using Allure.Commons;
using DiplomaProject.Configuration;
using DiplomaProject.Fakers;
using DiplomaProject.Models;
using DiplomaProject.Pages;
using DiplomaProject.Steps;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace DiplomaProject.Tests.UI;

[AllureNUnit]
[AllureParentSuite("UI")]
[AllureSuite("Projects-UI")]
[AllureEpic("Projects-UI")]
[AllureSeverity(SeverityLevel.critical)]
[Category("CRUD-UI")]
public class ProjectsCrudUiTest : BaseUiTest
{
    private readonly Project _projectToAdd = new ProjectFaker().Generate();
    private readonly Project _projectToUpdateWith = new ProjectFaker().Generate();

    [Test]
    [Order(1)]
    [AllureName("Create a project with requited fields filled")]
    [AllureTms("tms", "suite=6&previewMode=modal&case=11")]
    public void CreateProject_PopulateProjectForm_ProjectIsCreated()
    {
        new LoginStep().LogIn(Configurator.Admin.Email, Configurator.Admin.Password)
            .ClickCreateNewProjectButton()
            .PopulateProjectData(_projectToAdd)
            .SubmitProjectForm();

        using (new AssertionScope())
        {
            new ProjectPage().IsOpened.Should().BeTrue();
            ProjectPage.ProjectTitleText().Should().Be(_projectToAdd.Title);
        }
    }

    [Test]
    [Order(2)]
    [AllureName("Update the project with required fields filled")]
    [AllureTms("tms", "suite=6&previewMode=modal&case=12")]
    public void UpdateProject_PopulateUpdateProjectForm_ProjectIsUpdated()
    {
        ProjectPage
            .NavigateToSettings()
            .PopulateUpdatedProjectData(_projectToUpdateWith)
            .SubmitProjectForm();

        using (new AssertionScope())
        {
            ProjectSettingsPage.AlertDisplayed().Should().BeTrue();
            ProjectSettingsPage.AlertMessage().Should().Be("Project settings were successfully updated!");
            ProjectSettingsPage.UpdatedData().Title.Should().Be(_projectToUpdateWith.Title);
            ProjectSettingsPage.UpdatedData().Code.Should().Be(_projectToUpdateWith.Code);
        }
    }

    [Test]
    [Order(3)]
    [AllureName("Delete the updated project")]
    [AllureTms("tms", "suite=6&previewMode=modal&case=13")]
    public void DeleteProject_ConfirmDeletion_ProjectIsDeleted()
    {
        ProjectSettingsPage
            .DeleteProject()
            .ConfirmDeletion();

        ProjectsPage.ProjectExistsInTable(_projectToUpdateWith.Title).Should().BeFalse();
    }
}
