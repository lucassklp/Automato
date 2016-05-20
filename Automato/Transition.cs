using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automato
{
    [Serializable]
    class Transition
    {
        public Node From;
        public Node To;
        public char Element;

        public Transition(Node From, Node To, char Element)
        {
            this.From = From;
            this.To = To;
            this.Element = Element;
        }
    }
}
