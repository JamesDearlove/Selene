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
            var remainingCharge = PowerManager.RemainingChargePercent;
            bool charging = PowerManager.PowerSupplyStatus != PowerSupplyStatus.NotPresent;
            var tooltipText = "";
            var flyoutText = "";

            if (charging)
            {
                if (remainingCharge == 100)
                {
                    tooltipText = "Fully charged";
                }
                else if (PowerManager.BatteryStatus == BatteryStatus.Charging)
                {
                    tooltipText = "Charging";
                }
                else if (PowerManager.BatteryStatus == BatteryStatus.Idle)
                {
                    tooltipText = "Plugged in";
                }
                flyoutText = tooltipText;
            }
            else
            {
                if (remainingTime.TotalDays < 5)
                {
                    tooltipText = remainingTime.ToString("%h' hr '%m' min remaining'");

                    if (remainingTime.TotalHours > 1)
                    {
                        flyoutText += remainingTime.ToString("%h' hours '");
                    }
                    else if (remainingTime.TotalHours == 1)
                    {
                        flyoutText += remainingTime.ToString("%h' hour '");
                    }
                    if (remainingTime.TotalMinutes > 1)
                    {
                        flyoutText += remainingTime.ToString("%m' minutes '");
                    }
                    else if (remainingTime.TotalMinutes == 1)
                    {
                        flyoutText += remainingTime.ToString("%m' minute '");
                    }
                    flyoutText += "remaining";
                }
                else
                {
                    tooltipText = $"{remainingCharge}% remaining";
                    flyoutText = tooltipText;
                }
            }

            await Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() =>
            {
                ControlButton.ToolTip = tooltipText;
                Flyout.BatteryTimeText.Text = flyoutText;
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
                    batteryGlyph = GlyphMethods.BatteryVerticalNormalGlyph(remaining);
                }
                else
                {
                    batteryGlyph = GlyphMethods.BatteryVerticalChargingGlyph(remaining);
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
