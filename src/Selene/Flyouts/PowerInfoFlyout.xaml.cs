using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Selene.Controls;
using Selene.Glyphs;
using Windows.System;

namespace Selene.Flyouts
{
    /// <summary>
    /// Interaction logic for PowerInfoFlyout.xaml
    /// </summary>
    public partial class PowerInfoFlyout : FlyoutWindow
    {
        public PowerInfoFlyout()
        {
            InitializeComponent();
        }

        private async void BatterySettingsLink_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var uri = new Uri("ms-settings:batterysaver");

            await Launcher.LaunchUriAsync(uri);
        }

        private async void BatteryUsageLink_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var uri = new Uri("ms-settings:batterysaver-usagedetails");

            await Launcher.LaunchUriAsync(uri);
        }
    }
}
