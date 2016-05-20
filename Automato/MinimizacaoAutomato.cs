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
            duplaEstados = duplaEstados.FindAll(p => (p.Estado1.Estado == Estado.Aceitacao && p.Estado2.Estado == Estado.Aceitacao) ||
                                        (p.Estado1.Estado == Estado.NaoAceitacao && p.Estado2.Estado == Estado.NaoAceitacao) ||
                                        (p.Estado1.Estado == Estado.InicialAceitacao && p.Estado2.Estado == Estado.Aceitacao) ||
                                        (p.Estado1.Estado == Estado.InicialNaoAceitacao && p.Estado2.Estado == Estado.NaoAceitacao));
            

            LeitorAutomato leitorAutomato = new LeitorAutomato(this.listaEstados, this.listaTransicao, this.Alfabeto);
            List<DuplaEstado> itensToRemove = new List<DuplaEstado>();


            foreach (var dupla in duplaEstados)
            {
                foreach (var item in this.Alfabeto)
                {
                    Node estado1 = leitorAutomato.GetEstado(item.ToString(), dupla.Estado1);
                    Node estado2 = leitorAutomato.GetEstado(item.ToString(), dupla.Estado2);

                    //É ao contrário.
                    //Codigo Antigo:
                    //dupla.Link(new DuplaEstado(estado1, estado2));

                    //Exemplo (o contrário): 
                    DuplaEstado resultadoBusca = duplaEstados.Find(x => x.Estado1.Nome == estado1.Nome && x.Estado2.Nome == estado2.Nome);
                    if (resultadoBusca != null && estado1.Nome != estado2.Nome)
                        resultadoBusca.Link(dupla);
                    else if (resultadoBusca == null && estado1.Nome != estado2.Nome)
                        itensToRemove.Add(dupla);
                }
            }

            //foreach (var dupla in duplaEstados)
            //{
            //    foreach (var linkedItem in dupla.LinkedList)
            //    {
            //        var resultadoBusca = duplaEstados.Find(x => x.Estado1.Nome == linkedItem.Estado1.Nome && x.Estado2.Nome == linkedItem.Estado2.Nome);
            //        if (resultadoBusca == null && linkedItem.Estado1.Nome != linkedItem.Estado2.Nome)
            //        {
            //            itensToRemove.Add(dupla);
            //            break;
            //        }
            //    }
            //}





            List<DuplaEstado> estadosEquivalentes = new List<DuplaEstado>();

            foreach (var item in itensToRemove)
                duplaEstados.RemoveAll(x => x.Estado1.Nome == item.Estado1.Nome && x.Estado2.Nome == item.Estado2.Nome);

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
