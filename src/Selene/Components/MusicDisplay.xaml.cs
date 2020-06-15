using Selene.Flyouts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;
using Windows.Media.Control;

namespace Selene.Components
{
    /// <summary>
    /// Interaction logic for MusicDisplay.xaml
    /// </summary>
    public partial class MusicDisplay : UserControl
    {
        private GlobalSystemMediaTransportControlsSessionManager SMTC;

        private GlobalSystemMediaTransportControlsSession CurrentSession;

        private MusicFlyout Flyout;

        public MusicDisplay()
        {
            InitializeComponent();

            SetupNowPlaying();
        }

        public async void UpdateMediaProperties(GlobalSystemMediaTransportControlsSession session, MediaPropertiesChangedEventArgs args)
        {
            await Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() =>
            {
                if (session != null && session.GetPlaybackInfo() != null)
                {
                    UpdateSessionInfo(session);
                }
            }));
        }

        private async void UpdateSessionInfo(GlobalSystemMediaTransportControlsSession session)
        {
            try
            {
                var mediaInfo = await session.TryGetMediaPropertiesAsync();
                if (mediaInfo.Artist != "")
                {
                    MusicPlaying.Text = mediaInfo.Artist + " - " + mediaInfo.Title;
                } 
                else
                {
                    MusicPlaying.Text = mediaInfo.Title;
                }
            }
            catch (Exception)
            {

            }
        }

        public async void SetupNowPlaying()
        {
            SMTC = await GlobalSystemMediaTransportControlsSessionManager.RequestAsync();

            CurrentSession = SMTC.GetCurrentSession();

            if (CurrentSession != null)
            {
                UpdateSessionInfo(CurrentSession);
                CurrentSession.MediaPropertiesChanged += UpdateMediaProperties;
                MainButton.Visibility = Visibility.Visible;
            }
            else
            {
                MainButton.Visibility = Visibility.Collapsed;
            }

            SMTC.CurrentSessionChanged += SMTC_CurrentSessionChanged;
            Flyout = new MusicFlyout(SMTC);
        }

        private async void SMTC_CurrentSessionChanged(GlobalSystemMediaTransportControlsSessionManager smtc, CurrentSessionChangedEventArgs args)
        {
            await Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() =>
            {
                if (CurrentSession != null)
                {
                    CurrentSession.MediaPropertiesChanged -= UpdateMediaProperties;
                }
                CurrentSession = smtc.GetCurrentSession();
                if (CurrentSession != null)
                {
                    UpdateSessionInfo(CurrentSession);
                    CurrentSession.MediaPropertiesChanged += UpdateMediaProperties;
                    MainButton.Visibility = Visibility.Visible;
                }
                else
                {
                    MainButton.Visibility = Visibility.Collapsed;
                }
            }));
        }

        private void ShowFlyout()
        {
            if (Flyout.Visibility == Visibility.Hidden)
            {
                // Grab DPI scale beacuse PointToScreen doesn't use that for some reason.
                double factor = PresentationSource.FromVisual(this).CompositionTarget.TransformToDevice.M11;

                var screenLoc = this.PointToScreen(new Point(0d, 0d));
                var widthDif = this.ActualWidth - Flyout.Width;

                Flyout.Top = screenLoc.Y + 30;
                Flyout.Left = screenLoc.X / factor + widthDif / 2 ;

                Flyout.Show();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShowFlyout();
        }
    }
}
