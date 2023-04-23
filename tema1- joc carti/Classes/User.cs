using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace tema1__joc_carti.Classes
{
    [Serializable]
    public class User
    {
        public string? Username { get; set; }

        public string? AvatarPath { get; set; }
        public int? TotalGames { get; set; }
        public int? GamesWon { get; set; }
        public Board? Game { get; set; }
       
        public User(string username, string avatarImagePath)
        {
            Username = username;
            AvatarPath = avatarImagePath;
            TotalGames = 0;
            GamesWon = 0;
            Game = null;
        }
        public User() { }
    }

}
