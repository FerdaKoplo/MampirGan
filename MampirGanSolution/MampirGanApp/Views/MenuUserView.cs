using MampirGanApp.Seeder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MampirGanApp.Views
{
    public class MenuUserView
    {
        public void ShowMenu()
        {

            Console.WriteLine("=== Mampir Gan ===");
            Console.WriteLine("1. Lihat Menu");
            Console.WriteLine("2. Cari Menu");
            Console.WriteLine("3. Cari Kerdasarkan Kategori");
            Console.WriteLine("4. Keranjang");
            Console.WriteLine("5. History Pembelian");
            Console.WriteLine("0. Keluar");
            Console.Write("Pilih: ");
        }

        public void ShowInvalidInput()
        {
            Console.WriteLine("Input tidak valid.");
        }
    }
}
