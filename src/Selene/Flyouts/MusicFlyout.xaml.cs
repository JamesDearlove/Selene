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

        GlobalSystemMediaTransportControlsSession currentSession;

        public MusicFlyout(GlobalSystemMediaTransportControlsSessionManager smtc)
        {
            InitializeComponent();

            this.Hide();
            SMTC = smtc;
            Setup();
        }

        public void Setup()
        {
            currentSession = SMTC.GetCurrentSession();
            
            SMTC.CurrentSessionChanged += SMTC_CurrentSessionChanged;
            SMTC_CurrentSessionChanged(SMTC, null);
        }

        public void UpdatePlaybackProperties()
        {
            try
            {
                var playbackInfo = currentSession.GetPlaybackInfo();

                if (playbackInfo != null)
                {
                    PreviousButton.IsEnabled = playbackInfo.Controls.IsPreviousEnabled;
                    NextButton.IsEnabled = playbackInfo.Controls.IsNextEnabled;

                    if (playbackInfo.PlaybackStatus == GlobalSystemMediaTransportControlsSessionPlaybackStatus.Playing)
                    {
                        PlayPauseButton.Content = "\uE769";
                    }
                    else if (playbackInfo.PlaybackStatus == GlobalSystemMediaTransportControlsSessionPlaybackStatus.Paused)
                    {
                        PlayPauseButton.Content = "\uE768";
                    }
                }
            }
            catch (Exception) { }
        }

        public async void UpdateMediaProperties()
        {
            try
            {
                var mediaInfo = await currentSession.TryGetMediaPropertiesAsync();

                Storyboard sbOut = this.FindResource("NextSongDisappear") as Storyboard;
                EventHandler onComplete = null;
                onComplete = (s, e) =>
                {
                    sbOut.Completed -= onComplete;
                    SongTitleText.Text = mediaInfo.Title;
                    SongArtistText.Text = mediaInfo.Artist;
                    Storyboard sbIn = this.FindResource("NextSongAppear") as Storyboard;
                    sbIn.Begin();
                };
                sbOut.Completed += onComplete;
                sbOut.Begin();
                await UpdateThumbnail(mediaInfo.Thumbnail);
            }
            catch (Exception) { }
        }

        private async Task UpdateThumbnail(IRandomAccessStreamReference thumbnail)
        {
            if (thumbnail != null)
            {
                using (var stream = await thumbnail.OpenReadAsync())
                {
                    if (stream != null)
                    {
                        using (var nstream = stream.AsStream())
                        {
                            if (nstream != null && nstream.Length > 0)
                            {
                                AlbumArtImage.Source = BitmapFrame.Create(nstream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                                AlbumArtImage.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                AlbumArtImage.Visibility = Visibility.Hidden;
                                AlbumArtImage.Source = null;
                            }
                        }
                    }
                    else
                    {
                        AlbumArtImage.Visibility = Visibility.Hidden;
                        AlbumArtImage.Source = null;
                    }
                }
            }
        }

        private async void SMTC_CurrentSessionChanged(GlobalSystemMediaTransportControlsSessionManager sender, CurrentSessionChangedEventArgs args)
        {
            await Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() =>
            {
                if (currentSession != null)
                {
                    currentSession.MediaPropertiesChanged -= CurrentSession_MediaPropertiesChanged;
                    currentSession.PlaybackInfoChanged -= CurrentSession_PlaybackInfoChanged;
                }
                currentSession = sender.GetCurrentSession();
                if (currentSession != null)
                {
                    currentSession.MediaPropertiesChanged += CurrentSession_MediaPropertiesChanged;
                    currentSession.PlaybackInfoChanged += CurrentSession_PlaybackInfoChanged;
                }
                UpdateMediaProperties();
                UpdatePlaybackProperties();
            }));
        }

        private async void CurrentSession_PlaybackInfoChanged(GlobalSystemMediaTransportControlsSession sender, PlaybackInfoChangedEventArgs args)
        {
            await Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() =>
            {
                UpdatePlaybackProperties();
            }));
        }

        private async void CurrentSession_MediaPropertiesChanged(GlobalSystemMediaTransportControlsSession sender, MediaPropertiesChangedEventArgs args)
        {
            await Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() =>
            {
                UpdateMediaProperties();
            }));
        }

        public void HideFlyout()
        {
            Storyboard sb = this.FindResource("HideAnimation") as Storyboard;
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
                this.Activate();
                Storyboard sb = this.FindResource("ShowAnimation") as Storyboard;
                sb.Begin();
            }
        }

        private void Sb_Completed(object sender, EventArgs e)
        {
            this.Hide();
            this.Topmost = false;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Topmost = true;
            HideFlyout();
        }

        private async void PreviousButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                await currentSession.TrySkipPreviousAsync();
            }
            catch (Exception) { }
        }

        private async void PlayPauseClick(object sender, RoutedEventArgs e)
        {
            try
            {
                await currentSession.TryTogglePlayPauseAsync();
            }
            catch (Exception) { }
        }

        private async void NextButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                await currentSession.TrySkipNextAsync();
            }
            catch (Exception) { }
        }
    }
}
