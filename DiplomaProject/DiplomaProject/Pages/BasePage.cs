using DiplomaProject.Services.SeleniumServices;
using OpenQA.Selenium.Support.UI;

namespace DiplomaProject.Pages
{
    public abstract class BasePage : LoadableComponent<BasePage>
    {
        public bool IsOpened => IsLoaded;
    }
}
