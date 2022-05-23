using System;
using DiplomaProject.Services.SeleniumServices;
using OpenQA.Selenium;

namespace DiplomaProject.Pages
{
    public abstract class BasePage
    {
        [field: ThreadStatic] 
        protected static IWebDriver Driver { get; private set; } = null!;

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
