namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.imgJojo1 = new System.Windows.Forms.PictureBox();
            this.setaDireita = new System.Windows.Forms.PictureBox();
            this.setaEsquerda = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgJojo1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.setaDireita)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.setaEsquerda)).BeginInit();
            this.SuspendLayout();
            // 
            // imgJojo1
            // 
            this.imgJojo1.Location = new System.Drawing.Point(304, 107);
            this.imgJojo1.Name = "imgJojo1";
            this.imgJojo1.Size = new System.Drawing.Size(179, 198);
            this.imgJojo1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgJojo1.TabIndex = 0;
            this.imgJojo1.TabStop = false;
            // 
            // setaDireita
            // 
            this.setaDireita.Image = ((System.Drawing.Image)(resources.GetObject("setaDireita.Image")));
            this.setaDireita.Location = new System.Drawing.Point(761, 187);
            this.setaDireita.Name = "setaDireita";
            this.setaDireita.Size = new System.Drawing.Size(27, 50);
            this.setaDireita.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.setaDireita.TabIndex = 1;
            this.setaDireita.TabStop = false;
            this.setaDireita.Click += new System.EventHandler(this.setaDireita_Click);
            // 
            // setaEsquerda
            // 
            this.setaEsquerda.Image = ((System.Drawing.Image)(resources.GetObject("setaEsquerda.Image")));
            this.setaEsquerda.Location = new System.Drawing.Point(12, 187);
            this.setaEsquerda.Name = "setaEsquerda";
            this.setaEsquerda.Size = new System.Drawing.Size(27, 50);
            this.setaEsquerda.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.setaEsquerda.TabIndex = 2;
            this.setaEsquerda.TabStop = false;
            this.setaEsquerda.Click += new System.EventHandler(this.setaEsquerda_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.setaEsquerda);
            this.Controls.Add(this.setaDireita);
            this.Controls.Add(this.imgJojo1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.imgJojo1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.setaDireita)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.setaEsquerda)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox imgJojo1;
        private System.Windows.Forms.PictureBox setaDireita;
        private System.Windows.Forms.PictureBox setaEsquerda;
    }
}

