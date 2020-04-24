using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Selene
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Mutex AppMutex;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
#if !DEBUG
            // Ensures only one instance is running
            bool aIsNewInstance = false;
            AppMutex = new Mutex(true, "Selene", out aIsNewInstance);
            if (!aIsNewInstance)
            {
                MessageBox.Show("Selene is already running.", "Selene",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                App.Current.Shutdown();
            }
#endif
        }
    }
}
