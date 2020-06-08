using MrCalico_s_Quick_Solitaire.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static MrCalico_s_Quick_Solitaire.Models.DeckOfCards;

namespace MrCalico_s_Quick_Solitaire.Pages
{
    public class QuickSolitaireBase : ComponentBase
    {
        public Deck MainDeck { get; set; }
        public Card LastCard;
        public bool CardFlag;
        public List<Card> DrewPile;

        public List<Card> Aces_Hearts;
        public List<Card> Aces_Spades;
        public List<Card> Aces_Diamonds;
        public List<Card> Aces_Clubs;
        public List<Card> Kings_Hearts;
        public List<Card> Kings_Spades;
        public List<Card> Kings_Diamonds;
        public List<Card> Kings_Clubs;
        public List<List<Card>> KingStack;
        public List<List<Card>> AceStack;
        public List<List<List<Card>>> PlayStacks;

        protected override Task OnInitializedAsync()
        {
            InitializeGame();
            return base.OnInitializedAsync();
        }

        public void InitializeGame()
        {
            MainDeck = new Deck();
            DrewPile = new List<Card>();
            Aces_Hearts = new List<Card>();
            Aces_Spades = new List<Card>();
            Aces_Diamonds = new List<Card>();
            Aces_Clubs = new List<Card>();
            Kings_Hearts = new List<Card>();
            Kings_Spades = new List<Card>();
            Kings_Diamonds = new List<Card>();
            Kings_Clubs = new List<Card>();
            KingStack = new List<List<Card>>();
            AceStack = new List<List<Card>>();
            KingStack.Add(Kings_Clubs);
            KingStack.Add(Kings_Diamonds);
            KingStack.Add(Kings_Hearts);
            KingStack.Add(Kings_Spades);
            AceStack.Add(Aces_Clubs);
            AceStack.Add(Aces_Diamonds);
            AceStack.Add(Aces_Hearts);
            AceStack.Add(Aces_Spades);
            //PlayStacks.Add(AceStack);
            //PlayStacks.Add(KingStack);

            Shuffle();
            Debug.WriteLine($"OnInit: Cards Initialized and {MainDeck.cards.Count} Shuffled");
        }
        public void DrawCards()
        {
            Debug.WriteLine("Draw: about to count deck");
            try
            {
                if (MainDeck.cards.Count <= 0) /* Flip Deck */
                {
                    Debug.WriteLine("No Cards in Deck");
                    MainDeck.cards = DrewPile;
                    MainDeck.cards.Reverse();
                    DrewPile = new List<Card>();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("{0} Exception caught in Count-Deck/Flip", e);
            }

            Debug.WriteLine("Draw: about to draw 3 loop");
            try
            {
                for (int x = 0; x < 3; x++) // Draw Three
                {
                    CardFlag = false;
                    if (MainDeck.cards.Count > 0)
                    {
                        LastCard = MainDeck.Deal();
                        DrewPile.Add(LastCard);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("{0} Exception caught in LOOP", e);
            }
            // LastCard should be facing card ** See if it goes up to stacks.
            Debug.WriteLine("Draw: about to switch");
            int CurrentCard = DrewPile.Count - 1;
            try
            {
                switch (LastCard.Rank)
                {
                    case "Ace":
                        CardFlag = true;
                        switch (LastCard.Suit)
                        {
                            case "Clubs":
                                Aces_Clubs.Add(LastCard);
                                DrewPile.RemoveAt(CurrentCard);
                                break;
                            case "Diamonds":
                                Aces_Diamonds.Add(LastCard);
                                DrewPile.RemoveAt(CurrentCard);
                                break;
                            case "Hearts":
                                Aces_Hearts.Add(LastCard);
                                DrewPile.RemoveAt(CurrentCard);
                                break;
                            case "Spades":
                                Aces_Spades.Add(LastCard);
                                DrewPile.RemoveAt(CurrentCard);
                                break;
                        }
                        break;
                    case "King":
                        CardFlag = true;
                        switch (LastCard.Suit)
                        {
                            case "Clubs":
                                Kings_Clubs.Add(LastCard);
                                DrewPile.RemoveAt(CurrentCard);
                                break;
                            case "Diamonds":
                                Kings_Diamonds.Add(LastCard);
                                DrewPile.RemoveAt(CurrentCard);
                                break;
                            case "Hearts":
                                Kings_Hearts.Add(LastCard);
                                DrewPile.RemoveAt(CurrentCard);
                                break;
                            case "Spades":
                                Kings_Spades.Add(LastCard);
                                DrewPile.RemoveAt(CurrentCard);
                                break;
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("{0} Exception Caught in SwitchBlock", e);
            }
        }
        void Shuffle()
        {
            if (MainDeck != null)
                MainDeck.Shuffle();
        }
    }
}

