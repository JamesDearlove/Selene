﻿using System;
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
using Windows.System;

namespace Selene.Components
{
    /// <summary>
    /// Interaction logic for ActionCenterButton.xaml
    /// </summary>
    public partial class ActionCenterButton : UserControl
    {
        public ActionCenterButton()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var uri = new Uri("ms-actioncenter:");

            await Launcher.LaunchUriAsync(uri);
        }
    }
}
