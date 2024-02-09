using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
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

namespace ADONET_hw_2_artists
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        IDbConnection connection = new SqlConnection(
                @"Data Source=(localdb)\.;Initial Catalog=music2;Integrated Security=True"
            );

        ICollection<Artist> artists; 
        public MainWindow()
        {
            InitializeComponent();

            connection.Open();

            artists = new ObservableCollection<Artist>();

            IDbCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM artists";

            IDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);

                artists.Add(new Artist(id, name));
            }

            DataContext = artists;
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            Artist artist = new Artist();
            AddArtist addArtist = new AddArtist(artist);

            if (addArtist.ShowDialog() == true)
            {
                using IDbCommand command = connection.CreateCommand();

                command.CommandText = $"INSERT INTO artists (Name)" + $"VALUES( @Name)";

                IDbDataParameter param = command.CreateParameter();
                param.ParameterName = "@Name";
                param.Value = artist.Name;
                command.Parameters.Add(param);

                int ex = command.ExecuteNonQuery();

                artists.Add(artist);

                MessageBox.Show($"{ex} исполнитель добавлен");

               
            }
            
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex != -1)
            {
                Artist[] artistArr = artists.ToArray();

                Artist artist = new Artist();

                for (int i = 0; i < artists.Count; i++)
                {
                    if (artistArr[i] == dataGrid.SelectedItem)
                    {
                        artist = artistArr[i];
                        break;

                    }
                }
                AddArtist changeArtist = new AddArtist(artist);

                if (changeArtist.ShowDialog() == true)
                {
                    using IDbCommand command = connection.CreateCommand();
                    command.CommandText = "UPDATE artists " + "SET Name = @Name" + $" WHERE Id = {artist.Id}";

                    IDbDataParameter param = command.CreateParameter();
                    param.ParameterName = "@Name";
                    param.Value = artist.Name;
                    command.Parameters.Add(param);

                    int rowsAdded = command.ExecuteNonQuery();


                    MessageBox.Show(rowsAdded + " исполнитель обновлен");
                }
            }
            
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex != -1)
            {

                Artist[] artistArr = artists.ToArray();

                Artist artist = null;

                for (int i = 0; i < artists.Count; i++)
                {
                    if (artistArr[i] == dataGrid.SelectedItem)
                    {
                        artist = artistArr[i];
                        break;

                    }
                }


                using IDbCommand command = connection.CreateCommand();
                command.CommandText = $"DELETE FROM artists WHERE Id = {artist.Id}";

                int rowsAdded = command.ExecuteNonQuery();

                dataGrid.SelectedItems.Remove(dataGrid.SelectedItem);
                artists.Remove(artist);

                MessageBox.Show(rowsAdded + " исполнитель удален");
            }
            
        }
    }
}
