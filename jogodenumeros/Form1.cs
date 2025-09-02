using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace jogodenumeros
{
    public partial class frmJogoDeNumeros : Form
    {

        int randomNumber;
        int numeroTentativas = 10;
        int palpitedoJogador;
        bool jogoGanho = false;
        string dica;
        public frmJogoDeNumeros()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmJogoDeNumeros_Load(object sender, EventArgs e)
        {
            Random random = new Random();
            randomNumber = random.Next(1, 101); //Número aleatório entre 1 e 100
        }

        private void btnTentativas_Click(object sender, EventArgs e)
        {
            //Verifica se o jogo já foi ganho

            if (jogoGanho)
            {
                txtResultado.Text = "Você já acertou o número, reinicie o jogo para jogar novamente";
                return;

            }
            //Verifica se o número de tentativas chegou a 0
            if (numeroTentativas == 0)
            {
                txtResultado.Text = "você não tem mais tentativas. O jogo acabou";
                return;
            }
            //Validação do valor do palpite (entre 1 a 100)
            if (!int.TryParse(txtNumeroInserido.Text, out palpitedoJogador) || palpitedoJogador < 1 || palpitedoJogador > 100)
            {

                txtResultado.Text = "Por favor insira um Núremo entre 1 e 100";
                return;
            }

            numeroTentativas--;
            lblNumeroTentativas.Text = numeroTentativas.ToString();

            if (palpitedoJogador == randomNumber)
            {
                jogoGanho = true;
                dica = "Parabéns, você acertou!!!";
            }
            else if (palpitedoJogador < randomNumber)
            {
                dica = "O número que você digitou é menor, digite um número maior";
            }
            else
            {
                dica = "O número que você digitou é maior, digie um número menor";
            }

            txtResultado.Text = dica;
        }
    }      
}

