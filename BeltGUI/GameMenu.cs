using BeltGUI.Animations;
using BeltGUI.Enums;
using BeltGUI.Properties;
using BeltLib.Core;
using BeltLib.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BeltLib;

namespace BeltGUI
{
    public partial class GameMenu : Form
    {
        public PlayerType CurrentPlayer { get; private set; }
        private readonly List<Control> _playerCards;
        private readonly List<Control> _botCards;
        private readonly List<Control> _fieldCards;
        private readonly ControlAnimation animation;
        private readonly List<Card> _cards;
        private readonly Deck _deck;
        private readonly GameLogic _gameLogic;
        public GameMenu()
        {
            InitializeComponent();
            deck.Hide();
            CurrentPlayer = PlayerType.Player;
            _playerCards = new List<Control>();
            _botCards = new List<Control>();
            _fieldCards = new List<Control>();
            animation = new ControlAnimation();
            _cards = new List<Card>();
            _deck = new Deck();
            _gameLogic = new GameLogic(_deck);
            InitializeDeck();
        }

        private Control AddButton(Card card)
        {
            Button button = new()
            {
                Name = card.ToString(),
                Location = deck.Location,
                Size = deck.Size,
                BackgroundImage = card.CardBack,
                BackgroundImageLayout = ImageLayout.Stretch,
                FlatStyle = FlatStyle.Popup,
                TabStop = false,
                Enabled = false
            };

            button.Click += CardClick;
            button.FlatAppearance.BorderSize = 0;
            Invoke((MethodInvoker)(() => Controls.Add(button)));

            return button;
        }

        private void CardClick(object sender, EventArgs e)
        {
            Control control = sender as Control;
            MoveToField(control);
            CurrentPlayer = CurrentPlayer.Equals(PlayerType.Player) ? PlayerType.Bot : PlayerType.Player;
        }

        private void StartButtonClick(object sender, EventArgs e)
        {
            Button currentButton = sender as Button;
            currentButton?.Hide();
            deck.Show();
        }

        private void MoveToPile(Control control, Card card)
        {
            int index = _fieldCards.IndexOf(control);
            if (index < 0 || index > _fieldCards.Count)
            {
                return;
            }

            animation.Control = control;
            animation.Animate(pile.Location.X, pile.Location.Y);

            _fieldCards.RemoveAt(index);
            control.BackgroundImage = card.CardBack;
        }

        private void MoveFromDeck(Control control, Card card)
        {
            Point endPoint;
            int multiplier;
            if (CurrentPlayer.Equals(PlayerType.Player))
            {
                endPoint = playerCards.Location;
                multiplier = _playerCards.Count;
            }
            else
            {
                endPoint = botCards.Location;
                multiplier = _botCards.Count;
            }

            animation.Control = control;
            animation.Animate(endPoint.X + multiplier * 20, endPoint.Y);

            if (CurrentPlayer.Equals(PlayerType.Player))
            {
                _playerCards.Add(control);
                control.BackgroundImage = card.CardFace;
                control.Enabled = true;
                return;
            }

            _botCards.Add(control);
        }

        private void MoveToField(Control control)
        {
            int index = CurrentPlayer.Equals(PlayerType.Player) ? _playerCards.IndexOf(control) : _botCards.IndexOf(control);
            if (index < 0 || index > _playerCards.Count)
            {
                return;
            }

            if (CurrentPlayer.Equals(PlayerType.Player))
            {
                _playerCards.RemoveAt(index);
            }
            else
            {
                _botCards.RemoveAt(index);
            }

            control.Enabled = false;
            animation.Control = control;
            animation.Animate(fieldCards.Location.X + _fieldCards.Count * 20, fieldCards.Location.Y);

            _fieldCards.Add(control);
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
                    Card card = new(suit, type, cardFace!, cardBack!);
                    _deck.DeckCards.Add(card);
                    _cards.Add(card);
                }
            }

            _deck.Shuffle();
        }

        private void DeckClick(object sender, EventArgs e)
        {
            Card card = _deck.DeckCards.FirstOrDefault();
            if (card is null)
            {
                deck.Visible = false;
                return;
            }

            _deck.DeckCards.Remove(card);
            Control control = AddButton(card);
            MoveFromDeck(control, card);
        }
    }
}
