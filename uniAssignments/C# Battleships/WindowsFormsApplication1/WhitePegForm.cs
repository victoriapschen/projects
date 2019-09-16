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
    public partial class WhitePegForm : Form
    {
        private BindingList<PlayableCards> handList_;
        public BindingList<PlayableCards> ReturnValue { get; set; }
        public WhitePegForm(BindingList<PlayableCards> handList, int handIndex)
        {
            InitializeComponent();
            //copies the handList so it can be accessible by other parts of the form
            handList_ = new BindingList<PlayableCards>();
            foreach (PlayableCards p in handList)
            {
                handList_.Add(p);
            }
            //removes the special card from the list
            handList_.RemoveAt(handIndex);
            //Sets the listbox to have each card in the hand
            for (int i = 0; i < handList_.Count; i++)
            {
                ((ListBox)checkedListBox).DataSource = handList_;
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            List<int> selectedCardsIndex_;
            selectedCardsIndex_ = new List<int>();
            bool hasWhiteCard = false;
            //makes sure something is checked
            if (checkedListBox.CheckedItems.Count != 0)
            {
                //loop through list and add the index of each selected item
                for (int i = 0; i < checkedListBox.Items.Count; i++)
                {
                    //if the item is checked
                    if (checkedListBox.GetItemChecked(i))
                    {
                        //add the index of that item
                        selectedCardsIndex_.Add(i);
                    }
                }
                //checks if the selected cards are white peg cards. If they are not white peg cards, remove them from the selected index list.
                for (int i = (selectedCardsIndex_.Count-1); i >= 0 ; i--)
                {
                    if (handList_[selectedCardsIndex_[i]].Type_ != "White")
                    {
                        selectedCardsIndex_.RemoveAt(i);
                    }
                    //If there is a white peg card then set the bool to true
                    else if (handList_[selectedCardsIndex_[i]].Type_ == "White")
                    {
                        hasWhiteCard = true;
                    }
                }
                //If there is a white card selected, remove all the selected cards from the handList.
                if (hasWhiteCard == true)
                {
                    for (int i = selectedCardsIndex_.Count-1; i >= 0; i--)
                    {
                        handList_.RemoveAt(selectedCardsIndex_[i]);
                    }
                    //return the handlist.
                    this.ReturnValue = handList_;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please select at least one white peg.");
                }

            }
            //if nothing is checked
            else
            {
                MessageBox.Show("Please selected at least one white peg to discard.");
            }
        }
    }
}
