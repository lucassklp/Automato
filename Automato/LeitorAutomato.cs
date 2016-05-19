using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automato
{
    class LeitorAutomato
    {
        private List<Node> listaEstados;
        private List<Transition> listaTransicoes;

        /// <summary>
        /// Construtor do leitor de automato
        /// </summary>
        /// <param name="listaEstados">Lista de estados do Autômato</param>
        /// <param name="listTransicoes">Lista de transições do Autômato</param>
        /// <param name="Alfabeto">Alfabeto do Autômato</param>
        public LeitorAutomato(List<Node> listaEstados, List<Transition> listaTransicoes, List<char> Alfabeto)
        {
            this.listaEstados = listaEstados;
            this.listaTransicoes = listaTransicoes;
        }


        /// <summary>
        /// Função que retorna se uma determinada cadeia é aceita ou não pelo automato
        /// </summary>
        /// <param name="Cadeia">A cadeia que deseja ser testada</param>
        /// <returns>Uma boolean indicando a cadeia se pertence ou não a linguagem do automato, a partir do Estado inicial.</returns>
        public Node GetEstado(string Cadeia)
        {
            //Pega o estado inicial
            Node estadoAtual = this.listaEstados.Find(p => p.Estado == Estado.InicialAceitacao || p.Estado == Estado.InicialNaoAceitacao);
            
            foreach (var item in Cadeia)
            {
                Transition transicao = listaTransicoes.Find(t => t.Element == item && t.From.Nome == estadoAtual.Nome);
                estadoAtual = transicao.To;
            }

            return estadoAtual;
         
        }


        /// <summary>
        /// Função que retorna se uma determinada cadeia é aceita ou não pelo automato
        /// </summary>
        /// <param name="Cadeia">A cadeia que deseja ser testada</param>
        /// <param name="Inicial">Por qual nó comeca a verificação</param>
        /// <returns>Uma boolean indicando a cadeia se pertence ou não a linguagem do automato</returns>
        public Node GetEstado(string Cadeia, Node Inicial)
        {
            Node estadoAtual = Inicial;
            foreach (var item in Cadeia)
            {
                Transition transicao = listaTransicoes.Find(t => t.Element == item && t.From.Nome == estadoAtual.Nome);
                estadoAtual = transicao.To;
            }

            return estadoAtual;
        }




    }
}
