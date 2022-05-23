using DiplomaProject.Models;
using OpenQA.Selenium;

namespace DiplomaProject.Pages;

public class ProjectSettingsPage : BasePage
{
    private static readonly By TitleInputLocator = By.Id("inputTitle");
    private static readonly By CodeInputLocator = By.Id("inputCode");
    private static readonly By DescriptionInputLocator = By.Id("inputDescription");
    private static readonly By UpdateSettingsButtonLocator = By.CssSelector(".col button");
    private static readonly By AlertLocator = By.ClassName("alert-message");
    private static readonly By DeleteProjectButtonLocator = By.ClassName("btn-cancel");

    private IWebElement TitleInput => WaitService.WaitUntilElementExists(TitleInputLocator);

    private IWebElement CodeInput => WaitService.WaitUntilElementExists(CodeInputLocator);

    private IWebElement DescriptionInput => WaitService.WaitUntilElementExists(DescriptionInputLocator);

    private IWebElement UpdateSettingsButton => WaitService.WaitUntilElementExists(UpdateSettingsButtonLocator);

    private IWebElement Alert => WaitService.WaitQuickElement(AlertLocator);

    private IWebElement DeleteProjectButton => WaitService.WaitUntilElementExists(DeleteProjectButtonLocator);

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

    public bool AlertDisplayed()
    {
        return Alert.Displayed;
    }

    public string AlertMessage()
    {
        return Alert.Text;
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

    public DeleteProjectPage DeleteProject()
    {
        DeleteProjectButton.Click();

        return new DeleteProjectPage(Driver);
    }

    protected override By GetPageIdentifier()
    {
        return UpdateSettingsButtonLocator;
    }
}
