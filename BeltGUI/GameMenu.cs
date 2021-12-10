using BeltGUI.Animations;
using BeltGUI.Enums;
using BeltGUI.Properties;
using BeltLib.Core;
using BeltLib.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeltGUI
{
    public partial class GameMenu : Form
    {
        public PlayerType CurrentPlayer { get; private set; }
        private readonly List<Control> _playerCards;
        private readonly List<Control> _botCards;
        private readonly List<Control> _fieldCards;
        private readonly ControlAnimation _animation;
        private readonly Deck _deck;
        public GameMenu()
        {
            InitializeComponent();
            deck.Hide();
            CurrentPlayer = PlayerType.Player;
            _playerCards = new List<Control>();
            _botCards = new List<Control>();
            _fieldCards = new List<Control>();
            _animation = new ControlAnimation();
            _deck = new Deck();
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

            button.Click += CardButtonClick;
            button.FlatAppearance.BorderSize = 0;
            Invoke((MethodInvoker)(() => Controls.Add(button)));

            return button;
        }

        private void CardButtonClick(object sender, EventArgs e)
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

            _animation.Control = control;
            _animation.Animate(endPoint.X + multiplier * 20, endPoint.Y);
            if (CurrentPlayer.Equals(PlayerType.Player))
            {
                _playerCards.Add(control);
                Task.Delay(1000);
                control.BackgroundImage = card.CardFace;
                control.Enabled = true;
                return;
            }

            _botCards.Add(control);
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

        private void DeckClick(object sender, EventArgs e)
        {
            Card card = _deck.DeckCards.FirstOrDefault();
            if (card is null)
            {
                return;
            }

            Control control = AddButton(card);
            MoveFromDeck(control, card);
        }
    }
}
