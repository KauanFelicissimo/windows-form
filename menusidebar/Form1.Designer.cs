namespace menusidebar
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.menu = new System.Windows.Forms.PictureBox();
            this.menuContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlPerfil = new System.Windows.Forms.Panel();
            this.btnPerfil = new System.Windows.Forms.Button();
            this.menuTransition = new System.Windows.Forms.Timer(this.components);
            this.sideBarTransition = new System.Windows.Forms.Timer(this.components);
            this.pnlHome = new System.Windows.Forms.Panel();
            this.btnHome = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.menu)).BeginInit();
            this.menuContainer.SuspendLayout();
            this.pnlPerfil.SuspendLayout();
            this.pnlHome.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.menu);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 43);
            this.panel1.TabIndex = 0;
            // 
            // menu
            // 
            this.menu.Image = ((System.Drawing.Image)(resources.GetObject("menu.Image")));
            this.menu.Location = new System.Drawing.Point(3, 3);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(34, 32);
            this.menu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.menu.TabIndex = 1;
            this.menu.TabStop = false;
            this.menu.Click += new System.EventHandler(this.menu_Click);
            // 
            // menuContainer
            // 
            this.menuContainer.BackColor = System.Drawing.Color.White;
            this.menuContainer.Controls.Add(this.pnlPerfil);
            this.menuContainer.Controls.Add(this.pnlHome);
            this.menuContainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuContainer.Location = new System.Drawing.Point(0, 43);
            this.menuContainer.Name = "menuContainer";
            this.menuContainer.Size = new System.Drawing.Size(183, 407);
            this.menuContainer.TabIndex = 1;
            this.menuContainer.Paint += new System.Windows.Forms.PaintEventHandler(this.menuContainer_Paint);
            // 
            // pnlPerfil
            // 
            this.pnlPerfil.Controls.Add(this.btnPerfil);
            this.pnlPerfil.Location = new System.Drawing.Point(3, 3);
            this.pnlPerfil.Name = "pnlPerfil";
            this.pnlPerfil.Size = new System.Drawing.Size(180, 39);
            this.pnlPerfil.TabIndex = 3;
            // 
            // btnPerfil
            // 
            this.btnPerfil.BackColor = System.Drawing.Color.White;
            this.btnPerfil.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnPerfil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPerfil.Location = new System.Drawing.Point(30, 3);
            this.btnPerfil.Name = "btnPerfil";
            this.btnPerfil.Size = new System.Drawing.Size(98, 31);
            this.btnPerfil.TabIndex = 2;
            this.btnPerfil.Text = "Perfil";
            this.btnPerfil.UseVisualStyleBackColor = false;
            // 
            // menuTransition
            // 
            this.menuTransition.Interval = 10;
            this.menuTransition.Tick += new System.EventHandler(this.menuTransition_Tick);
            // 
            // pnlHome
            // 
            this.pnlHome.Controls.Add(this.btnHome);
            this.pnlHome.Location = new System.Drawing.Point(3, 48);
            this.pnlHome.Name = "pnlHome";
            this.pnlHome.Size = new System.Drawing.Size(180, 35);
            this.pnlHome.TabIndex = 2;
            // 
            // btnHome
            // 
            this.btnHome.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.Location = new System.Drawing.Point(30, 0);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(98, 31);
            this.btnHome.TabIndex = 0;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuContainer);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.menu)).EndInit();
            this.menuContainer.ResumeLayout(false);
            this.pnlPerfil.ResumeLayout(false);
            this.pnlHome.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox menu;
        private System.Windows.Forms.FlowLayoutPanel menuContainer;
        private System.Windows.Forms.Panel pnlPerfil;
        private System.Windows.Forms.Button btnPerfil;
        private System.Windows.Forms.Timer menuTransition;
        private System.Windows.Forms.Timer sideBarTransition;
        private System.Windows.Forms.Panel pnlHome;
        private System.Windows.Forms.Button btnHome;
    }
}

