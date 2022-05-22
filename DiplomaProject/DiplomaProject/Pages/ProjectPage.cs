using OpenQA.Selenium;

namespace DiplomaProject.Pages;

public class ProjectPage : BasePage
{
    private static readonly By ProjectTitleLocator = By.CssSelector(".d-flex p.header");
    
    private IWebElement ProjectTitle => WaitService.WaitUntilElementExists(ProjectTitleLocator);
    
    public ProjectPage(IWebDriver driver) : base(driver)
    {
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
