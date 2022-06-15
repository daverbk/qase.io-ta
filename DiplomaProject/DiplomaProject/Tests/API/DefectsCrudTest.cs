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
[AllureSuite("Defects-API")]
[AllureEpic("Defects-API")]
[AllureSeverity(SeverityLevel.critical)]
[Category("CRUD-API")]
public class DefectsCrudApiTest : BaseApiTest
{
    private readonly Project _projectToAdd = new ProjectFaker().Generate();
    private readonly Defect _defectToAdd = new DefectFaker().Generate();
    private readonly Defect _defectToUpdateWith = new DefectFaker().Generate();

    private string _onSiteProjectCodeAfterCreation = null!;
    private long _onSiteDefectIdAfterCreation;

    [OneTimeSetUp]
    public async Task PreconditionCreateProject()
    {
        var creationProjectResponse = await ProjectService.CreateNewProject(_projectToAdd);
        _onSiteProjectCodeAfterCreation = creationProjectResponse.Result.Code;
    }

    [Test]
    [Order(1)]
    [AllureName("Create a new defect with required data filled")]
    [AllureStep("Send a \"create a new defect\" request")]
    [AllureTms("tms", "suite=12&previewMode=side&case=33")]
    public async Task CreateDefect_CreateRequest_DefectIsCreated()
    {
        var defectCreationResponse = await DefectService.CreateNewDefect(_defectToAdd, _onSiteProjectCodeAfterCreation);
        _onSiteDefectIdAfterCreation = defectCreationResponse.Result.Id;

        using (new AssertionScope())
        {
            RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            defectCreationResponse.Status.Should().BeTrue();
        }
    }

    [Test]
    [Order(2)]
    [AllureName("Update the defect with required data filled")]
    [AllureStep("Send a \"update a defect\" request")]
    [AllureTms("tms", "suite=12&previewMode=side&case=34")]
    public async Task UpdateDefect_UpdateRequest_DefectIsUpdated()
    {
        _defectToUpdateWith.Id = _onSiteDefectIdAfterCreation;

        var updateDefectResponse = await DefectService.UpdateDefect(_defectToUpdateWith, _onSiteProjectCodeAfterCreation);

        using (new AssertionScope())
        {
            RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            updateDefectResponse.Status.Should().BeTrue();
            updateDefectResponse.Result.Id.Should().Be(_onSiteDefectIdAfterCreation);
        }
    }

    [Test]
    [Order(3)]
    [AllureName("Read the defect")]
    [AllureStep("Send a \"get a defect by id\" request")]
    [AllureTms("tms", "suite=12&previewMode=side&case=35")]
    public async Task GetDefect_GetRequest_DefectIsReturned()
    {
        var getDefectResponse = await DefectService
            .GetSpecificDefect(_onSiteDefectIdAfterCreation.ToString(), _onSiteProjectCodeAfterCreation);

        using (new AssertionScope())
        {
            RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            getDefectResponse.Status.Should().BeTrue();
            getDefectResponse.Result.Title.Should().Be(_defectToUpdateWith.Title);
            getDefectResponse.Result.ActualResult.Should().Be(_defectToUpdateWith.ActualResult);
        }
    }

    [Test]
    [Order(4)]
    [AllureName("Delete the defect")]
    [AllureStep("Send a \"delete a defect\" request")]
    [AllureTms("tms", "suite=12&previewMode=side&case=36")]
    public async Task DeleteDefect_DeleteRequest_DefectIsDeleted()
    {
        var deleteDefectResponse = await DefectService
            .DeleteDefect(_onSiteDefectIdAfterCreation.ToString(), _onSiteProjectCodeAfterCreation);

        using (new AssertionScope())
        {
            RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            deleteDefectResponse.Status.Should().BeTrue();
            deleteDefectResponse.Result.Id.Should().Be(_onSiteDefectIdAfterCreation);
        }
    }

    [Test]
    [Order(5)]
    [AllureName("Read all the remaining defects")]
    [AllureStep("Send a \"get all defects\" request")]
    [AllureTms("tms", "suite=12&previewMode=side&case=37")]
    public async Task GetAllDefects_GetAllRequest_AllDefectsAreReturned()
    {
        var getAllDefectsResponse = await DefectService.GetAllDefects(_onSiteProjectCodeAfterCreation);

        using (new AssertionScope())
        {
            RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            getAllDefectsResponse.Status.Should().BeTrue();
            getAllDefectsResponse.Result.Count.Should().Be(0);
        }
    }

    [OneTimeTearDown]
    public async Task PostconditionDeleteProject()
    {
        await ProjectService.DeleteProjectByCode(_onSiteProjectCodeAfterCreation);
    }
}
