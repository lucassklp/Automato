using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class DialogGetTransitionElement : Form
    {
        private List<char> alfabeto;
        public char Element;
        public DialogGetTransitionElement(List<char> alfabeto)
        {
            this.alfabeto = alfabeto;
            InitializeComponent();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (txtElementTransition.Text.Length > 1)
                MessageBox.Show("Erro:", "Digite apenas um caractere", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (alfabeto.Contains(char.Parse(txtElementTransition.Text)) == null)
                MessageBox.Show("Erro:", "Caractere não pertence ao alfabeto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                this.Element = char.Parse(txtElementTransition.Text);
        }
    }
}
