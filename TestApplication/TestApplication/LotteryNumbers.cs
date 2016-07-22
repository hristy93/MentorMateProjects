using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication
{
    class LotteryNumbers
    {
        private const int LottteryNumbersCount = 6;
        private int[] _lotteryNumbers = new int[LottteryNumbersCount];

        public LotteryNumbers()
        {
            InitializeLotteryNumbers(LotteryNumberOptions.NoPrefferance);
        }

        public LotteryNumbers(LotteryNumberOptions lotteryNumberOption)
        {
            InitializeLotteryNumbers(lotteryNumberOption);
        }

        private void InitializeLotteryNumbers(LotteryNumberOptions lotteryNumberOption)
        {
            LotteryNumbersFactory factory = new LotteryNumbersFactory();
            for (int i = 0; i < 6; i++)
            {
                INumber number;
                int generatedNumberValue;
                do
                {
                    number = factory.GetNumber(lotteryNumberOption);
                    generatedNumberValue = number.GetNumberValue();
                } while (_lotteryNumbers.Contains(generatedNumberValue));
                _lotteryNumbers[i] = generatedNumberValue;
            }
        }

        public int[] GetLotteryNumbers()
        {
            return _lotteryNumbers;
        }

        public void DisplayLotteryNumbers()
        {
            foreach (var number in _lotteryNumbers)
            {
                Console.Write($"{number} ");
            }
        }
    }
}
