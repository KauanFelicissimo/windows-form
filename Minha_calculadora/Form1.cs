using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minha_calculadora
{
    public partial class Form1 : Form
    {

        private double valor1;
        private double valor2;
        private double resultado;
        private int conta;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnNumberZero_Click(object sender, EventArgs e)
        {
            txtResultado.Text = txtResultado.Text + "0";
        }

        private void btnNumberOne_Click(object sender, EventArgs e)
        {
            txtResultado.Text = txtResultado.Text + "1";
        }

        private void btnNumberTwo_Click(object sender, EventArgs e)
        {
            txtResultado.Text = txtResultado.Text + "2";
        }

        private void btnNumberThree_Click(object sender, EventArgs e)
        {
            txtResultado.Text = txtResultado.Text + "3";
        }

        private void btnNumberFour_Click(object sender, EventArgs e)
        {
            txtResultado.Text = txtResultado.Text + "4";
        }

        private void btnNumberFive_Click(object sender, EventArgs e)
        {
            txtResultado.Text = txtResultado.Text + "5";
        }

        private void btnNumberSix_Click(object sender, EventArgs e)
        {
            txtResultado.Text = txtResultado.Text + "6";
        }

        private void btnNumberSeven_Click(object sender, EventArgs e)
        {
            txtResultado.Text = txtResultado.Text + "7";
        }

        private void btnNumberEight_Click(object sender, EventArgs e)
        {
            txtResultado.Text = txtResultado.Text + "8";
        }

        private void btnNumberNine_Click(object sender, EventArgs e)
        {
            txtResultado.Text = txtResultado.Text + "9";
        }

        private void btnSomar_Click(object sender, EventArgs e)
        {
            conta = 1;

            valor1 = Convert.ToDouble(txtResultado.Text);
            txtResultado.Text = ""; 
        }

        private void btnSubtrair_Click(object sender, EventArgs e)
        {
            conta = 2;

            valor1 = Convert.ToDouble(txtResultado.Text);
            txtResultado.Text = "";
        }

        private void btnMultiplicar_Click(object sender, EventArgs e)
        {
            conta = 3;

            valor1 = Convert.ToDouble(txtResultado.Text);
            txtResultado.Text = "";
        }

        private void btn_Click(object sender, EventArgs e)
        {
            conta = 4;

            valor1 = Convert.ToDouble(txtResultado.Text);
            txtResultado.Text = "";
        }

        private void btnResultado_Click(object sender, EventArgs e)
        {

            valor2 = Convert.ToDouble(txtResultado.Text);
            resultado = valor1 + valor2;

            switch (conta)
            {
                case 1:
                    resultado = valor1 + valor2;
                    break;
                case 2:
                    resultado = valor1 - valor2;
                    break;
                case 3:
                    resultado = valor1 * valor2;
                    break;
                case 4:
                    resultado = valor1 / valor2;
                    break;
            }

            txtResultado.Text = resultado.ToString();
        }

        private void txtResultado_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnErase_Click(object sender, EventArgs e)
        {
            txtResultado.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
