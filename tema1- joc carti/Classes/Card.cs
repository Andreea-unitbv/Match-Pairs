using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace tema1__joc_carti.Classes
{
    [Serializable]
    public class Card
    {
        [XmlElement]
        public string? BackImagePath { get; set; }
        [XmlElement]
        public string? FrontImagePath { get; set; }
        [XmlElement]
        public bool? IsMatched { get; set; }
        [XmlElement]
        public bool? IsFlipped { get; set; }

        public Card(string val1, string val2)
        {
            BackImagePath = val1;
            FrontImagePath = val2;
            IsMatched = false;
            IsFlipped = false;

        }
        public Card() { }
    }

}
