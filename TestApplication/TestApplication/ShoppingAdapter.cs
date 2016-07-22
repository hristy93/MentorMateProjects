using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication
{
    public enum ProductType
    {
        apple,
        ornage,
        melon,
        grape,
        lime,
        peach
    }

    public class ShoppingAdapter1 : IDisplayProducts
    {
        private NewShop _newShop = new NewShop();

        public void DisplayProducts()
        {
            _newShop.PrintProducts();
        }
    }

    public class ShoppingAdapter2 : IDisplayProducts
    {
        private NewShop _newShop;

        public ShoppingAdapter2(NewShop newShop)
        {
            this._newShop = newShop;
        }

        public void DisplayProducts()
        {
            _newShop.PrintProducts();
        }
    }

    public class ShoppingAdapter3 : IDisplayProducts
    {
        public void DisplayProducts()
        {
            new NewShop().PrintProducts();
        }
    }

    public class ShoppingAdapter4 : NewShop, IDisplayProducts
    {
        public void DisplayProducts()
        {
            PrintProducts();
        }
    }


    public class Shop : IDisplayProducts
    {
        private List<ProductType> _products = new List<ProductType>
        {
            ProductType.apple,
            ProductType.melon
        };

        public void DisplayProducts()
        {
            Console.WriteLine("My shop's products:");
            foreach (var product in _products)
            {
                Console.WriteLine(product);
            }
        }
    }

    public class NewShop
    {
        private List<string> _products = new List<string>
        {
            "apple",
            "melon",
            "grape",
            "lemon"
        };

        public void PrintProducts()
        {
            Console.WriteLine("New shop's products:");
            foreach (var product in _products)
            {
                Console.WriteLine(product);
            }
        }
    }
}
