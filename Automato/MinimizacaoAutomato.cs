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


        /// <summary>
        /// Função usada para a minimização de automato.
        /// Fortemente baseado no PDF anexado nesse projeto.
        /// </summary>
        /// <returns>A dupla de estados equivalentes</returns>
        public List<DuplaEstado> GetEstadosEquivalentes()
        {
            
            //1. Construção da tabela: Relaciona estados distintos
            List<DuplaEstado> duplaEstados = new List<DuplaEstado>();
            for (int i = 0; i < listaEstados.Count; i++)
                for (int j = i + 1; j < listaEstados.Count; j++)
                    if (this.listaEstados[i].Nome != this.listaEstados[j].Nome)
                        duplaEstados.Add(new DuplaEstado(listaEstados[i], this.listaEstados[j]));


            //2. Marcação dos estados trivialmente não equivalentes
            List<DuplaEstado> NaoMarcados;
            NaoMarcados = duplaEstados.FindAll(p => (p.Estado1.Estado == Estado.Aceitacao && p.Estado2.Estado == Estado.Aceitacao) ||
                                        (p.Estado1.Estado == Estado.NaoAceitacao && p.Estado2.Estado == Estado.NaoAceitacao) ||
                                        (p.Estado1.Estado == Estado.InicialAceitacao && p.Estado2.Estado == Estado.Aceitacao) ||
                                        (p.Estado1.Estado == Estado.InicialNaoAceitacao && p.Estado2.Estado == Estado.NaoAceitacao));
            

            
            //3. Marcação dos estados não equivalentes
            int count = 0;
            List<DuplaEstado> ItensMarcados = new List<DuplaEstado>();
            
            foreach (var dupla in NaoMarcados)
            {

                List<DuplaEstado> duplaAlfabeto = this.GetDuplasAlfabeto(dupla);
                foreach (DuplaEstado item in duplaAlfabeto)
                {
                    if (this.EstadosSãoIguais(item))
                        continue;
                    else if (this.Contém(item, NaoMarcados) && !this.Contém(item, ItensMarcados))
                        this.GetDuplaEstado(item, NaoMarcados).Link(dupla);
                    else if (!this.Contém(item, NaoMarcados) || this.Contém(item, ItensMarcados))
                    {
                        if (duplaEstados.Exists(x => x.Estado1.Nome == item.Estado1.Nome && x.Estado2.Nome == item.Estado2.Nome))
                        {
                            ItensMarcados.Add(dupla);
                            foreach (var linked in dupla.LinkedList)
                                ItensMarcados.Add(linked);
                            break;
                        }
                        else
                            count++;
                    }
                }
            }

            foreach (var itemToRemove in ItensMarcados)
            {
                var itemInList = NaoMarcados.Find(x => x.Estado1.Nome == itemToRemove.Estado1.Nome && x.Estado2.Nome == itemToRemove.Estado2.Nome);
                if (itemInList != null)
                    NaoMarcados.Remove(itemInList);
            }

            return NaoMarcados;
        }

        private List<DuplaEstado> GetDuplasAlfabeto(DuplaEstado dupla)
        {
            List<DuplaEstado> duplasAlfabeto = new List<DuplaEstado>();
            LeitorAutomato leitorAutomato = new LeitorAutomato(this.listaEstados, this.listaTransicao, this.Alfabeto);
            foreach (var item in this.Alfabeto)
            {
                Node estado1 = leitorAutomato.GetEstado(item.ToString(), dupla.Estado1);
                Node estado2 = leitorAutomato.GetEstado(item.ToString(), dupla.Estado2);
                duplasAlfabeto.Add(new DuplaEstado(estado1, estado2));
            }
            return duplasAlfabeto;
        }

        private bool Contém(DuplaEstado item, List<DuplaEstado> duplaEstado)
        {
            return duplaEstado.Exists(x => x.Estado1.Nome == item.Estado1.Nome && x.Estado2.Nome == item.Estado2.Nome);
        }

        private DuplaEstado GetDuplaEstado(DuplaEstado item, List<DuplaEstado> duplaEstado)
        {
            return duplaEstado.Find(x => x.Estado1.Nome == item.Estado1.Nome && x.Estado2.Nome == item.Estado2.Nome);
        }

        private bool EstadosSãoIguais(DuplaEstado item)
        {
            return (item.Estado1.Nome == item.Estado2.Nome);
        }
    }
}
