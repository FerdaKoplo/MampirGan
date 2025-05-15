using MampirGan_Automata__dan_Table_driven_.Fitur;
using MampirGan_Automata__dan_Table_driven_.Objects;

namespace MampirGanTest;
{
    [TestClass]
    public sealed class CartService
    {
        [TestMethod]
        public void AddItem_Harus_Bertambah_Ke_Cart()
        {
            // Arrange
            var service = new CartServiceFake();
            var feature = new CartFeature();

            //Act
            service.AddItem("Produk Z", 1);

            //Assert
            Assert.AreEqual(1, service.GetCart().Count);
            Assert.AreEqual(1, service.GetCart()[0].Quantity);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddItem_Should_Throw_When_ProductName_Is_Empty()
        {
            var service = new CartServiceFake();
            service.AddItem("", 1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddItem_Should_Throw_When_Product_Not_Found()
        {
            var service = new CartServiceFake();
            service.AddItem("Produk X", 1);
        }
    }

    [TestClass]
    public class CartServiceFake : CartService
    {
        private readonly List<Products> dummyProducts = new()
        {
            new Products { ProductID = 1, ProductName = "Produk A", Price = 10000 },
            new Products { ProductID = 2, ProductName = "Produk B", Price = 15000 },
        };

        private readonly List<Cart> fakeCart = new();

        public override void AddItem(string productName, int quantity)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new ArgumentException();

            var prod = dummyProducts.Find(p => p.ProductName == productName);
            if (prod == null)
                throw new InvalidOperationException("Produk tidak ditemukan.");

            var existing = fakeCart.Find(c => c.ProductID == prod.ProductID);
            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                fakeCart.Add(new Cart
                {
                    CartID = 1,
                    ProductID = prod.ProductID,
                    Quantity = quantity,
                    products = prod
                });
            }
        }

        public override List<Cart> GetCart() => fakeCart;
        public override bool HasItems() => fakeCart.Any();
    }
}

