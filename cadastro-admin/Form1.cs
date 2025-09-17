using BCrypt.Net; 
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

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

        private bool EmailValido(string email)
        {
            string padrao = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, padrao, RegexOptions.IgnoreCase);
        }


        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                Aurora = new MySqlConnection(data_source);
                Aurora.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = Aurora;

                if (string.IsNullOrEmpty(AdminId))
                {
                    // CADASTRO → todos os campos obrigatórios
                    if (string.IsNullOrEmpty(txtEmail.Text.Trim()) ||
                        string.IsNullOrEmpty(txtName.Text.Trim()) ||
                        string.IsNullOrEmpty(txtPassword.Text.Trim()))
                    {
                        MessageBox.Show("Todos os campos devem ser preenchidos para cadastrar.",
                                        "Validação",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                        return;
                    }

                    if (!EmailValido(txtEmail.Text.Trim()))
                    {
                        MessageBox.Show("Digite um email válido!",
                                        "Validação",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                        return;
                    }

                    string hashed = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text.Trim(), workFactor: 10);
                    if (hashed.StartsWith("$2a$"))
                        hashed = "$2y$" + hashed.Substring(4);

                    cmd.CommandText = "INSERT INTO users(name, username, email, `password`, role) " +
                                      "VALUES(@name, @username, @email, @password, 'admin')";
                    cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@username", txtName.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", hashed);

                }
                else
                {
                    // UPDATE → só atualiza os campos preenchidos
                    var updates = new List<string>();

                    if (!string.IsNullOrEmpty(txtName.Text.Trim()))
                    {
                        updates.Add("name=@name, username=@username");
                        cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("@username", txtName.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtEmail.Text.Trim()))
                    {
                        updates.Add("email=@email");
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtPassword.Text.Trim()))
                    {
                        string hashed = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text.Trim(), workFactor: 10);
                        if (hashed.StartsWith("$2a$"))
                            hashed = "$2y$" + hashed.Substring(4);

                        updates.Add("`password`=@password");
                        cmd.Parameters.AddWithValue("@password", hashed);
                    }

                    if (updates.Count == 0)
                    {
                        MessageBox.Show("Nenhum campo preenchido para atualizar.",
                                        "Aviso",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                        return;
                    }

                    cmd.CommandText = "UPDATE users SET " + string.Join(", ", updates) + " WHERE id=@id";
                    cmd.Parameters.AddWithValue("@id", AdminId);
                }

                cmd.ExecuteNonQuery();

                string msg = string.IsNullOrEmpty(AdminId) ? "Admin cadastrado com sucesso!" : "Admin atualizado com sucesso!";
                MessageBox.Show(msg, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Admin tela = new Admin();
                // quando a tela Admin for fechada, mostra novamente o cadastro ou fecha tudo
                tela.FormClosed += (s, args) => this.Show();

                this.Hide(); // esconde o cadastro
                tela.Show();
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
                    Aurora.Close();
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