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
            this.listaEstados = listaEstados;
            this.listaTransicao = listaTransicao;
            this.Alfabeto = Alfabeto;
        }

        public List<DuplaEstado> GetEstadosEquivalentes()
        {
            List<DuplaEstado> duplaEstados = new List<DuplaEstado>();
            for (int i = 0; i < listaEstados.Count; i++)
                for (int j = i + 1; j < listaEstados.Count; j++)
                    if (this.listaEstados[i].Nome != this.listaEstados[j].Nome)
                        duplaEstados.Add(new DuplaEstado(listaEstados[i], this.listaEstados[j]));

            //Deleta as duplas que são de estados diferentes
            duplaEstados.RemoveAll(p => (p.Estado1.Estado == Estado.Aceitacao && p.Estado2.Estado == Estado.NaoAceitacao) ||
                                        (p.Estado1.Estado == Estado.NaoAceitacao && p.Estado2.Estado == Estado.Aceitacao) ||
                                        (p.Estado1.Estado == Estado.InicialAceitacao && p.Estado2.Estado == Estado.NaoAceitacao) ||
                                        (p.Estado1.Estado == Estado.InicialNaoAceitacao && p.Estado2.Estado == Estado.Aceitacao));


            return duplaEstados;
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
