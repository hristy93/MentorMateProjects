using System.Collections.ObjectModel;

namespace FunnySoundsUWPApp
{
    interface IMenuItemsViewModel
    {
        ObservableCollection<MenuItemModel> GetMenuItems();
    }
}