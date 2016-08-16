using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nameday
{
   public class MainPageData : INotifyPropertyChanged
    {
        private string _greeting;
        private string _filter;
        private NamedayModel _selectedNameDay;
        private List<NamedayModel> _allNamedays = new List<NamedayModel>();

        public event PropertyChangedEventHandler PropertyChanged;

        public string Greeting
        {
            get { return _greeting; }
            set
            {
                if (_greeting == value)
                {
                    return;
                }

                _greeting = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Greeting)));
            }
        }

        public string Filter
        {
            get { return _filter; }
            set
            {
                if (_filter == value)
                {
                    return;
                }

                _filter = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Filter)));

                PerformFiltering();
            }
        }

        public ObservableCollection<NamedayModel> Namedays { get; set; }

        public NamedayModel SelectedNameday
        {
            get { return _selectedNameDay; }
            set
            {
                if (value == null)
                {
                    Greeting = "Hello World";
                }
                else
                {
                    Greeting = "Hello" + value.NamesAsString;
                }
            }
        }

        private void PerformFiltering()
        {
            if (_filter == null)
                _filter = "";

            var lowerCaseFilter = Filter.ToLowerInvariant().Trim();

            var result =
                _allNamedays.Where(d => d.NamesAsString.ToLowerInvariant()
                .Contains(lowerCaseFilter))
                .ToList();

            var toRemove = Namedays.Except(result).ToList();

            foreach (var x in toRemove)
                Namedays.Remove(x);

            var resultCount = result.Count;
            for (int i = 0; i < resultCount; i++)
            {
                var resultItem = result[i];
                if (i + 1 > Namedays.Count || !Namedays[i].Equals(resultItem))
                    Namedays.Insert(i, resultItem);
            }
        }

        public MainPageData()
        {
            Namedays = new ObservableCollection<NamedayModel>();

            for (int month = 1; month <= 12; month++)
            {
                _allNamedays.Add(new NamedayModel(month, 1,
                    new string[] { "Adam" }));
                _allNamedays.Add(new NamedayModel(month, 24,
                    new string[] { "Eve", "Andrew" }));
            }

            PerformFiltering();
        }
    }
}
