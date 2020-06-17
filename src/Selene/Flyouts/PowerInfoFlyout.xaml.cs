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
using Selene.Controls;
using Windows.System.Power;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(PowerManager.BatteryStatus);
            Console.WriteLine(PowerManager.EnergySaverStatus);
            Console.WriteLine(PowerManager.RemainingChargePercent);
            Console.WriteLine(PowerManager.RemainingDischargeTime);

            PowerManager.RemainingChargePercentChanged += PowerManager_RemainingChargePercentChanged;
        }

        private void PowerManager_RemainingChargePercentChanged(object sender, object e)
        {
            throw new NotImplementedException();
        }
    }
}
