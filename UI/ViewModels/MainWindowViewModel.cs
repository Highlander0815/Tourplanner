using BLL;

namespace UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private DisplayRouteViewModel _displayRouteViewModel;
        private DisplayInfoViewModel _displayInfoViewModel;
        private DbManager _dbManager;
        public MainWindowViewModel(DisplayRouteViewModel displayRouteViewModel, DisplayInfoViewModel displayInfoViewModel, DbManager dbManager) //hier noch die anderen Adden
        {
            _displayRouteViewModel = displayRouteViewModel;
            _displayInfoViewModel = displayInfoViewModel;
            _dbManager = dbManager;

            _dbManager.EnsureDbCreated();
        }
    }
}
