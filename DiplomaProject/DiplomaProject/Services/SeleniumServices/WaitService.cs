using System;
using DiplomaProject.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DiplomaProject.Services.SeleniumServices
{
    public class WaitService
    {
        private readonly WebDriverWait _explicitWait;
        private readonly DefaultWait<IWebDriver> _fluentWait;

        public WaitService(IWebDriver driver)
        {
            _explicitWait =
                new WebDriverWait(driver, TimeSpan.FromSeconds(Configurator.AppSettings.SeleniumWaitTimeout));

            _fluentWait = new DefaultWait<IWebDriver>(driver)
            {
                Timeout = TimeSpan.FromSeconds(Configurator.AppSettings.SeleniumWaitTimeout),
                PollingInterval = TimeSpan.FromMilliseconds(250)
            };
            _fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        }

        public IWebElement WaitUntilElementExists(By locator)
        {
            return _explicitWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
        }
    }
}
