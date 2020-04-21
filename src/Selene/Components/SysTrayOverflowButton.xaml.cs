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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WinApi.User32;

namespace Selene.Components
{
    /// <summary>
    /// Interaction logic for SysTrayOverflowButton.xaml
    /// </summary>
    public partial class SysTrayOverflowButton : UserControl
    {
        public SysTrayOverflowButton()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IntPtr statusOverflow = User32Methods.FindWindow("NotifyIconOverflowWindow", null);

            if (User32Methods.IsWindowVisible(statusOverflow))
            {
                User32Methods.ShowWindow(statusOverflow, ShowWindowCommands.SW_HIDE);
            }
            else
            {
                int buttonWidth = (int)MainButton.Width;
                int buttonHeight = (int)MainButton.Height;
                Point buttonPos = MainButton.PointToScreen(new Point(0d, 0d));
                User32Methods.ShowWindow(statusOverflow, ShowWindowCommands.SW_SHOWNORMAL);

                NetCoreEx.Geometry.Rectangle windowRect;
                User32Methods.GetWindowRect(statusOverflow, out windowRect);

                int X = (int)buttonPos.X + 15 - windowRect.Width / 2;
                int Y = (int)buttonPos.Y + buttonHeight;

                User32Methods.MoveWindow(statusOverflow, X, Y, windowRect.Width, windowRect.Height, true);
            }
        }
    }
}
