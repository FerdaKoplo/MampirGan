using MampirGan_Automata__dan_Table_driven_.Fitur;
using MampirGan_Automata__dan_Table_driven_.Objects;
using MampirGan_Automata__dan_Table_driven_.Utils;
using MampirGanApp.Models;

namespace MampirGan_Automata__dan_Table_driven_.Testing
{
    //Unit test Cart Service
    [TestClass]
    public class CartServiceTest
    {
        [TestMethod]
        public void AddItem_Should_Add_Product_To_Cart()
        {
            var service = new CartService();
            var cart = service.GetCart();

            service.AddItem("Indomie Goreng", 1);

            Assert.AreEqual(1, cart.Count);
            Assert.AreEqual("Indomie Goreng", cart[0].products.ProductName);
            Assert.AreEqual(1, cart[0].Quantity);
        }

        [TestMethod]
        public void AddItem_Same_Product_Should_Increase_Quantity()
        {
            var service = new CartService();
            var cart = service.GetCart();

            service.AddItem("Indomie Goreng", 1);
            service.AddItem("Indomie Goreng", 3);

            Assert.AreEqual(1, cart.Count);
            Assert.AreEqual(4, cart[0].Quantity);
        }

        [TestMethod]
        public void RemoveItem_Should_Remove_Product_From_Cart()
        {
            var service = new CartService();
            service.AddItem("Indomie Goreng", 1);

            int productId = service.GetCart()[0].ProductID;

            service.RemoveItem(productId);
            Assert.AreEqual(0, service.GetCart().Count);
        }

        [TestMethod]
        public void ViewCart_Should_Display_Cart_Items_Without_Exception()
        {
            var service = new CartService();
            service.AddItem("Indomie Goreng", 2);
            service.ViewCart();
        }

        [TestMethod]
        public void ClearCart_Should_Remove_All_Items()
        {
            var service = new CartService();
            service.AddItem("Indomie Goreng", 2);
            service.AddItem("Aqua Botol", 1);
            service.ClearCart();
            Assert.AreEqual(0, service.GetCart().Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddItem_Should_Throw_When_ProductName_Is_Empty()
        {
            var service = new CartService();
            service.AddItem("", 1);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddItem_Should_Throw_When_Product_Not_Found()
        {
            var service = new CartService();
            service.AddItem("Produk X", 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RemoveItem_Throw_Jika_ProductID_Kurang_Dari_Sama_Dengan_0()
        {
            var service = new CartService();
            service.RemoveItem(0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RemoveItem_Throw_Jika_Item_Tidak_Ditemukan_Di_Cart()
        {
            var service = new CartService();
            service.RemoveItem(99);
        }
        [TestMethod]
        public void ViewCart_Should_Not_Throw_When_Empty()
        {
            var service = new CartService();
            service.ViewCart();
        }
    }

       // Unit test Checkout Service
    [TestClass]
    public class CheckoutServiceTest
    {
        [TestMethod]
        public void ProcessCheckout_ShouldAddTransaction_WhenValidCartProvided()
        {
            // Arrange
            var cart = new List<Cart>
        {
            new Cart
            {
                ProductID = 1,
                Quantity = 2,
                products = new Products { ProductName = "Indomie Goreng", Price = 10000 }
            }
        };

            var service = new CheckoutService();

            // Act
            service.ProcessCheckout(cart);

            // Assert
            Assert.IsTrue(CheckoutService.transactions.Count > 0);
            Assert.IsTrue(CheckoutService.transactions.Exists(t => t.Items.Exists(i => i.ProductName == "Indomie Goreng")));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ProcessCheckout_ShouldThrowException_WhenCartIsEmpty()
        {
            // Arrange
            var service = new CheckoutService();
            var cart = new List<Cart>();

            // Act
            service.ProcessCheckout(cart);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ProcessCheckout_ShouldThrowException_WhenCartContainsNullProduct()
        {
            // Arrange
            var service = new CheckoutService();
            var cart = new List<Cart>
        {
            new Cart { ProductID = 1, Quantity = 1, products = null }
        };

            // Act
            service.ProcessCheckout(cart);
        }
    }

    [TestClass]
    public class CategoryServiceTest
    {
        [TestMethod]
        public void ViewAll_ShouldDisplayCategories_WhenCategoriesAreAvailable()
        {
            // Arrange
            var service = new CategoryService();
            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            service.ViewAll();
            var output = sw.ToString();

            // Assert
            Assert.IsTrue(output.Contains("Daftar Kategori") || output.Contains("Kategori"));
        }

        [TestMethod]
        public void ViewProductsByCategory_ShouldPromptUser_WhenInputIsEmpty()
        {
            // Arrange
            var service = new CategoryService();
            using var sw = new StringWriter();
            Console.SetOut(sw);

            using var sr = new StringReader("");
            Console.SetIn(sr);

            // Act
            service.ViewProductsByCategory();
            var output = sw.ToString();

            // Assert
            Assert.IsTrue(output.Contains("Input tidak valid."));
        }

        [TestMethod]
        public void ViewProductsByCategory_ShouldHandleUnknownCategory()
        {
            // Arrange
            var service = new CategoryService();
            using var sw = new StringWriter();
            Console.SetOut(sw);

            using var sr = new StringReader("KategoriTidakAda");
            Console.SetIn(sr);

            // Act
            try
            {
                service.ViewProductsByCategory();
            }
            catch (InvalidOperationException ex)
            {
                // Assert
                Assert.AreEqual("Kategori tidak ditemukan.", ex.Message);
                return;
            }

            Assert.Fail("Expected exception was not thrown.");
        }
    }

}