using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup.Localizer;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml.Serialization;
using tema1__joc_carti.Classes;

namespace tema1__joc_carti
{
    public partial class Menu : Window
    {
        private User user;
        public User User { get => user; set => user = value; }
        private List<Button> pair = new();

        private Board? gameBoard;
        private ObservableCollection<User>? Users { get; set; }
        private int Columns { get; set; }
        private int Rows { get; set; }
        public Menu(User user)
        {
            InitializeComponent();
            this.user = user;
            avatar.Source = new BitmapImage(new Uri(user.AvatarPath));
            gameBoard = new();
            DataContext = gameBoard;
            playerName.Text = "Player name: " + user.Username+"\n"+"Level:" +gameBoard.Level;
            Columns = 5; Rows = 5;

        }

        private void OnCardButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Card card = button.DataContext as Card;

            if (card.IsFlipped == false)
            {
                // Show the front image of the card
                button.Content = new Image
                {
                    Source = new BitmapImage(new Uri(card.FrontImagePath)),
                    Stretch = Stretch.Uniform

                };
                pair.Add(button);
            }
            else
            {
                // Show the back image of the card
                button.Content = new Image
                {
                    Source = new BitmapImage(new Uri(card.BackImagePath)),
                    Stretch = Stretch.Uniform
                };
                pair.Remove(button);
            }

            card.IsFlipped = !card.IsFlipped;

            if (pair.Count == 2)
            {
                Card c1 = pair[0].DataContext as Card;
                Card c2 = pair[1].DataContext as Card;

                bool isMatched = AreMatched(c1, c2);
                if (isMatched)
                {
                    pair[0].IsEnabled = false;
                    pair[1].IsEnabled = false;
                    if (IsLevelWon())
                    {
                        MessageBox.Show("Congratulations! You have won the level!");
                        if (gameBoard.Level == 3)
                        {
                            user.GamesWon++;
                            MessageBox.Show("Congratulations! You have won the game!");
                        }
                        else
                        {
                            gameBoard.Level += 1;
                            gameBoard = new(Rows, Columns, gameBoard.Level);
                            itemsControl.DataContext = gameBoard.Cards;

                        }
                    }
                }
                else
                {
                    pair[1].Content = new Image
                    {
                        Source = new BitmapImage(new Uri(c2.BackImagePath)),
                        Stretch = Stretch.Uniform
                    };
                    pair[0].Content = new Image
                    {
                        Source = new BitmapImage(new Uri(c1.BackImagePath)),
                        Stretch = Stretch.Uniform
                    };
                    c1.IsFlipped = false;
                    c2.IsFlipped = false;

                }

                pair.Clear();
            }
        }




        private bool AreMatched(Card c1, Card c2)
        {
            if (c1.FrontImagePath == c2.FrontImagePath)
            {
                c1.IsMatched = true;
                c2.IsMatched = true;
                return true;
            }
            return false;
        }

        private bool IsLevelWon()

        {
            foreach (List<Card> cards in gameBoard.Cards)
            {
                foreach (Card card in cards)
                {
                    if (card.IsMatched == false)
                    {
                        return false;
                    }
                }

            }
            return true;
        }


        //.....................................................................................

        private void StatisticsButton_Click(Object sender, RoutedEventArgs e)
        {
            Statistics w = new(user);
            w.Show();
        }

        private void NewGameButton_Click(Object sender, RoutedEventArgs e)
        {
            gameBoard = new(Rows, Columns,1);
            itemsControl.DataContext = gameBoard.Cards;
            user.TotalGames++;
        }

        private void CustomButton_Click(object sender, RoutedEventArgs e)
        {
            Custom customWindow = new();
            customWindow.ShowDialog();

            Rows = customWindow.Rows;
            Columns = customWindow.Cols;
        }


        private void StandardButton_Click(Object sender, RoutedEventArgs e)
        {
            Rows = 5; Columns = 5;

        }

        private void SaveGameButton_Click(Object sender, RoutedEventArgs e)
        {
            Users = LoadUsers();
            foreach (User u in Users)
            {
                if (u.Username == user.Username)
                {
                    if (u.Game != null)
                    {
                        u.Game = gameBoard;
                        break;
                    }
                    else
                    {
                        u.Game = gameBoard; break;
                    }
                }
            }
            SaveUsers(Users);
        }

        private void OpenGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (user.Game != null)
            {
                gameBoard = new();
                gameBoard = user.Game;
                itemsControl.DataContext = gameBoard.Cards;

                //for (int i = 0; i < gameBoard.Cards.Count; i++)
                //{
                //    for (int j = 0; j < gameBoard.Cards[i].Count; j++)
                //    {
                //        Card card = gameBoard.Cards[i][j];
                //        Button button = itemsControl.ItemContainerGenerator.ContainerFromIndex(i * gameBoard.Cards.Count + j) as Button;

                //        if (card.IsMatched == true)
                //        {
                //            button.IsEnabled = false;
                //            button.Content = new Image
                //            {
                //                Source = new BitmapImage(new Uri(card.FrontImagePath)),
                //                Stretch = Stretch.Uniform
                //            };
                //        }
                //        else
                //        {
                //            button.Content = new Image
                //            {
                //                Source = new BitmapImage(new Uri(card.BackImagePath)),
                //                Stretch = Stretch.Uniform
                //            };
                //        }
                //    }
                //}
            }
            else
            {
                MessageBox.Show("Nu exista niciun joc salvat!");
            }
        }



        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Musca Andreea, 10LF212, Informatica");
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Users = LoadUsers();
            foreach (User u in Users)
            {
                if (u.Username == user.Username)
                {
                    u.GamesWon = user.GamesWon;
                    u.TotalGames = user.TotalGames;
                    break;
                }
            }
            SaveUsers(Users);
            Close();
        }


        //....................................................................

        private static ObservableCollection<User>? LoadUsers()
        {
            string usersFile = "C:\\Users\\Andreea\\OneDrive\\Desktop\\an II\\sem 2\\MVP\\teme\\tema1- joc carti\\Users.xml";

            if (!File.Exists(usersFile))
            {
                return new ObservableCollection<User>();
            }

            XmlSerializer serializer = new(typeof(ObservableCollection<User>));

            StreamReader streamReader = new(usersFile);
            using StreamReader reader = streamReader;
            return serializer.Deserialize(reader) as ObservableCollection<User>;
        }

        private static void SaveUsers(ObservableCollection<User> users)
        {
            string usersFile = "C:\\Users\\Andreea\\OneDrive\\Desktop\\an II\\sem 2\\MVP\\teme\\tema1- joc carti\\Users.xml";

            XmlSerializer serializer = new(typeof(ObservableCollection<User>));

            using StreamWriter writer = new(usersFile);
            serializer.Serialize(writer, users);
        }

    }


}
