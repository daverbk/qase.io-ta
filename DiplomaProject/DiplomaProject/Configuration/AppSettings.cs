namespace DiplomaProject.Configuration
{
    public class AppSettings
    {
        public string BaseUiUrl { get; init; } = string.Empty;
        
        public string BaseApiUrl { get; init; } = string.Empty;
        
        public string BrowserType { get; init; } = string.Empty;
        
        public int SeleniumWaitTimeout { get; init; }
    }
}
