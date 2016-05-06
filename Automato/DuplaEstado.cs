using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automato
{
    class DuplaEstado
    {
        public Node Estado1;
        public Node Estado2;

        public DuplaEstado(Node Estado1, Node Estado2)
        {
            this.Estado1 = Estado1;
            this.Estado2 = Estado2;

        }
    }
}
