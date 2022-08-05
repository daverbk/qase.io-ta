using DiplomaProject.Configuration;
using DiplomaProject.Pages;
using DiplomaProject.Services.SeleniumServices;
using NUnit.Allure.Attributes;

namespace DiplomaProject.Steps;

public class LoginStep
{
    [AllureStep("Log in with {0} and {1}")]
    public ProjectsPage LogIn(string email, string password)
    {
        DriverFactory.Driver.Navigate().GoToUrl(Configurator.AppSettings.BaseUiUrl + "/login");

        new AuthorizationPage()
            .PopulateAuthorizationData(email, password)
            .SubmitAuthorizationForm();

        return new ProjectsPage();
    }
}
