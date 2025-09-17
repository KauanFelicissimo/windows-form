using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace cadastro_admin
{
    public partial class Admin : Form
    {
        bool menuExpand = true;
        MySqlConnection Aurora;
        string data_source = "datasource=localhost;username=root;password=;database=aurora-platform";

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

            lstAdmins.MouseDoubleClick += lstAdmins_DoubleClick;
            lstAdmins.LabelEdit = true;



            // Adiciona colunas
            lstAdmins.Columns.Clear();
            lstAdmins.Columns.Add("ID", 200, HorizontalAlignment.Left); // primeira coluna editável
            lstAdmins.Columns.Add("Name", 200, HorizontalAlignment.Left);
            lstAdmins.Columns.Add("Email", 500, HorizontalAlignment.Left);
            lstAdmins.Columns.Add("Role", 150, HorizontalAlignment.Left);

            // Carrega dados do banco
            carregar_admins();
        }

        private void carregar_admins()
        {
            string query = "SELECT id ,name, email, role FROM users WHERE role = 'admin'";
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
                        reader["id"].ToString(),
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
            string query = "SELECT id, name, email, role FROM users " +
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

        private void lstAdmins_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



    

private void lstAdmins_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Label))
                return;

            string id = lstAdmins.Items[e.Item].SubItems[0].Text; // pega o ID do admin
            string novoNome = e.Label;

            // Pergunta ao usuário se deseja realmente editar
            DialogResult resultado = MessageBox.Show(
                $"Deseja alterar o nome do admin para '{novoNome}'?",
                "Confirmar edição",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                // Atualiza no banco
                string query = "UPDATE users SET name=@name WHERE id=@id";
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(data_source))
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@name", novoNome);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Nome atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    carregar_admins(); // recarrega a lista
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao atualizar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Se o usuário cancelar, restaura o valor antigo
                e.CancelEdit = true;
            }
        }

        private void lstAdmins_DoubleClick(object sender, EventArgs e)
        {
            if (lstAdmins.SelectedItems.Count == 0)
                return;

            ListViewItem item = lstAdmins.SelectedItems[0];

            string id = item.SubItems[0].Text;
            string name = item.SubItems[1].Text;
            string email = item.SubItems[2].Text;

            // Cria e preenche o Form1
            frmCadastro editForm = new frmCadastro();
            editForm.AdminId = id;
            editForm.AdminName = name;
            editForm.AdminEmail = email;

            // Mostra o form como modal
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                // Quando o usuário clicar em salvar, atualiza a ListView
                carregar_admins();
            }
        }
    }
}
