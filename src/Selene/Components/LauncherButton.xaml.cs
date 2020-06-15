using Selene.Flyouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace Selene.Components
{
    /// <summary>
    /// Interaction logic for LauncherButton.xaml
    /// </summary>
    public partial class LauncherButton : UserControl
    {
        LauncherFlyout Flyout;

        public LauncherButton()
        {
            InitializeComponent();

            Flyout = new LauncherFlyout();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShowFlyout();
        }

        private void ShowFlyout()
        {
            if (!Flyout.IsVisible)
            {
                Flyout.Show(this);
            }
        }
    }
}
