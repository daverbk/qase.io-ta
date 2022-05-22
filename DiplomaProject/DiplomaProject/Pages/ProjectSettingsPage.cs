using DiplomaProject.Models;
using OpenQA.Selenium;

namespace DiplomaProject.Pages;

public class ProjectSettingsPage : BasePage
{
    private static readonly By TitleInputLocator = By.Id("inputTitle");
    private static readonly By CodeInputLocator = By.Id("inputCode");
    private static readonly By DescriptionInputLocator = By.Id("inputDescription");
    private static readonly By UpdateSettingsButtonLocator = By.CssSelector(".col button");
    private static readonly By UpdatedSuccessfullyAlertLocator = By.ClassName("alert-message");

    private IWebElement TitleInput => WaitService.WaitUntilElementExists(TitleInputLocator);

    private IWebElement CodeInput => WaitService.WaitUntilElementExists(CodeInputLocator);

    private IWebElement DescriptionInput => WaitService.WaitUntilElementExists(DescriptionInputLocator);

    private IWebElement UpdateSettingsButton => WaitService.WaitUntilElementExists(UpdateSettingsButtonLocator);

    private IWebElement UpdatedSuccessfullyAlert => WaitService.WaitQuickElement(UpdatedSuccessfullyAlertLocator);

    public ProjectSettingsPage(IWebDriver driver) : base(driver)
    {
    }

    public ProjectSettingsPage PopulateUpdatedProjectData(Project projectToUpdateWith)
    {
        TitleInput.Clear();
        TitleInput.SendKeys(projectToUpdateWith.Title);

        CodeInput.Clear();
        CodeInput.SendKeys(projectToUpdateWith.Code);

        DescriptionInput.Clear();
        DescriptionInput.SendKeys(projectToUpdateWith.Description);

        return this;
    }

    public void SubmitProjectForm()
    {
        UpdateSettingsButton.Click();
    }

    public bool AlertUpdatedSuccessfullyDisplayed()
    {
        return UpdatedSuccessfullyAlert.Displayed;
    }

    public string AlertUpdatedSuccessfullyMessage()
    {
        return UpdatedSuccessfullyAlert.Text;
    }

    public Project UpdatedData()
    {
        return new Project
        {
            Title = TitleInput.GetAttribute("value"),
            Code = CodeInput.GetAttribute("value"),
            Description = DescriptionInput.GetAttribute("value")
        };
    }

    protected override By GetPageIdentifier()
    {
        return UpdateSettingsButtonLocator;
    }
}
