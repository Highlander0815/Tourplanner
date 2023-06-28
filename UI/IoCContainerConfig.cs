using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using UI.ViewModels;
using System.ComponentModel;

namespace UI
{
    public class IoCContainerConfig
    {
        private readonly ServiceProvider _serviceProvider;

        /// <summary>
        /// Builds the IoC service provider, see also App.xaml which instantiates it as a resource
        /// </summary>
        public IoCContainerConfig()
        {
            var services = new ServiceCollection();
            //services.AddSingleton(typeof(IServiceProvider));
            services.AddSingleton<MainWindowViewModel>(); //Add Sigleton besteht nur einmal //AddTransient wird erstellt und wenn ich es nimmer brauche wird es gelöscht
            services.AddSingleton<SideMenuViewModel>();
            services.AddTransient<AddTourViewModel>();
            services.AddTransient<EditTourViewModel>();
            services.AddSingleton<BottomMenuViewModel>();
            services.AddSingleton<CenterWindowViewModel>();
            services.AddSingleton<DisplayRouteViewModel>();
            services.AddSingleton<DisplayInfoViewModel>();
            services.AddSingleton<CreateTourLogViewModel>();
            services.AddSingleton<MenuViewModel>();
            services.AddSingleton<TourViewModel>();
            // whenever an IArgumentHandler is required, the service will inject a CommandLineArgumentHandler
            // it will always provide the same CommandLineArgumentHandler instance, because we register it as a singleton
            /*services.AddSingleton<IArgumentHandler, CommandLineArgumentHandler>();*/

            // same for ICommunicationHandler, IContentInterpreter, IFilterHandler
            /*services.AddSingleton<ICommunicationHandler, NetworkCommunicationHandler>();
            services.AddSingleton<IContentInterpreter, HTTPOutputInterpreter>();
            services.AddSingleton<IFilterHandler, CsvBasedFilter>();*/

            // register the MainViewModel as well, the ServiceProvider will provide the constructor parameters
            // for the MainViewModel based on the configuration above
            /*services.AddSingleton<MainViewModel>();*/

            // finish configuration and build the provider
            /*_serviceProvider = services.BuildServiceProvider();*/
            _serviceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// Getter for retrieving and binding the MainViewModel in MainWindow.xaml as its DataContext
        /// </summary>
        public SideMenuViewModel SideMenuViewModel => _serviceProvider.GetService<SideMenuViewModel>(); //hier wird 
        public MainWindowViewModel MainWindowViewModel => _serviceProvider.GetService<MainWindowViewModel>();
        public AddTourViewModel AddTourViewModel => _serviceProvider.GetService<AddTourViewModel>();
        public EditTourViewModel EditTourViewModel => _serviceProvider.GetService<EditTourViewModel>();
        public BottomMenuViewModel BottomMenuViewModel => _serviceProvider.GetService<BottomMenuViewModel>();
        public CenterWindowViewModel CenterWindowViewModel => _serviceProvider.GetService<CenterWindowViewModel>();
        public DisplayRouteViewModel DisplayRouteViewModel => _serviceProvider.GetService<DisplayRouteViewModel>();
        public DisplayInfoViewModel DisplayInfoViewModel => _serviceProvider.GetService<DisplayInfoViewModel>();
        public CreateTourLogViewModel CreateTourLogViewModel => _serviceProvider.GetService<CreateTourLogViewModel>();
        public MenuViewModel MenuViewModel => _serviceProvider.GetService<MenuViewModel>();
        public TourViewModel TourViewModel => _serviceProvider.GetService<TourViewModel>();
        //public MainViewModel AddTourViewModel => _serviceProvider.GetService<AddTourViewModel>();
    }
}
