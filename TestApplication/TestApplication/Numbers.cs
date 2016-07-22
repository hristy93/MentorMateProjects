using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication
{
    class Numbers
    {
        public List<int> GetEvenNumbers()
        {
            List<int> someNumbers = new List<int>
            {
                4,
                5,
                9,
                16
            };

            //var evenNumbers = numbers.Where(x => x % 2 == 0);

            //foreach (var number in evenNumbers)
            //{
            //    Console.WriteLine(number.ToString());
            //}


            //GetNumbers getEvenNumbersWithDelegate = delegate (List<int> numbers)
            //{
            //    List<int> evenNumbers = new List<int>();
            //    foreach (var number in numbers)
            //    {
            //        if (number % 2 == 0)
            //        {
            //            evenNumbers.Add(number);
            //        }
            //    }

            //    return evenNumbers;
            //};

            Func<List<int>, List<int>> getEvenNumbersWithFuncVariable = delegate (List<int> numbers)
            {
                List<int> evenNumbers = new List<int>();
                foreach (var number in numbers)
                {
                    if (number % 2 == 0)
                    {
                        evenNumbers.Add(number);
                    }
                }

                return evenNumbers;
            };

            someNumbers = getEvenNumbersWithFuncVariable(someNumbers);

            return someNumbers;
        }

        private static void PrintNumbers(List<int> someNumbers)
        {
            foreach (var number in someNumbers)
            {
                Console.WriteLine(number.ToString());
            }
        }
    }
}
