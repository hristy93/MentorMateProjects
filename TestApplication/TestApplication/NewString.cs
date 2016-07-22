using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication
{
    public class NewString
    {
        private const int InitialSize = 10;
        private string[] _strings = null;

        public NewString()
        {
            _strings = new string[InitialSize];
        }

        public NewString(int size)
        {
            _strings = new string[size];
        }

        //public delegate List<int> GetNumbers(List<int> numbers);
        public Func<List<int>, List<int>> GetNumbers;

        public int Length { get { return _strings.Length; }}

        public string this[int i]
        {
            get
            {
                return _strings?[i] ?? "NewString is null";
            }
            
            set
            {
                _strings[i] = value;
            }
        }

        public void DisplayNewString()
        {
            foreach (var newString in _strings)
            {
                Console.WriteLine(newString);
            }
           
        }
    }
}
