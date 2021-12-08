using BeltGUI.Properties;
using BeltLib.Core;
using BeltLib.Enums;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BeltGUI
{
    public partial class GameMenu : Form
    {
        private readonly Bitmap[] _sprites;
        private readonly Deck _deck;
        public GameMenu()
        {
            InitializeComponent();
            _deck = new Deck();
            _sprites = InitializeSprites();
        }

        private Bitmap[] InitializeSprites()
        {
            Bitmap[] sprites = new Bitmap[53];
            int i = 0;
            Bitmap cardBack = (Bitmap)Resources.ResourceManager.GetObject("back");
            sprites[i++] = cardBack;
            foreach (CardType type in Enum.GetValues(typeof(CardType)))
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    Bitmap cardFace =
                        (Bitmap)Resources.ResourceManager.GetObject($"{type.ToString().ToLower()}_{suit.ToString().ToLower()}");
                    _deck.DeckCards.Add(new Card(suit, type, cardFace, cardBack));
                    sprites[i++] = cardFace;
                }
            }

            return sprites;
        }
    }
}
