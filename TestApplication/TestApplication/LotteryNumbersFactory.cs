using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication
{
    public enum LotteryNumberOptions
    {
        Even,
        Odd,
        Top,
        TopEven,
        TopOdd,
        Bottom,
        BottomEven,
        BottomOdd,
        NoPrefferance
    }

    public sealed class RandomNumberGeneratior
    {
        private static RandomNumberGeneratior _instnce = null;
        private static readonly object syncLock = new object();

        public static RandomNumberGeneratior Instance
        {
            get
            {
                if (_instnce == null)
                {
                    lock (syncLock)
                    {
                        if (_instnce == null)
                        {
                            _instnce = new RandomNumberGeneratior();
                        }
                    }
                }

                return _instnce;
            }
        }

        public Random GetRandom() => new Random();
    }

    public static class LotteryNumberRestrictions
    {
        public const int Max = 49;
        public const int Min = 1;
        
    }

    #region LotteryNumbersFactory

    /// <summary>
    /// Implementing Factory Method Pattern for the lottery numbers
    /// </summary>
    public class LotteryNumbersFactory
    {
        public static Random Random { get; private set; }

        public LotteryNumbersFactory()
        {
            RandomNumberGeneratior randomNumbergenerator = RandomNumberGeneratior.Instance;
            Random = randomNumbergenerator.GetRandom();
        }

        public INumber GetNumber(LotteryNumberOptions chosenNumberOption)
        {
            switch (chosenNumberOption)
            {
                case LotteryNumberOptions.Even:
                    {
                        return new EvenNumber();
                    }
                case LotteryNumberOptions.Odd:
                    {
                        return new OddNumber();
                    }
                case LotteryNumberOptions.Top:
                    {
                        return new TopNumber();
                    }
                case LotteryNumberOptions.TopEven:
                    {
                        return new TopEvenNumber(new TopNumber());
                    }
                case LotteryNumberOptions.TopOdd:
                    {
                        return new TopOddNumber(new TopNumber());
                    }
                case LotteryNumberOptions.Bottom:
                    {
                        return new BottomNumber();
                    }
                case LotteryNumberOptions.BottomEven:
                    {
                        return new BottomEvenNumber(new BottomNumber());
                    }
                case LotteryNumberOptions.BottomOdd:
                    {
                        return new BottomOddNumber(new BottomNumber());
                    }
                default:
                    {
                        return new NoPrefferanceNumber();
                    }
            }
        }
    }

    public interface INumber
    {
        int GetNumberValue();
    }

    public class EvenNumber : INumber
    {
        public int GetNumberValue() => LotteryNumbersFactory.Random.Next(LotteryNumberRestrictions.Min, LotteryNumberRestrictions.Max / 2) * 2;
    }

    public class OddNumber : INumber
    {
        public int GetNumberValue() => LotteryNumbersFactory.Random.Next(LotteryNumberRestrictions.Min + 1, LotteryNumberRestrictions.Max / 2) * 2 - 1;
    }

    public class TopNumber : INumber
    {
        public int GetNumberValue() => LotteryNumbersFactory.Random.Next(LotteryNumberRestrictions.Max / 2 + 1, LotteryNumberRestrictions.Max);
    }

    public class BottomNumber : INumber
    {
        public int GetNumberValue() => LotteryNumbersFactory.Random.Next(LotteryNumberRestrictions.Min, LotteryNumberRestrictions.Max / 2);
    }

    public class NoPrefferanceNumber : INumber
    {
        public int GetNumberValue() => LotteryNumbersFactory.Random.Next(LotteryNumberRestrictions.Min, LotteryNumberRestrictions.Max);
    }

    #endregion

    #region NumberDecoratoи

    /// <summary>
    /// Implementing Decorator Pattern for the lottery numbers
    /// </summary>
    public abstract class NumberDecorator : INumber
    {
        protected INumber numberDecorator = null;

        public NumberDecorator(INumber numberDecorator)
        {
            this.numberDecorator = numberDecorator;
        }

        public abstract int GetNumberValue();
    }

    public class TopEvenNumber : NumberDecorator
    {
        public TopEvenNumber(INumber numberDecorator) : base(numberDecorator)
        {
            
        }

        public override int GetNumberValue()
        {
            int numberFromDecorator = numberDecorator.GetNumberValue();
            return (numberFromDecorator % 2 == 0) ? numberFromDecorator : numberFromDecorator - 1;
        }
    }

    public class BottomEvenNumber : NumberDecorator
    {
        public BottomEvenNumber(INumber numberDecorator) : base(numberDecorator)
        {

        }

        public override int GetNumberValue()
        {
            int numberFromDecorator = numberDecorator.GetNumberValue();
            return (numberFromDecorator % 2 == 0) ? numberFromDecorator : numberFromDecorator + 1;
        }
    }

    public class TopOddNumber : NumberDecorator
    {
        public TopOddNumber(INumber numberDecorator) : base(numberDecorator)
        {

        }

        public override int GetNumberValue()
        {
            int numberFromDecorator = numberDecorator.GetNumberValue();
            return (numberFromDecorator % 2 != 0) ? numberFromDecorator : numberFromDecorator + 1;
        }
    }

    public class BottomOddNumber : NumberDecorator
    {
        public BottomOddNumber(INumber numberDecorator) : base(numberDecorator)
        {

        }

        public override int GetNumberValue()
        {
            int numberFromDecorator = numberDecorator.GetNumberValue();
            return (numberFromDecorator % 2 != 0) ? numberFromDecorator : numberFromDecorator - 1;
        }
    }

    #endregion NumberDecorator
}
