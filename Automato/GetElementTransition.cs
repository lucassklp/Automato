﻿using System;
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
    public partial class GetElementTransition : Form
    {
        public char Element;
        public bool isCanceled = false;

        //Properties para impedir o close da janela
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }


        public GetElementTransition(List<char> Alphabet)
        {
            InitializeComponent();

            this.cbElementos.DataSource = Alphabet;
            this.cbElementos.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnAdicionarElemento_Click(object sender, EventArgs e)
        {
            this.Element = char.Parse(cbElementos.SelectedItem.ToString());
            this.Hide();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.isCanceled = true;
            this.Hide();
        }
    }
}
