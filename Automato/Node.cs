using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automato
{
    [Serializable]
    class Node
    {
        public string Nome;
        public Point Coordenada;
        public Estado Estado { get; set; }

        public Node(string Nome, Point Coordenada, Estado estado)
        {
            this.Nome = Nome;
            this.Coordenada = Coordenada;
            this.Estado = estado;
        }
    }
}
