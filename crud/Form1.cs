using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Protobuf;
using MySql.Data.MySqlClient;


namespace crud
{
    public partial class frmCadastroDeClientes : Form
    {
        //Conexão com o banco de ados do MySql
        MySqlConnection Conexao;
        string data_source = "datasource = localhost; username=root; password=; database=db_cadastro";

        public frmCadastroDeClientes()
        {
            InitializeComponent();

            //Configuração incial de ListView para exibição dos dados dos clientes
            lstCliente.View = View.Details;         //Define a visualização em "detalhes"
            lstCliente.LabelEdit = true;            //Permite editar os títulos das colunas
            lstCliente.AllowColumnReorder = true;   //Permite reordenar as colunas
            lstCliente.FullRowSelect = true;        //Selecione a linha inteira ao clicar
            lstCliente.GridLines = true;            //Exibe as linhas de grade no ListView

            //Definindo as colunas do ListView
            lstCliente.Columns.Add("Codigo", 100, HorizontalAlignment.Left);        //Coluna de Código
            lstCliente.Columns.Add("Nome Completo", 200, HorizontalAlignment.Left); //Coluna de Nome Completo
            lstCliente.Columns.Add("Nome Social", 200, HorizontalAlignment.Left);   //Coluna de Nome Social
            lstCliente.Columns.Add("Email", 200, HorizontalAlignment.Left);         //Coluna de Email
            lstCliente.Columns.Add("CPF", 200, HorizontalAlignment.Left);            //Coluna de CPF

            //Carrega os dados dos clientes na inferface
            carregar_cliente();

        }

        private void carregar_clientes_com_query(string query)
        {
            try
            {

                //Cria a conexão com o banco de dados
                Conexao = new MySqlConnection(data_source);
                Conexao.Open();

                //Executa a consulta SQL fornecida
                MySqlCommand cmd = new MySqlCommand(query, Conexao);

                //Se a consulta contém o parâmetro @q , adiciona o valor da caixa de pesquisa
                if (query.Contains("@q"))
                {
                    cmd.Parameters.AddWithValue("@q", "%" + txtBuscar.Text + "%");
                }

                //Executa o comando e obtém os resultados
                MySqlDataReader reader = cmd.ExecuteReader();

                //Limpa os itens existentes no ListView antes de adicionar novos
                lstCliente.Items.Clear();

                //Preenche a ListView com os dados do cliente
                while (reader.Read())
                {
                    //Cria uma linha para cada cliente com os dados restaurados da consulta
                    string[] row =
                    {
                        Convert.ToString(reader.GetInt32(0)), //Codigo
                        reader.GetString(1),                  //Nome Completo
                        reader.GetString(2),                  //Nome Social
                        reader.GetString(3),                  //Email
                        reader.GetString(4),                  //CPF

                    };

                    //Adiciona a linha ao ListVIew
                    lstCliente.Items.Add(new ListViewItem(row));
                }
            }

            catch (MySqlException ex)
            {
                //trata erros relacionados ao MYSQL, qualquer erro envolvendo o banco de dados será informado, importante na hora de testar o sistema pois ele avisa exatamente onde está o erro
                MessageBox.Show("Erro" + ex.Number + " ocorreu: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            catch (Exception ex)
            {
                //Trata outros tipos de erro,  qualquer erro envolvendo o back-end será informado, importante na hora de testar o sistema pois ele avisa exatamente onde está o erro
                MessageBox.Show("Ocorreu: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                //Garante que a conexão com o bnaco será fechada, mesmo se ocorrer um erro, deve sempre ser feito para a proteção dos dados
                if (Conexao != null && Conexao.State == ConnectionState.Open)
                {
                    Conexao.Close();
                }
            }
        }


        //Método para carregar todos os clientes no ListView (usando uma consulta sem paramêtros
        private void carregar_cliente()
        {
            string query = "SELECT * FROM dadosdocliente ORDER BY idcliente DESC";
            carregar_clientes_com_query(query);
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                //Validação de campos obrigátorios
                if (string.IsNullOrEmpty(txtNomeCompleto.Text.Trim()) ||
                    string.IsNullOrEmpty(txtEmail.Text.Trim()) ||
                    string.IsNullOrEmpty(txtCPF.Text.Trim()))
                {
                    MessageBox.Show("Todos os Campos devem ser preenchidos.",
                                    "Validação",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return; //Impede o progressimento se algum campo estiver ativo
                }

                //Validação do CPF
                string cpf = txtCPF.Text.Trim();

                if (!isValidCPFLength(cpf))
                {
                    MessageBox.Show("CPF inválido. Certifique-se de que o CPF tenha 11 dígitos numéricos.",
                                    "Validação",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return; //Impede o prosseguimento se o CPF for inválido 
                }

                //Cra a conexão com o banco de dados , obrigátorio abrir a conexao
                Conexao = new MySqlConnection(data_source);
                Conexao.Open();

                //Comando SQL para inserir um novo cliente no banco de dados
                MySqlCommand cmd = new MySqlCommand
                {
                    Connection = Conexao
                };

                 
                //preparamento dos dados que serão inseridos no banco de dados

                cmd.Prepare();
                cmd.CommandText = "INSERT INTO dadosdocliente(nomecompleto, nomesocial, email, cpf) " +
                                  "VALUES(@nomecompleto, @nomesocial, @email, @cpf)";

                //Adiciona os parâmetros com os dados do formulário
                cmd.Parameters.AddWithValue("@nomecompleto", txtNomeCompleto.Text.Trim());
                cmd.Parameters.AddWithValue("@nomesocial", txtNomeSocial.Text.Trim());
                cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@cpf", cpf);

                //Executa o comando de inserção no banco, este código permite que as informações sejam inseridas no banco de dados, ele é extremamente importante sem ele os dados não seram inserido corretamente
                cmd.ExecuteNonQuery();

                //Mensagem de sucesso
                MessageBox.Show("Contato inserido com Sucesso: ",
                                "Sucesso",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                // Limpa os campos após o sucesso
                txtNomeCompleto.Text = string.Empty;
                txtNomeSocial.Text = "";
                txtEmail.Text = "";
                txtCPF.Text = "";

                //Recarrega os clientes no ListView
                carregar_cliente();

                //Muda para a aba de pesquisa
                tbControl.SelectedIndex = 1;

            }

            catch (MySqlException ex)
            {
                //trata erros relacionados ao MYSQL, qualquer erro envolvendo o banco de dados será informado, importante na hora de testar o sistema pois ele avisa exatamente onde está o erro
                MessageBox.Show("Erro" + ex.Number + " ocorreu: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            catch (Exception ex)
            {
                //Trata outros tipos de erro,  qualquer erro envolvendo o back-end será informado, importante na hora de testar o sistema pois ele avisa exatamente onde está o erro
                MessageBox.Show("Ocorreu: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally
            {
                //Garante que a conexão com o bnaco será fechada, mesmo se ocorrer um erro, deve sempre ser feito para a proteção dos dados
                if (Conexao != null && Conexao.State == ConnectionState.Open)
                {
                    Conexao.Close();
                }
            }
        }
        private bool isValidCPFLength(string cpf)
        {

            //Remove todos os caracteres não numériocos
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            //Verifica se o CPF tem exatamente 11 dígitos
            return cpf.Length == 11;
        }

        private void frmCadastroDeClientes_Load(object sender, EventArgs e)
        {

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string query = "SELECT * FROM dadosdocliente WHERE nomecompleto LIKE @q OR nomesocial LIKE @q ORDER BY idcliente DESC";
            carregar_clientes_com_query(query);
        }
    }
     
}



