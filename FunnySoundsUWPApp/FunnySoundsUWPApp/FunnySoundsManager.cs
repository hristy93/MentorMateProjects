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
                _creator.CreateFunnySounds(FunnySoundTypes.Animals, "Cat"),
                _creator.CreateFunnySounds(FunnySoundTypes.Animals, "Cat"),
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
            return (ObservableCollection <FunnySound>) _funnySounds.Where(s => s.Type == funnySoundType);
        }

        public ObservableCollection<FunnySound> GetFunnySoundsByNames(List<string> funnySoundsNames)
        {
            ObservableCollection<FunnySound> funnySoundsByNames = new ObservableCollection<FunnySound>();
            funnySoundsNames.ForEach(a => funnySoundsByNames.Concat(_funnySounds.Where(s => s.Name == a)));
            return funnySoundsByNames;
        }
    }
}
