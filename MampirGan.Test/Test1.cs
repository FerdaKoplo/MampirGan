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
            var service = new CartServiceFake();
            var cart = service.GetCart();

            service.AddItem("Produk A", 1);

            Assert.AreEqual(1, cart.Count);
            Assert.AreEqual("Produk A", cart[0].products.ProductName);
            Assert.AreEqual(1, cart[0].Quantity);
        }

        [TestMethod]
        public void AddItem_Same_Product_Should_Increase_Quantity()
        {
            var service = new CartServiceFake();
            var cart = service.GetCart();

            service.AddItem("Produk A", 1);
            service.AddItem("Produk A", 2);

            Assert.AreEqual(1, cart.Count); 
            Assert.AreEqual(3, cart[0].Quantity); 
        }
        
        [TestMethod]
        public void RemoveItem_Should_Remove_Product_From_Cart()
        {
            var service = new CartServiceFake();
            service.AddItem("Produk A", 1);

            int productId = service.GetCart()[0].ProductID;

            service.RemoveItem(productId);
            Assert.AreEqual(0, service.GetCart().Count);
        }

        [TestMethod]
        public void ViewCart_Should_Display_Cart_Items_Without_Exception()
        {
            var service = new CartServiceFake();
            service.AddItem("Produk A", 2);
            service.ViewCart();
        }

        [TestMethod]
        public void ClearCart_Should_Remove_All_Items()
        {
            var service = new CartServiceFake();
            service.AddItem("Produk A", 2);
            service.AddItem("Produk B", 1);
            service.ClearCart();
            Assert.AreEqual(0, service.GetCart().Count);
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

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RemoveItem_Throw_Jika_ProductID_Kurang_Dari_Sama_Dengan_0()
        {
            var service = new CartServiceFake();
            service.RemoveItem(0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RemoveItem_Throw_Jika_Item_Tidak_Ditemukan_Di_Cart()
        {
            var service = new CartServiceFake();
            service.RemoveItem(99);         
        }
        [TestMethod]
        public void ViewCart_Should_Not_Throw_When_Empty()
        {
            var service = new CartServiceFake();
            service.ViewCart(); 
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

        public override void ViewCart()
        {
            if (fakeCart == null)
                throw new InvalidOperationException("Data keranjang tidak tersedia.");

            if (!fakeCart.Any())
            {
                Console.WriteLine("Keranjang kosong.");
                return;
            }

            Console.WriteLine("=== Isi Keranjang ===");
            foreach (var item in fakeCart)
            {
                Console.WriteLine(
                    $"- {item.products.ProductName.PadRight(15)} | Qty: {item.Quantity,3} | " +
                    $"Harga: Rp{item.products.Price:N0} | Subtotal: Rp{(item.products.Price * item.Quantity):N0}");
            }

            var total = fakeCart.Sum(i => i.products.Price * i.Quantity);
            Console.WriteLine($"Total belanja: Rp{total:N0}");
        }



        public override void ClearCart()
        {
            fakeCart.Clear();
            Console.WriteLine("Semua item di keranjang telah dihapus.");

            if (fakeCart.Count != 0)
                throw new Exception("Keranjang belum benar-benar kosong setelah dibersihkan.");
        }

        public override void RemoveItem(int productID)
        {
            if (productID <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(productID), "ID produk tidak valid.");
            }


            var existingItem = fakeCart.FirstOrDefault(c => c.ProductID == productID);
            if (existingItem == null)
            {
                throw new InvalidOperationException("Item tidak ditemukan di keranjang.");
            }

            fakeCart.Remove(existingItem);
            Console.WriteLine($"Item {existingItem.products.ProductName} dihapus dari keranjang");

            if (fakeCart.Any(c => c.ProductID == productID))
                throw new Exception("Item masih ada di keranjang setelah penghapusan.");

        }
        public override List<Cart> GetCart() => fakeCart;
        public override bool HasItems() => fakeCart.Any();
    }



    // Unit test Checkout Service
    [TestClass]
    public class CheckoutServiceTest
    {

    }

    [TestClass]
    public class CartServiecFake
    {

    }

}