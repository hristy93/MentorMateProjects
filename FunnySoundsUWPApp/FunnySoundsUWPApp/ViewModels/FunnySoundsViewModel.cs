using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySoundsUWPApp
{
    public class FunnySoundsViewModel : IFunnySoundsViewModel //, INotifyPropertyChanged
    {
        //private ObservableCollection<FunnySoundModel> _funnySounds;
        private FunnySoundsCreator _creator = new FunnySoundsCreator();

        public ObservableCollection<FunnySoundModel> AllFunnySounds { get; set; }
        public ObservableCollection<FunnySoundModel> FunnySounds { get; set; }

        //public ObservableCollection<FunnySoundModel> FunnySounds
        //{
        //    get
        //    {
        //        return _funnySounds;
        //    }

        //    set
        //    {
        //        _funnySounds = value;
        //        if (PropertyChanged != null)
        //        {
        //            PropertyChanged(this, new PropertyChangedEventArgs("VideoGridItems"));
        //        }
        //    }
        //}

        //public event PropertyChangedEventHandler PropertyChanged;

        public FunnySoundsViewModel()
        {
            AllFunnySounds = new ObservableCollection<FunnySoundModel>()
            {
                //_creator.CreateFunnySounds(FunnySoundTypes.All, "All sounds"),
                _creator.CreateFunnySounds(FunnySoundTypes.Animals, "Cat"),
                _creator.CreateFunnySounds(FunnySoundTypes.Animals, "Cow"),
                _creator.CreateFunnySounds(FunnySoundTypes.Cartoons, "Gun"),
                _creator.CreateFunnySounds(FunnySoundTypes.Cartoons, "Spring"),
                _creator.CreateFunnySounds(FunnySoundTypes.Taunts, "Clock"),
                _creator.CreateFunnySounds(FunnySoundTypes.Taunts, "LOL"),
                _creator.CreateFunnySounds(FunnySoundTypes.Warnings, "Ship"),
                _creator.CreateFunnySounds(FunnySoundTypes.Warnings, "Siren"),
            };

            FunnySounds = new ObservableCollection<FunnySoundModel>();
            AllFunnySounds.ToList().ForEach(s => FunnySounds.Add(s));
        }

        public void GetAllFunnySounds()
        {
            ModifyObservableCollecton(AllFunnySounds.ToList());
        }

        public void GetFunnySoundsByType(FunnySoundTypes funnySoundType)
        {
            //ObservableCollection<FunnySoundModel> funnySoundsByType = new ObservableCollection<FunnySoundModel>();
            if (funnySoundType == FunnySoundTypes.All)
            {
                GetAllFunnySounds();
                return;
            }
            var result = AllFunnySounds.Where(s => s.Type == funnySoundType).ToList();
            //FunnySounds = new ObservableCollection<FunnySoundModel>(result);
            //result.ForEach(s => FunnySounds.Add(s));
            ModifyObservableCollecton(result.ToList());
        }

        public void GetFunnySoundByName(string funnySoundName)
        {
            //ObservableCollection<FunnySound> funnySoundsByNames = new ObservableCollection<FunnySound>();
            var result = AllFunnySounds.Where(s => string.Compare(s.Name, funnySoundName, true) == 0);
            ModifyObservableCollecton(result.ToList());
        }

        private void ModifyObservableCollecton(List<FunnySoundModel> result)
        {
            FunnySounds.Clear();
            result.ForEach(s => FunnySounds.Add(s));
            //var toRemove = FunnySounds.Except(result).ToList();
            //toRemove.ForEach(a => FunnySounds.Remove(a));
        }
    }
}
