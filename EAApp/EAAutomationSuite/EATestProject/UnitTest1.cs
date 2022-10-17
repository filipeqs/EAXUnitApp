using AutoFixture.Xunit2;
using EATestProject.Models;
using EATestProject.Pages;
using FluentAssertions;

namespace EATestProject
{
    public class UnitTest1
    {
        private readonly IHomePage _homePage;
        private readonly IProductPage _productPage;

        public UnitTest1(IHomePage homePage, IProductPage createProductPage)
        {
            _homePage = homePage;
            _productPage = createProductPage;
        }

        [Theory, AutoData]
        public void Test1(Product product)
        {
            _homePage.GoToCreateProductPage();

            _productPage.EnterProductDetails(product);
            _productPage.CreateProduct();

            _homePage.PerformClickOnOperation(product.Name, "Details");
        }

        [Theory, AutoData]
        public void Test2(Product product)
        {
            _homePage.GoToCreateProductPage();

            _productPage.EnterProductDetails(product);
            _productPage.CreateProduct();

            _homePage.PerformClickOnOperation(product.Name, "Details");

            var actualProduct = _productPage.GetProductDetails();

            actualProduct
                .Should()
                .BeEquivalentTo(product, option => option.Excluding(q => q.Id));
        }
    }
}
