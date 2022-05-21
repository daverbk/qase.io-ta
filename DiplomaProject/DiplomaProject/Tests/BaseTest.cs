using Bogus;
using DiplomaProject.Clients;
using DiplomaProject.Configuration.Enums;
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
        var adminClient = new RestClientExtended(UserType.Admin);

        ProjectService = new ProjectService(adminClient);
        CaseService = new CaseService(adminClient);
        DefectService = new DefectService(adminClient);
    }
}
