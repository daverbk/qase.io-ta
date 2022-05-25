using NUnit.Allure.Attributes;
using OpenQA.Selenium;

namespace DiplomaProject.Pages;

public class AuthorizationPage : BasePage
{
    private static readonly By EmailFieldLocator = By.Id("inputEmail");
    private static readonly By PasswordFieldLocator = By.Id("inputPassword");
    private static readonly By LoginButtonLocator = By.Id("btnLogin");
    private static readonly By AuthorizationErrorMessageLocator = By.ClassName("form-control-feedback");

    private IWebElement EmailField => WaitService.WaitUntilElementExists(EmailFieldLocator);

    private IWebElement PasswordField => WaitService.WaitUntilElementExists(PasswordFieldLocator);

    private IWebElement LoginButton => WaitService.WaitUntilElementExists(LoginButtonLocator);

    private IWebElement AuthorizationErrorMessage => WaitService.WaitUntilElementExists(AuthorizationErrorMessageLocator);

    public AuthorizationPage(IWebDriver driver) : base(driver)
    {
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
    
    public bool ErrorMessageDisplayed()
    {
        return AuthorizationErrorMessage.Displayed;
    }

    protected override By GetPageIdentifier()
    {
        return LoginButtonLocator;
    }
}
