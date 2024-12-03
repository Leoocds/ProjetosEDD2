using System;
using System.Linq;
using System.Windows.Forms;


namespace ProjAtendimento
{
    public partial class Form1 : Form
    {

        private Senhas senhas = new Senhas();
        private Guiches guiches = new Guiches();

        public Form1()
        {
            InitializeComponent();
            AtualizarQuantidadeGuiches();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void lsSenhas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            senhas.Gerar();
            AtualizarListaSenhas();
        }

        private void btnChamar_Click(object sender, EventArgs e)
        {
            int guicheId = int.Parse(txtGuiche.Text);
            Guiche guiche = guiches.ListaGuiches.FirstOrDefault(g => g.Id == guicheId);

            if (guiche != null && guiche.Chamar(senhas.FilaSenhas))
            {
                AtualizarListaAtendimentos(guiche);
                AtualizarListaSenhas();
            }
            else
            {
                MessageBox.Show("Nenhuma senha disponível ou guichê inválido!");
            }
        }
        g
        private void AtualizarListaSenhas()
        {
            lsSenhas.Items.Clear();
            foreach (Senha senha in senhas.FilaSenhas)
            {
                lsSenhas.Items.Add(senha.DadosParciais());
            }
        }

        private void AtualizarListaAtendimentos(Guiche guiche)
        {
            lsAtendimento.Items.Clear();
            foreach (Senha senha in guiche.Atendimentos)
            {
                lsAtendimento.Items.Add(senha.DadosCompletos());
            }
        }

        private void AtualizarQuantidadeGuiches()
        {
            lblQtdGuiches.Text = guiches.ListaGuiches.Count.ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            guiches.Adicionar(new Guiche(guiches.ListaGuiches.Count + 1));
            AtualizarQuantidadeGuiches();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnListarSenhas_Click(object sender, EventArgs e)
        {

        }
    }
}
