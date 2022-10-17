using EATestDBB.Pages;
using ProductAPI.Data;
using ProductAPI.Repository;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace EATestDBB.StepDefinitions
{
    [Binding]
    public sealed class ProductSteps
    {
        private readonly IHomePage _homePage;
        private readonly IProductPage _productPage;
        private readonly ScenarioContext _scenarioContext;

        public ProductSteps(ScenarioContext scenarioContext, IHomePage homePage, IProductPage productPage)
        {
            _homePage = homePage;
            _productPage = productPage;
            _scenarioContext = scenarioContext;
        }

        [Given(@"I click the product menu")]
        public void GivenIClickTheProductMenu()
        {
            _homePage.ClickProductMenu();
        }

        [Given(@"I click the ""([^""]*)"" link")]
        public void GivenIClickTheLink(string create)
        {
            _homePage.ClickCreateProductLink();
        }

        [Given(@"I click product with folowing details")]
        public void GivenIClickProductWithFolowingDetails(Table table)
        {
            var product = table.CreateInstance<Product>();
            _productPage.EnterProductDetails(product);
            _productPage.CreateProduct();

            _scenarioContext.Set(product);
        }

        [When(@"I click the (.*) link of the newly create product")]
        public void WhenIClickTheDetailsLinkOfTheNewlyCreateProduct(string operation)
        {
            var product = _scenarioContext.Get<Product>();
            _homePage.PerformClickOnOperation(product.Name, operation);
        }

        [Then(@"I see all the product details are created as expected")]
        public void ThenISeeAllTheProductDetailsAreCreatedAsExpected()
        {
            var product = _scenarioContext.Get<Product>();
            var actualProduct = _productPage.GetProductDetails();

            actualProduct
                .Should()
                .BeEquivalentTo(product, option => option.Excluding(q => q.Id));
        }

        [When(@"I Edit the product details with following")]
        public void WhenIEditTheProductDetailsWithFollowing(Table table)
        {
            var product = table.CreateInstance<Product>();

            _productPage.EditProduct(product);

            //Store the product details
            _scenarioContext.Set(product);
        }
    }
}