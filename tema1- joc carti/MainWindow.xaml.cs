using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using System.Xml.Serialization;
using tema1__joc_carti.Classes;

namespace tema1__joc_carti
{
    public partial class MainWindow : Window
    {
        public Image? AvatarPath;

        public ObservableCollection<User>? Users { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            // Load the user data from the XML file
            _ = XDocument.Load("C:\\Users\\Andreea\\OneDrive\\Desktop\\an II\\sem 2\\MVP\\teme\\tema1- joc carti\\Users.xml");
            Users = LoadUsers();
            // Set the DataContext of the window to the current instance
            DataContext = this;
        }


        private void NewUserButton_Click(object sender, RoutedEventArgs e)
        {
            NewUser? newUserWindow = new();
            newUserWindow.ShowDialog();


            User user = newUserWindow.CreatedUser;
            Users.Add(user);
            UsersListBox.SelectedItem = user;

        }

        private void UsersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Enable the Play button when a user is selected
            PlayButton.IsEnabled = (UsersListBox.SelectedItem != null);
            DeleteUserButton.IsEnabled = (UsersListBox.SelectedItem != null);

        }


        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersListBox.SelectedItem != null)
            {
                User selectedUser = (User)UsersListBox.SelectedItem;
                Menu m = new(selectedUser);
                m.Show();
                Hide();
            }
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            // Make sure a user is selected in the ListBox
            if (UsersListBox.SelectedItem != null)
            {
                User selectedUser = (User)UsersListBox.SelectedItem;

                Users.Remove(selectedUser);

                SaveUsers(Users);

                UsersListBox.ItemsSource = null;
                UsersListBox.ItemsSource = Users;
            }

        }


        //....................................

        private static void SaveUsers(ObservableCollection<User> users)
        {
            string usersFile = "C:\\Users\\Andreea\\OneDrive\\Desktop\\an II\\sem 2\\MVP\\teme\\tema1- joc carti\\Users.xml";

            XmlSerializer serializer = new(typeof(ObservableCollection<User>));

            using StreamWriter writer = new(usersFile);
            serializer.Serialize(writer, users);
        }

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
    }
}
