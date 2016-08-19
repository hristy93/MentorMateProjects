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
                _creator.CreateFunnySounds(FunnySoundTypes.Animals),
                _creator.CreateFunnySounds(FunnySoundTypes.Cartoons),
                _creator.CreateFunnySounds(FunnySoundTypes.Taunts),
                _creator.CreateFunnySounds(FunnySoundTypes.Warnings),
            };
        }

        public ObservableCollection<FunnySound> GetAllFunnySounds() => _funnySounds;

        public ObservableCollection<FunnySound> GetFunnySoundsByType(FunnySoundTypes funnySoundType)
        {
            return (ObservableCollection <FunnySound>) _funnySounds.Where(s => s.Type == funnySoundType);
        }
    }
}
