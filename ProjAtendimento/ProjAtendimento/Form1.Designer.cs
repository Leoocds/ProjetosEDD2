namespace ProjAtendimento
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lsAtendimento = new ListBox();
            lsSenhas = new ListBox();
            btnGerar = new Button();
            btnChamar = new Button();
            txtGuiche = new TextBox();
            label1 = new Label();
            btnAdicionar = new Button();
            label2 = new Label();
            lblQtdGuiches = new Label();
            btnListarSenhas = new Button();
            btnListarAtendimentos = new Button();
            SuspendLayout();
            // 
            // lsAtendimento
            // 
            lsAtendimento.FormattingEnabled = true;
            lsAtendimento.ItemHeight = 15;
            lsAtendimento.Location = new Point(487, 82);
            lsAtendimento.Name = "lsAtendimento";
            lsAtendimento.Size = new Size(200, 184);
            lsAtendimento.TabIndex = 1;
            lsAtendimento.SelectedIndexChanged += listBox1_SelectedIndexChanged_1;
            // 
            // lsSenhas
            // 
            lsSenhas.FormattingEnabled = true;
            lsSenhas.ItemHeight = 15;
            lsSenhas.Location = new Point(64, 82);
            lsSenhas.Name = "lsSenhas";
            lsSenhas.Size = new Size(200, 184);
            lsSenhas.TabIndex = 2;
            lsSenhas.SelectedIndexChanged += lsSenhas_SelectedIndexChanged;
            // 
            // btnGerar
            // 
            btnGerar.Location = new Point(113, 35);
            btnGerar.Name = "btnGerar";
            btnGerar.Size = new Size(75, 23);
            btnGerar.TabIndex = 3;
            btnGerar.Text = "Gerar";
            btnGerar.UseVisualStyleBackColor = true;
            btnGerar.Click += btnGerar_Click;
            // 
            // btnChamar
            // 
            btnChamar.Location = new Point(612, 35);
            btnChamar.Name = "btnChamar";
            btnChamar.Size = new Size(75, 23);
            btnChamar.TabIndex = 4;
            btnChamar.Text = "Chamar";
            btnChamar.UseVisualStyleBackColor = true;
            btnChamar.Click += btnChamar_Click;
            // 
            // txtGuiche
            // 
            txtGuiche.Location = new Point(557, 35);
            txtGuiche.Name = "txtGuiche";
            txtGuiche.Size = new Size(30, 23);
            txtGuiche.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(504, 39);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 6;
            label1.Text = "Guichê:";
            // 
            // btnAdicionar
            // 
            btnAdicionar.Location = new Point(341, 183);
            btnAdicionar.Name = "btnAdicionar";
            btnAdicionar.Size = new Size(66, 23);
            btnAdicionar.TabIndex = 7;
            btnAdicionar.Text = "Adicionar";
            btnAdicionar.TextAlign = ContentAlignment.MiddleRight;
            btnAdicionar.UseVisualStyleBackColor = true;
            btnAdicionar.Click += btnAdicionar_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(322, 99);
            label2.Name = "label2";
            label2.Size = new Size(117, 15);
            label2.TabIndex = 8;
            label2.Text = "Quantidade Guiches:";
            label2.Click += label2_Click;
            // 
            // lblQtdGuiches
            // 
            lblQtdGuiches.AutoSize = true;
            lblQtdGuiches.Font = new Font("Segoe UI", 14F);
            lblQtdGuiches.Location = new Point(364, 134);
            lblQtdGuiches.Name = "lblQtdGuiches";
            lblQtdGuiches.Size = new Size(22, 25);
            lblQtdGuiches.TabIndex = 10;
            lblQtdGuiches.Text = "0";
            // 
            // btnListarSenhas
            // 
            btnListarSenhas.Location = new Point(91, 284);
            btnListarSenhas.Name = "btnListarSenhas";
            btnListarSenhas.Size = new Size(128, 25);
            btnListarSenhas.TabIndex = 11;
            btnListarSenhas.Text = "Listar Senhas";
            btnListarSenhas.UseVisualStyleBackColor = true;
            btnListarSenhas.Click += btnListarSenhas_Click;
            // 
            // btnListarAtendimentos
            // 
            btnListarAtendimentos.Location = new Point(528, 284);
            btnListarAtendimentos.Name = "btnListarAtendimentos";
            btnListarAtendimentos.Size = new Size(128, 25);
            btnListarAtendimentos.TabIndex = 12;
            btnListarAtendimentos.Text = "Listar Atendimentos";
            btnListarAtendimentos.UseVisualStyleBackColor = true;
            btnListarAtendimentos.Click += button2_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnListarAtendimentos);
            Controls.Add(btnListarSenhas);
            Controls.Add(lblQtdGuiches);
            Controls.Add(label2);
            Controls.Add(btnAdicionar);
            Controls.Add(label1);
            Controls.Add(txtGuiche);
            Controls.Add(btnChamar);
            Controls.Add(btnGerar);
            Controls.Add(lsSenhas);
            Controls.Add(lsAtendimento);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ListBox lsAtendimento;
        private ListBox lsSenhas;
        private Button btnGerar;
        private Button btnChamar;
        private TextBox txtGuiche;
        private Label label1;
        private Button btnAdicionar;
        private Label label2;
        private Label lblQtdGuiches;
        private Button btnListarSenhas;
        private Button btnListarAtendimentos;
    }
}
