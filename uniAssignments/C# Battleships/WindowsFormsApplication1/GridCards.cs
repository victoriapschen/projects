using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class GridCards : PlayableCards
    {
        private enum Type { Submarine, PTBoat, Destroyer, Battleship, AircraftCarrier, MISS };
        Type type_;
        private bool faceDown_;
        int hp_;
        private int totalGrid_;
        private BindingList<PlayableCards> attachedCardsList_;

        //Constructor
        public GridCards(int type)
        {
            type_ = (Type)type;
            if (type == 0)
            {
                hp_ = 3;
            }
            else if (type == 1)
            {
                hp_ = 2;
            }
            else if (type == 2)
            {
                hp_ = 3;
            }
            else if (type == 3)
            {
                hp_ = 4;
            }
            else if (type == 4)
            {
                hp_ = 5;
            }
            else
            {
                //THE SEA TAKES ALL
                hp_ = 100;
            }
            faceDown_ = true;
            attachedCardsList_ = new BindingList<PlayableCards>();
        }


        public bool FaceDown
        {
            get { return faceDown_; }
            set { faceDown_ = value; }
        }

        public override string Type_
        {
            get { return type_.ToString(); }
        }

        public BindingList<PlayableCards> AttachedCards
        {
            get { return attachedCardsList_; }
            set { attachedCardsList_ = value; }
        }

        public int TotalDamage
        {
            get
            {
                totalGrid_ = 0;
                //Adds up the total value of all the attached grid cards.
                foreach (PegCards p in attachedCardsList_)
                {
                    totalGrid_ += p.Value;
                }
                return totalGrid_;
            }
        }

        //Checks if the ship is alive or not
        public bool Alive
        {
            get { if (TotalDamage >= hp_)
                {
                    return false;
                }
                else
                {
                    return true;
                } }
        }

        public override string ToString()
        {
            //If card is face down
            if (faceDown_ == true)
            {
                return "HIDDEN";
            }
            else
            {
                if ((int)type_ == 1)
                {
                    return "PT Boat".PadRight(20) + (hp_-TotalDamage).ToString() + " HP";
                }
                else if ((int)type_ == 4)
                {
                    return "Aircraft Carrier".PadRight(20) + (hp_ - TotalDamage).ToString() + " HP";
                }
                else if ((int)type_ == 5)
                {
                    return type_.ToString();
                }
                else
                {
                    return type_.ToString().PadRight(20) + (hp_ - TotalDamage).ToString() + " HP";
                }
            }
        }

    }
}
