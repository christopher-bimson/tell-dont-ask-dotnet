using System;
using System.Collections.Generic;
using System.Linq;
using TellDontAskKata.Domain.Orders;
using TellDontAskKata.Domain.Products;
using TellDontAskKata.Tests.Doubles;
using TellDontAskKata.UseCase;

using Xunit;

namespace TellDontAskKata.Tests.UseCases
{
    public class OrderCreationUseCaseTest
    {
        private readonly TestOrderRepository orderRepository;

        private readonly Category food;

        private readonly IProductCatalog productCatalogue;

        private readonly OrderCreationUseCase useCase;

        public OrderCreationUseCaseTest()
        {
            orderRepository = new TestOrderRepository();
	        food = new Category("Food", 10m);
            productCatalogue = new InMemoryProductCatalog(
                new List<Product>()
                {
                    new Product("salad", food, 3.56m),
                    new Product("tomato", food, 4.65m)
                });
            useCase = new OrderCreationUseCase(orderRepository, productCatalogue);
        }

        [Fact]
        public void SellMultipleItems()
        {
            SellItemRequest saladRequest = new SellItemRequest
            {
                ProductName = "salad",
                Quantity = 2
            };

            SellItemRequest tomatoRequest = new SellItemRequest
            {
                ProductName = "tomato",
                Quantity = 3
            };

            SellItemsRequest request =
                new SellItemsRequest
                {
                    Requests = new List<SellItemRequest>
                    {
                        saladRequest, tomatoRequest
                    }
                };

            useCase.Run(request);

            Order insertedOrder = orderRepository.SavedOrder;
            Assert.Equal(OrderStatus.Created, insertedOrder.Status);
            Assert.Equal(23.20m, insertedOrder.Total);
            Assert.Equal(2.13m, insertedOrder.Tax);
            Assert.Equal("EUR", insertedOrder.Currency);
            Assert.Equal(2, insertedOrder.Items.Count());
	        
	        var saladItem = insertedOrder.Items.ElementAt(0);
	        Assert.Equal("salad", saladItem.Name);
            Assert.Equal(3.56m, saladItem.Price);
            Assert.Equal(2, saladItem.Quantity);
            Assert.Equal(7.84m, saladItem.TaxedAmount);
            Assert.Equal(0.72m, saladItem.Tax);

	        var tomatoItem = insertedOrder.Items.ElementAt(1);
	        Assert.Equal("tomato", tomatoItem.Name);
            Assert.Equal(4.65m, tomatoItem.Price);
            Assert.Equal(3, tomatoItem.Quantity);
            Assert.Equal(15.36m, tomatoItem.TaxedAmount);
            Assert.Equal(1.41m, tomatoItem.Tax);
        }

        [Fact]
        public void UnknownProduct()
        {
            SellItemsRequest request = new SellItemsRequest
            {
                Requests = new List<SellItemRequest>()
            };
            SellItemRequest unknownProductRequest = new SellItemRequest
            {
                ProductName = "unknown product"
            };
            request.Requests.Add(unknownProductRequest);

            Action runAction = () => useCase.Run(request);
            Assert.Throws<UnknownProductException>(runAction);
        }
    }
}
