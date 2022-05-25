using Allure.Commons;
using DiplomaProject.Configuration;
using DiplomaProject.Pages;
using FluentAssertions;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework;

namespace DiplomaProject.Tests.UI;

[AllureNUnit]
[AllureParentSuite("UI")]
[AllureEpic("Authorization-UI")]
[AllureSeverity(SeverityLevel.blocker)]
[Category("Authorization-UI")]
public class AuthorizationTests : BaseUiTest
{
    private WelcomingPage _welcomingPage = null!;
    private AuthorizationPage _authorizationPage = null!;
    private ProjectsPage _projectsPage = null!;

    [SetUp]
    public void InstantiateRequiredPages()
    {
        _welcomingPage = new WelcomingPage(Driver);
        _authorizationPage = new AuthorizationPage(Driver);
        _projectsPage = new ProjectsPage(Driver);
    }

    [Test]
    [Category("Positive")]
    [AllureSuite("Authorization-UI")]
    [AllureName("Authorization using valid data")]
    [AllureTms("tms", "suite=9&previewMode=modal&case=20")]
    public void Authorization_PopulatedValidData_ProjectsPageOpened()
    {
        _welcomingPage
            .ProceedToLoggingIn()
            .PopulateAuthorizationData(Configurator.Admin.Email, Configurator.Admin.Password)
            .SubmitAuthorizationForm();

        _projectsPage.PageOpened.Should().BeTrue();
    }

    [Test]
    [Category("Negative")]
    [AllureSuite("Authorization-UI")]
    [AllureName("Authorization using invalid data")]
    [AllureTms("tms", "suite=9&previewMode=modal&case=20")]
    public void Authorization_PopulatedInvalidData_ErrorMessageDisplayed()
    {
        const string invalidEmail = "1111@sma.b";
        const string invalidPassword = "11111";

        _welcomingPage
            .ProceedToLoggingIn()
            .PopulateAuthorizationData(invalidEmail, invalidPassword)
            .SubmitAuthorizationForm();

        _authorizationPage.ErrorMessageDisplayed().Should().BeTrue();
    }

    [Test]
    [Category("Security")]
    [AllureSuite("SQL Injections")]
    [AllureStory("SQL Injections")]
    [TestCase("' or \""), TestCase("UNION ALL SELECT USER()--"), TestCase("admin' or 1=1")]
    [AllureName("Sql injections input into password field")]
    [AllureTms("tms", "suite=9&previewMode=modal&case=22")]
    public void Authorization_InsertedSqlInjection_ErrorMessageDisplayed(string sqlInjections)
    {
        _welcomingPage
            .ProceedToLoggingIn()
            .PopulateAuthorizationData(Configurator.Admin.Email, sqlInjections)
            .SubmitAuthorizationForm();

        _authorizationPage.ErrorMessageDisplayed().Should().BeTrue();
    }
}
