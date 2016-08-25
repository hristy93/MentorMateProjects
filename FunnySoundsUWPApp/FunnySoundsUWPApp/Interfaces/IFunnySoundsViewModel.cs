using System.Collections.ObjectModel;

namespace FunnySoundsUWPApp
{
    public interface IFunnySoundsViewModel
    {
        ObservableCollection<FunnySoundModel> GetAllFunnySounds();
        FunnySoundModel GetFunnySoundByName(string funnySoundName);
        ObservableCollection<FunnySoundModel> GetFunnySoundsByType(FunnySoundTypes funnySoundType);
    }
}