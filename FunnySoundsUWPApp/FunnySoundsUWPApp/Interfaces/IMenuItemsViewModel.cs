using System.Collections.ObjectModel;

namespace FunnySoundsUWPApp
{
    public interface IMenuItemsViewModel
    {
        ObservableCollection<MenuItemModel> GetMenuItems();
    }
}