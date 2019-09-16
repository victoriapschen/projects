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
    public partial class SpecialCardForm : Form
    {
        //This allows the form to return a value to the main form
        public int ReturnValue { get; set; }

        public SpecialCardForm(string cardType)
        {
            InitializeComponent();
            if (cardType== "SpecialCard1")
            {
                button1.Text = "Discard white peg cards.";
                button2.Text = "Play two cards.";
                labelOption1.Text = "Discard one or more white peg cards. This allows you to draw more cards when reloading your hand.";
                labelOption2.Text = "Play two more cards.";
            }
            else
            {
                button1.Text = "Repair a ship, then play a card.";
                button2.Text = "Draw 3 cards, then play one.";
                labelOption1.Text = "Remove one peg card from one of your damaged ships, then play another card on the same turn. This power does not work on shields.";
                labelOption2.Text = " Draw three cards from your deck, then play one of them. Keep the remaining two cards in your hand.";
            }
        }

        /// <summary>
        /// Plays the first option.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //returns that option 1 was chosen
            this.ReturnValue = 1;
            //returns that an option was chosen
            this.DialogResult = DialogResult.OK;
            //closes the form
            this.Close();
        }

        /// <summary>
        /// Plays the second option.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //returns that option 2 was chosen
            this.ReturnValue = 2;
            //returns that an option was chosen
            this.DialogResult = DialogResult.OK;
            //closes the form
            this.Close();
        }
    }
}
