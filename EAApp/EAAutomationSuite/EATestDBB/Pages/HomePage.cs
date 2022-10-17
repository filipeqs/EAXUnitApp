using EATestFramework.Driver;
using EATestFramework.Extensions;
using OpenQA.Selenium;

namespace EATestDBB.Pages
{
    public interface IHomePage
    {
        void ClickProductMenu();
        void ClickCreateProductLink();
        void PerformClickOnOperation(string name, string operation);
    }

    public class HomePage : IHomePage
    {
        private readonly IWebDriver _driver;

        public HomePage(IDriverFixture driverFixture)
        {
            _driver = driverFixture.Driver;
        }

        IWebElement linkProduct => _driver.FindElement(By.LinkText("Product"));
        IWebElement linkCreate => _driver.FindElement(By.LinkText("Create"));
        IWebElement tblList => _driver.FindElement(By.ClassName("table"));

        public void ClickProductMenu() => linkProduct.Click();

        public void ClickCreateProductLink() => linkCreate.Click();

        public void PerformClickOnOperation(string name, string operation)
        {
            tblList.PerformActionOnCell("5", "Name", name, operation);
        }
    }
}
