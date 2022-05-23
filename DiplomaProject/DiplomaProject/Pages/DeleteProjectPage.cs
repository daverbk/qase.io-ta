using OpenQA.Selenium;

namespace DiplomaProject.Pages;

public class DeleteProjectPage : BasePage
{
    private static readonly By DeleteButtonLocator = By.ClassName("btn-cancel");

    private IWebElement DeleteButton => WaitService.WaitUntilElementExists(DeleteButtonLocator);

    public DeleteProjectPage(IWebDriver driver) : base(driver)
    {
    }

    public void ConfirmDeletion()
    {
        DeleteButton.Click();
    }

    protected override By GetPageIdentifier()
    {
        return DeleteButtonLocator;
    }
}
