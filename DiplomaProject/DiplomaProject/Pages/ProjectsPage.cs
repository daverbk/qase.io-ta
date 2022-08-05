using DiplomaProject.Configuration;
using DiplomaProject.Services.SeleniumServices;
using DiplomaProject.Wrappers;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;

namespace DiplomaProject.Pages;

public class ProjectsPage : BasePage
{
    private static readonly By CreateProjectButtonLocator = By.Id("createButton");
    private static readonly By ProjectsTableLocator = By.TagName("table");

    private static IWebElement CreateProjectButton => new WaitService().WaitUntilElementExists(CreateProjectButtonLocator);

    private static Table ProjectsTable => new(ProjectsTableLocator);
    
    protected override void ExecuteLoad()
    {
        DriverFactory.Driver.Navigate().GoToUrl(Configurator.AppSettings.BaseUiUrl + "/projects");
    }

    protected override bool EvaluateLoadedStatus()
    {
        return new WaitService().WaitUntilElementExists(CreateProjectButtonLocator).Displayed;
    }

    [AllureStep("Click \"Create new project\" button")]
    public CreateProjectPage ClickCreateNewProjectButton()
    {
        CreateProjectButton.Click();

        return new CreateProjectPage();
    }

    public static bool ProjectExistsInTable(string projectName)
    {
        if (ProjectsTableExists() == false)
        {
            return false;
        }
        
        return ProjectsTable.ProjectExists(projectName);
    }

    private static bool ProjectsTableExists()
    {
        var tableFoundIndicator = DriverFactory.Driver.FindElements(By.TagName("table"));
        
        return tableFoundIndicator.Count != 0;
    }
}
