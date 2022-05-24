using System.Net;
using Allure.Commons;
using DiplomaProject.Clients;
using DiplomaProject.Configuration.Enums;
using DiplomaProject.Services.ApiServices;
using FluentAssertions;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace DiplomaProject.Tests.API;

[AllureNUnit]
[AllureParentSuite("API")]
[AllureSuite("Authentication-API")]
[AllureEpic("Authentication-API")]
[Category("Authentication-API")]
[AllureSeverity(SeverityLevel.blocker)]
public class AuthenticationTests : BaseTest
{
    private ProjectService _projectServiceUserWithInvalidToken = null!;
    private ProjectService _projectServiceUnauthorizedUser = null!;

    [OneTimeSetUp]
    public void SetUpInvalidClients()
    {
        var clientWithInvalidToken = new RestClientExtended(UserType.WithInvalidAuthenticationData);
        _projectServiceUserWithInvalidToken = new ProjectService(clientWithInvalidToken);

        var unauthorizedClient = new RestClientExtended(UserType.Unauthorized);
        _projectServiceUnauthorizedUser = new ProjectService(unauthorizedClient);
    }

    [Test]
    [Category("Positive")]
    [AllureStep("Authenticate using valid data")]
    [AllureTms("tms", "suite=15&previewMode=modal&case=28")]
    public void RequestValidAuthentication()
    {
        ProjectService.GetAllProjects().Wait();

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Test]
    [Category("Negative")]
    [AllureStep("Authenticate using invalid data")]
    [AllureTms("tms", "suite=15&previewMode=modal&case=26")]
    public void RequestInvalidAuthentication()
    {
        _projectServiceUserWithInvalidToken.GetAllProjects().Wait();

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        RestClientExtended.LastCallResponse.Content.Should().Contain("API token is invalid");
    }

    [Test]
    [Category("Negative")]
    [AllureStep("Authenticate using no authentication set in client")]
    [AllureTms("tms", "suite=15&previewMode=modal&case=27")]
    public void RequestNoAuthentication()
    {
        _projectServiceUnauthorizedUser.GetAllProjects().Wait();

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        RestClientExtended.LastCallResponse.Content.Should().Contain("API token not provided");
    }
}
