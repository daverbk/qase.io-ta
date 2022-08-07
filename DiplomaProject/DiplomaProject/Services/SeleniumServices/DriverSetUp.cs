using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace DiplomaProject.Services.SeleniumServices
{
    public class DriverSetUp
    {
        public static ThreadLocal<IWebDriver> GetChromeDriver()
        {
            var chromeOptions = new ChromeOptions();
            
            chromeOptions.AddArguments("--disable-gpu");
            chromeOptions.AddArguments("--disable-extensions");

            chromeOptions.SetLoggingPreference(LogType.Browser, LogLevel.All);
            chromeOptions.SetLoggingPreference(LogType.Driver, LogLevel.All);

            new DriverManager().SetUpDriver(new ChromeConfig());
            return new ThreadLocal<IWebDriver>(() => new ChromeDriver(chromeOptions));
        }

        public static ThreadLocal<IWebDriver> GetFirefoxDriver()
        {
            var mimeTypes =
                "image/png,image/gif,image/jpeg,image/pjpeg,application/pdf,text/csv,application/vnd.ms-excel," +
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" +
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            
            var ffOptions = new FirefoxOptions();
            var ffProfile = new FirefoxProfile();
            
            ffProfile.SetPreference("browser.download.folderList", 2);
            ffProfile.SetPreference("browser.helperApps.neverAsk.saveToDisk", mimeTypes);
            ffProfile.SetPreference("browser.helperApps.neverAsk.openFile", mimeTypes);
            ffOptions.Profile = ffProfile;
            
            ffOptions.SetLoggingPreference(LogType.Browser, LogLevel.All);
            ffOptions.SetLoggingPreference(LogType.Driver, LogLevel.All);

            new DriverManager().SetUpDriver(new FirefoxConfig());
            return new ThreadLocal<IWebDriver>(() => new FirefoxDriver(ffOptions));
        }
    }
}
