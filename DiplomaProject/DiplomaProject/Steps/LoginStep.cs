using DiplomaProject.Pages;
using OpenQA.Selenium;

namespace DiplomaProject.Steps;

public class LoginStep
{
    private readonly IWebDriver _webDriver;
    private readonly WelcomingPage _welcomingPage;
    private AuthorizationPage _authorizationPage;

    public LoginStep(IWebDriver webDriver)
    {
        _webDriver = webDriver;
        _welcomingPage = new WelcomingPage(_webDriver);
        _authorizationPage = new AuthorizationPage(_webDriver);
    }

    public ProjectsPage LogIn(string email, string password)
    {
        _welcomingPage
            .ProceedToLoggingIn()
            .PopulateAuthorizationData(email, password)
            .SubmitAuthorizationForm();

        return new ProjectsPage(_webDriver);
    }
}
