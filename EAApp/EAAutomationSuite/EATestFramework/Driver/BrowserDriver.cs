using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using OpenQA.Selenium.Edge;

namespace EATestFramework.Driver
{
    public class BrowserDriver : IBrowserDriver
    {
        public IWebDriver GetChromeDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            return new ChromeDriver();
        }

        public IWebDriver GetMicrosoftEdgeDriver()
        {
            new DriverManager().SetUpDriver(new EdgeConfig());
            return new EdgeDriver();
        }
    }
}

public enum BrowserType
{
    Chrome,
    Edge
}
