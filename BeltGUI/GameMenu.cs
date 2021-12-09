using BeltGUI.Properties;
using BeltLib.Core;
using BeltLib.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BeltGUI.Animations;

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

        private void CardButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            RemoveButton(button);
            ShowCards(button);
        }

        private void StartButtonClick(object sender, EventArgs e)
        {
            Button currentButton = sender as Button;
            currentButton?.Hide();
            AddButton(_deck.DeckCards[5]);
            AddButton(_deck.DeckCards[5]);
            AddButton(_deck.DeckCards[5]);

        }

        private void RemoveButton(Button button)
        {
            int index = _playerCards.IndexOf(button);
            if (index < 0 || index > _playerCards.Count)
            {
                return;
            }

            ComponentAnimation animation = new (button);
            animation.Animate(button.Location.X, button.Location.Y);
            //_playerCards.RemoveAt(index);
            //int controlIndex = Controls.IndexOf(button!);
            //Invoke((MethodInvoker)(() => Controls.RemoveAt(controlIndex)));
        }

        private void ShowCards(Button playedCard = null)
        {
            for (int i = 0; i < _playerCards.Count; i++)
            {
                Invoke((MethodInvoker)(() => _playerCards[i].Location = new Point(150, 492)));
                Invoke((MethodInvoker)(() => _playerCards[i].Left = i * 30 + 150));
            }

            if (playedCard is null)
            {
                return;
            }

            Invoke((MethodInvoker)(() => playedCard.Location = playedCards.Location));
            Invoke((MethodInvoker)(() => playedCard.Left = playedCards.Left + 10));
            playedCard.Enabled = false;

            Controls.Add(playedCard);
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
