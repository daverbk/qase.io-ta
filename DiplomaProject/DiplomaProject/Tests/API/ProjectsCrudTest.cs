using System.Net;
using Allure.Commons;
using DiplomaProject.Clients;
using DiplomaProject.Fakers;
using DiplomaProject.Models;
using FluentAssertions;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace DiplomaProject.Tests.API;

[AllureNUnit]
[AllureParentSuite("API")]
[AllureSuite("Projects-API")]
[AllureEpic("Projects-API")]
[AllureSeverity(SeverityLevel.critical)]
[Category("CRUD-API")]
[AllureTms("tms", "suite=11&case=23&previewMode=modal")]
public class ProjectsCrudApiTest : BaseApiTest
{
    private readonly Project _projectToAdd = new ProjectFaker().Generate();
    private string _onSiteProjectCodeAfterCreation = null!;

    [Test]
    [Order(1)]
    [AllureName("Create a new project with required data filled")]
    [AllureStep("Send a \"create a new project\" request")]
    public void CreateProject_CreateRequest_ProjectIsCreated()
    {
        var creationProjectResponse = ProjectService.CreateNewProject(_projectToAdd).Result;
        _onSiteProjectCodeAfterCreation = creationProjectResponse.Result.Code;

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        creationProjectResponse.Status.Should().BeTrue();
    }

    [Test]
    [Order(2)]
    [AllureName("Read the project")]
    [AllureStep("Send a \"read a project by code\" request")]
    public void UpdateProject_UpdateRequest_ProjectIsUpdated()
    {
        var getProjectResponse = ProjectService.GetProjectByCode(_onSiteProjectCodeAfterCreation).Result;

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        getProjectResponse.Status.Should().BeTrue();
        getProjectResponse.Result.Title.Should().Be(_projectToAdd.Title);
        getProjectResponse.Result.Code.Should().Be(_onSiteProjectCodeAfterCreation);
    }

    [Test]
    [Order(3)]
    [AllureName("Delete the project")]
    [AllureStep("Send a \"delete a project\" request")]
    public void DeleteProject_DeleteRequest_ProjectIsDeleted()
    {
        var deleteProjectResponse = ProjectService.DeleteProjectByCode(_onSiteProjectCodeAfterCreation).Result;

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        deleteProjectResponse.Status.Should().BeTrue();
    }

    [Test]
    [Order(4)]
    [AllureName("Read all the remaining projects")]
    [AllureStep("Send a \"get all projects\" request")]
    public void GetAllProjects_GetAllRequest_AllProjectsAreReturned()
    {
        var getAllProjectsResponse = ProjectService.GetAllProjects().Result;

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        getAllProjectsResponse.Status.Should().BeTrue();
        getAllProjectsResponse.Result.Entities.Should().NotContain(project => project.Title == _projectToAdd.Title);
    }
}
