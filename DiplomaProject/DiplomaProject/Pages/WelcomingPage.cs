using DiplomaProject.Configuration;
using DiplomaProject.Services.SeleniumServices;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;

namespace DiplomaProject.Pages;

public class WelcomingPage : BasePage
{
    private static readonly By LoginButtonLocator = By.Id("signin");

    private static IWebElement LoginButton => new WaitService().WaitUntilElementExists(LoginButtonLocator);

    protected override void ExecuteLoad()
    {
        DriverFactory.Driver.Navigate().GoToUrl(Configurator.AppSettings.BaseUiUrl);
    }

    protected override bool EvaluateLoadedStatus()
    {
        return new WaitService().WaitUntilElementExists(LoginButtonLocator).Displayed;
    }

    [AllureStep("Click \"Login\" button")]
    public static AuthorizationPage ProceedToLoggingIn()
    {
        LoginButton.Click();

        return new AuthorizationPage();
    }
}
