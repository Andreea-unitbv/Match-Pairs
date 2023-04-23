using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace tema1__joc_carti.Classes
{
    [Serializable]
    public class Board
    {
        public List<List<Card>>? Cards { get; set; }
        public int Level { get; set; }


        public Board(int rows, int columns, int level)
        {
            var backImagePath = "C:\\Users\\Andreea\\OneDrive\\Desktop\\an II\\sem 2\\MVP\\teme\\tema1- joc carti\\0.jpg";
            var folderPath = "C:\\Users\\Andreea\\OneDrive\\Desktop\\an II\\sem 2\\MVP\\teme\\tema1- joc carti\\Cards";
            var numPairs = rows * columns / 2;
            string[] images = Directory.GetFiles(folderPath, "*.jpg");

            // Create a list of card values (pairs of images)
            var cardValues = new List<string>();

            int i = 0;
            foreach (var image in images)
            {
                if (i < numPairs)
                {
                    cardValues.Add(image);
                    cardValues.Add(image);
                }
                else break;
                i++;
            }

            if (rows * columns % 2 == 1)
                cardValues.Add(backImagePath);

            // Shuffle the card values
            var rng = new Random();
            var shuffledValues = cardValues.OrderBy(x => rng.Next()).ToList();

            // Create the board and assign card values
            Cards = CreateCards(rows, columns, backImagePath, shuffledValues);


            Level = level;

        }

        private List<List<Card>> CreateCards(int rows, int columns, string backImagePath, List<string> shuffledValues)
        {
            var cards = new List<List<Card>>();
            var index = 0;
            Card card;
            for (int i = 0; i < rows; i++)
            {
                var row = new List<Card>();
                for (int j = 0; j < columns; j++)
                {
                    card = new Card(backImagePath, shuffledValues[index]);
                    if (shuffledValues[index] == backImagePath)
                        card.IsMatched = true;

                    index++;
                    row.Add(card);
                }
                cards.Add(row);
            }

            return cards;
        }


        public Board()
        {
            Level = 0;
        }


    }
}
