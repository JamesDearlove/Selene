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
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace Selene.Flyouts
{
    /// <summary>
    /// Interaction logic for MusicFlyout.xaml
    /// </summary>
    public partial class MusicFlyout : Window
    {
        public MusicFlyout()
        {
            InitializeComponent();
            this.Hide();
        }

        public void HideFlyout()
        {
            Storyboard sb = this.FindResource("HideAnimation") as Storyboard;
            //Storyboard.SetTarget(sb, this.MainGrid);
            sb.Begin();
            sb.Completed += Sb_Completed;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                Storyboard sb = this.FindResource("ShowAnimation") as Storyboard;
                //Storyboard.SetTarget(sb, this.MainGrid);
                sb.Begin();
                this.Topmost = true;
            }
        }

        private void Sb_Completed(object sender, EventArgs e)
        {
            this.Hide();
            //throw new NotImplementedException();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            HideFlyout();
        }
    }
}
