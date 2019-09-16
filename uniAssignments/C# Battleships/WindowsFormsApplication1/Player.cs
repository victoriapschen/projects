using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Player
    {
        private BindingList<PlayableCards> deckList_;
        private BindingList<PlayableCards> handList_;
        private BindingList<GridCards> gridList_;

        //Constructor
        public Player()
        {
            //Creates the deck
            deckList_ = new BindingList<PlayableCards>();
            NewDeck();
            //Draws the starting hand
            handList_ = new BindingList<PlayableCards>();
            DrawCards();
            //Creates the grid
            gridList_ = new BindingList<GridCards>();
            GenerateGrid();
        }

        public BindingList<PlayableCards> DeckList
        {
            get { return deckList_; }
        }
        public BindingList<PlayableCards> HandList
        {
            get { return handList_; }
        }
        public BindingList<GridCards> GridList
        {
            get { return gridList_; }
        }

        /// <summary>
        /// Generates a new grid
        /// </summary>
        private void GenerateGrid()
        {
            //new list for shuffling
            BindingList<GridCards> unshuffledGridList_;
            unshuffledGridList_ = new BindingList<GridCards>();
            //Add one card for each of the ships
            GridCards p = new GridCards(0);
            unshuffledGridList_.Add(p);
            p = new GridCards(1);
            unshuffledGridList_.Add(p);
            p = new GridCards(2);
            unshuffledGridList_.Add(p);
            p = new GridCards(3);
            unshuffledGridList_.Add(p);
            p = new GridCards(4);
            unshuffledGridList_.Add(p);
            //Add seven ocean cards
            for (int i = 0; i < 7; i++)
            {
                unshuffledGridList_.Add(new GridCards(5));
            }
            //Shuffle it
            Random random = new Random();
            //While the list still has cards in it
            while (unshuffledGridList_.Count > 0)
            {
                //randomly choose one card and add it to other list
                int cardIndex = random.Next(unshuffledGridList_.Count);
                gridList_.Add(unshuffledGridList_[cardIndex]);
                //remove card from deck
                unshuffledGridList_.RemoveAt(cardIndex);
            }

        }

        /// <summary>
        /// Creates a new deck
        /// </summary>
        private void NewDeck()
        {

            //Add 10x1 white peg cards
            //loop adding the card 10 times
            for (int i = 0; i < 10; i++)
            {
                deckList_.Add(new PegCards(0, 1));
            }
            //Add 6x1 red peg cards
            for (int i = 0; i < 6; i++)
            {
                deckList_.Add(new PegCards(1, 1));
            }
            //Add 3 × 2 red peg cards

            for (int i = 0; i < 3; i++)
            {
                deckList_.Add(new PegCards(1, 2));
            }
            //Add 1 × 4 red peg cards
            deckList_.Add(new PegCards(1, 4));

            //Add 2 Shield Cards
            for (int i = 0; i < 2; i++)
            {
                deckList_.Add(new PegCards(2, -2));
            }
            //Add 2 type 1 special cards
            for (int i = 0; i < 2; i++)
            {
                deckList_.Add(new SpecialCards(0));
            }
            //Add 2 type 2 special cards
            for (int i = 0; i < 2; i++)
            {
                deckList_.Add(new SpecialCards(1));
            }
            //shuffles the deck

            ShuffleDeck();
        }

        /// <summary>
        /// Shuffles the deck
        /// </summary>
        private void ShuffleDeck()
        {
            //new list for shuffled deck to go to
             BindingList<PlayableCards> shuffledList_;
            shuffledList_ = new BindingList<PlayableCards>();
            Random random = new Random();
            //While the deck still has cards in it
            while (deckList_.Count > 0)
            {
                //randomly choose one card and add it to other list
                int cardIndex = random.Next(deckList_.Count);
                shuffledList_.Add(deckList_[cardIndex]);
                //remove card from deck
                deckList_.RemoveAt(cardIndex);
            }
            //Makes the shuffled list the deck list
            deckList_ = shuffledList_;
        }

        /// <summary>
        /// Draws cards until hand is full
        /// </summary>
        public void DrawCards()
        {
            //while hand list is less than 5
            while (handList_.Count < 5)
            {
                //If deck is empty, make a new deck
                if (deckList_.Count == 0)
                {
                    NewDeck();
                }
                else
                {
                    //adds the first card from the deck, then removes it from the deck
                    handList_.Add(deckList_[0]);
                    deckList_.RemoveAt(0);
                }
            }
        }

        /// <summary>
        /// Draws 3 cards from the deck.
        /// </summary>
        public void Draw3Cards()
        {
            for (int i = 0; i < 3; i++)
            {
                //If deck is empty, make a new deck
                if (deckList_.Count == 0)
                {
                    NewDeck();
                }
                else
                {
                    //adds the first card from the deck, then removes it from the deck
                    handList_.Add(deckList_[0]);
                    deckList_.RemoveAt(0);
                }
            }
        }
    }
}
