using DiplomaProject.Services.SeleniumServices;
using NUnit.Framework;

namespace DiplomaProject.Tests;

public class BaseUiTest
{
    [OneTimeSetUp]
    public void InitializeTests()
    {
        DriverFactory.InitBrowser();
    }
    
    [OneTimeTearDown]
    public void QuitBrowser()
    {
        DriverFactory.Driver.Value!.Quit();
    }
}
