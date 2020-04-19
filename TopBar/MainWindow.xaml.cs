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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using WpfAppBar;
using System.Windows.Threading;
using WinApi.User32;
using WinApi.Core;

namespace TopBar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int StatusBarHeight = 30;

        Timer dispatcherTimer;

        double batteryPercent;
        System.Windows.Forms.PowerLineStatus plStatus;

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dispatcherTimer = new Timer();
                dispatcherTimer.Tick += new EventHandler(Timer_Tick);
                dispatcherTimer.Interval = 100;

                dispatcherTimer.Start();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

            this.Height = StatusBarHeight;

            AppBarFunctions.SetAppBar(this, ABEdge.Top);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            PowerStatus pwr = SystemInformation.PowerStatus;

            var percent = pwr.BatteryLifePercent * 100;
            if (percent != batteryPercent || pwr.PowerLineStatus != plStatus)
            {
                batteryPercent = percent;
                plStatus = pwr.PowerLineStatus;

                BatteryLevelText.Text = percent.ToString() + "%";

                int percentRange = (int)Math.Floor(percent / 10);

                BatteryLevels level;

                switch (percentRange)
                {
                    case 0:
                        level = BatteryLevels.Battery0;
                        break;
                    case 1:
                        level = BatteryLevels.Battery1;
                        break;
                    case 2:
                        level = BatteryLevels.Battery2;
                        break;
                    case 3:
                        level = BatteryLevels.Battery3;
                        break;
                    case 4:
                        level = BatteryLevels.Battery4;
                        break;
                    case 5:
                        level = BatteryLevels.Battery5;
                        break;
                    case 6:
                        level = BatteryLevels.Battery6;
                        break;
                    case 7:
                        level = BatteryLevels.Battery7;
                        break;
                    case 8:
                        level = BatteryLevels.Battery8;
                        break;
                    case 9:
                        level = BatteryLevels.Battery9;
                        break;
                    case 10:
                        level = BatteryLevels.Battery10;
                        break;
                    default:
                        level = BatteryLevels.Battery0;
                        break;
                }

                int batteryLevel = (int)level;
                if (pwr.PowerLineStatus == System.Windows.Forms.PowerLineStatus.Online)
                {
                    batteryLevel += 11;
                }

                string b = char.ConvertFromUtf32(batteryLevel);

                BatteryIcon.Text = b;
            }

            CurrentTimeText.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AppBarFunctions.SetAppBar(this, ABEdge.None);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OverflowTrayToggle(object sender, RoutedEventArgs e)
        {
            
            IntPtr statusOverflow = User32Methods.FindWindow("NotifyIconOverflowWindow", null);

            if (User32Methods.IsWindowVisible(statusOverflow))
            {
                User32Methods.ShowWindow(statusOverflow, ShowWindowCommands.SW_HIDE);
            }
            else
            {
                int buttonWidth = (int)StatusOverflow.Width;
                int buttonHeight = (int)StatusOverflow.Height;
                Point buttonPos = StatusOverflow.PointToScreen(new Point(0d, 0d));
                User32Methods.ShowWindow(statusOverflow, ShowWindowCommands.SW_SHOWNORMAL);

                NetCoreEx.Geometry.Rectangle windowRect;
                User32Methods.GetWindowRect(statusOverflow, out windowRect);

                int X = (int)buttonPos.X + 15 - windowRect.Width / 2;
                int Y = (int)buttonPos.Y + StatusBarHeight;

                User32Methods.MoveWindow(statusOverflow, X, Y, windowRect.Width, windowRect.Height, true);
            }
            //User32Methods.ShowWindow(statusOverflow, ShowWindowCommands.)
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            IntPtr window = User32Methods.FindWindow("SystemTray_Main", "Battery Meter");
            User32Methods.ShowWindow(window, ShowWindowCommands.SW_SHOWDEFAULT);
        }

        private void ActionCenterButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("ms-actioncenter:");
        }
    }

    public enum BatteryLevels
    {
        Battery0 = '\uF5F2',
        Battery1 = '\uF5F3',
        Battery2 = '\uF5F4',
        Battery3 = '\uF5F5',
        Battery4 = '\uF5F6',
        Battery5 = '\uF5F7',
        Battery6 = '\uF5F8',
        Battery7 = '\uF5F9',
        Battery8 = '\uF5FA',
        Battery9 = '\uF5FB', 
        Battery10 = '\uF5FC'
        //    ,
        //BatteryCharge0 = '\uF5FD',
        //BatteryCharge1 = '\uF5FE',
        //BatteryCharge2 = '\uF5FF',
        //BatteryCharge3 = '\uF600',
        //BatteryCharge4 = '\uF601',
        //BatteryCharge5 = '\uF602',
        //BatteryCharge6 = '\uF603',
        //BatteryCharge7 = '\uF604',
        //BatteryCharge8 = '\uF605',
        //BatteryCharge9 = '\uF606',
        //BatteryCharge10 = '\uF607'
    }
}
