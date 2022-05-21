using System.Net;
using DiplomaProject.Clients;
using DiplomaProject.Models;
using FluentAssertions;
using NUnit.Framework;

namespace DiplomaProject.Tests;

[Category("CRUD-API")]
[Description("This test suite should be run as a whole, don't run the tests one by one.")]
public class DefectsCrudTest : BaseTest
{
    private readonly Project _projectToAdd = FakeProject.Generate();
    private readonly Defect _defectToAdd = FakeDefect.Generate();
    private readonly Defect _defectToUpdateWith = FakeDefect.Generate();

    private string _onSiteProjectCodeAfterCreation = null!;
    private long _onSiteDefectIdAfterCreation;

    [OneTimeSetUp]
    public void PreconditionCreateProject()
    {
        var creationProjectResponse = ProjectService.CreateNewProject(_projectToAdd).Result;
        _onSiteProjectCodeAfterCreation = creationProjectResponse.Result.Code;
    }

    [Test]
    [Order(1)]
    public void CreateDefect()
    {
        var defectCreationResponse =
            DefectService.CreateNewDefect(_defectToAdd, _onSiteProjectCodeAfterCreation).Result;
        _onSiteDefectIdAfterCreation = defectCreationResponse.Result.Id;

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        defectCreationResponse.Status.Should().BeTrue();
    }

    [Test]
    [Order(2)]
    public void UpdateDefect()
    {
        _defectToUpdateWith.Id = _onSiteDefectIdAfterCreation;

        var updateDefectResponse =
            DefectService.UpdateDefect(_defectToUpdateWith, _onSiteProjectCodeAfterCreation).Result;

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        updateDefectResponse.Status.Should().BeTrue();
        updateDefectResponse.Result.Id.Should().Be(_onSiteDefectIdAfterCreation);
    }

    [Test]
    [Order(3)]
    public void GetDefect()
    {
        var getDefectResponse = DefectService
            .GetSpecificDefect(_onSiteDefectIdAfterCreation.ToString(), _onSiteProjectCodeAfterCreation).Result;

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        getDefectResponse.Status.Should().BeTrue();
        getDefectResponse.Result.Title.Should().Be(_defectToUpdateWith.Title);
        getDefectResponse.Result.ActualResult.Should().Be(_defectToUpdateWith.ActualResult);
    }

    [Test]
    [Order(4)]
    public void DeleteDefect()
    {
        var deleteDefectResponse = DefectService
            .DeleteDefect(_onSiteDefectIdAfterCreation.ToString(), _onSiteProjectCodeAfterCreation).Result;

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        deleteDefectResponse.Status.Should().BeTrue();
        deleteDefectResponse.Result.Id.Should().Be(_onSiteDefectIdAfterCreation);
    }

    [Test]
    [Order(5)]
    public void GetAllDefects()
    {
        var getAllDefectsResponse = DefectService.GetAllDefects(_onSiteProjectCodeAfterCreation).Result;
        
        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        getAllDefectsResponse.Status.Should().BeTrue();
        getAllDefectsResponse.Result.Count.Should().Be(0);
    }

    [OneTimeTearDown]
    public void PostconditionDeleteProject()
    {
        ProjectService.DeleteProjectByCode(_onSiteProjectCodeAfterCreation).Wait();
    }
}
