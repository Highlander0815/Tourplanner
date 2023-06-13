using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;


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
        }

        /// <summary>
        /// Getter for retrieving and binding the MainViewModel in MainWindow.xaml as its DataContext
        /// </summary>
        /*public MainViewModel MainViewModel
            => _serviceProvider.GetService<MainViewModel>();*/

    }
}
