using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automato
{
    class EstadoUnificado
    {
        public DuplaEstado Dupla { get; private set; }
        public Node EstadoEquivalente { get; private set; }

        public EstadoUnificado(DuplaEstado dupla, Node EstadoEquivalente)
        {
            this.Dupla = dupla;
            this.EstadoEquivalente = EstadoEquivalente;
        }
    }
}
