using OpenQA.Selenium;

namespace DiplomaProject.Pages;

public class AuthorizationPage : BasePage
{
    private static readonly By EmailFieldLocator = By.Id("inputEmail");
    private static readonly By PasswordFieldLocator = By.Id("inputPassword");
    private static readonly By LoginButtonLocator = By.Id("btnLogin");

    private IWebElement EmailField => WaitService.WaitUntilElementExists(EmailFieldLocator);
    
    private IWebElement PasswordField => WaitService.WaitUntilElementExists(PasswordFieldLocator);
    
    private IWebElement LoginButton => WaitService.WaitUntilElementExists(LoginButtonLocator);

    public AuthorizationPage(IWebDriver driver) : base(driver)
    {
    }

    public AuthorizationPage PopulateAuthorizationData(string login, string password)
    {
        EmailField.SendKeys(login);
        PasswordField.SendKeys(password);
        
        return this;
    }

    public void SubmitAuthorizationForm()
    {
        LoginButton.Click();
    }

    protected override By GetPageIdentifier()
    {
        return LoginButtonLocator;
    }
}
