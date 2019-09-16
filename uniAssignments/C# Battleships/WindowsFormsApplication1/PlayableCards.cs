using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    /// <summary>
    /// This abstract class allows both the peg card and special card classes to be in the same list.
    /// </summary>
    public abstract class PlayableCards
    {
        private enum Type {  };
        Type type_;
        public abstract string Type_
        {
            get;
        }
    }
}
