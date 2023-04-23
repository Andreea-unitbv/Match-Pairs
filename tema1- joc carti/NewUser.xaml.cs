using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using tema1__joc_carti.Classes;

namespace tema1__joc_carti
{
    public partial class NewUser : Window
    {
        private readonly List<string> avatarImages;
        private int currentAvatarIndex;
        public ObservableCollection<User>? Users { set; get; }
        public User? CreatedUser { get; set; }

        public NewUser()
        {
            InitializeComponent();

            avatarImages = new List<string>();
            currentAvatarIndex = 0;

            LoadAvatarImages();
            imgAvatar.Source = new BitmapImage(new Uri(avatarImages[currentAvatarIndex]));

            Users = LoadUsers();
        }

        private void LoadAvatarImages()
        {
           
            string imagesDir = "C:\\Users\\Andreea\\OneDrive\\Desktop\\an II\\sem 2\\MVP\\teme\\tema1- joc carti\\Avatars\\";

            if (!Directory.Exists(imagesDir))
            {
                MessageBox.Show("The avatars directory does not exist.");
                return;
            }

            string[] imageFiles = Directory.GetFiles(imagesDir, "*.jpg");
            avatarImages.AddRange(imageFiles);

            if (avatarImages.Count == 0)
            {
                MessageBox.Show("There are no avatar images in the avatars directory.");
            }
        }

       

        private void BtnNextAvatar_Click(object sender, RoutedEventArgs e)
        {
            currentAvatarIndex++;

            if (currentAvatarIndex >= avatarImages.Count)
            {
                currentAvatarIndex = 0;
            }

            imgAvatar.Source = new BitmapImage(new Uri(avatarImages[currentAvatarIndex]));
        }

        private void BtnPrevAvatar_Click(object sender, RoutedEventArgs e)
        {
            currentAvatarIndex--;

            if (currentAvatarIndex < 0)
            {
                currentAvatarIndex = avatarImages.Count - 1;
            }

            imgAvatar.Source = new BitmapImage(new Uri(avatarImages[currentAvatarIndex]));
        }

        private void BtnCreateUser_Click(object sender, RoutedEventArgs e)
        {
            string selectedAvatarPath = avatarImages[currentAvatarIndex];
            bool ok=false;

            string newUsername = txtUsername.Text.Trim();

            if (string.IsNullOrEmpty(newUsername))
            {
                MessageBox.Show("Please enter a valid username.");
                return;
            }

            foreach(User user in Users)
            {
                if(user.Username == newUsername)
                {
                    MessageBox.Show("Acest user exista deja");
                    ok = true;
                    break;
                }

                
            }
            if (ok==false)
            {
                CreatedUser = new(newUsername, selectedAvatarPath);
                Users.Add(CreatedUser);
                SaveUsers(Users);

                // Show a success message and navigate back to the main menu
                MessageBox.Show("Userul a fost creat cu succes!");
                Close();
            }

        }


        //..............................................

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

            using (StreamWriter writer = new(usersFile))
            {
                serializer.Serialize(writer, users);
            }
        }
    }
}