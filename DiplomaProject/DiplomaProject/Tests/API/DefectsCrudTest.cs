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
[AllureSuite("Defects-API")]
[AllureEpic("Defects-API")]
[AllureSeverity(SeverityLevel.critical)]
[Category("CRUD-API")]
[AllureTms("tms", "suite=12&previewMode=modal&case=25")]
public class DefectsCrudApiTest : BaseApiTest
{
    private readonly Project _projectToAdd = new ProjectFaker().Generate();
    private readonly Defect _defectToAdd = new DefectFaker().Generate();
    private readonly Defect _defectToUpdateWith = new DefectFaker().Generate();

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
    [AllureName("Create a new defect with required data filled")]
    [AllureStep("Send a \"create a new defect\" request")]
    public void CreateDefect_CreateRequest_DefectIsCreated()
    {
        var defectCreationResponse =
            DefectService.CreateNewDefect(_defectToAdd, _onSiteProjectCodeAfterCreation).Result;
        _onSiteDefectIdAfterCreation = defectCreationResponse.Result.Id;

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        defectCreationResponse.Status.Should().BeTrue();
    }

    [Test]
    [Order(2)]
    [AllureName("Update the defect with required data filled")]
    [AllureStep("Send a \"update a defect\" request")]
    public void UpdateDefect_UpdateRequest_DefectIsUpdated()
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
    [AllureName("Read the defect")]
    [AllureStep("Send a \"get a defect by id\" request")]
    public void GetDefect_GetRequest_DefectIsReturned()
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
    [AllureName("Delete the defect")]
    [AllureStep("Send a \"delete a defect\" request")]
    public void DeleteDefect_DeleteRequest_DefectIsDeleted()
    {
        var deleteDefectResponse = DefectService
            .DeleteDefect(_onSiteDefectIdAfterCreation.ToString(), _onSiteProjectCodeAfterCreation).Result;

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        deleteDefectResponse.Status.Should().BeTrue();
        deleteDefectResponse.Result.Id.Should().Be(_onSiteDefectIdAfterCreation);
    }

    [Test]
    [Order(5)]
    [AllureName("Read all the remaining defects")]
    [AllureStep("Send a \"get all defects\" request")]
    public void GetAllDefects_GetAllRequest_AllDefectsAreReturned()
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
