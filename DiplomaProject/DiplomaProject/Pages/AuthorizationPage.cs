using DiplomaProject.Configuration;
using DiplomaProject.Services.SeleniumServices;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;

namespace DiplomaProject.Pages;

public class AuthorizationPage : BasePage
{
    private static readonly By EmailFieldLocator = By.Id("inputEmail");
    private static readonly By PasswordFieldLocator = By.Id("inputPassword");
    private static readonly By LoginButtonLocator = By.Id("btnLogin");
    private static readonly By AuthorizationErrorMessageLocator = By.ClassName("form-control-feedback");

    private static IWebElement EmailField => new WaitService().WaitUntilElementExists(EmailFieldLocator);

    private static IWebElement PasswordField => new WaitService().WaitUntilElementExists(PasswordFieldLocator);

    private static IWebElement LoginButton => new WaitService().WaitUntilElementExists(LoginButtonLocator);

    private static IWebElement AuthorizationErrorMessage => new WaitService().WaitUntilElementExists(AuthorizationErrorMessageLocator);

    protected override void ExecuteLoad()
    {
        DriverFactory.Driver.Value!.Navigate().GoToUrl(Configurator.AppSettings.BaseUiUrl + "/login");
    }

    protected override bool EvaluateLoadedStatus()
    {
        return new WaitService().WaitUntilElementExists(LoginButtonLocator).Displayed;
    }

    [AllureStep("Populate authorization data with: login {0} password {1}")]
    public AuthorizationPage PopulateAuthorizationData(string login, string password)
    {
        EmailField.SendKeys(login);
        PasswordField.SendKeys(password);

        return this;
    }

    [AllureStep("Submit authorization form")]
    public void SubmitAuthorizationForm()
    {
        LoginButton.Click();
    }
    
    public static bool ErrorMessageDisplayed()
    {
        return AuthorizationErrorMessage.Displayed;
    }
    
    public static string ErrorMessageText()
    {
        return AuthorizationErrorMessage.Text.Normalize();
    }
}
