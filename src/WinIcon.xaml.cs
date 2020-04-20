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

namespace Selene
{
    /// <summary>
    /// Interaction logic for Icon.xaml
    /// </summary>
    public partial class WinIcon : UserControl
    {
        public int Size { get; set; }
        public string Glyph { get; set; }

        public WinIcon()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
