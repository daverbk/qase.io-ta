using System;
using System.Collections.Concurrent;
using DiplomaProject.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;

namespace DiplomaProject.Services.SeleniumServices
{
    public class DriverFactory
    {
        private static readonly ConcurrentDictionary<string, IWebDriver> DriverCollection = new();

        public static IWebDriver Driver
        {
            get
            {
                DriverCollection.TryGetValue(TestContext.CurrentContext.Test.ClassName!, out var driver);
                
                return driver!;
            }

            private set => DriverCollection.TryAdd(TestContext.CurrentContext.Test.ClassName!, value);
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
