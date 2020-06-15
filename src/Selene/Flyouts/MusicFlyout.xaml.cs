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
using Selene.Controls;

namespace Selene.Flyouts
{
    /// <summary>
    /// Interaction logic for MusicFlyout.xaml
    /// </summary>
    public partial class MusicFlyout : FlyoutWindow
    {
        GlobalSystemMediaTransportControlsSessionManager SMTC;

        public MusicFlyout(GlobalSystemMediaTransportControlsSessionManager smtc)
        {
            InitializeComponent();

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

                this.Height = sessions.Count * 60;
            }));
        }
    }
}
