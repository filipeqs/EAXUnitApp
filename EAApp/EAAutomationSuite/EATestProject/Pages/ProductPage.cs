using OpenQA.Selenium;
using EATestFramework.Driver;
using EATestProject.Models;
using EATestFramework.Extensions;

namespace EATestProject.Pages
{
    public interface IProductPage
    {
        void CreateProduct();
        void EnterProductDetails(Product product);
        Product GetProductDetails();
    }

    public class ProductPage : IProductPage
    {
        private readonly IWebDriver _driver;

        public ProductPage(IDriverFixture driverFixture)
        {
            _driver = driverFixture.Driver;
        }

        IWebElement txtName => _driver.FindElement(By.Id("Name"));
        IWebElement txtDescription => _driver.FindElement(By.Id("Description"));
        IWebElement txtPrice => _driver.FindElement(By.Id("Price"));
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

        public Product GetProductDetails()
        {
            return new Product
            {
                Name = txtName.Text,
                Description = txtDescription.Text,
                Price = int.Parse(txtPrice.Text),
                ProductType = (ProductType)Enum.Parse(
                        typeof(ProductType), 
                        ddlProductType.GetAttribute("innerText").ToString()
                    ),

            };
        }
    }
}
