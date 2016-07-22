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
        Bottom,
        NoPrefferance
    }

    //public sealed class RandomNumberGeneratior
    //{
    //    private static RandomNumberGeneratior _instnce = null;
    //    private static readonly object syncLock = new object();

    //    public RandomNumberGeneratior Instance
    //    {
    //        get
    //        {
    //            if (_instnce == null)
    //            {
    //                lock (syncLock)
    //                {
    //                    if (_instnce == null)
    //                    {
    //                        _instnce = new RandomNumberGeneratior();
    //                    }
    //                }
    //            }

    //            return _instnce;
    //        }
    //    }

    //    public static Random GetRandom() => new Random();
    //}

    public static class LotteryNumberUtilities
    {
        public const int Max = 49;
        public const int Min = 1;
        public static Random Random { get; } = new Random();
    }

    public interface INumber
    {
        int GetNumber();
    }

    public class EvenNumber : INumber
    { 
        public int GetNumber() => LotteryNumberUtilities.Random.Next(LotteryNumberUtilities.Min, LotteryNumberUtilities.Max / 2) * 2;
    }

    public class OddNumber : INumber
    {
        public int GetNumber() => LotteryNumberUtilities.Random.Next(LotteryNumberUtilities.Min + 1, LotteryNumberUtilities.Max / 2) * 2 - 1;
    }

    public class TopNumber : INumber
    {
        public int GetNumber() => LotteryNumberUtilities.Random.Next(LotteryNumberUtilities.Max / 2 + 1, LotteryNumberUtilities.Max);
    }

    public class BottomNumber : INumber
    {
        public int GetNumber() => LotteryNumberUtilities.Random.Next(LotteryNumberUtilities.Min, LotteryNumberUtilities.Max / 2);
    }

    public class NoPrefferanceNumber : INumber
    {
        public int GetNumber() => LotteryNumberUtilities.Random.Next(LotteryNumberUtilities.Min, LotteryNumberUtilities.Max);
    }

    public abstract class TopNumberdecorator : INumber
    {
        private INumber numberDecorator = null;

        public TopNumberdecorator(INumber numberDecorator)
        {
            this.numberDecorator = numberDecorator;
        }

        public int GetNumber()
        {

        }
    }

    public class LotteryNumbersFactory
    {
        public INumber GetNumbers(LotteryNumberOptions chosenNumberOption)
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
                case LotteryNumberOptions.Bottom:
                    {
                        return new BottomNumber();
                    }
                default:
                    {
                        return new NoPrefferanceNumber();
                    }
            }
        }
    }
}
