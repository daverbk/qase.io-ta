using DiplomaProject.Configuration;
using DiplomaProject.Models;
using DiplomaProject.Services.SeleniumServices;
using NUnit.Allure.Attributes;
using OpenQA.Selenium;

namespace DiplomaProject.Pages;

public class CreateProjectPage : BasePage
{
    private static readonly By TitleInputLocator = By.Id("inputTitle");
    private static readonly By CodeInputLocator = By.Id("inputCode");
    private static readonly By CreateProjectButtonLocator = By.CssSelector(".col button");

    private static IWebElement TitleInput => new WaitService().WaitUntilElementExists(TitleInputLocator);

    private static IWebElement CodeInput => new WaitService().WaitUntilElementExists(CodeInputLocator);

    private static IWebElement CreateProjectButton => new WaitService().WaitUntilElementExists(CreateProjectButtonLocator);

    protected override void ExecuteLoad()
    {
        DriverFactory.Driver.Value!.Navigate().GoToUrl(Configurator.AppSettings.BaseUiUrl + "/project/create");
    }

    protected override bool EvaluateLoadedStatus()
    {
        return new WaitService().WaitUntilElementExists(CreateProjectButtonLocator).Displayed;
    }

    [AllureStep("Populate project data")]
    public CreateProjectPage PopulateProjectData(Project projectToAdd)
    {
        TitleInput.SendKeys(projectToAdd.Title);
        CodeInput.SendKeys(projectToAdd.Code);

        return new CreateProjectPage();
    }

    [AllureStep("Submit project form")]
    public void SubmitProjectForm()
    {
        CreateProjectButton.Click();
    }
}
