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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Windows.Media.Control;
using System.IO;
using Windows.Storage.Streams;

namespace Selene.Flyouts
{
    /// <summary>
    /// Interaction logic for MusicSessionControl.xaml
    /// </summary>
    public partial class MusicSessionControl : UserControl
    {
        public GlobalSystemMediaTransportControlsSession Session;

        public MusicSessionControl(GlobalSystemMediaTransportControlsSession session)
        {
            Session = session;
            InitializeComponent();

            Session.MediaPropertiesChanged += MediaPropertiesChanged;
            Session.PlaybackInfoChanged += PlaybackInfoChanged;
            UpdateMediaProperties();
            UpdatePlaybackProperties();
        }

        public void ClearSession()
        {
            Session.MediaPropertiesChanged -= MediaPropertiesChanged;
            Session.PlaybackInfoChanged -= PlaybackInfoChanged;
            Session = null;
        }

        public void UpdatePlaybackProperties()
        {
            try
            {
                var playbackInfo = Session.GetPlaybackInfo();

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
                var mediaInfo = await Session.TryGetMediaPropertiesAsync();

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

        private async void PlaybackInfoChanged(GlobalSystemMediaTransportControlsSession sender, PlaybackInfoChangedEventArgs args)
        {
            await Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() =>
            {
                UpdatePlaybackProperties();
            }));
        }

        private async void MediaPropertiesChanged(GlobalSystemMediaTransportControlsSession sender, MediaPropertiesChangedEventArgs args)
        {
            await Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() =>
            {
                UpdateMediaProperties();
            }));
        }

        private async void PreviousButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                await Session.TrySkipPreviousAsync();
            }
            catch (Exception) { }
        }

        private async void PlayPauseClick(object sender, RoutedEventArgs e)
        {
            try
            {
                await Session.TryTogglePlayPauseAsync();
            }
            catch (Exception) { }
        }

        private async void NextButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                await Session.TrySkipNextAsync();
            }
            catch (Exception) { }
        }

    }
}
