using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class SpecialCards : PlayableCards
    {
        private enum Type { SpecialCard1, SpecialCard2 };
        Type type_;
        public SpecialCards(int type)
        {
            type_ = (Type)type;
        }

        public override string Type_
        {
            get { return type_.ToString(); }
        }

        public override string ToString()
        {
            if ((int)type_ == 0)
            {
                return "Discard white peg cards. || Play two cards.";
            }
            else
            {
                return "Repair a ship, then play a card. || Draw 3 cards, then play one.";
            }
        }
    }
}
