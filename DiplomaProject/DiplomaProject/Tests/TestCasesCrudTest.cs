using System.Net;
using DiplomaProject.Clients;
using DiplomaProject.Models;
using FluentAssertions;
using NUnit.Framework;

namespace DiplomaProject.Tests;

[Category("CRUD-API")]
[Description("This test suite should be run as a whole, don't run the tests one by one.")]
public class TestCasesCrudTest : BaseTest
{
    private readonly Project _projectToAdd = FakeProject.Generate();
    private readonly TestCase _testCaseToAdd = FakeTestCase.Generate();
    private readonly TestCase _testCaseToUpdateWith = FakeTestCase.Generate();

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
    public void CreateTestCase()
    {
        var testCaseCreationResponse =
            CaseService.CreateNewTestCase(_testCaseToAdd, _onSiteProjectCodeAfterCreation).Result;
        _onSiteTestCaseIdAfterCreation = testCaseCreationResponse.Result.Id;

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        testCaseCreationResponse.Status.Should().BeTrue();
    }

    [Test]
    [Order(2)]
    public void UpdateTestCase()
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
    public void GetTestCase()
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
    public void DeleteTestCase()
    {
        var deleteTestCaseResponse = CaseService
            .DeleteTestCase(_onSiteTestCaseIdAfterCreation.ToString(), _onSiteProjectCodeAfterCreation).Result;

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        deleteTestCaseResponse.Status.Should().BeTrue();
        deleteTestCaseResponse.Result.Id.Should().Be(_onSiteTestCaseIdAfterCreation);
    }

    [Test]
    [Order(5)]
    public void GetAllTestCases()
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
