using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class PegCards : PlayableCards
    {
        private enum Type { White, Red, Shield };
        //White or Red missile 
        Type type_;
        private int value_;
        //Constructor
        public PegCards(int type, int value) :base()
        {
            type_ = (Type)type;
            value_ = value;
        }

        public override string Type_
        {
            get { return type_.ToString(); }
        }
        public int Value
        {
            get { return value_; }
        }

        public override string ToString()
        {
            //If it is a shield card
            if ((int)type_ == 2)
            {
                return type_.ToString().PadRight(10) + (value_*(-1)).ToString();
            }
            else
            {
                return type_.ToString() + " Peg".PadRight(10) + value_.ToString();
            }
        }
    }
}
