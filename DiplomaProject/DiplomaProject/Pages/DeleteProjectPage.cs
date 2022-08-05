using DiplomaProject.Configuration;
using DiplomaProject.Services.SeleniumServices;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;

namespace DiplomaProject.Pages;

public class DeleteProjectPage : BasePage
{
    private static readonly By DeleteButtonLocator = By.ClassName("btn-cancel");

    // DEVNOTE: Use ProjectName property to form correct url to access specific project's settings page.
    public string ProjectName { private get; set; } = null!;
    
    private static IWebElement DeleteButton => new WaitService().WaitUntilElementExists(DeleteButtonLocator);
    
    protected override void ExecuteLoad()
    {
        DriverFactory.Driver.Navigate().GoToUrl(Configurator.AppSettings.BaseUiUrl + $"/project/{ProjectName}/delete");
    }

    protected override bool EvaluateLoadedStatus()
    {
        return new WaitService().WaitUntilElementExists(DeleteButtonLocator).Displayed;
    }

    [AllureStep("Confirm project deletion")]
    public void ConfirmDeletion()
    {
        DeleteButton.Click();
    }
}
