using BLL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourplannerModel;

namespace UI.ViewModels
{
    public class SearchbarViewModel : ViewModelBase
    {
        private SideMenuViewModel _sideMenuViewModel;
        public SearchbarViewModel(SideMenuViewModel sideMenuViewModel) 
        {
            _sideMenuViewModel = sideMenuViewModel;
        }

        private RelayCommand _searchCommand = null;
        public RelayCommand SearchCommand => _searchCommand ??= new RelayCommand(StartSearching);

        private RelayCommand _sortAsc = null;
        public RelayCommand SortAsc => _sortAsc ??= new RelayCommand(SortAscending);

        private RelayCommand _sortDesc = null;
        public RelayCommand SortDesc => _sortDesc ??= new RelayCommand(SortDescending);

        private string _searchText;
        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                }
            }
        }

        public ObservableCollection<string> Options { get; set; } = new ObservableCollection<string>()
        {
            "Name",
            "Description",
            "Difficulty",
            "...ist noch nicht fertig"
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
                if (_sortAfter != value)
                {
                    _sortAfter = value;
                }
            }
        }

        private void StartSearching()
        {
            throw new NotImplementedException();
        }

        private void SortDescending()
        {
            throw new NotImplementedException();
        }

        private void SortAscending()
        {
            throw new NotImplementedException();
        }
    }
}
