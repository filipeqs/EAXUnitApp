using EATestFramework.Settings;
using OpenQA.Selenium;

namespace EATestFramework.Driver
{
    public class DriverFixture : IDriverFixture, IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly TestSettings _testSettings;
        private readonly IBrowserDriver _browserDriver;

        public DriverFixture(TestSettings testSettings, IBrowserDriver browserDriver)
        {
            _testSettings = testSettings;
            _browserDriver = browserDriver;
            _driver = GetDriver();
            _driver.Navigate().GoToUrl(testSettings.ApplicationUrl);
        }

        public IWebDriver Driver => _driver;

        private IWebDriver GetDriver()
        {
            return _testSettings.BrowserType switch
            {
                BrowserType.Chrome => _browserDriver.GetChromeDriver(),
                BrowserType.Edge => _browserDriver.GetMicrosoftEdgeDriver(),
                _ => _browserDriver.GetChromeDriver()
            };
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
