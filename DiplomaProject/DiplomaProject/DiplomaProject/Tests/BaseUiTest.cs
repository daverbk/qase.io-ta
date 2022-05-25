using System;
using DiplomaProject.Configuration;
using DiplomaProject.Services.SeleniumServices;
using NUnit.Framework;
using OpenQA.Selenium;

namespace DiplomaProject.Tests;

public class BaseUiTest
{
    [field: ThreadStatic] 
    protected static IWebDriver Driver { get; private set; } = null!;

    [SetUp]
    public void OpenBrowserAtWelcomingPage()
    {
        DriverFactory.InitBrowser();
        Driver = DriverFactory.Driver;

        DriverFactory.Driver.Navigate().GoToUrl(Configurator.AppSettings.BaseUiUrl);
    }

    [TearDown]
    public void QuitBrowser()
    {
        Driver.Quit();
    }
}
