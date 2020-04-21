using Selene.Glyphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Selene.Components
{
    /// <summary>
    /// Interaction logic for BatteryDisplay.xaml
    /// </summary>
    public partial class BatteryDisplay : System.Windows.Controls.UserControl
    {
        System.Threading.Timer UpdateTimer;

        double batteryPercent;
        System.Windows.Forms.PowerLineStatus plStatus;

        public BatteryDisplay()
        {
            InitializeComponent();

            UpdateTimer = new System.Threading.Timer(TimerTick, null,
                TimeSpan.Zero, TimeSpan.FromMilliseconds(1000));
        }

        private async void TimerTick(object state)
        {
            PowerStatus pwr = SystemInformation.PowerStatus;

            var percent = pwr.BatteryLifePercent * 100;
            if (percent != batteryPercent || pwr.PowerLineStatus != plStatus)
            {
                batteryPercent = percent;
                plStatus = pwr.PowerLineStatus;

                int percentRange = (int)Math.Floor(percent / 10);

                BatteryLevels level;

                switch (percentRange)
                {
                    case 0:
                        level = BatteryLevels.VerticalBattery0;
                        break;
                    case 1:
                        level = BatteryLevels.VerticalBattery1;
                        break;
                    case 2:
                        level = BatteryLevels.VerticalBattery2;
                        break;
                    case 3:
                        level = BatteryLevels.VerticalBattery3;
                        break;
                    case 4:
                        level = BatteryLevels.VerticalBattery4;
                        break;
                    case 5:
                        level = BatteryLevels.VerticalBattery5;
                        break;
                    case 6:
                        level = BatteryLevels.VerticalBattery6;
                        break;
                    case 7:
                        level = BatteryLevels.VerticalBattery7;
                        break;
                    case 8:
                        level = BatteryLevels.VerticalBattery8;
                        break;
                    case 9:
                        level = BatteryLevels.VerticalBattery9;
                        break;
                    case 10:
                        level = BatteryLevels.VerticalBattery10;
                        break;
                    default:
                        level = BatteryLevels.VerticalBattery0;
                        break;
                }

                int batteryLevel = (int)level;
                if (pwr.PowerLineStatus == System.Windows.Forms.PowerLineStatus.Online)
                {
                    batteryLevel += 11;
                }

                string b = char.ConvertFromUtf32(batteryLevel);

                await Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() =>
                {
                    BatteryLevelText.Text = percent.ToString() + "%";
                    BatteryIcon.Text = b;
                }));
            }
        }
    }
}
