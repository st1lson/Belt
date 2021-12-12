using BeltGUI.Animations;
using BeltGUI.Enums;
using BeltGUI.Properties;
using BeltLib;
using BeltLib.Core;
using BeltLib.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BeltGUI
{
    public partial class GameMenu : Form
    {
        public PlayerType CurrentPlayer { get; private set; }
        public List<Card> Cards { get; }
        private readonly List<Control> _playerCards;
        private readonly List<Control> _botCards;
        private readonly List<Control> _fieldCards;
        private readonly List<Control> _playerStore;
        private readonly List<Control> _botStore;
        private readonly GameLogic _gameLogic;
        private readonly Deck _deck;
        public GameMenu()
        {
            InitializeComponent();
            deck.Hide();
            CurrentPlayer = PlayerType.Player;
            Cards = new List<Card>();
            _playerCards = new List<Control>();
            _botCards = new List<Control>();
            _fieldCards = new List<Control>();
            _playerStore = new List<Control>();
            _botStore = new List<Control>();
            _deck = new Deck();
            _gameLogic = new GameLogic(_deck);
            InitializeDeck();
        }

        private void InitializeGame()
        {
            CurrentPlayer = PlayerType.Bot;
            for (int i = 0; i < 4; i++)
            {
                Card card = _deck.DeckCards.FirstOrDefault();
                _deck.DeckCards.Remove(card);
                Control control = AddButton(card);
                MoveFromDeck(control, card);
            }

            CurrentPlayer = PlayerType.Player;
            for (int i = 0; i < 4; i++)
            {
                Card card = _deck.DeckCards.FirstOrDefault();
                _deck.DeckCards.Remove(card);
                Control control = AddButton(card);
                MoveFromDeck(control, card);
            }
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
            Controls.SetChildIndex(control!, _fieldCards.Count);
            MoveToField(control);
            CurrentPlayer = CurrentPlayer.Equals(PlayerType.Player) ? PlayerType.Bot : PlayerType.Player;
            BotMove();
        }

        private void BotMove()
        {
            Card card = _gameLogic.SelectCard(ConvertToCard(_botCards), ConvertToCard(_fieldCards));
            Control botControl = _botCards.Find(x => x.Name.Equals(card.ToString()));
            if (botControl is null)
            {
                return;
            }

            botControl.BackgroundImage = card.CardFace;
            Controls.SetChildIndex(botControl, _fieldCards.Count);
            MoveToField(botControl);
            CurrentPlayer = CurrentPlayer.Equals(PlayerType.Player) ? PlayerType.Bot : PlayerType.Player;
        }

        private void StartButtonClick(object sender, EventArgs e)
        {
            Button currentButton = sender as Button;
            currentButton?.Hide();
            InitializeGame();
            deck.Show();
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

            ControlAnimation animation = new()
            {
                Control = control
            };
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
            ControlAnimation animation = new()
            {
                Control = control
            };
            animation.Animate(fieldCards.Location.X + _fieldCards.Count * 20, fieldCards.Location.Y);

            _fieldCards.Add(control);
        }

        private void MoveToStore(List<Control> controls)
        {
            foreach (Control control in controls)
            {
                MoveToStore(control);
            }
        }

        private void MoveToStore(Control control)
        {
            int index = _fieldCards.IndexOf(control);
            if (index < 0 || index > _fieldCards.Count)
            {
                return;
            }

            Point destinationPoint;
            if (CurrentPlayer.Equals(PlayerType.Player))
            {
                _playerCards.RemoveAt(index);
                _playerStore.Add(control);
                destinationPoint = playerStore.Location;
            }
            else
            {
                _botCards.RemoveAt(index);
                _botStore.Add(control);
                destinationPoint = botStore.Location;
            }

            _fieldCards.RemoveAt(index);
            control.BackgroundImage = Resources.back;
            ControlAnimation animation = new()
            {
                Control = control
            };

            animation.Animate(destinationPoint.X, destinationPoint.Y);
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
                    Cards.Add(card);
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

        private List<Card> ConvertToCard(IEnumerable<Control> controls)
        {
            return controls.Select(control => Cards.Find(x => x.ToString().Equals(control?.Name))).ToList();
        }

        private Winner SelectWinner()
        {
            int playerScore = 0;
            List<CardType> cardTypes = new();
            foreach (Control control in _playerStore)
            {
                Card card = Cards.Find(x => x.ToString().Equals(control.Name));
                if (cardTypes.Contains(card!.Type))
                {
                    continue;
                }

                cardTypes.Add(card!.Type);
                playerScore++;
            }

            int botScore = 0;
            cardTypes.Clear();
            foreach (Control control in _botStore)
            {
                Card card = Cards.Find(x => x.ToString().Equals(control.Name));
                if (cardTypes.Contains(card!.Type))
                {
                    continue;
                }

                cardTypes.Add(card!.Type);
                botScore++;
            }

            if (playerScore == botScore)
            {
                return Winner.Draw;
            }

            return playerScore > botScore ? Winner.Player : Winner.Bot;
        }
    }
}
