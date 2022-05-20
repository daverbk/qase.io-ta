using System.Net;
using DiplomaProject.Clients;
using DiplomaProject.Models;
using DiplomaProject.Services.ApiServices;
using FluentAssertions;
using NUnit.Framework;

namespace DiplomaProject.Tests;

[Category("CRUD")]
[Description("This test suite should be run as a whole, don't run the tests one by one.")]
public class ProjectsCrudTest : BaseTest
{
    private ProjectService _projectService = null!;
    private Project _projectToAdd = null!;
    private string _onSiteProjectCodeAfterCreation = null!;

    [OneTimeSetUp]
    public void CreateClient()
    {
        var client = new RestClientExtended();
        _projectService = new ProjectService(client);
        _projectToAdd = FakeProject.Generate();
    }

    [Test]
    [Order(1)]
    public void CreateProject()
    {
        var creationProjectResponse = _projectService.CreateNewProject(_projectToAdd).Result;
        _onSiteProjectCodeAfterCreation = creationProjectResponse.Result.Code;

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        creationProjectResponse.Status.Should().BeTrue();
    }

    [Test]
    [Order(2)]
    public void GetProject()
    {
        var getProjectResponse = _projectService.GetProjectByCode(_onSiteProjectCodeAfterCreation).Result;

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        getProjectResponse.Status.Should().BeTrue();
        getProjectResponse.Result.Title.Should().Be(_projectToAdd.Title);
        getProjectResponse.Result.Code.Should().Be(_onSiteProjectCodeAfterCreation);
    }

    [Test]
    [Order(3)]
    public void DeleteProject()
    {
        var deleteProjectResponse = _projectService.DeleteProjectByCode(_onSiteProjectCodeAfterCreation).Result;

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        deleteProjectResponse.Status.Should().BeTrue();
    }

    [Test]
    [Order(4)]
    public void GetAllProjects()
    {
        var getAllProjectsResponse = _projectService.GetAllProjects().Result;

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        getAllProjectsResponse.Status.Should().BeTrue();
        getAllProjectsResponse.Result.Count.Should().Be(0);
    }
}
