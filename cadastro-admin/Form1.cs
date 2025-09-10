using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;
using BCrypt.Net; 

namespace cadastro_admin
{
    public partial class frmCadastro : Form
    {
        bool menuExpand = true;
        MySqlConnection Aurora;
        string data_source = "datasource = localhost; username=root; password=; database=aurora-project";
        public frmCadastro()
        {
           

            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtEmail.Text.Trim()) ||
                    string.IsNullOrEmpty(txtName.Text.Trim()) ||
                    string.IsNullOrEmpty(txtPassword.Text.Trim()))
                {
                    MessageBox.Show("Para cadastrar um novo usuário todos os campos devem ser preenchidos.",
                                    "Validação",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                string hashed = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text.Trim(), workFactor: 10);

                // Ajustar para $2y$ se vier $2a$ (compatibilidade com Laravel)
                if (hashed.StartsWith("$2a$"))
                {
                    hashed = "$2y$" + hashed.Substring(4);
                }

                Aurora = new MySqlConnection(data_source);
                Aurora.Open();

                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = Aurora,
                };

                cmd.Prepare();
                cmd.CommandText = "INSERT INTO users(name, username, email, `password`, role) " +
                  "VALUES(@name, @username, @email, @password, 'admin')";


                cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@username", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@password", hashed);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Admin salvo com Sucesso: ",
                                "Parabens",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                }

                catch (MySqlException ex)
                {
                MessageBox.Show("Ocorreu: " + ex.Number + "ocorreu: "+ ex.Message,
                                "Erro",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                }

                catch (Exception ex)
                {
                MessageBox.Show("Ocorreu: " + ex.Message,
                                "Erro", 
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                
                }

                finally 
                {

                    if (Aurora != null && Aurora.State == ConnectionState.Open)
                    {
                       Aurora.Close();
                    }


               
                




        }
    }
       

        private void menu_Tick(object sender, EventArgs e)
        {
            if (menuExpand == false)
            {
                miniSideBar.Width += 10;
                if (miniSideBar.Width >= 112)
                {
                    menu.Stop();
                    menuExpand = true;
                }
            }
            else
            {
                miniSideBar.Width -= 10;
                if (miniSideBar.Width <= 0)
                {
                    menu.Stop();
                    menuExpand = false;
                }
            }
        }

        private void pbMenu_Click(object sender, EventArgs e)
        {
            menu.Start();
        }

        private void btnAdmins_Click(object sender, EventArgs e)
        {
            Admin tela = new Admin();

            // Quando a nova tela for fechada, fecha também a atual
            tela.FormClosed += (s, args) => this.Close();

            this.Hide(); // só esconde a atual
            tela.Show();
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            frmCadastro tela = new frmCadastro();

            // Quando a nova tela for fechada, fecha também a atual
            tela.FormClosed += (s, args) => this.Close();

            this.Hide(); // só esconde a atual
            tela.Show();
        }
    }
}