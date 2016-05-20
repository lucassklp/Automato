using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Automato
{
    [Serializable]
    class AutomatoFile
    {
        public List<Node> listEstados { get; set; }
        public List<Transition> listTransition { get; set; }
        public List<char> Alfabeto { get; set; }

        public AutomatoFile(List<Node> listEstados, List<Transition> listTransition, List<char> Alfabeto)
        {
            this.listEstados = listEstados;
            this.listTransition = listTransition;
            this.Alfabeto = Alfabeto;
        }
    }
}
