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

namespace ADONET_hw_2_artists
{
    /// <summary>
    /// Interaction logic for AddArtist.xaml
    /// </summary>
    public partial class AddArtist : Window
    {
        public AddArtist(Artist artist)
        {
            InitializeComponent();

            DataContext = artist;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
