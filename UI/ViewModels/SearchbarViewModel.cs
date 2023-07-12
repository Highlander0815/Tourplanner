using BLL;
using MiNET.UI;
using MiNET.Utils.Skins;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using TourplannerModel;

namespace UI.ViewModels
{
    public class SearchbarViewModel : ViewModelBase
    {
        private SideMenuViewModel _sideMenuViewModel;
        private IEnumerable<TourModel> _tours;
        public SearchbarViewModel(SideMenuViewModel sideMenuViewModel) 
        {
            _sideMenuViewModel = sideMenuViewModel;
            _tours = _sideMenuViewModel.Tours;
            TypOfSorting = "Asc";
            SortAfter = "Name";
        }

        private RelayCommand _updateCommand = null;
        public RelayCommand UpdateCommand => _updateCommand ??= new RelayCommand(Update);


        private string _searchText;
        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                Search();
            }
        }

        private void Search()
        {
            _tours = _sideMenuViewModel.Tours;
            if (_searchText == null)
                return;

            try
            {
                var regex = new Regex(_searchText);

                foreach (var tour in _tours)
                {
                    tour.Visible = regex.IsMatch(tour.Searchstring);
                }

                if(_typOfSorting == "Desc")
                {
                    if(_sortAfter == "Name")
                        _sideMenuViewModel.Tours = new ObservableCollection<TourModel>(_sideMenuViewModel.Tours.OrderByDescending(tour => tour.Name));
                    else if (_sortAfter == "EstimatedTime")
                        _sideMenuViewModel.Tours = new ObservableCollection<TourModel>(_sideMenuViewModel.Tours.OrderByDescending(tour => tour.EstimatedTime));
                    else if (_sortAfter == "Popularity")
                        _sideMenuViewModel.Tours = new ObservableCollection<TourModel>(_sideMenuViewModel.Tours.OrderByDescending(tour => tour.Popularity));
                    else if (_sortAfter == "Childfriendliness")
                        _sideMenuViewModel.Tours = new ObservableCollection<TourModel>(_sideMenuViewModel.Tours.OrderByDescending(tour => tour.ChildFriendliness));
                }
                if (_typOfSorting == "Asc")
                {
                    if(_sortAfter == "Name")
                        _sideMenuViewModel.Tours = new ObservableCollection<TourModel>(_sideMenuViewModel.Tours.OrderBy(tour => tour.Name));
                    else if (_sortAfter == "EstimatedTime")
                        _sideMenuViewModel.Tours = new ObservableCollection<TourModel>(_sideMenuViewModel.Tours.OrderBy(tour => tour.EstimatedTime));
                    else if (_sortAfter == "Popularity")
                        _sideMenuViewModel.Tours = new ObservableCollection<TourModel>(_sideMenuViewModel.Tours.OrderBy(tour => tour.Popularity));
                    else if (_sortAfter == "Childfriendliness")
                        _sideMenuViewModel.Tours = new ObservableCollection<TourModel>(_sideMenuViewModel.Tours.OrderBy(tour => tour.ChildFriendliness));
                }

                _sideMenuViewModel.UpdateView();
            }
            catch (Exception e)
            {
                MessageBox.Show("Invalid Input! Try to Search for Full Words only!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                SearchText = null;
            }
        }

        public ObservableCollection<string> Options { get; set; } = new ObservableCollection<string>()
        {
            "Name",
            "EstimatedTime",
            "Popularity",
            "Childfriendliness"
        };
        public ObservableCollection<string> TypesOfSorting { get; set; } = new ObservableCollection<string>()
        {
            "Desc",
            "Asc"
        };
        private string _sortAfter;
        public string SortAfter
        {
            get
            {
                return _sortAfter;
            }
            set
            {          
                 _sortAfter = value;
                Search();
            }
        }
        private string _typOfSorting;

        public string TypOfSorting
        {
            get { return _typOfSorting; }
            set
            {
                _typOfSorting = value;
                OnPropertyChanged(nameof(TypOfSorting));
                Search();
            }
        }

        private void Update()
        {
            Search();
        }

    }
}
