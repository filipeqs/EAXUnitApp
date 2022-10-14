using EATestFramework.Driver;
using EATestFramework.Extensions;
using OpenQA.Selenium;

namespace EATestProject.Pages
{
    public interface IHomePage
    {
        void GoToCreateProductPage();
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

        public void GoToCreateProductPage()
        {
            linkProduct.Click();
            linkCreate.Click();
        }

        public void PerformClickOnOperation(string name, string operation)
        {
            tblList.PerformActionOnCell("5", "Name", name, operation);
        }
    }
}
