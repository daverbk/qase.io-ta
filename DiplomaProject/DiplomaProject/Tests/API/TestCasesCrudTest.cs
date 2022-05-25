using System.Net;
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
[AllureTms("tms", "suite=13&case=24&previewMode=modal")]
public class ApiTestCasesCrudApiTest : BaseApiTest
{
    private readonly Project _projectToAdd = new ProjectFaker().Generate();
    private readonly TestCase _testCaseToAdd = new TestCaseFaker().Generate();
    private readonly TestCase _testCaseToUpdateWith = new TestCaseFaker().Generate();

    private string _onSiteProjectCodeAfterCreation = null!;
    private long _onSiteTestCaseIdAfterCreation;

    [OneTimeSetUp]
    public void PreconditionCreateProject()
    {
        var creationProjectResponse = ProjectService.CreateNewProject(_projectToAdd).Result;
        _onSiteProjectCodeAfterCreation = creationProjectResponse.Result.Code;
    }

    [Test]
    [Order(1)]
    [AllureName("Create a test case with required data filled")]
    [AllureStep("Send a \"create a test case\" request")]
    public void CreateTestCase_CreateRequest_TestCaseIsCreated()
    {
        var testCaseCreationResponse =
            CaseService.CreateNewTestCase(_testCaseToAdd, _onSiteProjectCodeAfterCreation).Result;
        _onSiteTestCaseIdAfterCreation = testCaseCreationResponse.Result.Id;

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        testCaseCreationResponse.Status.Should().BeTrue();
    }

    [Test]
    [Order(2)]
    [AllureName("Update the test case with required data filled")]
    [AllureStep("Send a \"update a test case\" request")]
    public void UpdateTestCase_UpdateRequest_TestCaseIsUpdated()
    {
        _testCaseToUpdateWith.Id = _onSiteTestCaseIdAfterCreation;

        var updateTestCaseResponse =
            CaseService.UpdateTestCase(_testCaseToUpdateWith, _onSiteProjectCodeAfterCreation).Result;

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        updateTestCaseResponse.Status.Should().BeTrue();
        updateTestCaseResponse.Result.Id.Should().Be(_onSiteTestCaseIdAfterCreation);
    }

    [Test]
    [Order(3)]
    [AllureName("Read the test case")]
    [AllureStep("Send a \"get a test case by id\" request")]
    public void GetTestCase_GetRequest_TestCaseIsReturned()
    {
        var getTestCaseResponse = CaseService
            .GetSpecificTestCase(_onSiteTestCaseIdAfterCreation.ToString(), _onSiteProjectCodeAfterCreation).Result;

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        getTestCaseResponse.Status.Should().BeTrue();
        getTestCaseResponse.Result.Title.Should().Be(_testCaseToUpdateWith.Title);
        getTestCaseResponse.Result.Description.Should().Be(_testCaseToUpdateWith.Description);
        getTestCaseResponse.Result.Preconditions.Should().Be(_testCaseToUpdateWith.Preconditions);
        getTestCaseResponse.Result.Postconditions.Should().Be(_testCaseToUpdateWith.Postconditions);
    }

    [Test]
    [Order(4)]
    [AllureName("Delete the test case")]
    [AllureStep("Send a \"delete a test case\" request")]
    public void DeleteTestCase_DeleteRequest_TestCaseIsDeleted()
    {
        var deleteTestCaseResponse = CaseService
            .DeleteTestCase(_onSiteTestCaseIdAfterCreation.ToString(), _onSiteProjectCodeAfterCreation).Result;

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        deleteTestCaseResponse.Status.Should().BeTrue();
        deleteTestCaseResponse.Result.Id.Should().Be(_onSiteTestCaseIdAfterCreation);
    }

    [Test]
    [Order(5)]
    [AllureName("Read the remaining test cases")]
    [AllureStep("Send a \"get all test cases\" request")]
    public void GetAllTestCases_GetAllRequest_AllTestCasesAreReturned()
    {
        var getAllTestCasesResponse = CaseService.GetAllTestCases(_onSiteProjectCodeAfterCreation).Result;

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        getAllTestCasesResponse.Status.Should().BeTrue();
        getAllTestCasesResponse.Result.Count.Should().Be(0);
    }

    [OneTimeTearDown]
    public void PostconditionDeleteProject()
    {
        ProjectService.DeleteProjectByCode(_onSiteProjectCodeAfterCreation).Wait();
    }
}
