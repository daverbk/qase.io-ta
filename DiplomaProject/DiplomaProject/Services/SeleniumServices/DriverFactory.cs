using System;
using DiplomaProject.Configuration;
using OpenQA.Selenium;

namespace DiplomaProject.Services.SeleniumServices
{
    public class DriverFactory
    {
        private static IWebDriver _driver = null!;

        public static IWebDriver Driver
        {
            get
            {
                if(_driver == null)
                    throw new NullReferenceException("The WebDriver browser instance was not initialized. You should first call the method InitBrowser.");
                return _driver;
            }
            
            private set => _driver = value;
        }

        public static void InitBrowser()
        {
            Driver = Configurator.AppSettings.BrowserType switch
            {
                "chrome" => DriverSetUp.GetChromeDriver(),
                "firefox" => DriverSetUp.GetFirefoxDriver(),
                _ => throw new ArgumentException("Check that your BrowserType property in appsettings.json is set to either chrome or firefox.")
            };

            Driver.Manage().Window.Maximize();
            Driver.Manage().Cookies.DeleteAllCookies();
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
        }
    }
}
