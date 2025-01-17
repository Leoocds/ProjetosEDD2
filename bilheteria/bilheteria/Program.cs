using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProjetoBilheteria
{
    public class MainForm : Form
    {
        private const int NUM_FILEIRAS = 15;
        private const int POLTRONAS_POR_FILEIRA = 40;
        private Button[,] poltronas = new Button[NUM_FILEIRAS, POLTRONAS_POR_FILEIRA];
        private int lugaresOcupados = 0;
        private double faturamentoTotal = 0.0;

        private Label lblOcupados;
        private Label lblFaturamento;
        private Button btnResetar;
        private Button btnReservar;
        private TextBox txtFileira;
        private TextBox txtPoltrona;

        public MainForm()
        {
            this.Text = "Sistema de Bilheteria";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.AutoScroll = true;

            CriarInterface();
            CriarMapa();
        }

        private void CriarInterface()
        {
            lblOcupados = new Label
            {
                Text = "Lugares Ocupados: 0",
                Location = new Point(30, 500),
                Font = new Font("Arial", 12),
                AutoSize = true
            };
            this.Controls.Add(lblOcupados);

            lblFaturamento = new Label
            {
                Text = "Faturamento: R$ 0,00",
                Location = new Point(30, 530),
                Font = new Font("Arial", 12),
                AutoSize = true
            };
            this.Controls.Add(lblFaturamento);

            btnResetar = new Button
            {
                Text = "Resetar Mapa",
                Location = new Point(30, 570),
                Size = new Size(120, 40)
            };
            btnResetar.Click += BtnResetarMapa_Click;
            this.Controls.Add(btnResetar);

            txtFileira = new TextBox
            {
                Location = new Point(200, 500),
                Size = new Size(50, 20),
                PlaceholderText = "Fileira"
            };
            this.Controls.Add(txtFileira);

            txtPoltrona = new TextBox
            {
                Location = new Point(260, 500),
                Size = new Size(50, 20),
                PlaceholderText = "Poltrona"
            };
            this.Controls.Add(txtPoltrona);

            btnReservar = new Button
            {
                Text = "Reservar Poltrona",
                Location = new Point(200, 530),
                Size = new Size(120, 40)
            };
            btnReservar.Click += BtnReservar_Click;
            this.Controls.Add(btnReservar);
        }

        private void CriarMapa()
        {
            for (int i = 0; i < NUM_FILEIRAS; i++)
            {
                for (int j = 0; j < POLTRONAS_POR_FILEIRA; j++)
                {
                    Button btn = new Button
                    {
                        Size = new Size(25, 25),
                        Location = new Point(30 + j * 28, 30 + i * 28),
                        BackColor = Color.Green,
                        Tag = (i, j)
                    };
                    btn.Click += PoltronaClick;
                    Controls.Add(btn);
                    poltronas[i, j] = btn;
                }
            }
        }

        private void PoltronaClick(object sender, EventArgs e)
        {
            ReservarPoltrona((Button)sender);
        }

        private void BtnReservar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtFileira.Text, out int fileira) && int.TryParse(txtPoltrona.Text, out int poltrona))
            {
                if (fileira < 1 || fileira > NUM_FILEIRAS || poltrona < 1 || poltrona > POLTRONAS_POR_FILEIRA)
                {
                    MessageBox.Show("Fileira ou poltrona inválida!");
                    return;
                }

                ReservarPoltrona(poltronas[fileira - 1, poltrona - 1]);
            }
            else
            {
                MessageBox.Show("Por favor, insira valores válidos para a fileira e a poltrona.");
            }
        }

        private void ReservarPoltrona(Button poltrona)
        {
            var (fileira, coluna) = ((int, int))poltrona.Tag;

            if (poltrona.BackColor == Color.Red)
            {
                MessageBox.Show("Essa poltrona já está ocupada!");
                return;
            }

            poltrona.BackColor = Color.Red;
            lugaresOcupados++;

            if (fileira < 5)
                faturamentoTotal += 50.00;
            else if (fileira < 10)
                faturamentoTotal += 30.00;
            else
                faturamentoTotal += 15.00;

            AtualizarStatus();
        }

        private void AtualizarStatus()
        {
            lblOcupados.Text = $"Lugares Ocupados: {lugaresOcupados}";
            lblFaturamento.Text = $"Faturamento: R$ {faturamentoTotal:F2}";
        }

        private void BtnResetarMapa_Click(object sender, EventArgs e)
        {
            foreach (var poltrona in poltronas)
            {
                poltrona.BackColor = Color.Green;
            }
            lugaresOcupados = 0;
            faturamentoTotal = 0.0;
            AtualizarStatus();
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
