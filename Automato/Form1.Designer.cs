using System;

namespace Automato
{
    partial class Form1
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
            this.txtAlfabeto = new System.Windows.Forms.TextBox();
            this.btnDefinir = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtAlfabeto
            // 
            this.txtAlfabeto.Location = new System.Drawing.Point(75, 107);
            this.txtAlfabeto.Name = "txtAlfabeto";
            this.txtAlfabeto.Size = new System.Drawing.Size(119, 20);
            this.txtAlfabeto.TabIndex = 0;
            // 
            // btnDefinir
            // 
            this.btnDefinir.Location = new System.Drawing.Point(75, 157);
            this.btnDefinir.Name = "btnDefinir";
            this.btnDefinir.Size = new System.Drawing.Size(119, 23);
            this.btnDefinir.TabIndex = 1;
            this.btnDefinir.Text = "Definir Alfabeto";
            this.btnDefinir.UseVisualStyleBackColor = true;
            this.btnDefinir.Click += new System.EventHandler(this.btnDefinir_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(50, 73);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(185, 13);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Entre com o alfabeto para o automato";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnDefinir);
            this.Controls.Add(this.txtAlfabeto);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        
        }

        #endregion

        private System.Windows.Forms.TextBox txtAlfabeto;
        private System.Windows.Forms.Button btnDefinir;
        private System.Windows.Forms.Label lblStatus;
    }
}

