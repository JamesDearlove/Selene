using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Selene.Controls
{
    public class FlyoutWindow : Window
    {
        public static readonly RoutedEvent FlyoutVisibleEvent = EventManager.RegisterRoutedEvent(
            "FlyoutVisible", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FlyoutWindow));

        public event RoutedEventHandler FlyoutVisible
        {
            add { AddHandler(FlyoutVisibleEvent, value); }
            remove { RemoveHandler(FlyoutVisibleEvent, value); }
        }

        static FlyoutWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlyoutWindow), 
                new FrameworkPropertyMetadata(typeof(FlyoutWindow)));
        }

        public FlyoutWindow() : base() 
        {
            this.IsVisibleChanged += FlyoutWindow_IsVisibleChanged;   
        }

        private void FlyoutWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible)
            {
                // Visible?

                RoutedEventArgs newEventArgs = new RoutedEventArgs(FlyoutWindow.FlyoutVisibleEvent);
                RaiseEvent(newEventArgs);
            }
            else
            {
                // Not visible?
            }
        }

    }
}
