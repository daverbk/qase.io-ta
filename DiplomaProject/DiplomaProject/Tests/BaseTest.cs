using Bogus;
using DiplomaProject.Clients;
using DiplomaProject.Fakers;
using DiplomaProject.Models;
using DiplomaProject.Services.ApiServices;
using NUnit.Framework;

namespace DiplomaProject.Tests;

public class BaseTest
{
    protected ProjectService ProjectService { get; private set; } = null!;

    protected CaseService CaseService { get; private set; } = null!;

    protected DefectService DefectService { get; private set; } = null!;

    protected static Faker<Project> FakeProject => new ProjectFaker();

    protected static Faker<TestCase> FakeTestCase => new TestCaseFaker();

    protected static Faker<Defect> FakeDefect => new DefectFaker();

    [OneTimeSetUp]
    public void SetUpApi()
    {
        var client = new RestClientExtended();

        ProjectService = new ProjectService(client);
        CaseService = new CaseService(client);
        DefectService = new DefectService(client);
    }
}
