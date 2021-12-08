using BeltGUI.Properties;
using BeltLib.Core;
using BeltLib.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BeltGUI
{
    public partial class GameMenu : Form
    {
        private readonly List<Button> _playerCards;
        private readonly Deck _deck;
        public GameMenu()
        {
            InitializeComponent();
            _playerCards = new List<Button>();
            _deck = new Deck();
            InitializeDeck();
        }

        private void AddButton(Card card)
        {
            Button button = new()
            {
                Location = new Point(150 + _playerCards.Count * 18, 492),
                Size = new Size(105, 155),
                BackgroundImage = card.CardFace,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            button.Click += CardButtonClick;
            _playerCards.Add(button);
            Invoke((MethodInvoker)(() => Controls.Add(button)));
        }

        private static void CardButtonClick(object sender, EventArgs e)
        {
            MessageBox.Show("Hello world");
        }

        private void StartButtonClick(object sender, EventArgs e)
        {
            Button currentButton = sender as Button;
            currentButton?.Hide();
            AddButton(_deck.DeckCards[5]);
            ShowCards();
        }

        private void RemoveButton(int index)
        {
            if (index < 0 || index >= _playerCards.Count)
            {
                return;
            }

            _playerCards.RemoveAt(index);
            Invoke((MethodInvoker)(() => Controls.RemoveAt(index)));
        }

        private void ShowCards()
        {
            for (int i = 0; i < _playerCards.Count; i++)
            {
                Invoke((MethodInvoker)(() => _playerCards[i].Location = new Point(150, 492)));
                Invoke((MethodInvoker)(() => _playerCards[i].Left = i * 18 + 150));
            }
        }

        private void InitializeDeck()
        {
            Bitmap cardBack = (Bitmap)Resources.ResourceManager.GetObject("back");
            foreach (CardType type in Enum.GetValues(typeof(CardType)))
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    Bitmap cardFace =
                        (Bitmap)Resources.ResourceManager.GetObject($"{type.ToString().ToLower()}_{suit.ToString().ToLower()}");
                    _deck.DeckCards.Add(new Card(suit, type, cardFace!, cardBack!));
                }
            }

            _deck.Shuffle();
        }
    }
}
