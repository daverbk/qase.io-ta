using System.Net;
using Allure.Commons;
using DiplomaProject.Clients;
using DiplomaProject.Models;
using FluentAssertions;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace DiplomaProject.Tests.API;

[AllureNUnit]
[AllureParentSuite("API")]
[AllureSuite("Defects-API")]
[AllureEpic("Defects-API")]
[AllureSeverity(SeverityLevel.critical)]
[Category("CRUD-API")]
[AllureTms("tms", "suite=12&previewMode=modal&case=25")]
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
    [AllureStep("Create a new defect")]
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
    [AllureStep("Update the defect")]
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
    [AllureStep("Read the defect")]
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
    [AllureStep("Delete the defect")]
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
    [AllureStep("Read all the remaining defects")]
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
