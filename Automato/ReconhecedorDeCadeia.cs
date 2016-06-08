using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Automato
{
    public partial class ReconhecedorDeCadeia : Form
    {

        private List<Node> listEstados;
        private List<Transition> listTransition;
        private List<char> Alfabeto;

        public ReconhecedorDeCadeia(List<Node> listEstados, List<Transition> listTransition, List<char> Alfabeto)
        {
            InitializeComponent();

            this.listEstados = listEstados;
            this.listTransition = listTransition;
            this.Alfabeto = Alfabeto;
        }

        private void btnVerificar_Click(object sender, EventArgs e)
        {

            var match = txtCadeia.Text.IndexOfAny(this.Alfabeto.ToArray()) != 1;

            if (match)
            {
                try
                {
                    LeitorAutomato leitorAutomato = new LeitorAutomato(listEstados, listTransition, Alfabeto);
                    Node resultado = leitorAutomato.GetEstado(txtCadeia.Text);
                    string strResultado = (resultado.Estado == Estado.Aceitacao || resultado.Estado == Estado.InicialAceitacao ? "Estado de Aceitacao" : "Estado de rejeição");
                    MessageBox.Show("Estado resultante: " + resultado.Nome + "\n " + strResultado);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Um erro aconteceu: " + ex.Message);
                }
            }
            else
                MessageBox.Show("A cadeia de entrada contém erros. Verifique se os elementos pertencem ao alfabeto.");
        }
    }
}
