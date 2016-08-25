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
using Windows.UI.Xaml.Media.Animation;
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
        private Stack<string> _searchedFunnySoundNames = new Stack<string>();
        private FunnySoundTypes _currentSelectedType = FunnySoundTypes.All;
        //private FunnySoundTypes _previousSelectedType = FunnySoundTypes.None;
        private List<string> _suggestedFunnySoundsNames;

        public ObservableCollection<FunnySoundModel> FunnySounds { get; private set; }
        public ObservableCollection<MenuItemModel> MenuItems { get; private set; }

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

            //FunnySoundTypes previousSelectedType = FunnySoundTypes.All;
            
            if (_selectedTypes.Count != 1)
            {
                if (_currentSelectedType == FunnySoundTypes.Search)
                {
                    _searchedFunnySoundNames.Pop();
                }
                _currentSelectedType = _selectedTypes.Pop();
                FunnySounds = _funnySoundsManager.GetFunnySoundsByType(_currentSelectedType); 
            }
            else
            {
                _currentSelectedType = FunnySoundTypes.All;
                FunnySounds = _funnySoundsManager.GetAllFunnySounds();
                _selectedTypes.Pop();
            }

            if (_currentSelectedType != FunnySoundTypes.Search)
            {
                FunnySoundsMenuListView.SelectedItem = MenuItems.Where(m => m.Type == _currentSelectedType).Single();
                ClearSoundSearcAutoSuggestBoxtext();
                FunnySoundsGridView.ItemsSource = FunnySounds;
                SoundsTitleTextBlock.Text = _currentSelectedType.ToString();
                //if (_previousSelectedType == FunnySoundTypes.All)
                if (_currentSelectedType == FunnySoundTypes.All)
                {
                    BackButton.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                string searchedFunnySoundName = _searchedFunnySoundNames.Pop();
                DisplaySearchResults(searchedFunnySoundName);
                SoundsTitleTextBlock.Text = searchedFunnySoundName;
                FunnySoundsMenuListView.SelectedItem = null;
            }
        }

        private void FunnySoundsMenuListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            //SoundSearchAutoSuggestBox.Text = String.Empty;
            var clickedMenuItem = (MenuItemModel)e.ClickedItem;
            FunnySoundsMenuListView.SelectedItem = clickedMenuItem;
            SoundsTitleTextBlock.Text = clickedMenuItem.Type.ToString();
            //_previousSelectedType = _currentSelectedType;
            //_currentSelectedType = clickedMenuItem.Type;
            _selectedTypes.Push(_currentSelectedType);
            _currentSelectedType = clickedMenuItem.Type;
            FunnySounds = _funnySoundsManager.GetFunnySoundsByType(clickedMenuItem.Type);
            FunnySoundsGridView.ItemsSource = FunnySounds;
            if (clickedMenuItem.Type != FunnySoundTypes.All)
            {
                BackButton.Visibility = Visibility.Visible;
            }

            ClearSoundSearcAutoSuggestBoxtext();
        }

        private void ClearSoundSearcAutoSuggestBoxtext()
        {
            if (!String.IsNullOrEmpty(SoundSearchAutoSuggestBox.Text))
            {
                SoundSearchAutoSuggestBox.Text = String.Empty;
            }
        }

        private void SoundSearchAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            string suggestedFunnySoundName = SoundSearchAutoSuggestBoxInputValiation(sender, args);
            if (String.IsNullOrEmpty(suggestedFunnySoundName))
            {
                return;
            }

            DisplaySearchResults(suggestedFunnySoundName);
            SettingPostSearchParameters(suggestedFunnySoundName);
        }

        private void DisplaySearchResults(string suggestedFunnySoundName)
        {
            FunnySounds = new ObservableCollection<FunnySoundModel>
            {
                _funnySoundsManager.GetFunnySoundByName(suggestedFunnySoundName)
            };
            _searchedFunnySoundNames.Push(suggestedFunnySoundName);
            FunnySoundsGridView.ItemsSource = FunnySounds;
        }

        private void SettingPostSearchParameters(string suggestedFunnySoundName)
        {
            SoundsTitleTextBlock.Text = suggestedFunnySoundName;
            FunnySoundsMenuListView.SelectedItem = null;
            //_previousSelectedType = _currentSelectedType;
            //_currentSelectedType = FunnySoundTypes.Search;
            _selectedTypes.Push(_currentSelectedType);
            _currentSelectedType = FunnySoundTypes.Search;
            BackButton.Visibility = Visibility.Visible;
        }

        private string SoundSearchAutoSuggestBoxInputValiation(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                return args.ChosenSuggestion.ToString();
            }
            else if (!String.IsNullOrEmpty(sender.Text))
            {
                return sender.Text;
            }
            else
            {
                return null;
            }
        }

        private void SoundSearchAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            //if (String.IsNullOrEmpty(sender.Text))
            //{
            //    BackButton_Click(null, null);
            //}
            var startWith = _funnySoundsManager.GetAllFunnySounds().Where(s => s.Name.StartsWith(sender.Text, StringComparison.OrdinalIgnoreCase));
            _suggestedFunnySoundsNames = startWith.Select(s => s.Name.ToString()).ToList();
            SoundSearchAutoSuggestBox.ItemsSource = _suggestedFunnySoundsNames;
        }

        private void FunnySoundsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedSound = (FunnySoundModel)e.ClickedItem;
            FunnySoundsMediaElement.Source = new Uri(this.BaseUri, selectedSound.SoundFilePath);
        }

        //private void FunnySoundImage_PointerEntered(object sender, PointerRoutedEventArgs e)
        //{
        //    var storyboard = (Storyboard)this.Resources["expandStoryboard"];
        //    foreach (var storyboardChildren in storyboard.Children)
        //    {
        //        Storyboard.SetTarget(storyboardChildren, (sender as Image));
        //    }
        //    storyboard.Begin();
        //}

        //private void FunnySoundImage_PointerExited(object sender, PointerRoutedEventArgs e)
        //{
        //    var storyboard = (Storyboard)this.Resources["shrinkStoryboard"];
        //    foreach (var storyboardChildren in storyboard.Children)
        //    {
        //        Storyboard.SetTarget(storyboardChildren, (sender as Image));
        //    }
        //    storyboard.Begin();
        //}
    }
}