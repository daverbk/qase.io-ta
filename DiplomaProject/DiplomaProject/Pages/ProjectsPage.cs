using OpenQA.Selenium;

namespace DiplomaProject.Pages;

public class ProjectsPage : BasePage
{
    private static readonly By CreateProjectButtonLocator = By.Id("createButton");
    
    private IWebElement CreateProjectButton => WaitService.WaitUntilElementExists(CreateProjectButtonLocator);
    
    public ProjectsPage(IWebDriver driver) : base(driver)
    {
    }
   
    protected override By GetPageIdentifier()
    {
        return CreateProjectButtonLocator;
    }
}
