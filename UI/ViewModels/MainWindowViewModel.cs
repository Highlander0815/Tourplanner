using BLL;

namespace UI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private DisplayRouteViewModel _displayRouteViewModel;
        private DisplayInfoViewModel _displayInfoViewModel;
        private DbManager _dbManager;
        private PDFManager _pdfManager;
        public MainWindowViewModel(DisplayRouteViewModel displayRouteViewModel, DisplayInfoViewModel displayInfoViewModel, DbManager dbManager, PDFManager pdfManager) //hier noch die anderen Adden
        {
            _displayRouteViewModel = displayRouteViewModel;
            _displayInfoViewModel = displayInfoViewModel;
            _dbManager = dbManager;

            _dbManager.EnsureDbCreated();

            _pdfManager = pdfManager;

            pdfManager.createPDF(2);
        }
    }
}
