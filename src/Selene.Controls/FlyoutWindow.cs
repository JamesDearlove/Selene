using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Selene.Controls
{
    public class FlyoutWindow : Window
    {
        private bool hiding = false;

        static FlyoutWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlyoutWindow), 
                new FrameworkPropertyMetadata(typeof(FlyoutWindow)));
        }

        public FlyoutWindow()
        {
            this.IsVisibleChanged += FlyoutWindow_IsVisibleChanged;
            this.Deactivated += FlyoutWindow_Deactivated;
        }

        private void FlyoutWindow_Deactivated(object sender, EventArgs e)
        {
            Hide();
        }

        private void FlyoutWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible)
            {
                var _fadeInAnimation = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = new Duration(TimeSpan.Parse("0:0:0.2"))
                };

                BeginAnimation(FlyoutWindow.OpacityProperty, _fadeInAnimation);
            }
        }

        /// <summary>
        /// Shows the flyout at the given location.
        /// </summary>
        /// <param name="top">Distance from top of screen.</param>
        /// <param name="left">Distance from left of screen.</param>
        public void Show(double top, double left)
        {
            this.Top = top;
            this.Left = left;
            base.Show();
        }

        /// <summary>
        /// Hides the flyout.
        /// </summary>
        public new void Hide()
        {
            if (!hiding)
            {
                hiding = true;
                var _fadeOutAnimation = new DoubleAnimation
                {
                    From = 1,
                    To = 0,
                    Duration = new Duration(TimeSpan.Parse("0:0:0.2"))
                };

                _fadeOutAnimation.Completed += HideComplete;

                BeginAnimation(FlyoutWindow.OpacityProperty, _fadeOutAnimation);
            }
        }

        private void HideComplete(object sender, EventArgs e)
        {
            base.Hide();
            hiding = false;
        }
    }
}
