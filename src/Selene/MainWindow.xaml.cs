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
using System.Diagnostics;
using Windows.Media.Control;
using System.Windows.Interop;

namespace Selene
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int StatusBarHeight = 30;

        public MainWindow()
        {
            DataContext = this;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Height = StatusBarHeight;

            AppBarFunctions.SetAppBar(this, ABEdge.Top);
            HideFromTab();
        }

        private void HideFromTab()
        {
            var wndHelper = new WindowInteropHelper(this);
            var exStyle = (long)User32Methods.GetWindowLongPtr(wndHelper.Handle, (int)WindowLongFlags.GWL_EXSTYLE);

            exStyle |= (int)WindowExStyles.WS_EX_TOOLWINDOW;
            User32Methods.SetWindowLongPtr(wndHelper.Handle, (int)WindowLongFlags.GWL_EXSTYLE, (IntPtr)exStyle);

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
            
            
        }

        public void FixSize()
        {
            AppBarFunctions.SetAppBar(this, ABEdge.None);
            AppBarFunctions.SetAppBar(this, ABEdge.Top);
        }

        bool light;

        private void UpdateTheme()
        {
            var app = App.Current as App;
            if (light)
            {
                App.Current.Resources.Source = new Uri("/Themes/Dark.xaml", UriKind.RelativeOrAbsolute);
                light = false;
                App.Current.Resources["AccentColor"] = new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x78, 0xd4));
            }
            else
            {
                App.Current.Resources.Source = new Uri("/Themes/Light.xaml", UriKind.RelativeOrAbsolute);
                light = true;
                App.Current.Resources["AccentColor"] = new SolidColorBrush(Color.FromArgb(0xFF, 0xca, 0x50, 0x10));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //UpdateTheme();
        }
    }
}
