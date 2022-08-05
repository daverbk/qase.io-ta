using DiplomaProject.Clients;
using DiplomaProject.Configuration.Enums;
using DiplomaProject.Services.ApiServices;
using NUnit.Framework;

namespace DiplomaProject.Tests;

public class BaseApiTest
{
    private RestClientExtended _client = null!;
    
    protected ProjectService ProjectService { get; private set; } = null!;

    protected CaseService CaseService { get; private set; } = null!;

    protected DefectService DefectService { get; private set; } = null!;

    [OneTimeSetUp]
    public void SetUpClient()
    {
        _client = new RestClientExtended(UserType.Admin);

        ProjectService = new ProjectService(_client);
        CaseService = new CaseService(_client);
        DefectService = new DefectService(_client);
    }

    [OneTimeTearDown]
    public void DisposeClient()
    {
        _client.Dispose();
    }
}
