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
using tema1__joc_carti.Classes;

namespace tema1__joc_carti
{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : Window
    {
        private User? user; 
        public Statistics(User user)
        {
            InitializeComponent();
            this.user = user;
            username.Text = user.Username;
            avatar.Source = new BitmapImage(new Uri(user.AvatarPath));
            games.Text="Total games: "+user.TotalGames.ToString();
            wins.Text="Wins: "+user.GamesWon.ToString();

        }
        public User? User { get => user; set => user = value; }


    }
}
