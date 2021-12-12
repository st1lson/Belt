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
        private readonly List<Control> _playerStash;
        private readonly List<Control> _botStash;
        private readonly GameLogic _gameLogic;
        private readonly Deck _deck;
        public GameMenu()
        {
            InitializeComponent();
            deckPlace.Hide();
            CurrentPlayer = PlayerType.Player;
            Cards = new List<Card>();
            _playerCards = new List<Control>();
            _botCards = new List<Control>();
            _fieldCards = new List<Control>();
            _playerStash = new List<Control>();
            _botStash = new List<Control>();
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
                Location = deckPlace.Location,
                Size = deckPlace.Size,
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
            if (_deck.DeckCards.Count == 0)
            {
                deckPlace.Visible = false;
                deckPlace.Enabled = false;
            }

            Control control = sender as Control;
            Card card = Cards.Find(x => x.ToString().Equals(control?.Name));
            MoveToField(control);
            List<Control> controls = TryGetCards(_playerCards, control, card);
            if (controls is not null)
            {
                MoveToStash(controls);
            }

            while (_playerCards.Count < 4 && _deck.DeckCards.Count != 0)
            {
                Card newCard = _deck.DeckCards.FirstOrDefault();
                _deck.DeckCards.Remove(newCard);
                Control newControl = AddButton(newCard);
                MoveFromDeck(newControl, newCard);
            }

            CurrentPlayer = PlayerType.Bot;
            BotMove();
            if (_botCards.Count != 0 || _playerCards.Count != 0)
            {
                return;
            }

            Winner winner = SelectWinner();
            switch (winner)
            {
                case Winner.Draw:
                    MessageBox.Show(Resources.Draw_Message);
                    return;
                case Winner.Bot:
                    MessageBox.Show(Resources.Bot_WinMessage);
                    return;
                case Winner.Player:
                    MessageBox.Show(Resources.Player_WinMessage);
                    return;
                default:
                    throw new Exception();
            }
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
            MoveToField(botControl);
            List<Control> controls = TryGetCards(_botCards, botControl, card);
            if (controls is not null)
            {
                MoveToStash(controls);
            }

            while (_botCards.Count < 4 && _deck.DeckCards.Count != 0)
            {
                Card newCard = _deck.DeckCards.FirstOrDefault();
                _deck.DeckCards.Remove(newCard);
                Control newControl = AddButton(newCard);
                MoveFromDeck(newControl, newCard);
            }

            CurrentPlayer = PlayerType.Player;
        }

        private List<Control> TryGetCards(List<Control> controls, Control parentControl, Card card)
        {
            CardType cardType = card.Type;
            int counter = controls.Select(control => Cards.Find(x => x.ToString().Equals(control?.Name))!.Type).Count(fieldCardType => fieldCardType == cardType);

            if (counter < 2)
            {
                return default;
            }

            Dictionary<CardType, short> dictionary = new();
            foreach (Control fieldCard in _fieldCards)
            {
                CardType fieldCardType = Cards.Find(x => x.ToString().Equals(fieldCard?.Name))!.Type;
                if (fieldCardType >= cardType)
                {
                    continue;
                }

                if (dictionary.ContainsKey(fieldCardType))
                {
                    dictionary[fieldCardType]++;
                }
                else
                {
                    dictionary.Add(fieldCardType, 1);
                }
            }

            List<Control> fieldControls = new();
            foreach (var (key, value) in dictionary)
            {
                if (value < 3)
                {
                    continue;
                }

                foreach (Control fieldCard in _fieldCards)
                {
                    CardType fieldCardType = Cards.Find(x => x.ToString().Equals(fieldCard?.Name))!.Type;
                    if (fieldCardType == key && !fieldControls.Contains(fieldCard))
                    {
                        fieldControls.Add(fieldCard);
                    }
                }
            }

            List<Control> toMove = controls.Where(control =>
                cardType == Cards.Find(x => x.ToString().Equals(control?.Name))!.Type).ToList();
            foreach (Control control in toMove)
            {
                MoveToField(control);
                control.Visible = false;
            }

            parentControl.Visible = false;

            return fieldControls;
        }

        private void StartButtonClick(object sender, EventArgs e)
        {
            Button currentButton = sender as Button;
            currentButton?.Hide();
            InitializeGame();
            deckPlace.Show();
        }

        private void MoveFromDeck(Control control, Card card)
        {
            Point endPoint;
            int multiplier;
            if (CurrentPlayer.Equals(PlayerType.Player))
            {
                endPoint = playerCardsPlace.Location;
                multiplier = _playerCards.Count;
            }
            else
            {
                endPoint = botCardsPlace.Location;
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

            List<Control> controls;
            if (CurrentPlayer.Equals(PlayerType.Player))
            {
                _playerCards.RemoveAt(index);
                controls = _playerCards;
            }
            else
            {
                _botCards.RemoveAt(index);
                controls = _botCards;
            }

            control.Enabled = false;
            ControlAnimation animation = new()
            {
                Control = control
            };
            animation.Animate(fieldCardsPlace.Location.X + _fieldCards.Count * 20, fieldCardsPlace.Location.Y);
            RefreshControls(controls, index);

            _fieldCards.Add(control);
        }

        private void RefreshControls(IReadOnlyList<Control> controls, int index)
        {
            for (int i = 0; i < controls.Count; i++)
            {
                if (i < index)
                {
                    continue;
                }

                Controls.SetChildIndex(controls[i], Math.Abs(i - controls.Count));
                ControlAnimation animation = new()
                {
                    Control = controls[i]
                };
                animation.Animate(controls[i].Location.X - 20, controls[i].Location.Y);
            }
        }

        private void RefreshControls(IReadOnlyList<Control> controls)
        {
            for (int i = 0; i < controls.Count; i++)
            {
                Controls.SetChildIndex(controls[i], Math.Abs(i - controls.Count));
                ControlAnimation animation = new()
                {
                    Control = controls[i]
                };
                animation.Animate(controls[i].Location.X + 20, controls[i].Location.Y);
            }
        }

        private void MoveToStash(List<Control> controls)
        {
            foreach (Control control in controls)
            {
                MoveToStash(control);
            }

            RefreshControls(_fieldCards);
        }

        private void MoveToStash(Control control)
        {
            Point endPoint;
            int index = _fieldCards.IndexOf(control);

            if (CurrentPlayer.Equals(PlayerType.Player))
            {
                _playerStash.Add(control);
                endPoint = playerStashPlace.Location;
                playerStashPlace.Visible = true;
            }
            else
            {
                _botStash.Add(control);
                endPoint = botStashPlace.Location;
                botStashPlace.Visible = true;
            }

            control.Visible = false;
            _fieldCards.RemoveAt(index);
            ControlAnimation animation = new()
            {
                Control = control
            };
            animation.Animate(endPoint.X, endPoint.Y);
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
                deckPlace.Visible = false;
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
            foreach (Control control in _playerStash)
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
            foreach (Control control in _botStash)
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
