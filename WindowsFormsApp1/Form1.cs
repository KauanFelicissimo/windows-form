using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private List<Image> imagens = new List<Image>();
        private int indiceAtual = 0;
        private List<Image> fundos = new List<Image>();
        private int indiceFundo = 0;

        public Form1()
        {
            InitializeComponent();

            // Caminho da pasta Downloads
            string pastaDownloads = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "Downloads"
            );

            // ---------------- Imagens do PictureBox ----------------
            string[] arquivosImagens = { "imagem1.PNG", "imagem2.PNG", "imagem3.PNG" };
            foreach (var nome in arquivosImagens)
            {
                string caminho = Path.Combine(pastaDownloads, nome);
                if (File.Exists(caminho))
                {
                    using (var stream = new FileStream(caminho, FileMode.Open, FileAccess.Read))
                    {
                        imagens.Add(Image.FromStream(stream));
                    }
                }
            }

            // ---------------- Fundos do Form ----------------
            string[] arquivosFundos = { "fundo1.PNG", "fundo2.PNG", "fundo3.PNG" };
            foreach (var nome in arquivosFundos)
            {
                string caminho = Path.Combine(pastaDownloads, nome);

                // Verifica se o arquivo realmente existe
                if (File.Exists(caminho))
                {
                    try
                    {
                        using (var stream = new FileStream(caminho, FileMode.Open, FileAccess.Read))
                        {
                            fundos.Add(Image.FromStream(stream));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao carregar {nome}: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"Arquivo não encontrado: {caminho}");
                }
            }
        }

        private void setaDireita_Click(object sender, EventArgs e)
        {
            if (imagens.Count == 0) return;

            indiceAtual++;
            if (indiceAtual >= imagens.Count)
                indiceAtual = 0;

            imgJojo1.Image = imagens[indiceAtual];

            if (fundos.Count > 0)
            {
                indiceFundo++;
                if (indiceFundo >= fundos.Count)
                    indiceFundo = 0;

                this.BackgroundImage = fundos[indiceFundo];
            }
        }

        private void setaEsquerda_Click(object sender, EventArgs e)
        {
            if (imagens.Count == 0) return;

            indiceAtual--;
            if (indiceAtual < 0)
                indiceAtual = imagens.Count - 1;

            imgJojo1.Image = imagens[indiceAtual];

            if (fundos.Count > 0)
            {
                indiceFundo--;
                if (indiceFundo < 0)
                    indiceFundo = fundos.Count - 1;

                this.BackgroundImage = fundos[indiceFundo];
            }
        }
    }
}
