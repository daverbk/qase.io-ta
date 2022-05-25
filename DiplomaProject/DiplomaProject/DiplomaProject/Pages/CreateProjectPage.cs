using DiplomaProject.Models;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;

namespace DiplomaProject.Pages;

public class CreateProjectPage : BasePage
{
    private static readonly By TitleInputLocator = By.Id("inputTitle");
    private static readonly By CodeInputLocator = By.Id("inputCode");
    private static readonly By CreateProjectButtonLocator = By.CssSelector(".col button");

    private IWebElement TitleInput => WaitService.WaitUntilElementExists(TitleInputLocator);

    private IWebElement CodeInput => WaitService.WaitUntilElementExists(CodeInputLocator);

    private IWebElement CreateProjectButton => WaitService.WaitUntilElementExists(CreateProjectButtonLocator);

    public CreateProjectPage(IWebDriver driver) : base(driver)
    {
    }

    [AllureStep("Populate project data")]
    public CreateProjectPage PopulateProjectData(Project projectToAdd)
    {
        TitleInput.SendKeys(projectToAdd.Title);
        CodeInput.SendKeys(projectToAdd.Code);

        return this;
    }

    [AllureStep("Submit project form")]
    public void SubmitProjectForm()
    {
        CreateProjectButton.Click();
    }

    protected override By GetPageIdentifier()
    {
        return CreateProjectButtonLocator;
    }
}
