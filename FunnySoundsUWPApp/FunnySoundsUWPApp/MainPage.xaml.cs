using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FunnySoundsUWPApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private FunnySoundsManager _funnySoundsManager = new FunnySoundsManager();
        private MenuItemsManager _menuItemsManager = new MenuItemsManager();

        public ObservableCollection<FunnySound> FunnySounds;
        public ObservableCollection<MenuItem> MenuItems;


        public MainPage()
        {
            this.InitializeComponent();
            FunnySounds = _funnySoundsManager.GetAllFunnySounds();
            MenuItems = _menuItemsManager.GetMenuItems();
        }

        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {

        }

        private void HamburgerMenuButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void FunnySoundsMenuListView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
