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
using Windows.Media.Control;
using System.Windows.Threading;
using Windows.Storage.Streams;
using System.IO;
using Selene.Glyphs;

namespace Selene.Flyouts
{
    /// <summary>
    /// Interaction logic for MusicFlyout.xaml
    /// </summary>
    public partial class MusicFlyout : Window
    {
        GlobalSystemMediaTransportControlsSessionManager SMTC;

        public MusicFlyout(GlobalSystemMediaTransportControlsSessionManager smtc)
        {
            InitializeComponent();

            this.Hide();
            SMTC = smtc;
            Setup();
        }

        public void Setup()
        {
            SMTC.SessionsChanged += SMTC_SessionsChanged;
            SMTC_SessionsChanged(SMTC, null);
        }

        private async void SMTC_SessionsChanged(GlobalSystemMediaTransportControlsSessionManager sender, SessionsChangedEventArgs args)
        {
            await Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() =>
            {
                var sessions = SMTC.GetSessions();

                foreach (MusicSessionControl ses in SessionStackPanel.Children)
                {
                    ses.ClearSession();
                }
                SessionStackPanel.Children.Clear();

                foreach (var session in sessions)
                {
                    var newControl = new MusicSessionControl(session);
                    SessionStackPanel.Children.Add(newControl);
                }

                this.Height = 30 + sessions.Count * 60;
            }));
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
    }
}
