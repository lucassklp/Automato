namespace Automato
{
    partial class GetElementTransition
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbElementos = new System.Windows.Forms.ComboBox();
            this.btnAdicionarElemento = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbElementos
            // 
            this.cbElementos.FormattingEnabled = true;
            this.cbElementos.Location = new System.Drawing.Point(75, 111);
            this.cbElementos.Name = "cbElementos";
            this.cbElementos.Size = new System.Drawing.Size(121, 21);
            this.cbElementos.TabIndex = 7;
            // 
            // btnAdicionarElemento
            // 
            this.btnAdicionarElemento.Location = new System.Drawing.Point(75, 161);
            this.btnAdicionarElemento.Name = "btnAdicionarElemento";
            this.btnAdicionarElemento.Size = new System.Drawing.Size(119, 23);
            this.btnAdicionarElemento.TabIndex = 6;
            this.btnAdicionarElemento.Text = "Adicionar Elemento";
            this.btnAdicionarElemento.UseVisualStyleBackColor = true;
            this.btnAdicionarElemento.Click += new System.EventHandler(this.btnAdicionarElemento_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(50, 77);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(185, 13);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Entre com o alfabeto para o automato";
            // 
            // GetElementTransition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.cbElementos);
            this.Controls.Add(this.btnAdicionarElemento);
            this.Controls.Add(this.lblStatus);
            this.Name = "GetElementTransition";
            this.Text = "GetElementTransition";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbElementos;
        private System.Windows.Forms.Button btnAdicionarElemento;
        private System.Windows.Forms.Label lblStatus;
    }
}