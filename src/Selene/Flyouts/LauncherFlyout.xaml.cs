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

namespace Selene.Flyouts
{
    /// <summary>
    /// Interaction logic for LauncherFlyout.xaml
    /// </summary>
    public partial class LauncherFlyout : Window
    {
        public LauncherFlyout()
        {
            InitializeComponent();
            this.Hide();
        }

        public void HideFlyout()
        {
            Storyboard sb = this.FindResource("HideAnimation") as Storyboard;
            sb.Begin();
            sb.Completed += HideAnimationComplete;
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                this.Activate();
                Storyboard sb = this.FindResource("ShowAnimation") as Storyboard;
                sb.Begin();
            }
        }

        private void HideAnimationComplete(object sender, EventArgs e)
        {
            this.Hide();
            this.Topmost = false;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Topmost = true;
            HideFlyout();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow();
            settingsWindow.Show();
        }
    }
}
