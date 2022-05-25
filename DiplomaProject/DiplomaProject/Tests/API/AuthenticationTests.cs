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
public class AuthenticationApiTests : BaseApiTest
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
    [AllureName("Authentication using valid data")]
    [AllureStep("Send \"get all projects\" request from a validly authorized user")]
    [AllureTms("tms", "suite=15&previewMode=modal&case=26")]
    public void Authentication_ValidToken_SuccessfulAuthentication()
    {
        ProjectService.GetAllProjects().Wait();

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Test]
    [Category("Negative")]
    [AllureName("Authentication using invalid data")]
    [AllureStep("Send \"get all projects\" request from an invalidly authorized user")]
    [AllureTms("tms", "suite=15&previewMode=modal&case=26")]
    public void Authentication_InvalidToken_Unauthorized()
    {
        _projectServiceUserWithInvalidToken.GetAllProjects().Wait();

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        RestClientExtended.LastCallResponse.Content.Should().Contain("API token is invalid");
    }

    [Test]
    [Category("Negative")]
    [AllureName("Authentication using no authentication set in client")]
    [AllureStep("Send \"get all projects\" request from an unauthorized user")]
    [AllureTms("tms", "suite=15&previewMode=modal&case=26")]
    public void Authentication_NoToken_Unauthorized()
    {
        _projectServiceUnauthorizedUser.GetAllProjects().Wait();

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        RestClientExtended.LastCallResponse.Content.Should().Contain("API token not provided");
    }
}
