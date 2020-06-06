using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static MrCalico_s_Quick_Solitaire.Models.DeckOfCards;

namespace MrCalico_s_Quick_Solitaire
{
    public class QuickSolitaireBase: ComponentBase
    {
        Deck deck;
        Deck drewPile;
        public Card lastCard;
        public int currentCard = -100;

        protected override void OnInitialized()
        {
            deck = new Deck();
            drewPile = new Deck();
            shuffle();
            currentCard = deck.cards.Count;
        }

        public void draw()
        {
            if (deck.cards.Count <= 0) /* Flip Deck */
            {
                deck = drewPile;
                drewPile = new Deck();
                deck.Select(card => drewPile.ToList());
                currentCard = deck.cards.Count;

            }
            for (int x = 0; x < 3; x++) // Draw Three
            {
                if (deck.cards.Count > 0)
                {
                    lastCard = deck.Deal();
                    drewPile.cards[--currentCard] = lastCard;                 
                }
            }
        }

        void shuffle()
        {
            if (deck != null)
                deck.Shuffle();
        }
    }

}

