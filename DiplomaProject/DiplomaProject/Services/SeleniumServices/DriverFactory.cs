using System;
using System.Collections.Concurrent;
using System.Threading;
using DiplomaProject.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;

namespace DiplomaProject.Services.SeleniumServices
{
    public class DriverFactory
    {
        [field: ThreadStatic]
        public static ThreadLocal<IWebDriver> Driver { get; private set; } = null!;

        public static void InitBrowser()
        {
            Driver = Configurator.AppSettings.BrowserType switch
            {
                "chrome" => DriverSetUp.GetChromeDriver(),
                "firefox" => DriverSetUp.GetFirefoxDriver(),
                _ => throw new ArgumentException("Check that your BrowserType property in appsettings.json is set to either chrome or firefox.")
            };

            Driver.Value!.Manage().Window.Maximize();
            Driver.Value!.Manage().Cookies.DeleteAllCookies();
            Driver.Value!.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
        }
    }
}
