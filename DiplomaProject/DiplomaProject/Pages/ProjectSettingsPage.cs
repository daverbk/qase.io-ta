using DiplomaProject.Configuration;
using DiplomaProject.Models;
using DiplomaProject.Services.SeleniumServices;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;

namespace DiplomaProject.Pages;

public class ProjectSettingsPage : BasePage
{
    private static readonly By TitleInputLocator = By.Id("inputTitle");
    private static readonly By CodeInputLocator = By.Id("inputCode");
    private static readonly By UpdateSettingsButtonLocator = By.CssSelector(".col button");
    private static readonly By AlertLocator = By.CssSelector("[role='alert']");
    private static readonly By DeleteProjectButtonLocator = By.ClassName("btn-cancel");

    // DEVNOTE: Use ProjectName property to form correct url to access specific project's settings page.
    public string ProjectName { private get; set; } = null!;

    private static IWebElement TitleInput => new WaitService().WaitUntilElementExists(TitleInputLocator);

    private static IWebElement CodeInput => new WaitService().WaitUntilElementExists(CodeInputLocator);

    private static IWebElement UpdateSettingsButton => new WaitService().WaitUntilElementExists(UpdateSettingsButtonLocator);

    private static IWebElement Alert => new WaitService().WaitQuickElement(AlertLocator);

    private static IWebElement DeleteProjectButton => new WaitService().WaitUntilElementExists(DeleteProjectButtonLocator);

    protected override void ExecuteLoad()
    {
        DriverFactory.Driver.Navigate().GoToUrl(Configurator.AppSettings.BaseUiUrl + $"/project/{ProjectName}/settings/general");
    }

    protected override bool EvaluateLoadedStatus()
    {
        return new WaitService().WaitUntilElementExists(UpdateSettingsButtonLocator).Displayed;
    }

    [AllureStep("Populate updated project data")]
    public ProjectSettingsPage PopulateUpdatedProjectData(Project projectToUpdateWith)
    {
        TitleInput.Clear();
        TitleInput.SendKeys(projectToUpdateWith.Title);

        CodeInput.Clear();
        CodeInput.SendKeys(projectToUpdateWith.Code);

        return this;
    }

    [AllureStep("Submit updated project form")]
    public void SubmitProjectForm()
    {
        UpdateSettingsButton.Click();
    }

    public static bool AlertDisplayed()
    {
        return Alert.Displayed;
    }

    public static string AlertMessage()
    {
        return Alert.Text;
    }

    public static Project UpdatedData()
    {
        return new Project
        {
            Title = TitleInput.GetAttribute("value"),
            Code = CodeInput.GetAttribute("value"),
        };
    }

    [AllureStep("Click the delete option")]
    public static DeleteProjectPage DeleteProject()
    {
        DeleteProjectButton.Click();

        return new DeleteProjectPage();
    }
}
