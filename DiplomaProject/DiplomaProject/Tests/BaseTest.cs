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

    protected static Faker<Project> FakeProject => new ProjectFaker();
    
    protected static Faker<TestCase> FakeTestCase => new TestCaseFaker();
    
    [OneTimeSetUp]
    public void SetUpApi()
    {
        var client = new RestClientExtended();

        ProjectService = new ProjectService(client);
        CaseService = new CaseService(client);
    }
}

