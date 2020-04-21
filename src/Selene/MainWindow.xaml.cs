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

namespace Selene
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int StatusBarHeight = 30;

        bool SettingBar = true;

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Height = StatusBarHeight;

            AppBarFunctions.SetAppBar(this, ABEdge.Top);
            SettingBar = false;
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
    }
}
