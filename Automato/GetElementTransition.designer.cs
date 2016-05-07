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
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbElementos
            // 
            this.cbElementos.FormattingEnabled = true;
            this.cbElementos.Location = new System.Drawing.Point(83, 104);
            this.cbElementos.Name = "cbElementos";
            this.cbElementos.Size = new System.Drawing.Size(121, 21);
            this.cbElementos.TabIndex = 7;
            // 
            // btnAdicionarElemento
            // 
            this.btnAdicionarElemento.Location = new System.Drawing.Point(12, 156);
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
            this.lblStatus.Location = new System.Drawing.Point(64, 66);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(158, 13);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Informe o elemento de transição";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(163, 156);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(109, 23);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // GetElementTransition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.cbElementos);
            this.Controls.Add(this.btnAdicionarElemento);
            this.Controls.Add(this.lblStatus);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GetElementTransition";
            this.Text = "GetElementTransition";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbElementos;
        private System.Windows.Forms.Button btnAdicionarElemento;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnCancelar;
    }
}