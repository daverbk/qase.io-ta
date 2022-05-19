namespace DiplomaProject.Configuration
{
    public class AppSettings
    {
        public string BaseUrl { get; init; } = string.Empty;
        
        public string BrowserType { get; init; } = string.Empty;
        
        public int SeleniumWaitTimeout { get; init; }
    }
}
