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

        public WaitService()
        {
            _explicitWait =
                new WebDriverWait(DriverFactory.Driver.Value!, TimeSpan.FromSeconds(Configurator.AppSettings.SeleniumWaitTimeout));

            _fluentWait = new DefaultWait<IWebDriver>(DriverFactory.Driver.Value!)
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
        
        public IWebElement WaitUntilElementIsClickable(IWebElement webElement)
        {
            return _explicitWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(webElement));
        }
        
        public IWebElement WaitQuickElement(By locator)
        {
            return _fluentWait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
        }
    }
}
