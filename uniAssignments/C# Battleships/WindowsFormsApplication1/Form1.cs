using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int blueVictoryCount = 0;
        int redVictoryCount = 0;
        //counts how many times you/enemy can play a card
        int turnCounter = 1;
        bool validTurn = true;
        bool victory = false;
        private  Player bluePlayer_;
        private Player redPlayer_;
        public Form1()
        {
            InitializeComponent();
            bluePlayer_ = new Player();
            listBoxHand.DataSource = bluePlayer_.HandList;
            listBoxBlueGrid.DataSource = bluePlayer_.GridList;
            redPlayer_ = new Player();
            listBoxRedGrid.DataSource = redPlayer_.GridList;
        }

        /// <summary>
        /// Plays the selected card on the selected grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPlay_Click(object sender, EventArgs e)
        {
            validTurn = true;
            //if there are still turns left to be played
            if (turnCounter > 0)
            {
                //Plays your card
                PlayCard();
                CheckVictory();
                //
                if (validTurn == true)
                {
                    turnCounter--;
                }
                else
                {
                    return;
                }
            }
            //once there are no more turns to be played it becomes the enemy turn. Otherwise it goes back again
            if (turnCounter == 0)
            {
                turnCounter = 1;
                while (turnCounter > 0)
                {
                    //only play the enemy turn out if the player did a valid turn, and victory has not happened
                    if (validTurn == true && victory == false)
                    {
                        //Enemy plays their turn
                        EnemyPlay();
                        CheckVictory();
                        turnCounter--;
                    }
                    //if victory happened then reset it
                    else if (victory == true)
                    {
                        victory = false;
                        turnCounter--;
                    }
                }
            }
            else
            {
                return;
            }
            turnCounter = 1;

        }//endplay

        /// <summary>
        /// Plays the selected card.
        /// </summary>
        private void PlayCard()
        {
            //gets the selected index of the listboxes
            int blueGridIndex = listBoxBlueGrid.SelectedIndex;
            int redGridIndex = listBoxRedGrid.SelectedIndex;
            int handIndex = listBoxHand.SelectedIndex;
            PlayableCards p = bluePlayer_.HandList[handIndex];
            GridCards r = redPlayer_.GridList[redGridIndex];
            GridCards b = bluePlayer_.GridList[blueGridIndex];
            //Makes sure that a shield isn't trying to be used on a hidden grid card
            if (p.Type_ == "Shield" && bluePlayer_.GridList[blueGridIndex].FaceDown == true)
            {
                labelTurn.Text = "Can't play shield on a hidden grid card.";
                validTurn = false;
                //ends this function
                return;
            }
            //Checks if the hand card is a peg card
            if (p is PegCards)
            {
                //if the grid card is face down
                if (r.FaceDown == true)
                {
                    //sets the grid to face up
                    r.FaceDown = false;
                    textBoxLog.AppendText("You search the computer's ocean.");
                    textBoxLog.AppendText(Environment.NewLine);
                    //if the played card is a white peg and the targeted grid is a submarine
                    if (r.Type_ == "Submarine" && p.Type_ == "White")
                    {
                        //Attach the card to the ship
                        r.AttachedCards.Add(p);
                    }
                    //if the played card is a red peg, and it is not a miss or a submarine
                    else if (r.Type_ != "MISS" && r.Type_ != "Submarine" && p.Type_ == "Red")
                    {
                        //Attach the card to the ship
                        r.AttachedCards.Add(p);
                        CheckDestroyed(r, "red", redGridIndex);
                    }
                }
                //else if card is face up
                else
                {
                    //If the player is using a shield
                    if (p.Type_ == "Shield")
                    {
                        //if the player tries using a shield on a hidden spot or a miss
                        if (b.FaceDown == true || b.Type_ == "MISS")
                        {
                            labelTurn.Text = "Can't use a shield on that.";
                            validTurn = false;
                            return;
                        }
                        //otherwise 
                        else
                        {
                            b.AttachedCards.Add(p);
                            textBoxLog.AppendText("You place a shield on your ship.");
                            textBoxLog.AppendText(Environment.NewLine);
                        }
                    }
                    else
                    {
                        //if the player tries to attack an empty spot
                        if (r.Type_ == "MISS")
                        {
                            labelTurn.Text = "Don't waste a card on an empty spot!";
                            validTurn = false;
                            return;
                        }
                        //if the ship is a submarine 
                        if (r.Type_ == "Submarine")
                        {
                            //if the played card is a white peg
                            if (p.Type_ == "White")
                            {
                                //Attach the card to the ship
                                r.AttachedCards.Add(p);
                                textBoxLog.AppendText("You attack the computer's Submarine with a white peg.");
                                textBoxLog.AppendText(Environment.NewLine);
                                CheckDestroyed(r, "red", redGridIndex);
                            }
                            else
                            {
                                labelTurn.Text = "You cannot attack a Submarine with a red peg card.";
                                validTurn = false;
                                return;
                            }
                        }
                        //else if the ship is something else
                        else
                        {
                            //if the played card is a red peg
                            if (p.Type_ == "Red")
                            {
                                //Attach the card to the ship
                                r.AttachedCards.Add(p);
                                textBoxLog.AppendText("You attack the computer's ship with a red peg.");
                                textBoxLog.AppendText(Environment.NewLine);
                                //Check for ship death
                                CheckDestroyed(r, "red", redGridIndex);
                            }
                            else
                            {
                                labelTurn.Text = "You cannot attack a non-Submarine with a white peg card.";
                                validTurn = false;
                                return;
                            }
                        }
                    }
                }
                //refreshes listbox
                RefreshListBoxes();
                bluePlayer_.HandList.RemoveAt(handIndex);
                if (turnCounter < 2)
                {
                    //redraws hand, unless turn counter is at 1 which means that the special card was in effect
                    bluePlayer_.DrawCards();
                }
            }//endifpeg
            //otherwise if a special card is played
            else if (p is SpecialCards)
            {
                // Create new form for the dialog
                SpecialCardForm dialog = new SpecialCardForm(p.Type_);
                // Opens the dialog window
                dialog.ShowDialog();
                //If the user successfully chose an option
                if (dialog.DialogResult==DialogResult.OK)
                {
                    //If the first option was chosen
                    if (dialog.ReturnValue==1)
                    {
                        //if it was the type 1 card
                        if (p.Type_== "SpecialCard1")
                        {
                            //Discard white peg cards.
                            WhitePegForm whitePegDialog = new WhitePegForm(bluePlayer_.HandList, handIndex);
                            //if user select correct stuff
                            if (whitePegDialog.ShowDialog() == DialogResult.OK)
                            {
                                //replace current handlist with new handlist.
                                bluePlayer_.HandList.Clear();
                                foreach (PlayableCards c in whitePegDialog.ReturnValue)
                                {
                                    bluePlayer_.HandList.Add(c);
                                }
                                textBoxLog.AppendText("You discard white peg(s) from your hand.");
                                textBoxLog.AppendText(Environment.NewLine);
                                //Refresh
                                RefreshListBoxes();
                                //Redraws hand
                                bluePlayer_.DrawCards();

                            }
                            else
                            {
                                labelTurn.Text = "Please select only white peg cards.";
                                validTurn = false;
                            }
                        }
                        //if it was the other card
                        else
                        {
                            //Repair a ship, then play a card
                            //if a face up ship is selected, and it has damage on it
                            if (b.Type_ != "MISS" && b.FaceDown == false && b.TotalDamage>0)
                            {
                                //gets the last peg card attached to it
                                bool pegCardFound = false;
                                    //loop downwards through the list
                                    for (int i = b.AttachedCards.Count-1; i >= 0; i--)
                                    {
                                    if (b.AttachedCards[i].Type_ != "Shield" && pegCardFound == false)
                                        {
                                            pegCardFound = true;
                                            //remove the card
                                            b.AttachedCards.RemoveAt(i);
                                        }
                                    }

                                textBoxLog.AppendText("You remove a peg from your ship.");
                                textBoxLog.AppendText(Environment.NewLine);
                                //refresh
                                RefreshListBoxes();
                                bluePlayer_.HandList.RemoveAt(handIndex);
                                //adds a turn
                                turnCounter++;
                                //notifies player they can play again
                                textBoxLog.AppendText("You can play another card.");
                                textBoxLog.AppendText(Environment.NewLine);
                            }
                            //was not a valid turn
                            else
                            {
                                labelTurn.Text = "Select a damaged ship first.";
                                validTurn = false;
                            }
                        }
                    }
                    //if the second option was chosen
                    else
                    {
                        //if it was the type 1 card
                        if (p.Type_ == "SpecialCard1")
                        {
                            //Can only play if hand has more than 2 cards
                            if (bluePlayer_.HandList.Count > 2)
                            {
                                //Removes card from hand
                                bluePlayer_.HandList.RemoveAt(handIndex);
                                //Gives 2 turns so player can play 2 cards
                                turnCounter += 2;
                                textBoxLog.AppendText("You can play another card.");
                                textBoxLog.AppendText(Environment.NewLine);
                            }
                            //was not a valid turn
                            else
                            {
                                labelTurn.Text = "Cannot play that special card option without more than 2 cards.";
                                validTurn = false;
                            }

                        }
                        //if it was the other card
                        else
                        {
                            //Draw 3 cards then play one
                            //Removes card from hand
                            bluePlayer_.HandList.RemoveAt(handIndex);
                            //draws 3 cards
                            bluePlayer_.Draw3Cards();
                            textBoxLog.AppendText("You draw 3 cards.");
                            textBoxLog.AppendText(Environment.NewLine);
                            textBoxLog.AppendText("You can play another card.");
                            textBoxLog.AppendText(Environment.NewLine);
                            turnCounter++;
                        }
                    }
                }
                else
                {
                    //was not a valid turn
                    validTurn = false;
                }
            }
        }//endplaymethod

        /// <summary>
        /// The enemy's turn.
        /// </summary>
        private void EnemyPlay()
        {
            //logs the computer's hand to the console log to check their hand
            Console.WriteLine("Computer Hand");
            foreach(PlayableCards p in redPlayer_.HandList)
            {
                Console.WriteLine(p.ToString());
            }
            Random random = new Random();
            bool allHidden = true;
            bool redShipShowing = false;
            bool hasWhitePeg = false;
            bool hasRedPeg = false;
            bool hasShield = false;
            bool canAttack = false;
            bool hasDraw3CardsCard = false;
            bool hasPlay2CardsCard = false;
            int handWhiteIndex = 0 ;
            int handRedIndex = 0;
            int handShieldIndex = 0;
            int hand3CardsIndex = 0;
            int hand2PlayIndex = 0;
            int redGridIndex = 0;
            int blueShipIndex = 0;
            //Checks if all of the current grid on the player's side is hidden
            for (int i = 0; i < bluePlayer_.GridList.Count; i++)
            {
                //if the card is not face down
                if (bluePlayer_.GridList[i].FaceDown == false)
                {
                    //if the face up card is NOT a "Miss" card
                    if (bluePlayer_.GridList[i].Type_ != "MISS")
                    {
                        allHidden = false;
                        //Notes the position of the ship
                        blueShipIndex = i;
                    }
                }
            }//endforeach
            //Checks if there are revealed ships on the computer's side
            for (int i = 0; i<redPlayer_.GridList.Count; i++)
            {
                //if the card is not face down
                if (redPlayer_.GridList[i].FaceDown == false)
                {
                    //if the face up card is NOT a "Miss" card
                    if (redPlayer_.GridList[i].Type_ != "MISS")
                    {
                        redGridIndex = i;
                        redShipShowing = true;
                    }
                }
            }//endfor
           //check if hand has white/red/shield pegs/special cards, and notes down their index
            for (int i = 0; i < redPlayer_.HandList.Count; i++)
            {
                if (redPlayer_.HandList[i].Type_ == "White")
                {
                    hasWhitePeg = true;
                    handWhiteIndex = i;
                }
                //but note down the index for a red peg as well
                else if (redPlayer_.HandList[i].Type_ == "Red")
                {
                    hasRedPeg = true;
                    handRedIndex = i;
                }
                else if (redPlayer_.HandList[i].Type_ == "Shield")
                {
                    hasShield = true;
                    handShieldIndex = i;
                }
                else if (redPlayer_.HandList[i].Type_ == "SpecialCard1")
                {
                    hasPlay2CardsCard = true;
                    hand2PlayIndex = i;
                }
                else if (redPlayer_.HandList[i].Type_ == "SpecialCard2")
                {
                    hasDraw3CardsCard = true;
                    hand3CardsIndex = i;
                }
            }//endfor
            //Checks if computer can attack
            if ((bluePlayer_.GridList[blueShipIndex].Type_=="Submarine" && hasWhitePeg == true)||(bluePlayer_.GridList[blueShipIndex].Type_!="Submarine" && bluePlayer_.GridList[blueShipIndex].Type_ != "MISS" && hasRedPeg == true))
            {
                canAttack = true;
            }
            //If computer has draw 3 cards or play 2 cards special cards in hand
            if (hasDraw3CardsCard == true || hasPlay2CardsCard == true)
            {
                if (hasDraw3CardsCard == true)
                {
                    //Draw 3 cards then play one
                    //Removes card from hand
                    redPlayer_.HandList.RemoveAt(hand3CardsIndex);
                    //draws 3 cards
                    redPlayer_.Draw3Cards();
                    turnCounter++;
                    textBoxLog.AppendText("The computer draws 3 cards.");
                    textBoxLog.AppendText(Environment.NewLine);
                }
                else
                {
                    //Removes card from hand
                    redPlayer_.HandList.RemoveAt(hand2PlayIndex);
                    //Gives 2 turns so enemy can play 2 cards
                    turnCounter += 2;
                    textBoxLog.AppendText("The computer plays 2 cards.");
                    textBoxLog.AppendText(Environment.NewLine);
                }
            }
            //If player grid are all hidden or misses then
            // if there is a revealed blue player ship, and it can be attacked
            else if (allHidden==false && canAttack==true)
            {
                //if the ship is a submarine
                if (bluePlayer_.GridList[blueShipIndex].Type_ == "Submarine")
                {
                    //Play the white peg on it
                    bluePlayer_.GridList[blueShipIndex].AttachedCards.Add(redPlayer_.HandList[handWhiteIndex]);
                    textBoxLog.AppendText("The computer attacks your Submarine with a white peg.");
                    textBoxLog.AppendText(Environment.NewLine);
                    //check ship destruction
                    CheckDestroyed(bluePlayer_.GridList[blueShipIndex], "blue", blueShipIndex);
                    //remove card from hand
                    redPlayer_.HandList.RemoveAt(handWhiteIndex);
                }
                else
                {
                    //Play the red peg on it
                    bluePlayer_.GridList[blueShipIndex].AttachedCards.Add(redPlayer_.HandList[handRedIndex]);
                    textBoxLog.AppendText("The computer attacks your ship with a red peg.");
                    textBoxLog.AppendText(Environment.NewLine);
                    //check ship
                    CheckDestroyed(bluePlayer_.GridList[blueShipIndex], "blue", blueShipIndex);
                    //remove card from hand
                    redPlayer_.HandList.RemoveAt(handRedIndex);
                }
            }
            else
            {
                //if there is a revealed ship in your grid, and you have a shield then use a shield on it
                if (redShipShowing == true && hasShield == true)
                {
                    redPlayer_.GridList[redGridIndex].AttachedCards.Add(redPlayer_.HandList[handShieldIndex]);
                    redPlayer_.HandList.RemoveAt(handShieldIndex);
                    RefreshListBoxes();
                    textBoxLog.AppendText("The computer places a shield on their ship.");
                    textBoxLog.AppendText(Environment.NewLine);
                }
                //otherwise use a peg on enemy ship
                else
                {
                    int randomGrid = random.Next(bluePlayer_.GridList.Count);
                    //Makes sure that the random number isn't set on a face up
                    bool randomTrue = false;
                    while (randomTrue==false)
                    {
                        if (bluePlayer_.GridList[randomGrid].FaceDown == true)
                        {
                            randomTrue = true;
                        }
                        else
                        {
                            //randomizes a new number
                            randomGrid = random.Next(bluePlayer_.GridList.Count);
                        }
                    }
                    //If the hand has a white peg
                    if (hasWhitePeg == true)
                    {
                        //Play that card on a random grid
                        bluePlayer_.GridList[randomGrid].FaceDown = false;
                        //attach the card if it is a submarine 
                        if (bluePlayer_.GridList[randomGrid].Type_ == "Submarine")
                        {
                            bluePlayer_.GridList[randomGrid].AttachedCards.Add(redPlayer_.HandList[handWhiteIndex]);
                        }
                        redPlayer_.HandList.RemoveAt(handWhiteIndex);
                        textBoxLog.AppendText("The computer searches your ocean with a white peg.");
                        textBoxLog.AppendText(Environment.NewLine);
                    }
                    //If the hand doesn't have a white peg
                    else
                    {
                        //Play a red card on a random grid
                        bluePlayer_.GridList[randomGrid].FaceDown = false;
                        textBoxLog.AppendText("The computer searches your ocean with a red peg.");
                        textBoxLog.AppendText(Environment.NewLine);
                        //if the revealed grid is not a sub and not open sea
                        if (bluePlayer_.GridList[randomGrid].Type_ != "MISS" && bluePlayer_.GridList[randomGrid].Type_ != "Submarine")
                        {
                            bluePlayer_.GridList[randomGrid].AttachedCards.Add(redPlayer_.HandList[handRedIndex]);
                            //Checks for ship destruction
                            CheckDestroyed(bluePlayer_.GridList[randomGrid], "blue", randomGrid);
                        }
                        redPlayer_.HandList.RemoveAt(handRedIndex);
                    }
                }
            }//endallhiddenif
            //refreshes listbox
            RefreshListBoxes();
            if (turnCounter < 2)
            {
                //redraws hand, unless turn counter is at 1 which means that the special card was in effect
                redPlayer_.DrawCards();
            }

        }//endenemyplaymethod

        /// <summary>
        /// Checks if the given ship is destroyed or not. Removes ship if it is
        /// </summary>
        /// <param name="r"></param>
        /// <param name="player"></param>
        /// <param name="index"></param>
        private void CheckDestroyed(GridCards r, string player, int index)
        {
            //Is it the blue or red player ship we are checking for death
            if (player == "red")
            {
                //Checks if number of pegs exceeds ships HP, if so, delete ship.
                if (r.Alive == false)
                {
                    redPlayer_.GridList.RemoveAt(index);
                    textBoxLog.AppendText("You have destroyed an enemy ship!");
                    textBoxLog.AppendText(Environment.NewLine);
                    blueVictoryCount++;
                }
            }
            else
            {
                //Checks if number of pegs exceeds ships HP, if so, delete ship.
                if (r.Alive == false)
                {
                    bluePlayer_.GridList.RemoveAt(index);
                    textBoxLog.AppendText("The enemy has destroyed your ship!");
                    textBoxLog.AppendText(Environment.NewLine);
                    redVictoryCount++;
                }
            }
        }

        /// <summary>
        /// Checks if enough ships have been destroyed on either side for a victory, and resets the game afterwards.
        /// </summary>
        private void CheckVictory()
        {
            //if the victory count for either player is 5
            if (blueVictoryCount == 5 || redVictoryCount == 5)
            {
                if (blueVictoryCount == 5)
                {
                    MessageBox.Show("You win!");
                }
                else
                {
                    MessageBox.Show("You lose...");
                }

                //Refreshes the players
                bluePlayer_ = new Player();
                redPlayer_ = new Player();
                blueVictoryCount = 0;
                redVictoryCount = 0;
                victory = true;
                //Clears the log
                textBoxLog.Text = "";
                labelTurn.Text = "Your turn.";
                RefreshListBoxes();
                listBoxHand.DataSource = null;
                listBoxHand.DataSource = bluePlayer_.HandList;
            }
        }

        /// <summary>
        /// Refreshes the grid listboxes.
        /// </summary>
        private void RefreshListBoxes()
        {
            listBoxRedGrid.DataSource = null;
            listBoxRedGrid.DataSource = redPlayer_.GridList;
            listBoxBlueGrid.DataSource = null;
            listBoxBlueGrid.DataSource = bluePlayer_.GridList;
        }
    }
}
