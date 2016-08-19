using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunnySoundsUWPApp
{
    class MenuItemsManager
    {
        private ObservableCollection<MenuItem> _menuItems;

        public MenuItemsManager()
        {
            _menuItems = new ObservableCollection<MenuItem>()
            {
                new MenuItem() { Type = FunnySoundTypes.Animals, IconFilePath = "Assets/Icons/animal.png" },
                new MenuItem() { Type = FunnySoundTypes.Cartoons, IconFilePath = "Assets/Icons/cartoon.png" },
                new MenuItem() { Type = FunnySoundTypes.Taunts, IconFilePath = "Assets/Icons/taunt.png" },
                new MenuItem() { Type = FunnySoundTypes.Warnings, IconFilePath = "Assets/Icons/warning.png" }
            };
        }

        public ObservableCollection<MenuItem> GetMenuItems() => _menuItems;
    }
}
