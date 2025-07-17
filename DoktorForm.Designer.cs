namespace diyabetapp
{
    partial class DoktorForm
    {
        /// <summary>
        /// Designer değişkenleri
        /// </summary>
        private System.Windows.Forms.ComboBox cmbHastaSec;
        private System.Windows.Forms.DataGridView dgvDiyet;
        private System.Windows.Forms.DataGridView dgvEgzersiz;
        private System.Windows.Forms.DataGridView dgvInsulin;
        private System.Windows.Forms.Button btnHastaSec;
        private System.Windows.Forms.Button btnDiyetEkle;
        private System.Windows.Forms.Button btnEgzersizEkle;
        private System.Windows.Forms.TextBox txtTcNo;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtSifre;
        private System.Windows.Forms.Button btnHastaEkle;
        private System.Windows.Forms.DateTimePicker dateDogumTarihi;
        private System.Windows.Forms.Label lblDogumTarihi;
        private System.Windows.Forms.ComboBox cmbCinsiyet;
        private System.Windows.Forms.PictureBox picProfilResmi;
        private System.Windows.Forms.Button btnResimSec;
        private System.Windows.Forms.TextBox txtAdSoyad;
        private System.Windows.Forms.GroupBox grpBelirtiSecimi;
        private System.Windows.Forms.ComboBox cmbBelirtiler;
        private System.Windows.Forms.Button btnOneriGetir;
        private System.Windows.Forms.Panel pnlOneriler;
        private System.Windows.Forms.Label lblKanSekeriAraligi;
        private System.Windows.Forms.Label lblOnerilerDiyet;
        private System.Windows.Forms.Label lblOnerilerEgzersiz;
        private LiveCharts.WinForms.CartesianChart cartesianChart1;

        private void InitializeComponent()
        {
            cmbHastaSec = new ComboBox();
            dgvDiyet = new DataGridView();
            dgvEgzersiz = new DataGridView();
            dgvInsulin = new DataGridView();
            txtTcNo = new TextBox();
            txtEmail = new TextBox();
            txtSifre = new TextBox();
            btnHastaSec = new Button();
            btnHastaEkle = new Button();
            btnDiyetEkle = new Button();
            btnEgzersizEkle = new Button();
            lblDogumTarihi = new Label();
            cmbCinsiyet = new ComboBox();
            btnResimSec = new Button();
            btnOneriGetir = new Button();
            dateDogumTarihi = new DateTimePicker();
            picProfilResmi = new PictureBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            txtAdSoyad = new TextBox();
            tabPage2 = new TabPage();
            lblOrtalamaKan = new Label();
            progressBarEgzersiz = new ProgressBar();
            progressBarDiyet = new ProgressBar();
            lblEgzersizYuzde = new Label();
            lblDiyetYuzde = new Label();
            cmbEgzersiz = new ComboBox();
            cmbDiyet = new ComboBox();
            lblEmailDeger = new Label();
            lblCinsiyetDeger = new Label();
            lblDogumEtiket = new Label();
            lblCinsiyetEtiket = new Label();
            lblDogumDeger = new Label();
            lblTcEtiket = new Label();
            lblEmailEtiket = new Label();
            lblTcDeger = new Label();
            lblAdSoyadDeger = new Label();
            lblAdSoyadEtiket = new Label();
            picHastaProfil = new PictureBox();
            grpBelirtiSecimi = new GroupBox();
            pnlOneriler = new Panel();
            lblKanSekeriAraligi = new Label();
            lblOnerilerDiyet = new Label();
            lblOnerilerEgzersiz = new Label();
            cmbBelirtiler = new ComboBox();
            lblKanSekeriBaslik = new Label();
            tabPage3 = new TabPage();
            listViewUyarilar = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            btnUyariGetir = new Button();
            cmbUyariHastaSec = new ComboBox();
            txtOrtalamaDeger = new TextBox();
            txtOnerilenDoz = new TextBox();
            
            cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            cartesianChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            ((System.ComponentModel.ISupportInitialize)dgvDiyet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvEgzersiz).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvInsulin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picProfilResmi).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picHastaProfil).BeginInit();
            grpBelirtiSecimi.SuspendLayout();
            pnlOneriler.SuspendLayout();
            tabPage3.SuspendLayout();
            SuspendLayout();
            // 
            // cmbHastaSec
            // 
            cmbHastaSec.Location = new Point(15, 213);
            cmbHastaSec.Name = "cmbHastaSec";
            cmbHastaSec.Size = new Size(200, 28);
            cmbHastaSec.TabIndex = 9;
            cmbHastaSec.SelectedIndexChanged += cmbHastaSec_SelectedIndexChanged;
            // 
            // dgvDiyet
            // 
            dgvDiyet.ColumnHeadersHeight = 29;
            dgvDiyet.Location = new Point(613, 60);
            dgvDiyet.Name = "dgvDiyet";
            dgvDiyet.RowHeadersWidth = 51;
            dgvDiyet.Size = new Size(300, 147);
            dgvDiyet.TabIndex = 13;
            // 
            // dgvEgzersiz
            // 
            dgvEgzersiz.ColumnHeadersHeight = 29;
            dgvEgzersiz.Location = new Point(613, 246);
            dgvEgzersiz.Name = "dgvEgzersiz";
            dgvEgzersiz.RowHeadersWidth = 51;
            dgvEgzersiz.Size = new Size(300, 155);
            dgvEgzersiz.TabIndex = 16;
            // 
            // dgvInsulin
            // 
            dgvInsulin.ColumnHeadersHeight = 29;
            dgvInsulin.Location = new Point(613, 493);
            dgvInsulin.Name = "dgvInsulin";
            dgvInsulin.RowHeadersWidth = 51;
            dgvInsulin.Size = new Size(300, 135);
            dgvInsulin.TabIndex = 19;
            // 
            // txtTcNo
            // 
            txtTcNo.Location = new Point(16, 39);
            txtTcNo.Name = "txtTcNo";
            txtTcNo.PlaceholderText = "TC No";
            txtTcNo.Size = new Size(100, 27);
            txtTcNo.TabIndex = 5;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(16, 69);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "Email";
            txtEmail.Size = new Size(100, 27);
            txtEmail.TabIndex = 6;
            // 
            // txtSifre
            // 
            txtSifre.Location = new Point(16, 99);
            txtSifre.Name = "txtSifre";
            txtSifre.PlaceholderText = "Şifre";
            txtSifre.Size = new Size(100, 27);
            txtSifre.TabIndex = 7;
            // 
            // btnHastaSec
            // 
            btnHastaSec.Location = new Point(221, 212);
            btnHastaSec.Name = "btnHastaSec";
            btnHastaSec.Size = new Size(75, 28);
            btnHastaSec.TabIndex = 10;
            btnHastaSec.Text = "Seç";
            btnHastaSec.Click += btnHastaSec_Click;
            // 
            // btnHastaEkle
            // 
            btnHastaEkle.Location = new Point(17, 176);
            btnHastaEkle.Name = "btnHastaEkle";
            btnHastaEkle.Size = new Size(101, 30);
            btnHastaEkle.TabIndex = 8;
            btnHastaEkle.Text = "Hasta Ekle";
            btnHastaEkle.Click += btnHastaEkle_Click;
            // 
            // btnDiyetEkle
            // 
            btnDiyetEkle.Location = new Point(797, 27);
            btnDiyetEkle.Name = "btnDiyetEkle";
            btnDiyetEkle.Size = new Size(116, 27);
            btnDiyetEkle.TabIndex = 12;
            btnDiyetEkle.Text = "Diyet Ekle";
            btnDiyetEkle.Click += btnDiyetEkle_Click;
            // 
            // btnEgzersizEkle
            // 
            btnEgzersizEkle.Location = new Point(797, 214);
            btnEgzersizEkle.Name = "btnEgzersizEkle";
            btnEgzersizEkle.Size = new Size(116, 27);
            btnEgzersizEkle.TabIndex = 15;
            btnEgzersizEkle.Text = "Egzersiz Ekle";
            btnEgzersizEkle.Click += btnEgzersizEkle_Click;
            // 
            // lblDogumTarihi
            // 
            lblDogumTarihi.AutoSize = true;
            lblDogumTarihi.Location = new Point(15, 144);
            lblDogumTarihi.Name = "lblDogumTarihi";
            lblDogumTarihi.Size = new Size(101, 20);
            lblDogumTarihi.TabIndex = 2;
            lblDogumTarihi.Text = "Doğum Tarihi:";
            // 
            // cmbCinsiyet
            // 
            cmbCinsiyet.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCinsiyet.Items.AddRange(new object[] { "Erkek", "Kadın", "Diğer" });
            cmbCinsiyet.Location = new Point(122, 6);
            cmbCinsiyet.Name = "cmbCinsiyet";
            cmbCinsiyet.Size = new Size(200, 28);
            cmbCinsiyet.TabIndex = 0;
            // 
            // btnResimSec
            // 
            btnResimSec.Location = new Point(1146, 9);
            btnResimSec.Name = "btnResimSec";
            btnResimSec.Size = new Size(75, 30);
            btnResimSec.TabIndex = 1;
            btnResimSec.Text = "Resim Seç";
            btnResimSec.Click += btnResimSec_Click;
            // 
            // btnOneriGetir
            // 
            btnOneriGetir.Location = new Point(240, 30);
            btnOneriGetir.Name = "btnOneriGetir";
            btnOneriGetir.Size = new Size(120, 28);
            btnOneriGetir.TabIndex = 38;
            btnOneriGetir.Text = "Öneri Getir";
            btnOneriGetir.Click += btnOneriGetir_Click;
            // 
            // dateDogumTarihi
            // 
            dateDogumTarihi.Format = DateTimePickerFormat.Short;
            dateDogumTarihi.Location = new Point(122, 139);
            dateDogumTarihi.Name = "dateDogumTarihi";
            dateDogumTarihi.Size = new Size(200, 27);
            dateDogumTarihi.TabIndex = 3;
            dateDogumTarihi.ValueChanged += dateDogumTarihi_ValueChanged;
            // 
            // picProfilResmi
            // 
            picProfilResmi.BorderStyle = BorderStyle.FixedSingle;
            picProfilResmi.Location = new Point(1236, 9);
            picProfilResmi.Name = "picProfilResmi";
            picProfilResmi.Size = new Size(120, 120);
            picProfilResmi.SizeMode = PictureBoxSizeMode.StretchImage;
            picProfilResmi.TabIndex = 0;
            picProfilResmi.TabStop = false;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1370, 692);
            tabControl1.TabIndex = 20;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.SeaShell;
            tabPage1.Controls.Add(txtAdSoyad);
            tabPage1.Controls.Add(txtTcNo);
            tabPage1.Controls.Add(dateDogumTarihi);
            tabPage1.Controls.Add(txtEmail);
            tabPage1.Controls.Add(lblDogumTarihi);
            tabPage1.Controls.Add(cmbCinsiyet);
            tabPage1.Controls.Add(txtSifre);
            tabPage1.Controls.Add(btnHastaEkle);
            tabPage1.Controls.Add(btnResimSec);
            tabPage1.Controls.Add(picProfilResmi);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1362, 659);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "HastaKayıt";
            tabPage1.Click += tabPage1_Click;
            // 
            // txtAdSoyad
            // 
            txtAdSoyad.Location = new Point(16, 6);
            txtAdSoyad.Name = "txtAdSoyad";
            txtAdSoyad.PlaceholderText = "Ad Soyad";
            txtAdSoyad.Size = new Size(100, 27);
            txtAdSoyad.TabIndex = 4;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.SeaShell;
            tabPage2.Controls.Add(dgvDiyet);
            tabPage2.Controls.Add(cmbHastaSec);
            tabPage2.Controls.Add(btnHastaSec);
            tabPage2.Controls.Add(dgvInsulin);
            tabPage2.Controls.Add(btnEgzersizEkle);
            tabPage2.Controls.Add(btnDiyetEkle);
            tabPage2.Controls.Add(dgvEgzersiz);
            tabPage2.Controls.Add(grpBelirtiSecimi);
            tabPage2.Controls.Add(lblKanSekeriBaslik);
            tabPage2.Controls.Add(lblOrtalamaKan);
            tabPage2.Controls.Add(progressBarEgzersiz);
            tabPage2.Controls.Add(progressBarDiyet);
            tabPage2.Controls.Add(lblEgzersizYuzde);
            tabPage2.Controls.Add(lblDiyetYuzde);
            tabPage2.Controls.Add(cmbEgzersiz);
            tabPage2.Controls.Add(cmbDiyet);
            tabPage2.Controls.Add(lblEmailDeger);
            tabPage2.Controls.Add(lblCinsiyetDeger);
            tabPage2.Controls.Add(lblDogumEtiket);
            tabPage2.Controls.Add(lblCinsiyetEtiket);
            tabPage2.Controls.Add(lblDogumDeger);
            tabPage2.Controls.Add(lblTcEtiket);
            tabPage2.Controls.Add(lblEmailEtiket);
            tabPage2.Controls.Add(lblTcDeger);
            tabPage2.Controls.Add(lblAdSoyadDeger);
            tabPage2.Controls.Add(lblAdSoyadEtiket);
            tabPage2.Controls.Add(picHastaProfil);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1362, 659);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "HastaDetay";
            tabPage2.Click += tabPage2_Click_1;
            // 
            // lblOrtalamaKan
            // 
            lblOrtalamaKan.BackColor = Color.White;
            lblOrtalamaKan.Location = new Point(613, 450);
            lblOrtalamaKan.Name = "lblOrtalamaKan";
            lblOrtalamaKan.Size = new Size(231, 27);
            lblOrtalamaKan.TabIndex = 43;
            // 
            // progressBarEgzersiz
            // 
            progressBarEgzersiz.BackColor = Color.White;
            progressBarEgzersiz.Location = new Point(944, 597);
            progressBarEgzersiz.Name = "progressBarEgzersiz";
            progressBarEgzersiz.Size = new Size(139, 31);
            progressBarEgzersiz.TabIndex = 42;
            progressBarEgzersiz.Click += progressBar2_Click;
            // 
            // progressBarDiyet
            // 
            progressBarDiyet.BackColor = Color.White;
            progressBarDiyet.Location = new Point(944, 513);
            progressBarDiyet.Name = "progressBarDiyet";
            progressBarDiyet.Size = new Size(139, 31);
            progressBarDiyet.TabIndex = 41;
            // 
            // lblEgzersizYuzde
            // 
            lblEgzersizYuzde.BackColor = Color.MistyRose;
            lblEgzersizYuzde.Location = new Point(944, 565);
            lblEgzersizYuzde.Name = "lblEgzersizYuzde";
            lblEgzersizYuzde.Size = new Size(112, 25);
            lblEgzersizYuzde.TabIndex = 40;
            // 
            // lblDiyetYuzde
            // 
            lblDiyetYuzde.BackColor = Color.MistyRose;
            lblDiyetYuzde.Location = new Point(944, 475);
            lblDiyetYuzde.Name = "lblDiyetYuzde";
            lblDiyetYuzde.Size = new Size(112, 25);
            lblDiyetYuzde.TabIndex = 39;
            lblDiyetYuzde.Click += label1_Click_1;
            // 
            // cmbEgzersiz
            // 
            cmbEgzersiz.FormattingEnabled = true;
            cmbEgzersiz.Items.AddRange(new object[] { "Yürüyüs", "Bisiklet", "Klinik Egzersiz" });
            cmbEgzersiz.Location = new Point(613, 213);
            cmbEgzersiz.Name = "cmbEgzersiz";
            cmbEgzersiz.Size = new Size(178, 28);
            cmbEgzersiz.TabIndex = 37;
            // 
            // cmbDiyet
            // 
            cmbDiyet.FormattingEnabled = true;
            cmbDiyet.Items.AddRange(new object[] { "Az Sekerli ", "Sekersiz", "Dengeli" });
            cmbDiyet.Location = new Point(613, 29);
            cmbDiyet.Name = "cmbDiyet";
            cmbDiyet.Size = new Size(178, 28);
            cmbDiyet.TabIndex = 36;
            cmbDiyet.SelectedIndexChanged += cmbDiyet_SelectedIndexChanged;
            // 
            // lblEmailDeger
            // 
            lblEmailDeger.Location = new Point(117, 289);
            lblEmailDeger.Name = "lblEmailDeger";
            lblEmailDeger.Size = new Size(199, 20);
            lblEmailDeger.TabIndex = 35;
            // 
            // lblCinsiyetDeger
            // 
            lblCinsiyetDeger.Location = new Point(117, 394);
            lblCinsiyetDeger.Name = "lblCinsiyetDeger";
            lblCinsiyetDeger.Size = new Size(50, 20);
            lblCinsiyetDeger.TabIndex = 34;
            // 
            // lblDogumEtiket
            // 
            lblDogumEtiket.AutoSize = true;
            lblDogumEtiket.Location = new Point(29, 437);
            lblDogumEtiket.Name = "lblDogumEtiket";
            lblDogumEtiket.Size = new Size(98, 20);
            lblDogumEtiket.TabIndex = 33;
            lblDogumEtiket.Text = "Dogum Tarihi";
            // 
            // lblCinsiyetEtiket
            // 
            lblCinsiyetEtiket.AutoSize = true;
            lblCinsiyetEtiket.Location = new Point(27, 394);
            lblCinsiyetEtiket.Name = "lblCinsiyetEtiket";
            lblCinsiyetEtiket.Size = new Size(60, 20);
            lblCinsiyetEtiket.TabIndex = 32;
            lblCinsiyetEtiket.Text = "Cinsiyet";
            // 
            // lblDogumDeger
            // 
            lblDogumDeger.Location = new Point(133, 437);
            lblDogumDeger.Name = "lblDogumDeger";
            lblDogumDeger.Size = new Size(90, 20);
            lblDogumDeger.TabIndex = 31;
            // 
            // lblTcEtiket
            // 
            lblTcEtiket.AutoSize = true;
            lblTcEtiket.Location = new Point(27, 351);
            lblTcEtiket.Name = "lblTcEtiket";
            lblTcEtiket.Size = new Size(52, 20);
            lblTcEtiket.TabIndex = 30;
            lblTcEtiket.Text = "TC No:";
            // 
            // lblEmailEtiket
            // 
            lblEmailEtiket.Location = new Point(27, 289);
            lblEmailEtiket.Name = "lblEmailEtiket";
            lblEmailEtiket.Size = new Size(76, 20);
            lblEmailEtiket.TabIndex = 29;
            lblEmailEtiket.Text = "E-Mail:";
            // 
            // lblTcDeger
            // 
            lblTcDeger.Location = new Point(117, 351);
            lblTcDeger.Name = "lblTcDeger";
            lblTcDeger.Size = new Size(121, 20);
            lblTcDeger.TabIndex = 28;
            // 
            // lblAdSoyadDeger
            // 
            lblAdSoyadDeger.Location = new Point(117, 251);
            lblAdSoyadDeger.Name = "lblAdSoyadDeger";
            lblAdSoyadDeger.Size = new Size(121, 20);
            lblAdSoyadDeger.TabIndex = 27;
            lblAdSoyadDeger.Click += lblAdSoyadDeger_Click;
            // 
            // lblAdSoyadEtiket
            // 
            lblAdSoyadEtiket.AutoSize = true;
            lblAdSoyadEtiket.Location = new Point(27, 251);
            lblAdSoyadEtiket.Name = "lblAdSoyadEtiket";
            lblAdSoyadEtiket.Size = new Size(76, 20);
            lblAdSoyadEtiket.TabIndex = 26;
            lblAdSoyadEtiket.Text = "Ad Soyad:";
            // 
            // picHastaProfil
            // 
            picHastaProfil.BorderStyle = BorderStyle.FixedSingle;
            picHastaProfil.Location = new Point(15, 29);
            picHastaProfil.Name = "picHastaProfil";
            picHastaProfil.Size = new Size(196, 158);
            picHastaProfil.SizeMode = PictureBoxSizeMode.StretchImage;
            picHastaProfil.TabIndex = 25;
            picHastaProfil.TabStop = false;
            // 
            // grpBelirtiSecimi
            // 
            grpBelirtiSecimi.Controls.Add(pnlOneriler);
            grpBelirtiSecimi.Controls.Add(cmbBelirtiler);
            grpBelirtiSecimi.Controls.Add(btnOneriGetir);
            grpBelirtiSecimi.Location = new Point(15, 350);
            grpBelirtiSecimi.Name = "grpBelirtiSecimi";
            grpBelirtiSecimi.Size = new Size(500, 250);
            grpBelirtiSecimi.TabIndex = 38;
            grpBelirtiSecimi.TabStop = false;
            grpBelirtiSecimi.Text = "Belirti Seçimi ve Öneriler";
            // 
            // pnlOneriler
            // 
            pnlOneriler.BorderStyle = BorderStyle.FixedSingle;
            pnlOneriler.Controls.Add(lblKanSekeriAraligi);
            pnlOneriler.Controls.Add(lblOnerilerDiyet);
            pnlOneriler.Controls.Add(lblOnerilerEgzersiz);
            pnlOneriler.Location = new Point(20, 70);
            pnlOneriler.Name = "pnlOneriler";
            pnlOneriler.Size = new Size(460, 160);
            pnlOneriler.TabIndex = 0;
            // 
            // lblKanSekeriAraligi
            // 
            lblKanSekeriAraligi.AutoSize = true;
            lblKanSekeriAraligi.Location = new Point(10, 10);
            lblKanSekeriAraligi.MaximumSize = new Size(440, 0);
            lblKanSekeriAraligi.Name = "lblKanSekeriAraligi";
            lblKanSekeriAraligi.Size = new Size(129, 20);
            lblKanSekeriAraligi.TabIndex = 0;
            lblKanSekeriAraligi.Text = "Kan Şekeri Aralığı:";
            // 
            // lblOnerilerDiyet
            // 
            lblOnerilerDiyet.AutoEllipsis = true;
            lblOnerilerDiyet.AutoSize = true;
            lblOnerilerDiyet.Location = new Point(10, 50);
            lblOnerilerDiyet.MaximumSize = new Size(440, 0);
            lblOnerilerDiyet.Name = "lblOnerilerDiyet";
            lblOnerilerDiyet.Size = new Size(97, 20);
            lblOnerilerDiyet.TabIndex = 1;
            lblOnerilerDiyet.Text = "Diyet Önerisi:";
            // 
            // lblOnerilerEgzersiz
            // 
            lblOnerilerEgzersiz.AutoEllipsis = true;
            lblOnerilerEgzersiz.AutoSize = true;
            lblOnerilerEgzersiz.Location = new Point(10, 110);
            lblOnerilerEgzersiz.MaximumSize = new Size(440, 0);
            lblOnerilerEgzersiz.Name = "lblOnerilerEgzersiz";
            lblOnerilerEgzersiz.Size = new Size(116, 20);
            lblOnerilerEgzersiz.TabIndex = 2;
            lblOnerilerEgzersiz.Text = "Egzersiz Önerisi:";
            // 
            // cmbBelirtiler
            // 
            cmbBelirtiler.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBelirtiler.Items.AddRange(new object[] { "Nöropati", "Polifaji", "Yorgunluk", "Kilo Kaybı", "Bulanık Görme", "Polidipsi", "Poliüri", "Yaraların Yavaş İyileşmesi" });
            cmbBelirtiler.Location = new Point(20, 30);
            cmbBelirtiler.Name = "cmbBelirtiler";
            cmbBelirtiler.Size = new Size(200, 28);
            cmbBelirtiler.TabIndex = 1;
            // 
            // lblKanSekeriBaslik
            // 
            lblKanSekeriBaslik.AutoSize = true;
            lblKanSekeriBaslik.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblKanSekeriBaslik.Location = new Point(613, 493);
            lblKanSekeriBaslik.Name = "lblKanSekeriBaslik";
            lblKanSekeriBaslik.Size = new Size(193, 25);
            lblKanSekeriBaslik.TabIndex = 0;
            lblKanSekeriBaslik.Text = "Kan Şekeri Değerleri";
            // 
            // tabPage3
            // 
            tabPage3.BackColor = Color.SeaShell;
            tabPage3.Controls.Add(listViewUyarilar);
            tabPage3.Controls.Add(btnUyariGetir);
            tabPage3.Controls.Add(cmbUyariHastaSec);
            tabPage3.Location = new Point(4, 29);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1362, 659);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Uyarılar";
            // 
            // listViewUyarilar
            // 
            listViewUyarilar.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4 });
            listViewUyarilar.FullRowSelect = true;
            listViewUyarilar.Location = new Point(87, 125);
            listViewUyarilar.Name = "listViewUyarilar";
            listViewUyarilar.Size = new Size(1001, 513);
            listViewUyarilar.TabIndex = 2;
            listViewUyarilar.UseCompatibleStateImageBehavior = false;
            listViewUyarilar.View = View.Details;
            listViewUyarilar.SelectedIndexChanged += lvwUyarilar_SelectedIndexChanged;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Tarih";
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Uyarı Tipi";
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Saat";
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Mesaj";
            // 
            // btnUyariGetir
            // 
            btnUyariGetir.Location = new Point(87, 77);
            btnUyariGetir.Name = "btnUyariGetir";
            btnUyariGetir.Size = new Size(147, 29);
            btnUyariGetir.TabIndex = 1;
            btnUyariGetir.Text = "Uyarıları Getir";
            btnUyariGetir.UseVisualStyleBackColor = true;
            btnUyariGetir.Click += btnUyariGetir_Click;
            // 
            // cmbUyariHastaSec
            // 
            cmbUyariHastaSec.FormattingEnabled = true;
            cmbUyariHastaSec.Location = new Point(87, 30);
            cmbUyariHastaSec.Name = "cmbUyariHastaSec";
            cmbUyariHastaSec.Size = new Size(147, 28);
            cmbUyariHastaSec.TabIndex = 0;
            // 
            // txtOrtalamaDeger
            // 
            txtOrtalamaDeger.Location = new Point(0, 0);
            txtOrtalamaDeger.Name = "txtOrtalamaDeger";
            txtOrtalamaDeger.Size = new Size(100, 27);
            txtOrtalamaDeger.TabIndex = 0;
            // 
            // txtOnerilenDoz
            // 
            txtOnerilenDoz.Location = new Point(0, 0);
            txtOnerilenDoz.Name = "txtOnerilenDoz";
            txtOnerilenDoz.Size = new Size(100, 27);
            txtOnerilenDoz.TabIndex = 0;
            
        }

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private PictureBox picHastaProfil;
        private Label lblEmailDeger;
        private Label lblCinsiyetDeger;
        private Label lblDogumEtiket;
        private Label lblCinsiyetEtiket;
        private Label lblDogumDeger;
        private Label lblTcEtiket;
        private Label lblEmailEtiket;
        private Label lblTcDeger;
        private Label lblAdSoyadDeger;
        private Label lblAdSoyadEtiket;
        private ComboBox cmbUyariHastaSec;
        private ListView listViewUyarilar;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader4;
        private Button btnUyariGetir;
        private ComboBox cmbEgzersiz;
        private ComboBox cmbDiyet;
        private TextBox txtOnerilenDoz;
        private TextBox txtOrtalamaDeger;
        private Label lblKanSekeriBaslik;
        private ProgressBar progressBarEgzersiz;
        private ProgressBar progressBarDiyet;
        private Label lblEgzersizYuzde;
        private Label lblDiyetYuzde;
        private Label lblOrtalamaKan;
    }
}