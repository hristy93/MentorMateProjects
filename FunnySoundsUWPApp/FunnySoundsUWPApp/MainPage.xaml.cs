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
        private Stack<FunnySoundTypes> _selectedTypes = new Stack<FunnySoundTypes>();
        private FunnySoundTypes _currentSelectedType = FunnySoundTypes.All;
        private FunnySoundTypes _previousSelectedType = FunnySoundTypes.None;
        private List<string> _suggestedFunnySoundsNames;

        public ObservableCollection<FunnySound> FunnySounds;
        public ObservableCollection<MenuItem> MenuItems;

        public MainPage()
        {
            this.InitializeComponent();
            FunnySounds = _funnySoundsManager.GetAllFunnySounds();
            MenuItems = _menuItemsManager.GetMenuItems();
            BackButton.Visibility = Visibility.Collapsed;
            FunnySoundsMenuListView.SelectedItem = MenuItems[0];
            //_selectedTypes.Push(FunnySoundTypes.None);
        }

        private void HamburgerMenuButton_Click(object sender, RoutedEventArgs e)
        {
            SoundsCollectionSplitView.IsPaneOpen = !SoundsCollectionSplitView.IsPaneOpen;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            //FunnySounds = _funnySoundsManager.GetFunnySoundsByType(_previousSelectedType);
            //SoundsTitleTextBlock.Text = _previousSelectedType.ToString();
            FunnySoundTypes previousSelectedType = FunnySoundTypes.All;
            if (_selectedTypes.Count != 1)
            {
                previousSelectedType = _selectedTypes.Pop();
                FunnySounds = _funnySoundsManager.GetFunnySoundsByType(previousSelectedType); 
            }
            else
            {
                FunnySoundsGridView.ItemsSource = FunnySounds;
            }

            SoundsTitleTextBlock.Text = previousSelectedType.ToString();
            //if (_previousSelectedType == FunnySoundTypes.All)
            if (previousSelectedType == FunnySoundTypes.All)
            {
                BackButton.Visibility = Visibility.Collapsed;
            }
        }

        private void FunnySoundsMenuListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            //SoundSearchAutoSuggestBox.Text = String.Empty;
            var clickedMenuItem = (MenuItem) e.ClickedItem;
            SoundsTitleTextBlock.Text = clickedMenuItem.Type.ToString();
            //_previousSelectedType = _currentSelectedType;
            //_currentSelectedType = clickedMenuItem.Type;
            _selectedTypes.Push(_currentSelectedType);
            _currentSelectedType = clickedMenuItem.Type;
            FunnySounds = _funnySoundsManager.GetFunnySoundsByType(clickedMenuItem.Type);
            if (clickedMenuItem.Type != FunnySoundTypes.All)
            {
                BackButton.Visibility = Visibility.Visible;
            }
        }

        private void SoundSearchAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            FunnySounds = _funnySoundsManager.GetFunnySoundsByNames(_suggestedFunnySoundsNames);
            SoundsTitleTextBlock.Text = sender.Text;
            FunnySoundsMenuListView.SelectedItem = null;
            //_previousSelectedType = _currentSelectedType;
            //_currentSelectedType = FunnySoundTypes.Search;
            _selectedTypes.Push(_currentSelectedType);
            _currentSelectedType = FunnySoundTypes.Search;
            BackButton.Visibility = Visibility.Visible;
        }

        private void SoundSearchAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            //if (String.IsNullOrEmpty(sender.Text))
            //{
            //    BackButton_Click(null, null);
            //}

            _suggestedFunnySoundsNames = FunnySounds.Where(s => s.Name.ToString().StartsWith(sender.Text)).Select(s => s.Name.ToString()).ToList();
            SoundSearchAutoSuggestBox.ItemsSource = _suggestedFunnySoundsNames;
        }

        private void FunnySoundsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedSound = (FunnySound)e.ClickedItem;
            FunnySoundsMediaElement.Source = new Uri(this.BaseUri, selectedSound.SoundFilePath);
        }
    }
}