using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace FunnySoundsUWPApp
{
    class DataManipulationViewModel : INotifyPropertyChanged
    {
        public IFunnySoundsViewModel FunnySoundsViewModel = new FunnySoundsViewModel();
        public IMenuItemsViewModel MenuItemsViewModel = new MenuItemsViewModel();
        private DelegateCommand _delegatedCommand = null;
        private Stack<FunnySoundTypes> _selectedTypes = new Stack<FunnySoundTypes>();
        private Stack<string> _searchedFunnySoundNames = new Stack<string>();
        private FunnySoundTypes _currentSelectedType = FunnySoundTypes.All;
        //private FunnySoundTypes _previousSelectedType = FunnySoundTypes.None;
        private List<string> _suggestedFunnySoundsNames;


        public List<string> SuggestedFunnySoundsNames
        {
            get { return _suggestedFunnySoundsNames; }
            set
            {
                if (value != _suggestedFunnySoundsNames)
                {
                    _suggestedFunnySoundsNames = value;
                    OnPropertyChanged(nameof(SuggestedFunnySoundsNames));
                }
            }
        }

        private string _soundsTitle;

        public string SoundsTitle
        {
            get { return _soundsTitle; }
            set
            {
                if (value != _soundsTitle)
                {
                    _soundsTitle = value;
                    OnPropertyChanged(nameof(SoundsTitle));
                }
            }
        }

        private string _searchBoxText;

        public string SearchBoxText
        {
            get { return _searchBoxText; }
            set
            {
                if (value != _searchBoxText)
                {
                    _searchBoxText = value;
                    OnPropertyChanged(nameof(SearchBoxText));
                }
            }
        }

        private bool _isBackButtonVisible;

        public bool IsBackButtonVisible
        {
            get { return _isBackButtonVisible; }
            set
            {
                if (value != _isBackButtonVisible)
                {
                    _isBackButtonVisible = value;
                    OnPropertyChanged(nameof(IsBackButtonVisible));
                }
            }
        }


        private bool _isSplitViewPaneOpen;

        public bool IsSplitViewPaneOpen
        {
            get { return _isSplitViewPaneOpen; }
            set
            {
                if (value != _isSplitViewPaneOpen)
                {
                    _isSplitViewPaneOpen = value;
                    OnPropertyChanged(nameof(IsSplitViewPaneOpen));
                    _delegatedCommand?.OnCanExecuteChanged();
                }
            }
        }

        private MenuItemModel _selectedMenuItem;

        public MenuItemModel SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set
            {
                if (value != _selectedMenuItem)
                {
                    _selectedMenuItem = value;
                    OnPropertyChanged(nameof(SelectedMenuItem));
                }
            }
        }

        private FunnySoundModel _selectedFunnySound;

        public FunnySoundModel SelectedFunnySound
        {
            get { return _selectedFunnySound; }
            set
            {
                if (value != _selectedFunnySound)
                {
                    _selectedFunnySound = value;
                    OnPropertyChanged(nameof(SelectedFunnySound));
                }
            }
        }


        //public ObservableCollection<FunnySoundModel> FunnySounds { get; private set; }
        public ObservableCollection<MenuItemModel> MenuItems { get; private set; }

        //protected override void OnPropertyChanged(string propertyName) => base.OnPropertyChanged(propertyName);

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void MainPage()
        {
            //IFunnySoundsViewModel _funnySoundsViewModel = new FunnySoundsViewModel();
            //IMenuItemsViewModel _menuItemsViewModel = new MenuItemsViewModel();
            //FunnySounds = _funnySoundsViewModel.GetAllFunnySounds();


            //FunnySoundsGridView.ItemsSource = FunnySoundsViewModel.FunnySounds;


            MenuItems = MenuItemsViewModel.GetMenuItems();
            IsBackButtonVisible = false;
            SelectedMenuItem = MenuItems[0];
            //_selectedTypes.Push(FunnySoundTypes.None);
        }

        public ICommand HamburgerMenuButtonOnClick
        {
            get
            {
                _delegatedCommand = new DelegateCommand(() => { IsSplitViewPaneOpen = !IsSplitViewPaneOpen;});
                return _delegatedCommand;
            }
        }

        public ICommand BackButtonOnClick
        {
            get
            {
                return new DelegateCommand(BackButtonClick);
            }
        }

        private void BackButtonClick()
        {
            //FunnySounds = _funnySoundsManager.GetFunnySoundsByType(_previousSelectedType);
            //SoundsTitleText.Text = _previousSelectedType.ToString();

            //FunnySoundTypes previousSelectedType = FunnySoundTypes.All;

            if (_selectedTypes.Count != 1)
            {
                if (_currentSelectedType == FunnySoundTypes.Search && _searchedFunnySoundNames.Count != 0)
                {
                    _searchedFunnySoundNames.Pop();
                }
                _currentSelectedType = _selectedTypes.Pop();
                FunnySoundsViewModel.GetFunnySoundsByType(_currentSelectedType);
            }
            else
            {
                _currentSelectedType = FunnySoundTypes.All;
                FunnySoundsViewModel.GetAllFunnySounds();
                _selectedTypes.Pop();
            }

            if (_currentSelectedType != FunnySoundTypes.Search)
            {
                SelectedMenuItem = MenuItems.Where(m => m.Type == _currentSelectedType).Single();
                ClearSoundSearcAutoSuggestBoxtext();
                //FunnySoundsGridView.ItemsSource = FunnySounds;
                SoundsTitle = _currentSelectedType.ToString();
                //if (_previousSelectedType == FunnySoundTypes.All)
                if (_currentSelectedType == FunnySoundTypes.All)
                {
                    IsBackButtonVisible = false;
                }
            }
            else
            {
                string searchedFunnySoundName = _searchedFunnySoundNames.Pop();
                DisplaySearchResults(searchedFunnySoundName);
                SoundsTitle = searchedFunnySoundName;
                SelectedMenuItem = null;
            }
        }

        private void FunnySoundsMenuListViewOnClick()
        {
            //SoundSearchAutoSuggestBox.Text = String.Empty;
            SoundsTitle = SelectedMenuItem.Type.ToString();
            //_previousSelectedType = _currentSelectedType;
            //_currentSelectedType = clickedMenuItem.Type;
            _selectedTypes.Push(_currentSelectedType);
            _currentSelectedType = SelectedMenuItem.Type;
            FunnySoundsViewModel.GetFunnySoundsByType(SelectedMenuItem.Type);
            //FunnySoundsGridView.ItemsSource = _funnySoundsViewModel.
            if (SelectedMenuItem.Type != FunnySoundTypes.All)
            {
                IsBackButtonVisible = true;
            }

            ClearSoundSearcAutoSuggestBoxtext();
        }


        private void ClearSoundSearcAutoSuggestBoxtext()
        {
            if (!String.IsNullOrEmpty(SearchBoxText))
            {
                SearchBoxText = String.Empty;
            }
        }

        private void SoundSearchAutoSuggestBoxOnQuerySubmitted()
        {
            //string suggestedFunnySoundName = SoundSearchAutoSuggestBoxInputValiation(SearchBoxText);
            if (String.IsNullOrEmpty(SearchBoxText))
            {
                return;
            }

            DisplaySearchResults(SearchBoxText);
            SettingPostSearchParameters(SearchBoxText);
        }

        private void DisplaySearchResults(string suggestedFunnySoundName)
        {
            //FunnySounds = new ObservableCollection<FunnySoundModel>
            //{
            //    _funnySoundsViewModel.GetFunnySoundByName(suggestedFunnySoundName)
            //};
            //_searchedFunnySoundNames.Push(suggestedFunnySoundName);
            //FunnySoundsGridView.ItemsSource = FunnySounds;
            FunnySoundsViewModel.GetFunnySoundByName(suggestedFunnySoundName);
        }

        private void SettingPostSearchParameters(string suggestedFunnySoundName)
        {
            SoundsTitle = suggestedFunnySoundName;
            SelectedMenuItem = null;
            //_previousSelectedType = _currentSelectedType;
            //_currentSelectedType = FunnySoundTypes.Search;
            _selectedTypes.Push(_currentSelectedType);
            _searchedFunnySoundNames.Push(suggestedFunnySoundName);
            _currentSelectedType = FunnySoundTypes.Search;
            IsBackButtonVisible = true;
        }

        //private string SoundSearchAutoSuggestBoxInputValiation()
        //{
        //    return SearchBoxText;
        //    //if (args.chosensuggestion != null)
        //    //{
        //    //    return args.chosensuggestion.tostring();
        //    //}
        //    //else if (!string.isnullorempty(sender.text))
        //    //{
        //    //    return sender.text;
        //    //}
        //    //else
        //    //{
        //    //    return null;
        //    //}
        //}

        private void SoundSearchAutoSuggestBoxOnTextChanged()
        {
            //if (String.IsNullOrEmpty(sender.Text))
            //{
            //    BackButton_Click(null, null);
            //}
            var startWith = FunnySoundsViewModel.AllFunnySounds.Where(s => s.Name.StartsWith(SearchBoxText, StringComparison.OrdinalIgnoreCase));
            SuggestedFunnySoundsNames = startWith.Select(s => s.Name.ToString()).ToList();
            //SoundSearchAutoSuggestBox.ItemsSource = _suggestedFunnySoundsNames;
        }

        private void FunnySoundsGridViewOnItemClick()
        {
            //var selectedSound = (FunnySoundModel)e.ClickedItem;
            //FunnySoundsMediaElement.Source = new Uri(this.BaseUri, selectedSound.SoundFilePath);
        }
    }
}
