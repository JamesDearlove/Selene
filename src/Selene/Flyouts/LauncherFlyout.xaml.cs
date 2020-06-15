using Selene.Properties;
using Selene.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Selene.Controls;

namespace Selene.Flyouts
{
    /// <summary>
    /// Interaction logic for LauncherFlyout.xaml
    /// </summary>
    public partial class LauncherFlyout : FlyoutWindow
    {
        public LauncherFlyout()
        {
            InitializeComponent();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow();
            settingsWindow.Show();
        }
    }
}
