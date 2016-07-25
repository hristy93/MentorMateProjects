using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestConfigurationManager();

            //TestShoppingAdapter();

            //TestNewString();

            TestLotteryNumbers();

            Console.ReadLine();
        }

        private static void TestConfigurationManager()
        {
            ConfigurationManager configurationManager = ConfigurationManager.Instance;
            //ConfigurationManager notAllowedConfigurationManager = new ConfigurationManager();
        }

        private static void TestShoppingAdapter()
        {
            //Shop shop = new Shop();
            //shop.DisplayProducts();
            //Console.ReadLine();

            /// Apapter Design Pattrerns variantions 
            IDisplayProducts displayAdapter = null;

            //IDisplayProducts displayAdapter = new ShoppingAdapter1();

            //NewShop newShop = new NewShop();
            //IDisplayProducts displayAdapter = new ShoppingAdapter2(newShop);

            //IDisplayProducts displayAdapter = new ShoppingAdapter3();

            //IDisplayProducts displayAdapter = new ShoppingAdapter4();

            displayAdapter?.DisplayProducts();
        }

        private static void TestNewString()
        {
            NewString newString = new NewString();
            newString[0] = "hi";
            newString[1] = null;
            string newValue = newString[1];
            Console.WriteLine(newValue);
        }

        private static void TestLotteryNumbers()
        {
            LotteryNumbers lotteryNumers = new LotteryNumbers(LotteryNumberOptions.TopEven);
            int[] myLotteryNumbers = lotteryNumers.GetLotteryNumbers();
            lotteryNumers.DisplayLotteryNumbers();
        }
    }
}
