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

namespace Selene.Components
{
    /// <summary>
    /// Interaction logic for PowerButton.xaml
    /// </summary>
    public partial class PowerButton : UserControl
    {
        public PowerButton()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var myWindow = Window.GetWindow(this);
            //myWindow.Close();
            Application.Current.Shutdown();
        }
    }
}
