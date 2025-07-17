namespace diyabetapp
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
            panel1 = new Panel();
            labelAppName = new Label();
            pictureBoxLogo = new PictureBox();
            panel2 = new Panel();
            labelGirisYap = new Label();
            buttonGiris = new Button();
            label1 = new Label();
            label2 = new Label();
            textBoxSifre = new TextBox();
            textBoxTcNo = new TextBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(41, 128, 185);
            panel1.Controls.Add(labelAppName);
            panel1.Controls.Add(pictureBoxLogo);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(300, 591);
            panel1.TabIndex = 0;
            // 
            // labelAppName
            // 
            labelAppName.AutoSize = true;
            labelAppName.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            labelAppName.ForeColor = Color.White;
            labelAppName.Location = new Point(60, 225);
            labelAppName.Name = "labelAppName";
            labelAppName.Size = new Size(194, 37);
            labelAppName.TabIndex = 1;
            labelAppName.Text = "Diyabet Takip";
            // 
            // pictureBoxLogo
            // 
            pictureBoxLogo.Location = new Point(75, 60);
            pictureBoxLogo.Name = "pictureBoxLogo";
            pictureBoxLogo.Size = new Size(150, 150);
            pictureBoxLogo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxLogo.TabIndex = 0;
            pictureBoxLogo.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(labelGirisYap);
            panel2.Controls.Add(buttonGiris);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(textBoxSifre);
            panel2.Controls.Add(textBoxTcNo);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(300, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(410, 591);
            panel2.TabIndex = 1;
            // 
            // labelGirisYap
            // 
            labelGirisYap.AutoSize = true;
            labelGirisYap.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            labelGirisYap.ForeColor = Color.FromArgb(41, 128, 185);
            labelGirisYap.Location = new Point(130, 60);
            labelGirisYap.Name = "labelGirisYap";
            labelGirisYap.Size = new Size(128, 37);
            labelGirisYap.TabIndex = 5;
            labelGirisYap.Text = "Giriş Yap";
            // 
            // buttonGiris
            // 
            buttonGiris.BackColor = Color.FromArgb(41, 128, 185);
            buttonGiris.FlatAppearance.BorderSize = 0;
            buttonGiris.FlatStyle = FlatStyle.Flat;
            buttonGiris.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            buttonGiris.ForeColor = Color.White;
            buttonGiris.Location = new Point(80, 320);
            buttonGiris.Name = "buttonGiris";
            buttonGiris.Size = new Size(200, 40);
            buttonGiris.TabIndex = 0;
            buttonGiris.Text = "Giriş Yap";
            buttonGiris.UseVisualStyleBackColor = false;
            buttonGiris.Click += buttonGiris_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F);
            label1.ForeColor = Color.FromArgb(41, 128, 185);
            label1.Location = new Point(80, 150);
            label1.Name = "label1";
            label1.Size = new Size(92, 23);
            label1.TabIndex = 1;
            label1.Text = "T.C. Kimlik:";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F);
            label2.ForeColor = Color.FromArgb(41, 128, 185);
            label2.Location = new Point(80, 230);
            label2.Name = "label2";
            label2.Size = new Size(47, 23);
            label2.TabIndex = 2;
            label2.Text = "Şifre:";
            label2.Click += label2_Click;
            // 
            // textBoxSifre
            // 
            textBoxSifre.BorderStyle = BorderStyle.FixedSingle;
            textBoxSifre.Font = new Font("Segoe UI", 10F);
            textBoxSifre.Location = new Point(80, 255);
            textBoxSifre.Name = "textBoxSifre";
            textBoxSifre.Size = new Size(200, 30);
            textBoxSifre.TabIndex = 3;
            textBoxSifre.UseSystemPasswordChar = true;
            // 
            // textBoxTcNo
            // 
            textBoxTcNo.BorderStyle = BorderStyle.FixedSingle;
            textBoxTcNo.Font = new Font("Segoe UI", 10F);
            textBoxTcNo.Location = new Point(80, 175);
            textBoxTcNo.Name = "textBoxTcNo";
            textBoxTcNo.Size = new Size(200, 30);
            textBoxTcNo.TabIndex = 4;
            textBoxTcNo.TextChanged += textBox2_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(710, 591);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Diyabet Takip Uygulaması";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBoxLogo;
        private Label labelAppName;
        private Panel panel2;
        private Label labelGirisYap;
        private Button buttonGiris;
        private Label label1;
        private Label label2;
        private TextBox textBoxSifre;
        private TextBox textBoxTcNo;
    }
}
