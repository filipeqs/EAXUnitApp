using OpenQA.Selenium;
using EATestFramework.Driver;
using EATestFramework.Extensions;
using ProductAPI.Data;

namespace EATestDBB.Pages
{
    public interface IProductPage
    {
        void CreateProduct();
        void EnterProductDetails(Product product);
        Product GetProductDetails();
        void EditProduct(Product product);
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
        IWebElement btnSave => _driver.FindElement(By.Id("Save"));

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

        public void EditProduct(Product product)
        {
            txtName.ClearAndEnterText(product.Name);
            txtDescription.ClearAndEnterText(product.Description);
            txtPrice.ClearAndEnterText(product.Price.ToString());
            btnSave.Click();
        }
    }
}
