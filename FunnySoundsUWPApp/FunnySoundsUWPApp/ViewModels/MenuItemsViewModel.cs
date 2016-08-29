using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySoundsUWPApp
{
    public class MenuItemsViewModel : ModelViewBase, IMenuItemsViewModel
    {
        public ObservableCollection<MenuItemModel> MenuItems { get; private set; }

        public MenuItemsViewModel()
        {
            MenuItems = new ObservableCollection<MenuItemModel>()
            {
                new MenuItemModel() { Type = FunnySoundTypes.All, IconFilePath = "Assets/StoreLogo.png" },
                new MenuItemModel() { Type = FunnySoundTypes.Animals, IconFilePath = "Assets/Icons/animals.png" },
                new MenuItemModel() { Type = FunnySoundTypes.Cartoons, IconFilePath = "Assets/Icons/cartoon.png" },
                new MenuItemModel() { Type = FunnySoundTypes.Taunts, IconFilePath = "Assets/Icons/taunt.png" },
                new MenuItemModel() { Type = FunnySoundTypes.Warnings, IconFilePath = "Assets/Icons/warning.png" }
            };
        }

        public ObservableCollection<MenuItemModel> GetMenuItems() => MenuItems;
    }
}
