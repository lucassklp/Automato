using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automato
{
    class MinimizacaoAutomato
    {
        private List<Node> listaEstados;
        private List<Transition> listaTransicao;
        private List<char> Alfabeto;
        public MinimizacaoAutomato(List<Node> listaEstados, List<Transition> listaTransicao, List<char> Alfabeto)
        {

        }

        //A lista 'a', 'a' é a que vai ser validada
        //List<char> listaEntrada = new List<char>();
        //listaEntrada.Add('a');
        //listaEntrada.Add('a');

        //LeitorAutomato leitor = new LeitorAutomato(this.listNodes, this.listTransition, this.Alphabet);
        //bool isValidState = leitor.GetEstado(listaEntrada);

        //if (isValidState)
        //    MessageBox.Show("Essa linguagem é reconhecida como válida");
        //else
        //    MessageBox.Show("Essa linguagem não é reconhecida como válida");

        //this.Command = ' ';


    }
}
