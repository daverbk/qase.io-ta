using DiplomaProject.Configuration;
using DiplomaProject.Services.SeleniumServices;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;

namespace DiplomaProject.Pages;

public class ProjectPage : BasePage
{
    private static readonly By ProjectTitleLocator = By.XPath("//*[@id='layout']//img/following-sibling::*");
    private static readonly By SettingsButtonLocator = By.XPath("//span[text() = 'Settings']");

    // DEVNOTE: Use ProjectName property to form correct url to access specific project's settings page.
    public string ProjectName { private get; set; } = null!;
    
    private static IWebElement ProjectTitle => new WaitService().WaitUntilElementExists(ProjectTitleLocator);

    private static IWebElement SettingsButton => new WaitService().WaitUntilElementExists(SettingsButtonLocator);

    protected override void ExecuteLoad()
    {
        DriverFactory.Driver.Value!.Navigate().GoToUrl(Configurator.AppSettings.BaseUiUrl + $"/project/{ProjectName}?view=1&suite=15");
    }

    protected override bool EvaluateLoadedStatus()
    {
        return new WaitService().WaitUntilElementExists(ProjectTitleLocator).Displayed;
    }

    [AllureStep("Click \"settings\" section")]
    public static ProjectSettingsPage NavigateToSettings()
    {
        SettingsButton.Click();

        return new ProjectSettingsPage();
    }

    public static string ProjectTitleText()
    {
        return ProjectTitle.Text;
    }
}
