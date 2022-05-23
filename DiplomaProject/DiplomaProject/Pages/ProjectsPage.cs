using DiplomaProject.Wrappers;
using OpenQA.Selenium;

namespace DiplomaProject.Pages;

public class ProjectsPage : BasePage
{
    private static readonly By CreateProjectButtonLocator = By.Id("createButton");
    private static readonly By ProjectsTableLocator = By.TagName("table");

    private IWebElement CreateProjectButton => WaitService.WaitUntilElementExists(CreateProjectButtonLocator);

    private static Table ProjectsTable => new Table(Driver, ProjectsTableLocator);

    public ProjectsPage(IWebDriver driver) : base(driver)
    {
    }

    public CreateProjectPage ClickCreateNewProjectButton()
    {
        CreateProjectButton.Click();

        return new CreateProjectPage(Driver);
    }

    public bool ProjectExistsInProjectsTable(string projectName)
    {
        return ProjectsTable.ProjectExists(projectName);
    }

    protected override By GetPageIdentifier()
    {
        return CreateProjectButtonLocator;
    }
}
