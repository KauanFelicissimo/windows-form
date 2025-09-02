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
    }
     
}



