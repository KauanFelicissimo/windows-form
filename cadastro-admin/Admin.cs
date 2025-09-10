using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace cadastro_admin
{
    public partial class Admin : Form
    {
        bool menuExpand = true;
        MySqlConnection Aurora;
        string data_source = "datasource=localhost;username=root;password=;database=aurora-project";

        public Admin()
        {
            InitializeComponent();
            this.Load += Admin_Load; // garante que o Form_Load será chamado
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            // Configura ListView
            lstAdmins.View = View.Details;
            lstAdmins.FullRowSelect = true;
            lstAdmins.GridLines = true;
            lstAdmins.Columns.Clear();

            // Adiciona colunas
            lstAdmins.Columns.Add("Name", 200, HorizontalAlignment.Left);
            lstAdmins.Columns.Add("Email", 200, HorizontalAlignment.Left);
            lstAdmins.Columns.Add("Role", 150, HorizontalAlignment.Left);

            // Carrega dados do banco
            carregar_admins();
        }

        private void carregar_admins()
        {
            string query = "SELECT name, email, role FROM users WHERE role = 'admin'";
            carregar_clientes_com_query(query);
        }

        private void carregar_clientes_com_query(string query)
        {
            try
            {
                Aurora = new MySqlConnection(data_source);
                Aurora.Open();

                MySqlCommand cmd = new MySqlCommand(query, Aurora);

                if (query.Contains("@q"))
                    cmd.Parameters.AddWithValue("@q", "%" + txtSearch.Text + "%");

                MySqlDataReader reader = cmd.ExecuteReader();
                lstAdmins.Items.Clear();

                while (reader.Read())
                {
                    string[] row = {
                        reader.GetString("name"),
                        reader.GetString("email"),
                        reader.GetString("role")
                    };

                    lstAdmins.Items.Add(new ListViewItem(row));
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erro " + ex.Number + " ocorreu: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (Aurora != null && Aurora.State == System.Data.ConnectionState.Open)
                    Aurora.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Corrigido: seleciona todas as colunas necessárias
            string query = "SELECT name, email, role FROM users " +
                           "WHERE role = 'admin' AND (name LIKE @q) " +
                           "ORDER BY name ASC";

            carregar_clientes_com_query(query);
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
