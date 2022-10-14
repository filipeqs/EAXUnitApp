using AutoFixture.Xunit2;
using EATestProject.Models;
using EATestProject.Pages;

namespace EATestProject
{
    public class UnitTest1
    {
        private readonly IHomePage _homePage;
        private readonly ICreateProductPage _createProductPage;

        public UnitTest1(IHomePage homePage, ICreateProductPage createProductPage)
        {
            _homePage = homePage;
            _createProductPage = createProductPage;
        }

        [Theory, AutoData]
        public void Test(Product product)
        {
            _homePage.GoToCreateProductPage();

            _createProductPage.EnterProductDetails(product);
            _createProductPage.CreateProduct();

            _homePage.PerformClickOnOperation("Monitor", "Details");
        }
    }
}
