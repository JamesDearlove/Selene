using Selene.Flyouts;
using Selene.Glyphs;
using System;
using System.Windows;
using System.Windows.Threading;
using Windows.System.Power;

namespace Selene.Components
{
    /// <summary>
    /// Interaction logic for BatteryDisplay.xaml
    /// </summary>
    public partial class BatteryDisplay : System.Windows.Controls.UserControl
    {
        PowerInfoFlyout Flyout;

        public BatteryDisplay()
        {
            InitializeComponent();

            Flyout = new PowerInfoFlyout();

            PowerManager.RemainingChargePercentChanged += BatteryInfoChanged;
            PowerManager.BatteryStatusChanged += BatteryInfoChanged;
            PowerManager.RemainingDischargeTimeChanged += BatteryInfoChanged;
            UpdateInfo();
            UpdateTooltip();
        }

        private void BatteryInfoChanged(object sender, object e)
        {
            UpdateInfo();
            UpdateTooltip();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Flyout.Show(this);
        }

        private async void UpdateTooltip()
        {
            var remainingTime = PowerManager.RemainingDischargeTime;
            bool charging = PowerManager.PowerSupplyStatus != PowerSupplyStatus.NotPresent;
            var tooltipText = "";

            if (charging)
            {
                tooltipText = "Charging";
            }
            else
            {
                if (remainingTime.TotalDays < 5)
                {
                    tooltipText = remainingTime.ToString("%h' hr '%m' min remaining'");
                }
                else
                {
                    var remainingCharge = PowerManager.RemainingChargePercent;
                    tooltipText = $"{remainingCharge}% remaining";
                }
            }

            await Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() =>
            {
                ControlButton.ToolTip = tooltipText;
            }));
        }

        private async void UpdateInfo()
        {
            var batteryText = "?";
            var batteryGlyph = char.ConvertFromUtf32((int)BatteryLevels.VerticalBatteryUnknown);

            if (PowerManager.BatteryStatus != BatteryStatus.NotPresent)
            {
                var remaining = PowerManager.RemainingChargePercent;

                batteryText = $"{remaining}%";
                if (PowerManager.PowerSupplyStatus == PowerSupplyStatus.NotPresent)
                {
                    batteryGlyph = GlyphMethods.BatteryNormalGlyph(remaining);
                }
                else
                {
                    batteryGlyph = GlyphMethods.BatteryChargingGlyph(remaining);
                }
            }

            await Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() =>
            {
                BatteryLevelText.Text = batteryText;
                BatteryIcon.Text = batteryGlyph;
            }));
        }
    }
}
