using DiplomaProject.Clients;
using DiplomaProject.Configuration.Enums;
using DiplomaProject.Services.ApiServices;
using NUnit.Framework;

namespace DiplomaProject.Tests;

public class BaseApiTest
{
    protected ProjectService ProjectService { get; private set; } = null!;

    protected CaseService CaseService { get; private set; } = null!;

    protected DefectService DefectService { get; private set; } = null!;

    [OneTimeSetUp]
    public void SetUpClient()
    {
        var adminClient = new RestClientExtended(UserType.Admin);

        ProjectService = new ProjectService(adminClient);
        CaseService = new CaseService(adminClient);
        DefectService = new DefectService(adminClient);
    }
}
