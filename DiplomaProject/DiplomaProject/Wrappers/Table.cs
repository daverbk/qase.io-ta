using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;

namespace DiplomaProject.Wrappers
{
    public class Table
    {
        private readonly BaseElementWrapper _baseElementWrapper;

        public bool Displayed => _baseElementWrapper.Displayed;

        private ReadOnlyCollection<IWebElement> Headers => _baseElementWrapper.FindElements(By.CssSelector("thead th"));

        private ReadOnlyCollection<IWebElement> Rows => _baseElementWrapper.FindElements(By.CssSelector("tbody tr"));

        private ReadOnlyCollection<IWebElement> Cells(IWebElement row) => row.FindElements(By.TagName("td"));

        public Table(IWebDriver driver, By @by)
        {
            _baseElementWrapper = new BaseElementWrapper(driver, by);
        }

        public bool ProjectExists(string projectTitle)
        {
            const string projectNameColumnHeader = "Project name";

            var projectTitleColumnIndex =
                Headers.TakeWhile(header => !header.Text.Normalize().Equals(projectNameColumnHeader)).Count();

            foreach (var row in Rows)
            {
                var cells = Cells(row);
                var projectNameElement = cells[projectTitleColumnIndex].FindElement(By.CssSelector("a.defect-title"));

                if (projectNameElement.Text == projectTitle)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
