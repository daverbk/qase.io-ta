using System.Net;
using System.Threading.Tasks;
using Allure.Commons;
using DiplomaProject.Clients;
using DiplomaProject.Fakers;
using DiplomaProject.Models;
using FluentAssertions;
using FluentAssertions.Execution;
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
public class ProjectsCrudApiTest : BaseApiTest
{
    private readonly Project _projectToAdd = new ProjectFaker().Generate();
    private string _onSiteProjectCodeAfterCreation = null!;

    [Test]
    [Order(1)]
    [AllureName("Create a new project with required data filled")]
    [AllureStep("Send a \"create a new project\" request")]
    [AllureTms("tms", "suite=11&previewMode=side&case=29")]
    public async Task CreateProject_CreateRequest_ProjectIsCreated()
    {
        var creationProjectResponse = await ProjectService.CreateNewProject(_projectToAdd);
        _onSiteProjectCodeAfterCreation = creationProjectResponse.Result.Code;

        using (new AssertionScope())
        {
            RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            creationProjectResponse.Status.Should().BeTrue();
        }
    }

    [Test]
    [Order(2)]
    [AllureName("Read the project")]
    [AllureStep("Send a \"read a project by code\" request")]
    [AllureTms("tms", "suite=11&previewMode=side&case=30")]
    public async Task UpdateProject_UpdateRequest_ProjectIsUpdated()
    {
        var getProjectResponse = await ProjectService.GetProjectByCode(_onSiteProjectCodeAfterCreation);

        using (new AssertionScope())
        {
            RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            getProjectResponse.Status.Should().BeTrue();
            getProjectResponse.Result.Title.Should().Be(_projectToAdd.Title);
            getProjectResponse.Result.Code.Should().Be(_onSiteProjectCodeAfterCreation);
        }
    }

    [Test]
    [Order(3)]
    [AllureName("Delete the project")]
    [AllureStep("Send a \"delete a project\" request")]
    [AllureTms("tms", "suite=11&previewMode=side&case=31")]
    public async Task DeleteProject_DeleteRequest_ProjectIsDeleted()
    {
        var deleteProjectResponse = await ProjectService.DeleteProjectByCode(_onSiteProjectCodeAfterCreation);

        using (new AssertionScope())
        {
            RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            deleteProjectResponse.Status.Should().BeTrue();
        }
    }

    [Test]
    [Order(4)]
    [AllureName("Read all the remaining projects")]
    [AllureStep("Send a \"get all projects\" request")]
    [AllureTms("tms", "suite=11&previewMode=side&case=32")]
    public async Task GetAllProjects_GetAllRequest_AllProjectsAreReturned()
    {
        var getAllProjectsResponse = await ProjectService.GetAllProjects();

        using (new AssertionScope())
        {
            RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            getAllProjectsResponse.Status.Should().BeTrue();
            getAllProjectsResponse.Result.Entities.Should().NotContain(project => project.Title == _projectToAdd.Title);
        }
    }
}
