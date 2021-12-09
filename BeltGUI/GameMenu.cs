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
        private readonly List<Control> _playerCards;
        private readonly List<Control> _fieldCards;
        private readonly ControlAnimation _animation;
        private readonly Deck _deck;
        public GameMenu()
        {
            InitializeComponent();
            _playerCards = new List<Control>();
            _fieldCards = new List<Control>();
            _animation = new ControlAnimation();
            _deck = new Deck();
            InitializeDeck();
        }

        private void AddButton(Card card)
        {
            Button button = new()
            {
                Location = new Point(140 + _playerCards.Count * 20, 500),
                Size = new Size(105, 155),
                BackgroundImage = card.CardFace,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            button.Click += CardButtonClick;
            button.TabStop = false;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            _playerCards.Add(button);
            Invoke((MethodInvoker)(() => Controls.Add(button)));
        }

        private void CardButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            MoveToField(button);
        }

        private void StartButtonClick(object sender, EventArgs e)
        {
            Button currentButton = sender as Button;
            currentButton?.Hide();
            AddButton(_deck.DeckCards[5]);
            AddButton(_deck.DeckCards[0]);
            AddButton(_deck.DeckCards[5]);
        }

        private void MoveToPile(Control control)
        {
            int index = _fieldCards.IndexOf(control);
            if (index < 0 || index > _fieldCards.Count)
            {
                return;
            }

            _animation.Control = control;
            _animation.Animate(deck.Location.X, deck.Location.Y);
            _fieldCards.RemoveAt(index);
        }

        private void MoveFromDeck()
        {
            _animation.Control = deck;
            _animation.Animate(deck.Location.X, deck.Location.Y);
        }

        private void MoveToField(Control control)
        {
            int index = _playerCards.IndexOf(control);
            if (index < 0 || index > _playerCards.Count)
            {
                return;
            }

            _animation.Control = control;
            _animation.Animate(playedCards.Location.X + _fieldCards.Count * 20, playedCards.Location.Y);
            control.Enabled = false;
            _playerCards.RemoveAt(index);
            _fieldCards.Add(control);
        }

        private void ShowCards()
        {
            for (int i = 0; i < _playerCards.Count; i++)
            {
                Invoke((MethodInvoker)(() => _playerCards[i].Location = new Point(140, 500)));
                Invoke((MethodInvoker)(() => _playerCards[i].Left = i * 20 + 140));
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
