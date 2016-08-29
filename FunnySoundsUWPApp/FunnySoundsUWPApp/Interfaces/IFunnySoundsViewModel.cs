using System.Collections.ObjectModel;

namespace FunnySoundsUWPApp
{
    public interface IFunnySoundsViewModel
    {
        ObservableCollection<FunnySoundModel> AllFunnySounds { get; set; }
        ObservableCollection<FunnySoundModel> FunnySounds { get; set; }
        bool IsBackButtonVisible { get; set; }

        void GetAllFunnySounds();
        void GetFunnySoundByName(string funnySoundName);
        void GetFunnySoundsByType(FunnySoundTypes funnySoundType);
    }
}