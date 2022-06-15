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
[AllureSuite("Test cases-API")]
[AllureEpic("Test cases-API")]
[AllureSeverity(SeverityLevel.critical)]
[Category("CRUD-API")]
public class ApiTestCasesCrudApiTest : BaseApiTest
{
    private readonly Project _projectToAdd = new ProjectFaker().Generate();
    private readonly TestCase _testCaseToAdd = new TestCaseFaker().Generate();
    private readonly TestCase _testCaseToUpdateWith = new TestCaseFaker().Generate();

    private string _onSiteProjectCodeAfterCreation = null!;
    private long _onSiteTestCaseIdAfterCreation;

    [OneTimeSetUp]
    public async Task PreconditionCreateProject()
    {
        var creationProjectResponse = await ProjectService.CreateNewProject(_projectToAdd);
        _onSiteProjectCodeAfterCreation = creationProjectResponse.Result.Code;
    }

    [Test]
    [Order(1)]
    [AllureName("Create a test case with required data filled")]
    [AllureStep("Send a \"create a test case\" request")]
    [AllureTms("tms", "suite=13&previewMode=side&case=38")]
    public async Task CreateTestCase_CreateRequest_TestCaseIsCreated()
    {
        var testCaseCreationResponse = await CaseService
            .CreateNewTestCase(_testCaseToAdd, _onSiteProjectCodeAfterCreation);
        _onSiteTestCaseIdAfterCreation = testCaseCreationResponse.Result.Id;

        using (new AssertionScope())
        {
            RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            testCaseCreationResponse.Status.Should().BeTrue();
        }
    }

    [Test]
    [Order(2)]
    [AllureName("Update the test case with required data filled")]
    [AllureStep("Send a \"update a test case\" request")]
    [AllureTms("tms", "suite=13&previewMode=side&case=39")]
    public async Task UpdateTestCase_UpdateRequest_TestCaseIsUpdated()
    {
        _testCaseToUpdateWith.Id = _onSiteTestCaseIdAfterCreation;

        var updateTestCaseResponse = await 
            CaseService.UpdateTestCase(_testCaseToUpdateWith, _onSiteProjectCodeAfterCreation);

        using (new AssertionScope())
        {
            RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            updateTestCaseResponse.Status.Should().BeTrue();
            updateTestCaseResponse.Result.Id.Should().Be(_onSiteTestCaseIdAfterCreation);
        }
    }

    [Test]
    [Order(3)]
    [AllureName("Read the test case")]
    [AllureStep("Send a \"get a test case by id\" request")]
    [AllureTms("tms", "suite=13&previewMode=side&case=40")]
    public async Task GetTestCase_GetRequest_TestCaseIsReturned()
    {
        var getTestCaseResponse = await CaseService
            .GetSpecificTestCase(_onSiteTestCaseIdAfterCreation.ToString(), _onSiteProjectCodeAfterCreation);

        using (new AssertionScope())
        {
            RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            getTestCaseResponse.Status.Should().BeTrue();
            getTestCaseResponse.Result.Title.Should().Be(_testCaseToUpdateWith.Title);
        }
    }

    [Test]
    [Order(4)]
    [AllureName("Delete the test case")]
    [AllureStep("Send a \"delete a test case\" request")]
    [AllureTms("tms", "suite=13&previewMode=side&case=41")]
    public async Task DeleteTestCase_DeleteRequest_TestCaseIsDeleted()
    {
        var deleteTestCaseResponse = await CaseService
            .DeleteTestCase(_onSiteTestCaseIdAfterCreation.ToString(), _onSiteProjectCodeAfterCreation);

        using (new AssertionScope())
        {
            RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            deleteTestCaseResponse.Status.Should().BeTrue();
            deleteTestCaseResponse.Result.Id.Should().Be(_onSiteTestCaseIdAfterCreation);
        }
    }

    [Test]
    [Order(5)]
    [AllureName("Read the remaining test cases")]
    [AllureStep("Send a \"get all test cases\" request")]
    [AllureTms("tms", "suite=13&previewMode=side&case=42")]
    public async Task GetAllTestCases_GetAllRequest_AllTestCasesAreReturned()
    {
        var getAllTestCasesResponse = await CaseService.GetAllTestCases(_onSiteProjectCodeAfterCreation);

        using (new AssertionScope())
        {
            RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            getAllTestCasesResponse.Status.Should().BeTrue();
            getAllTestCasesResponse.Result.Count.Should().Be(0);
        }
    }

    [OneTimeTearDown]
    public async Task PostconditionDeleteProject()
    {
        await ProjectService.DeleteProjectByCode(_onSiteProjectCodeAfterCreation);
    }
}
