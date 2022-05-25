using NUnit.Allure.Attributes;
using OpenQA.Selenium;

namespace DiplomaProject.Pages;

public class WelcomingPage : BasePage
{
    private static readonly By LoginButtonLocator = By.Id("signin");

    private IWebElement LoginButton => WaitService.WaitUntilElementExists(LoginButtonLocator);

    public WelcomingPage(IWebDriver driver) : base(driver)
    {
    }

    [AllureStep("Click \"Login\" button")]
    public AuthorizationPage ProceedToLoggingIn()
    {
        LoginButton.Click();

        return new AuthorizationPage(Driver);
    }

    protected override By GetPageIdentifier()
    {
        return LoginButtonLocator;
    }
}
