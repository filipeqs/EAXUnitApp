using ProductAPI.Repository;
using TechTalk.SpecFlow.Assist;
using TechTalk.SpecFlow;
using ProductAPI.Data;

namespace EATestDBB.StepDefinitions
{
    [Binding]
    internal class ReusableSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IProductRepository _productRepository;

        public ReusableSteps(ScenarioContext scenarioContext, IProductRepository productRepository)
        {
            _scenarioContext = scenarioContext;
            _productRepository = productRepository;
        }

        [Then(@"I delete the product (.*) for cleanup")]
        public void ThenIDeleteTheProductHeadphonesForCleanup(string productName)
        {
            _productRepository.DeleteProduct(productName);
        }

        [Given(@"I ensure the following product is created")]
        public void GivenIEnsureTheFollowingProductIsCreated(Table table)
        {
            var product = table.CreateInstance<Product>();

            _productRepository.AddProduct(product);

            //Store the product details
            _scenarioContext.Set<Product>(product);
        }

        [Given(@"I cleanup following data")]
        public void GivenICleanupFollowingData(Table table)
        {
            var products = table.CreateSet<Product>();

            foreach (var product in products)
            {
                var prod = _productRepository.GetProductByName(product.Name);

                if (prod != null)
                    _productRepository.DeleteProduct(product.Name);
            }
        }
    }
}
