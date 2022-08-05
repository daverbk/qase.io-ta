using Allure.Commons;
using DiplomaProject.Configuration;
using DiplomaProject.Pages;
using FluentAssertions;
using FluentAssertions.Execution;
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
    [Test]
    [Category("Positive")]
    [AllureSuite("Authorization-UI")]
    [AllureName("Authorization using valid data")]
    [AllureTms("tms", "suite=9&previewMode=modal&case=20")]
    public void Authorization_PopulatedValidData_ProjectsPageOpened()
    {
        new WelcomingPage().Load();

        WelcomingPage
            .ProceedToLoggingIn()
            .PopulateAuthorizationData(Configurator.Admin.Email, Configurator.Admin.Password)
            .SubmitAuthorizationForm();

        new ProjectsPage().IsOpened.Should().BeTrue();
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

        new WelcomingPage().Load();
        
        WelcomingPage
            .ProceedToLoggingIn()
            .PopulateAuthorizationData(invalidEmail, invalidPassword)
            .SubmitAuthorizationForm();

        using (new AssertionScope())
        {
            AuthorizationPage.ErrorMessageDisplayed().Should().BeTrue();
            AuthorizationPage.ErrorMessageText().Should().Be("These credentials do not match our records.");
        }
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
        new WelcomingPage().Load();
        
        WelcomingPage
            .ProceedToLoggingIn()
            .PopulateAuthorizationData(Configurator.Admin.Email, sqlInjections)
            .SubmitAuthorizationForm();

        using (new AssertionScope())
        {
            AuthorizationPage.ErrorMessageDisplayed().Should().BeTrue();
            AuthorizationPage.ErrorMessageText().Should().Be("These credentials do not match our records.");  
        }
    }
}
