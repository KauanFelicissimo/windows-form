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

        public string AdminId { get; set; } // ID do usuário que vamos editar
    public string AdminName
    {
        get => txtName.Text;
        set => txtName.Text = value;
    }
    public string AdminEmail
    {
        get => txtEmail.Text;
        set => txtEmail.Text = value;
    }
    public string AdminPassword
    {
        get => txtPassword.Text;
        set => txtPassword.Text = value;
    }
        bool menuExpand = true;
        MySqlConnection Aurora;
        string data_source = "datasource = localhost; username=root; password=; database=aurora-platform";
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
                    MessageBox.Show("Todos os campos devem ser preenchidos.",
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

                if (string.IsNullOrEmpty(AdminId))
                {
                    // CADASTRO
                    cmd.CommandText = "INSERT INTO users(name, username, email, `password`, role) " +
                                      "VALUES(@name, @username, @email, @password, 'admin')";
                }
                else
                {
                    // UPDATE
                    cmd.CommandText = "UPDATE users SET name=@name, username=@username, email=@email, `password`=@password " +
                                      "WHERE id=@id";
                    cmd.Parameters.AddWithValue("@id", AdminId);
                }

                cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@username", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@password", hashed);

                cmd.ExecuteNonQuery();

                string msg = string.IsNullOrEmpty(AdminId) ? "Admin cadastrado com sucesso!" : "Admin atualizado com sucesso!";
                MessageBox.Show(msg, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK; // para atualizar a ListView no form anterior
                this.Close();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Ocorreu: " + ex.Number + " - " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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