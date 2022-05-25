using NUnit.Allure.Attributes;
using OpenQA.Selenium;

namespace DiplomaProject.Pages;

public class ProjectPage : BasePage
{
    private static readonly By ProjectTitleLocator = By.CssSelector(".d-flex p.header");
    private static readonly By SettingsButtonLocator = By.XPath("//span[text() = 'Settings']");

    private IWebElement ProjectTitle => WaitService.WaitUntilElementExists(ProjectTitleLocator);

    private IWebElement SettingsButton => WaitService.WaitUntilElementExists(SettingsButtonLocator);

    public ProjectPage(IWebDriver driver) : base(driver)
    {
    }

    [AllureStep("Click \"settings\" section")]
    public ProjectSettingsPage NavigateToSettings()
    {
        SettingsButton.Click();

        return new ProjectSettingsPage(Driver);
    }

    public string ProjectTitleText()
    {
        return ProjectTitle.Text;
    }

    protected override By GetPageIdentifier()
    {
        return ProjectTitleLocator;
    }
}
