using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private async void UpdatePlaybackInfo(GlobalSystemMediaTransportControlsSession session, PlaybackInfoChangedEventArgs args)
        {
            await Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() =>
            {
                if (session != null && session.GetPlaybackInfo() != null)
                {
                    UpdateStatusIcon(session);
                }
            }));
        }

        private void UpdateStatusIcon(GlobalSystemMediaTransportControlsSession session)
        {
            try
            {
                var playbackInfo = session.GetPlaybackInfo();
                switch (playbackInfo.PlaybackStatus)
                {
                    case GlobalSystemMediaTransportControlsSessionPlaybackStatus.Playing:
                        MusicStatus.Text = "\uE768";
                        break;
                    case GlobalSystemMediaTransportControlsSessionPlaybackStatus.Paused:
                        MusicStatus.Text = "\uE769";
                        break;
                    case GlobalSystemMediaTransportControlsSessionPlaybackStatus.Stopped:
                        MusicStatus.Text = "\uE71A";
                        break;
                    case GlobalSystemMediaTransportControlsSessionPlaybackStatus.Changing:
                        MusicStatus.Text = "\uE893";
                        break;
                    default:
                        MusicStatus.Text = "\uEC4F";
                        break;
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
                MainButton.Visibility = Visibility.Visible;
            }
            else
            {
                MainButton.Visibility = Visibility.Collapsed;
            }

            SMTC.CurrentSessionChanged += SMTC_CurrentSessionChanged;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SMTC_CurrentSessionChanged(SMTC, null);
        }
    }
}
