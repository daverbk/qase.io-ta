using DiplomaProject.Services.SeleniumServices;
using OpenQA.Selenium;

namespace DiplomaProject.Pages
{
    public abstract class BasePage
    {
        protected IWebDriver Driver { get; }

        protected static WaitService WaitService { get; private set; } = null!;

        public bool PageOpened => WaitService.WaitUntilElementExists(GetPageIdentifier()).Displayed;

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
            WaitService = new WaitService(Driver);
        }

        protected abstract By GetPageIdentifier();
    }
}
