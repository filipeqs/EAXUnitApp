using OpenQA.Selenium;
using EATestFramework.Driver;
using EATestProject.Models;
using EATestFramework.Extensions;

namespace EATestProject.Pages
{
    public interface ICreateProductPage
    {
        void CreateProduct();
        void EnterProductDetails(Product product);
    }

    public class CreateProductPage : ICreateProductPage
    {
        private readonly IWebDriver _driver;

        public CreateProductPage(IDriverFixture driverFixture)
        {
            _driver = driverFixture.Driver;
        }

        IWebElement txtName => _driver.FindElement(By.Name("Name"));
        IWebElement txtDescription => _driver.FindElement(By.Name("Description"));
        IWebElement txtPrice => _driver.FindElement(By.Name("Price"));
        IWebElement ddlProductType => _driver.FindElement(By.Id("ProductType"));
        IWebElement btnCreate => _driver.FindElement(By.Id("Create"));

        public void EnterProductDetails(Product product)
        {
            txtName.SendKeys(product.Name);
            txtDescription.SendKeys(product.Description);
            txtPrice.SendKeys(product.Price.ToString());
            ddlProductType.SelectDropDownByText(product.ProductType.ToString());
        }

        public void CreateProduct()
        {
            btnCreate.Click();
        }
    }
}
