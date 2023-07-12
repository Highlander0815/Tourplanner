using Microsoft.Extensions.DependencyInjection;
using UI.ViewModels;
using Microsoft.Extensions.Configuration;
using DAL;
using BLL;
using Microsoft.EntityFrameworkCore;
using BLL.Logging;

namespace UI
{
    public class IoCContainerConfig
    {
        //IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

        private readonly ServiceProvider _serviceProvider;

        public IoCContainerConfig()
        {
            var services = new ServiceCollection();

            services.AddDbContext<TourplannerContext>();
            //services.AddSingleton<TourHandler>();
            //services.AddSingleton<TourLogHandler>();

            //services.AddSingleton(typeof(IServiceProvider));
            services.AddSingleton<DbManager>();
            services.AddSingleton<PDFManager>();
            //services.AddSingleton<ImportExportManager>();
            services.AddSingleton<MainWindowViewModel>();
            
            services.AddSingleton<SideMenuViewModel>();
            services.AddTransient<AddTourViewModel>();
            services.AddTransient<EditTourViewModel>();

            services.AddSingleton<BottomMenuViewModel>();
            services.AddTransient<AddTourLogViewModel>();
            services.AddTransient<EditTourLogViewModel>();


            services.AddSingleton<CenterWindowViewModel>();
            services.AddSingleton<DisplayRouteViewModel>();
            services.AddSingleton<DisplayInfoViewModel>();

            services.AddSingleton<MenuViewModel>();
            services.AddSingleton<TourViewModel>();
            services.AddSingleton<TourLogViewModel>();

            services.AddSingleton<SearchbarViewModel>();

            _serviceProvider = services.BuildServiceProvider();
        }
        public MainWindowViewModel MainWindowViewModel => _serviceProvider.GetRequiredService<MainWindowViewModel>();

        public TourplannerContext TourplannerContext => _serviceProvider.GetRequiredService<TourplannerContext>();
        //public TourHandler TourHandler => _serviceProvider.GetRequiredService<TourHandler>();
        //public TourLogHandler TourLogHandler => _serviceProvider.GetRequiredService<TourLogHandler>();

        public SideMenuViewModel SideMenuViewModel => _serviceProvider.GetRequiredService<SideMenuViewModel>();
        public AddTourViewModel AddTourViewModel => _serviceProvider.GetRequiredService<AddTourViewModel>();
        public EditTourViewModel EditTourViewModel => _serviceProvider.GetRequiredService<EditTourViewModel>();
       
        public BottomMenuViewModel BottomMenuViewModel => _serviceProvider.GetRequiredService<BottomMenuViewModel>();
        public AddTourLogViewModel AddTourLogViewModel => _serviceProvider.GetRequiredService<AddTourLogViewModel>();
        public EditTourLogViewModel EditTourLogViewModel => _serviceProvider.GetRequiredService<EditTourLogViewModel>();
        public CenterWindowViewModel CenterWindowViewModel => _serviceProvider.GetRequiredService<CenterWindowViewModel>();
        public DisplayRouteViewModel DisplayRouteViewModel => _serviceProvider.GetRequiredService<DisplayRouteViewModel>();
        public DisplayInfoViewModel DisplayInfoViewModel => _serviceProvider.GetRequiredService<DisplayInfoViewModel>();
        
        public MenuViewModel MenuViewModel => _serviceProvider.GetRequiredService<MenuViewModel>();
        public TourViewModel TourViewModel => _serviceProvider.GetRequiredService<TourViewModel>();
        public TourLogViewModel TourLogViewModel => _serviceProvider.GetRequiredService<TourLogViewModel>();
        public SearchbarViewModel SearchbarViewModel => _serviceProvider.GetRequiredService<SearchbarViewModel>();
        //public MainViewModel AddTourViewModel => _serviceProvider.GetService<AddTourViewModel>();
    }
}
