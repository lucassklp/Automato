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
        /// 
        /// </summary>
        /// <param name="listaEstados"></param>
        /// <param name="listTransicoes"></param>
        /// <param name="Sequencia"></param>
        public LeitorAutomato(List<Node> listaEstados, List<Transition> listaTransicoes, List<char> Alfabeto)
        {

            //Validar se as transições estão completas

            int quantidadeTransicoes = listaEstados.Count * Alfabeto.Count;

            if (quantidadeTransicoes != listaTransicoes.Count)
                throw new Exception("Esse automato não é um AFD, pois não contém todos os elementos de transições para cada estado");

            this.listaEstados = listaEstados;
            this.listaTransicoes = listaTransicoes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool GetEstado(List<char> Sequencia)
        {
            //Pega o estado inicial
            Node estadoAtual = null;
            try
            {
                estadoAtual = this.listaEstados.Find(p => p.Estado == Estado.InicialAceitacao || p.Estado == Estado.InicialNaoAceitacao);
            }
            catch
            {
                throw new Exception("Estado inicial não encontrado");
            }

            foreach (var item in Sequencia)
            {
                Transition transicao = listaTransicoes.Find(t => t.Element == item && t.From.Nome == estadoAtual.Nome);
                estadoAtual = transicao.To;
                    
            }

            if (estadoAtual.Estado == Estado.Aceitacao || estadoAtual.Estado == Estado.InicialAceitacao)
                return true;
            else
                return false;
        }
    }
}
