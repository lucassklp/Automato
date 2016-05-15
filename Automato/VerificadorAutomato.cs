using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automato
{
    class VerificadorAutomato
    {
        private List<Node> listEstados;
        private List<Transition> listTransicoes;
        private List<char> Alfabeto;


        public VerificadorAutomato(List<Node> listEstados, List<Transition> listTransicoes, List<char> Alfabeto)
        {
            this.listEstados = listEstados;
            this.listTransicoes = listTransicoes;
            this.Alfabeto = Alfabeto;
        }


        /// <summary>
        /// Função usada para validar se o autômato é um AFD válido
        /// </summary>
        /// <returns>Uma boolean que indica se é um AFD válido ou não.</returns>
        public bool Validar()
        {
            if (this.listEstados == null || this.listTransicoes == null || this.Alfabeto == null)
                return false;
            else if (this.listEstados.Count <= 0 || this.listTransicoes.Count <= 0 || this.Alfabeto.Count <= 0)
                return false;


            //Verifica se há um estado inicial
            if (this.listEstados.FindAll(p => p.Estado == Estado.InicialAceitacao || p.Estado == Estado.InicialNaoAceitacao).Count == 0)
                return false;

            //Verifica se para cada estado há somente e apenas 
            //Uma transição para cada item do alfabeto
            foreach(char elemento in this.Alfabeto)
            {
                foreach(Node estado in this.listEstados)
                {
                    if (this.listTransicoes.FindAll(p => p.From.Nome == estado.Nome && p.Element == elemento).Count != 1)
                        return false;
                }
            }


            return true;




        }


    }
}
