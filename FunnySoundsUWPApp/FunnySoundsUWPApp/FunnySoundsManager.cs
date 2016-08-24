using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySoundsUWPApp
{
    public class FunnySoundsManager
    {
        private ObservableCollection<FunnySound> _funnySounds;
        private FunnySoundsCreator _creator = new FunnySoundsCreator();

        public FunnySoundsManager()
        {
            _funnySounds = new ObservableCollection<FunnySound>()
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
        }

        public ObservableCollection<FunnySound> GetAllFunnySounds() => _funnySounds;

        public ObservableCollection<FunnySound> GetFunnySoundsByType(FunnySoundTypes funnySoundType)
        {
            ObservableCollection<FunnySound> funnySoundsByType = new ObservableCollection<FunnySound>();
            if (funnySoundType == FunnySoundTypes.All)
            {
                return _funnySounds;
            }
           
            (_funnySounds.Where(s => s.Type == funnySoundType)).ToList().ForEach(a => funnySoundsByType.Add(a));
            return funnySoundsByType;
        }

        public FunnySound GetFunnySoundByName(string funnySoundName)
        {
            //ObservableCollection<FunnySound> funnySoundsByNames = new ObservableCollection<FunnySound>();
            var result = _funnySounds.Where(s => string.Compare(s.Name, funnySoundName, true) == 0);
            FunnySound funnySoundByName = result.Single();
            return funnySoundByName;
        }
    }
}
