using System.Net;
using DiplomaProject.Clients;
using DiplomaProject.Configuration.Enums;
using DiplomaProject.Services.ApiServices;
using FluentAssertions;
using NUnit.Framework;

namespace DiplomaProject.Tests;

[Category("Authentication-API")]
public class ApiAuthenticationTest : BaseTest
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
    [Category("Negative")]
    public void RequestInvalidAuthentication()
    {
        _projectServiceUserWithInvalidToken.GetAllProjects().Wait();

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        RestClientExtended.LastCallResponse.Content.Should().Contain("API token is invalid");
    }
    
    [Test]
    [Category("Negative")]
    public void RequestNoAuthentication()
    {
        _projectServiceUnauthorizedUser.GetAllProjects().Wait();

        RestClientExtended.LastCallResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        RestClientExtended.LastCallResponse.Content.Should().Contain("API token not provided");
    }
}
