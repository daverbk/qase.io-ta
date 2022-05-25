using DiplomaProject.Pages;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;

namespace DiplomaProject.Steps;

public class LoginStep
{
    private readonly IWebDriver _webDriver;
    private readonly WelcomingPage _welcomingPage;
    private readonly AuthorizationPage _authorizationPage;

    public LoginStep(IWebDriver webDriver)
    {
        _webDriver = webDriver;
        _welcomingPage = new WelcomingPage(_webDriver);
        _authorizationPage = new AuthorizationPage(_webDriver);
    }

    [AllureStep("Log in with {0} and {1}")]
    public ProjectsPage LogIn(string email, string password)
    {
        _welcomingPage
            .ProceedToLoggingIn();

        _authorizationPage
            .PopulateAuthorizationData(email, password)
            .SubmitAuthorizationForm();

        return new ProjectsPage(_webDriver);
    }
}
