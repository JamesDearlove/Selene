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
using System.Windows.Shapes;

namespace Selene.Windows
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            Setup();
        }

        private void Setup()
        {
            Version version = Assembly.GetEntryAssembly().GetName().Version;

            VersionText.Text = "Version " + version;

            Microsoft.Win32.RegistryKey startUpKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            var appExe = Assembly.GetExecutingAssembly().GetName().Name;
            var key = startUpKey.GetValue(appExe);
            if (key != null)
            {
                StartupCheckbox.IsChecked = true;
            }
        }

        private void DragBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GithubText_MouseUp(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.github.com/JamesDearlove/Selene");
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var appLocation = Assembly.GetEntryAssembly().Location;
            var appExe = Assembly.GetExecutingAssembly().GetName().Name;
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (key == null)
            {
                key.SetValue(appExe, appLocation);
                key.Close();
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var appExe = Assembly.GetExecutingAssembly().GetName().Name;
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            key.DeleteValue(appExe);
            key.Close();
        }
    }
}
