using System.Net;
using DiplomaProject.Clients;
using DiplomaProject.Models;
using FluentAssertions;
using NUnit.Framework;

namespace DiplomaProject.Tests.API;

[Category("CRUD-API")]
[Description("This test suite should be run as a whole, don't run the tests one by one.")]
public class ProjectsCrudTest : BaseTest
{
    private readonly Project _projectToAdd = FakeProject.Generate();
    private string _onSiteProjectCodeAfterCreation = null!;

    [Test]
    [Order(1)]
    public void CreateProject()
    {
        var creationProjectResponse = ProjectService.CreateNewProject(_projectToAdd).Result;
        _onSiteProjectCodeAfterCreation = creationProjectResponse.Result.Code;

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        creationProjectResponse.Status.Should().BeTrue();
    }

    [Test]
    [Order(2)]
    public void GetProject()
    {
        var getProjectResponse = ProjectService.GetProjectByCode(_onSiteProjectCodeAfterCreation).Result;

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        getProjectResponse.Status.Should().BeTrue();
        getProjectResponse.Result.Title.Should().Be(_projectToAdd.Title);
        getProjectResponse.Result.Code.Should().Be(_onSiteProjectCodeAfterCreation);
    }

    [Test]
    [Order(3)]
    public void DeleteProject()
    {
        var deleteProjectResponse = ProjectService.DeleteProjectByCode(_onSiteProjectCodeAfterCreation).Result;

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        deleteProjectResponse.Status.Should().BeTrue();
    }

    [Test]
    [Order(4)]
    public void GetAllProjects()
    {
        var getAllProjectsResponse = ProjectService.GetAllProjects().Result;

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        getAllProjectsResponse.Status.Should().BeTrue();
        getAllProjectsResponse.Result.Count.Should().Be(0);
    }
}
