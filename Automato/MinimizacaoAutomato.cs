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
                            this.RemoverRecursivo(dupla, ref ItensMarcados);
                        }
                        else if (dupla.Equals(NaoMarcados.First()))
                        {
                            DuplaEstado p = new DuplaEstado(item.Estado2, item.Estado1);
                            if (duplaEstados.Exists(x => x.Estado1.Nome == p.Estado1.Nome && x.Estado2.Nome == p.Estado2.Nome))
                            {
                                ItensMarcados.Add(dupla);
                                this.RemoverRecursivo(dupla, ref ItensMarcados);
                            }
                        }
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


        private void RemoverRecursivo(DuplaEstado dupla, ref List<DuplaEstado> ItensMarcados)
        {
            if (dupla.LinkedList == null)
                return;
            else
            {
                foreach (var item in dupla.LinkedList)
                {
                    RemoverRecursivo(item, ref ItensMarcados);
                    ItensMarcados.Add(item);
                }
            }
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



        public void UnificacaoDosEstados(List<DuplaEstado> listDuplas)
        {

            List<EstadoUnificado> estadosUnificados = new List<EstadoUnificado>();

            foreach (var item in listDuplas)
            {
                string NomeEstadoUnificado = item.Estado1.Nome + item.Estado2.Nome.Replace("q", "");

                Estado estado;

                if (item.Estado1.Estado == Estado.InicialAceitacao || item.Estado2.Estado == Estado.InicialAceitacao)
                    estado = Estado.InicialAceitacao;
                else if (item.Estado1.Estado == Estado.InicialNaoAceitacao || item.Estado2.Estado == Estado.InicialNaoAceitacao)
                    estado = Estado.InicialNaoAceitacao;
                else
                    estado = item.Estado1.Estado;
                
                Node novoEstado = new Node(NomeEstadoUnificado, item.Estado1.Coordenada, estado);
                estadosUnificados.Add(new EstadoUnificado(item, novoEstado));
                
            }

            foreach (var item in estadosUnificados)
            {
                List<Transition> transicoesEstado1 = this.listaTransicao.FindAll(x => x.From.Nome == item.Dupla.Estado1.Nome);
                List<Transition> transicoesEstado2 = this.listaTransicao.FindAll(x => x.From.Nome == item.Dupla.Estado2.Nome);

                foreach (var letra in this.Alfabeto)
                {
                    Node est1 = transicoesEstado1.Find(x => x.From.Nome == item.Dupla.Estado1.Nome).To;
                    Node est2 = transicoesEstado2.Find(x => x.From.Nome == item.Dupla.Estado2.Nome).To;

                    EstadoUnificado destino = estadosUnificados.Find(x => x.Dupla.Estado1.Nome == est1.Nome && x.Dupla.Estado2.Nome == est2.Nome);

                    if (destino != null)
                        this.listaTransicao.Add(new Transition(item.EstadoEquivalente, destino.EstadoEquivalente, letra));
                    else
                    {
                        this.listaTransicao.Add(new Transition(item.EstadoEquivalente, est1, letra));
                        //this.listaTransicao.Add(new Transition(item.EstadoEquivalente, est2, letra));
                    }

                }
            }

            foreach (var item in estadosUnificados)
            {
                //Adiciona os estados unificados
                listaEstados.Add(item.EstadoEquivalente);

                List<Node> estados = new List<Node>();
                List<EstadoUnificado> estunifc = new List<EstadoUnificado>();
                estunifc = estadosUnificados;

                foreach (var itemToList in estunifc)
                {
                    estados.Add(itemToList.Dupla.Estado1);
                    estados.Add(itemToList.Dupla.Estado2);
                }

                List<Transition> transicoes = new List<Transition>();

                transicoes = this.listaTransicao.FindAll(x => estados.Exists(y => y.Nome == x.To.Nome));
                transicoes.RemoveAll(x => estados.Exists(y => y.Nome == x.From.Nome));
                

                foreach (var itemTransition in transicoes)
                {
                    EstadoUnificado p = estadosUnificados.Find(x => x.Dupla.Estado1.Nome == itemTransition.To.Nome || x.Dupla.Estado2.Nome == itemTransition.To.Nome);
                    itemTransition.To = p.EstadoEquivalente;
                }
                        

                //Remove as duplas
                listaEstados.RemoveAll(x => x.Nome == item.Dupla.Estado1.Nome || x.Nome == item.Dupla.Estado2.Nome);

                //Remove as transições
                listaTransicao.RemoveAll(x => x.From.Nome == item.Dupla.Estado1.Nome || x.To.Nome == item.Dupla.Estado1.Nome);
                listaTransicao.RemoveAll(x => x.From.Nome == item.Dupla.Estado2.Nome || x.To.Nome == item.Dupla.Estado2.Nome);

            }






        }







    }
}
