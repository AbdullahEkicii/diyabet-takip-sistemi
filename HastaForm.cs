using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Collections.Generic;

namespace diyabetapp
{
    public partial class HastaForm : Form
    {
        private string connectionString = "Server=localhost\\SQLEXPRESS;Database=DiyabetDB;Trusted_Connection=True;Encrypt=False;";
        private int hastaID;
        private string hastaAdSoyad;
        private string hastaTC;
        private DateTime hastaDogumTarihi;
        private string hastaCinsiyet;
        private string hastaEmail;
        private PictureBox profilFotoBox;
        private string profilFotoYolu;
        private DataGridView dgvKanSekeri;
        private TabPage insulinTab;
        private Panel insulinPanel;

        public HastaForm(string tcNo)
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(255, 240, 245); // Hafif pembe arka plan
            hastaTC = tcNo;
        // DataGridView'leri başlat
            InitializeDataGridViews();

            HastaBilgileriniGetir();
            this.Text = $"Hoş Geldiniz - {hastaAdSoyad}";
            this.Size = new Size(1000, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeCustomComponents();
            LoadData();
        }

        private void HastaBilgileriniGetir()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT KullaniciID, AdSoyad, DogumTarihi, Cinsiyet, Email FROM Kullanicilar WHERE TcNo = @TcNo";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TcNo", hastaTC);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    hastaID = reader.GetInt32(0);
                    hastaAdSoyad = reader.GetString(1);
                    hastaDogumTarihi = reader.IsDBNull(2) ? DateTime.MinValue : reader.GetDateTime(2);
                    hastaCinsiyet = reader.IsDBNull(3) ? "Belirtilmemiş" : reader.GetString(3);
                    hastaEmail = reader.IsDBNull(4) ? "Belirtilmemiş" : reader.GetString(4);
                }
                reader.Close();
            }
        }

        private void InitializeCustomComponents()
        {
            TabControl tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            // Bilgilerim Sekmesi
            TabPage bilgilerimTab = new TabPage("Bilgilerim");
            bilgilerimTab.BackColor = Color.FromArgb(255, 240, 245);
            Panel bilgilerimPanel = new Panel();
            bilgilerimPanel.Dock = DockStyle.Fill;
            bilgilerimPanel.Padding = new Padding(30, 20, 30, 20);

            // Profil Fotoğrafı
            profilFotoBox = new PictureBox();
            profilFotoBox.Size = new Size(150, 150);
            profilFotoBox.Location = new Point(40, 30);
            profilFotoBox.SizeMode = PictureBoxSizeMode.StretchImage;
            profilFotoBox.BorderStyle = BorderStyle.FixedSingle;
            profilFotoBox.Click += ProfilFotoBox_Click;

            // Profil Fotoğrafı Değiştir Butonu
            Button btnFotoDegistir = new Button();
            btnFotoDegistir.Text = "Profil Fotoğrafı Değiştir";
            btnFotoDegistir.Location = new Point(40, 190);
            btnFotoDegistir.Size = new Size(150, 30);
            btnFotoDegistir.BackColor = Color.FromArgb(0, 123, 255);
            btnFotoDegistir.ForeColor = Color.White;
            btnFotoDegistir.FlatStyle = FlatStyle.Flat;
            btnFotoDegistir.Click += BtnFotoDegistir_Click;

            // Hasta Bilgileri
            Label lblAdSoyad = new Label();
            lblAdSoyad.Text = $"Ad Soyad: {hastaAdSoyad}";
            lblAdSoyad.Location = new Point(220, 30);
            lblAdSoyad.AutoSize = true;
            lblAdSoyad.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

            Label lblTC = new Label();
            lblTC.Text = $"T.C. Kimlik No: {hastaTC}";
            lblTC.Location = new Point(220, 60);
            lblTC.AutoSize = true;
            lblTC.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

            // Yeni Bilgiler
            Label lblDogumTarihi = new Label();
            lblDogumTarihi.Text = $"Doğum Tarihi: { (hastaDogumTarihi == DateTime.MinValue ? "Belirtilmemiş" : hastaDogumTarihi.ToShortDateString()) }";
            lblDogumTarihi.Location = new Point(220, 90); // TC'nin altına
            lblDogumTarihi.AutoSize = true;
            lblDogumTarihi.Font = new Font("Segoe UI", 11F);

            Label lblCinsiyet = new Label();
            lblCinsiyet.Text = $"Cinsiyet: {hastaCinsiyet}";
            lblCinsiyet.Location = new Point(220, 120); // Doğum Tarihi'nin altına
            lblCinsiyet.AutoSize = true;
            lblCinsiyet.Font = new Font("Segoe UI", 11F);

            Label lblEmail = new Label();
            lblEmail.Text = $"Email: {hastaEmail}";
            lblEmail.Location = new Point(220, 150); // Cinsiyet'in altına
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 11F);

            bilgilerimPanel.Controls.AddRange(new Control[] { profilFotoBox, btnFotoDegistir, lblAdSoyad, lblTC, lblDogumTarihi, lblCinsiyet, lblEmail });
            bilgilerimTab.Controls.Add(bilgilerimPanel);

            // Kan Şekeri Takibi Sekmesi
            TabPage kanSekeriTab = new TabPage("Kan Şekeri Takibi");
            kanSekeriTab.BackColor = Color.FromArgb(255, 240, 245);
            Panel kanSekeriPanel = new Panel();
            kanSekeriPanel.Dock = DockStyle.Fill;
            kanSekeriPanel.Padding = new Padding(30, 20, 30, 20);

            Label lblKanSekeri = new Label();
            lblKanSekeri.Text = "Kan Şekeri Değeri:";
            lblKanSekeri.Location = new Point(40, 30);
            lblKanSekeri.AutoSize = true;
            lblKanSekeri.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

            TextBox txtKanSekeri = new TextBox();
            txtKanSekeri.Name = "txtKanSekeri";
            txtKanSekeri.Location = new Point(220, 28);
            txtKanSekeri.Size = new Size(120, 28);
            txtKanSekeri.Font = new Font("Segoe UI", 11F);

            Button btnKanSekeriKaydet = new Button();
            btnKanSekeriKaydet.Text = "Kaydet";
            btnKanSekeriKaydet.Location = new Point(370, 26);
            btnKanSekeriKaydet.Size = new Size(110, 32);
            btnKanSekeriKaydet.BackColor = Color.FromArgb(0, 123, 255);
            btnKanSekeriKaydet.ForeColor = Color.White;
            btnKanSekeriKaydet.FlatStyle = FlatStyle.Flat;
            btnKanSekeriKaydet.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnKanSekeriKaydet.Click += BtnKanSekeriKaydet_Click;

            // Yeni Ortalama Kan Şekeri Label'ı
            Label lblOrtalamaKanSekeri = new Label();
            lblOrtalamaKanSekeri.Name = "lblOrtalamaKanSekeri";
            lblOrtalamaKanSekeri.Text = "Ortalama Kan Şekeri: -"; // Başlangıç metni
            lblOrtalamaKanSekeri.Location = new Point(500, 30); // Konumu ayarlayın
            lblOrtalamaKanSekeri.AutoSize = true;
            lblOrtalamaKanSekeri.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblOrtalamaKanSekeri.ForeColor = Color.DarkBlue;

            // dgvKanSekeri zaten sınıf seviyesinde tanımlandığı ve 
            // InitializeDataGridViews metodunda başlatıldığı için burada yeniden başlatma 
            // yapmıyoruz, sadece panel'e ekliyoruz

            kanSekeriPanel.Controls.AddRange(new Control[] { lblKanSekeri, txtKanSekeri, btnKanSekeriKaydet, lblOrtalamaKanSekeri, dgvKanSekeri });
            kanSekeriTab.Controls.Add(kanSekeriPanel);

            // Diyet Takibi Sekmesi
            TabPage diyetTab = new TabPage("Diyet Takibi");
            diyetTab.BackColor = Color.FromArgb(255, 240, 245);
            Panel diyetPanel = new Panel();
            diyetPanel.Dock = DockStyle.Fill;
            diyetPanel.Padding = new Padding(30, 20, 30, 20);

            DataGridView dgvDiyet = new DataGridView();
            dgvDiyet.Name = "dgvDiyet";
            dgvDiyet.Location = new Point(40, 20);
            dgvDiyet.Size = new Size(880, 180);
            dgvDiyet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDiyet.BackgroundColor = Color.White;
            dgvDiyet.Font = new Font("Segoe UI", 10F);

            Button btnDiyetUygulandi = new Button();
            btnDiyetUygulandi.Text = "Diyeti Uyguladım";
            btnDiyetUygulandi.Location = new Point(40, 220);
            btnDiyetUygulandi.Size = new Size(170, 38);
            btnDiyetUygulandi.BackColor = Color.FromArgb(40, 167, 69);
            btnDiyetUygulandi.ForeColor = Color.White;
            btnDiyetUygulandi.FlatStyle = FlatStyle.Flat;
            btnDiyetUygulandi.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDiyetUygulandi.Click += BtnDiyetUygulandi_Click;

            // ProgressBar ve Yüzde Label'ı panele ekle ve konumlarını ayarla
            // Konumları manuel olarak ayarla, dgvDiyet ve btnDiyetUygulandi ile çakışmayacak şekilde
            progressBarDiyet.Location = new Point(40, btnDiyetUygulandi.Location.Y + btnDiyetUygulandi.Height + 20); // Butonun altına
            progressBarDiyet.Size = new Size(200, 25);
            lblDiyetYuzde.Location = new Point(progressBarDiyet.Location.X + progressBarDiyet.Width + 10, progressBarDiyet.Location.Y); // ProgressBar yanına
            lblDiyetYuzde.AutoSize = true;
            lblDiyetYuzde.Text = "Diyet Uyum: %0"; // Başlangıç metni

            // Panelin kontrollerine ekle
            diyetPanel.Controls.Add(dgvDiyet);
            diyetPanel.Controls.Add(btnDiyetUygulandi);
            diyetPanel.Controls.Add(progressBarDiyet);
            diyetPanel.Controls.Add(lblDiyetYuzde);

            diyetTab.Controls.Add(diyetPanel);

            // Egzersiz Takibi Sekmesi
            TabPage egzersizTab = new TabPage("Egzersiz Takibi");
            egzersizTab.BackColor = Color.FromArgb(255, 240, 245);
            Panel egzersizPanel = new Panel();
            egzersizPanel.Dock = DockStyle.Fill;
            egzersizPanel.Padding = new Padding(30, 20, 30, 20);

            DataGridView dgvEgzersiz = new DataGridView();
            dgvEgzersiz.Name = "dgvEgzersiz";
            dgvEgzersiz.Location = new Point(40, 20);
            dgvEgzersiz.Size = new Size(880, 180);
            dgvEgzersiz.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEgzersiz.BackgroundColor = Color.White;
            dgvEgzersiz.Font = new Font("Segoe UI", 10F);

            Button btnEgzersizYapildi = new Button();
            btnEgzersizYapildi.Text = "Egzersizi Yaptım";
            btnEgzersizYapildi.Location = new Point(40, 220);
            btnEgzersizYapildi.Size = new Size(170, 38);
            btnEgzersizYapildi.BackColor = Color.FromArgb(40, 167, 69);
            btnEgzersizYapildi.ForeColor = Color.White;
            btnEgzersizYapildi.FlatStyle = FlatStyle.Flat;
            btnEgzersizYapildi.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEgzersizYapildi.Click += BtnEgzersizYapildi_Click;

            // ProgressBar ve Yüzde Label'ı panele ekle ve konumlarını ayarla
            // Konumları manuel olarak ayarla, dgvEgzersiz ve btnEgzersizYapildi ile çakışmayacak şekilde
            progressBarEgzersiz.Location = new Point(40, btnEgzersizYapildi.Location.Y + btnEgzersizYapildi.Height + 20); // Butonun altına
            progressBarEgzersiz.Size = new Size(200, 25);
            lblEgzersizYuzde.Location = new Point(progressBarEgzersiz.Location.X + progressBarEgzersiz.Width + 10, progressBarEgzersiz.Location.Y); // ProgressBar yanına
            lblEgzersizYuzde.AutoSize = true;
            lblEgzersizYuzde.Text = "Egzersiz Uyum: %0"; // Başlangıç metni

            // Panelin kontrollerine ekle
            egzersizPanel.Controls.Add(dgvEgzersiz);
            egzersizPanel.Controls.Add(btnEgzersizYapildi);
            egzersizPanel.Controls.Add(progressBarEgzersiz);
            egzersizPanel.Controls.Add(lblEgzersizYuzde);

            egzersizTab.Controls.Add(egzersizPanel);

            // İnsülin Takibi Sekmesi
            insulinTab = new TabPage("İnsülin Takibi");
            insulinTab.BackColor = Color.FromArgb(255, 240, 245);
            insulinPanel = new Panel();
            insulinPanel.Dock = DockStyle.Fill;
            insulinPanel.Padding = new Padding(30, 20, 30, 20);
            insulinPanel.Visible = true;
            insulinPanel.BackColor = Color.WhiteSmoke;

            Label lblGununOlcumleriBaslik = new Label
            {
                Text = "Bugünün Kayıtlı Kan Şekeri Ölçümleri:",
                Location = new Point(40, 20),
                AutoSize = true,
                Visible = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };

            Label lblGununOlcumleriDetay = new Label
            {
                Name = "lblGununOlcumleriDetay",
                Text = "Sabah: -\nÖğlen: -\nİkindi: -\nAkşam: -\nGece: -",
                Location = new Point(40, 55),
                AutoSize = true,
                Visible = true,
                Font = new Font("Segoe UI", 10F),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(5)
            };

            Label lblDegerlendirmeOgunu = new Label
            {
                Text = "Değerlendirme Yapılacak Öğün:",
                Location = new Point(40, 110),
                AutoSize = true,
                Visible = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            ComboBox cmbDegerlendirmeOgunu = new ComboBox
            {
                Name = "cmbDegerlendirmeOgunu",
                Location = new Point(250, 108),
                Size = new Size(180, 28),
                Font = new Font("Segoe UI", 10F),
                Visible = true,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbDegerlendirmeOgunu.Items.AddRange(new string[] { "Sabah", "Öğlen", "İkindi", "Akşam", "Gece" });
            if (cmbDegerlendirmeOgunu.Items.Count > 0) cmbDegerlendirmeOgunu.SelectedIndex = 0;

            Button btnHesaplaVeOner = new Button
            {
                Text = "İnsülin Önerisi Hesapla ve Kaydet",
                Name = "btnHesaplaVeOner",
                Location = new Point(40, 150),
                Size = new Size(390, 38),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Visible = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            btnHesaplaVeOner.Click += BtnHesaplaVeOner_Click;

            Label lblHesaplananOrtalamaSonuc = new Label
            {
                Name = "lblHesaplananOrtalamaSonuc",
                Text = "Ortalama Kan Şekeri: -",
                Location = new Point(500, 55),
                AutoSize = true,
                Visible = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            Label lblInsulinOnerisiSonuc = new Label
            {
                Name = "lblInsulinOnerisiSonuc",
                Text = "İnsülin Önerisi: -",
                Location = new Point(500, 85),
                AutoSize = true,
                Visible = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.DarkGreen
            };

            RichTextBox txtUyariMesajiSonuc = new RichTextBox
            {
                Name = "txtUyariMesajiSonuc",
                Text = "Uyarılar: -",
                Location = new Point(500, 115),
                Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                ForeColor = Color.Red,
                Visible = true,
                Size = new Size(350, 80),
                ReadOnly = true,
                BackColor = Color.WhiteSmoke,
                BorderStyle = BorderStyle.None,
                ScrollBars = RichTextBoxScrollBars.Vertical
            };

            // 📌 Tarih Filtreleme Kontrolleri Ekle
            Label lblFiltreTarih = new Label
            {
                Text = "Tarihe Göre Filtrele:",
                Location = new Point(40, 480), // Konumu ayarlayın
                AutoSize = true,
                Visible = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            DateTimePicker dtpInsulinFilter = new DateTimePicker
            {
                Name = "dtpInsulinFilter",
                Location = new Point(180, 478), // lblFiltreTarih yanına konumlandırın
                Size = new Size(200, 28),
                Font = new Font("Segoe UI", 10F),
                Visible = true,
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today // Başlangıçta bugünü seçili yap
            };

            Button btnFilterInsulinByDate = new Button
            {
                Text = "Filtrele",
                Name = "btnFilterInsulinByDate",
                Location = new Point(390, 476), // dtpInsulinFilter yanına konumlandırın
                Size = new Size(100, 32),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Visible = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            btnFilterInsulinByDate.Click += BtnFilterInsulinByDate_Click; // Olay işleyicisi ekle

            DataGridView dgvInsulin = (DataGridView)this.Controls.Find("dgvInsulin", true).FirstOrDefault();
            if (dgvInsulin == null)
            {
                dgvInsulin = new DataGridView();
                dgvInsulin.Name = "dgvInsulin";
                dgvInsulin.Location = new Point(40, 210);
                dgvInsulin.Size = new Size(880, 250);
                dgvInsulin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvInsulin.BackgroundColor = Color.White;
                dgvInsulin.Font = new Font("Segoe UI", 10F);
            }
            else
            {
                dgvInsulin.Location = new Point(40, 210);
                dgvInsulin.Size = new Size(880, 250);
            }

            insulinPanel.Controls.Clear();
            insulinPanel.Controls.AddRange(new Control[] {
                lblGununOlcumleriBaslik, lblGununOlcumleriDetay,
                lblDegerlendirmeOgunu, cmbDegerlendirmeOgunu,
                btnHesaplaVeOner,
                lblHesaplananOrtalamaSonuc, lblInsulinOnerisiSonuc, txtUyariMesajiSonuc,
                lblFiltreTarih, dtpInsulinFilter, btnFilterInsulinByDate, // Yeni eklenen kontroller
                dgvInsulin
            });

            insulinTab.Text = "İnsülin Öneri Sistemi";

            GununKanSekeriOlcumleriniYukle();

            tabControl.TabPages.AddRange(new TabPage[] { bilgilerimTab, kanSekeriTab, diyetTab, egzersizTab, insulinTab });
            this.Controls.Add(tabControl);
            ProfilFotografiniYukle();

            insulinTab.Controls.Add(insulinPanel);
        }
        private void InitializeDataGridViews()
        {
            // Kan Şekeri DataGridView
            dgvKanSekeri = new DataGridView();
            dgvKanSekeri.Name = "dgvKanSekeri";
            dgvKanSekeri.Location = new Point(40, 80);
            dgvKanSekeri.Size = new Size(880, 180);
            dgvKanSekeri.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKanSekeri.BackgroundColor = Color.White;
            dgvKanSekeri.Font = new Font("Segoe UI", 10F);
            dgvKanSekeri.AllowUserToAddRows = false;
            dgvKanSekeri.AllowUserToDeleteRows = false;
            dgvKanSekeri.ReadOnly = true;
            dgvKanSekeri.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
         private Dictionary<string, double> gunlukOlcumler = new Dictionary<string, double>();

private void GununKanSekeriOlcumleriniYukle()
{
    Label lblDetay = (Label)this.Controls.Find("lblGununOlcumleriDetay", true).FirstOrDefault();
    if (lblDetay == null) return;

    gunlukOlcumler.Clear();
    string displayText = "";
    DateTime bugunBaslangic = DateTime.Today;
    DateTime bugunBitis = DateTime.Today.AddDays(1).AddTicks(-1);

    try
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            // Sadece ZAMANINDA olan ve bugünkü ölçümleri al, her tipten en sonuncusunu alabiliriz (ya da ilkini)
            // Şimdilik her tipten bugünkü olanları alalım, eğer birden fazla varsa sonuncusu dikkate alınacak.
            string query = @"
                SELECT OlcumZamaniTipi, Deger
                FROM KanSekeriOlcumleri
                WHERE HastaID = @HastaID
                  AND TarihSaat BETWEEN @Baslangic AND @Bitis
                  AND ZamanindaMi = 1
                  AND OlcumZamaniTipi IN ('Sabah', 'Öğle', 'İkindi', 'Akşam', 'Gece')
                ORDER BY TarihSaat DESC"; // Her tipten sonuncuyu almak için bir gruplama ve ROW_NUMBER() gerekebilir,
                                        // şimdilik bu sorgu birden fazla aynı tip ölçüm varsa hepsini getirir.
                                        // Basitlik adına, her tipten ilk bulduğunu (veya son) alacak şekilde işlemeliyiz.

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@HastaID", hastaID);
            cmd.Parameters.AddWithValue("@Baslangic", bugunBaslangic);
            cmd.Parameters.AddWithValue("@Bitis", bugunBitis);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string tip = reader["OlcumZamaniTipi"].ToString();
                    double deger = Convert.ToDouble(reader["Deger"]);

                    // Eğer aynı tipte birden fazla ölçüm varsa, sonuncusunu (veya ilkini) almak için:
                    // Dictionary'ye eklerken kontrol edebiliriz.
                    if (!gunlukOlcumler.ContainsKey(tip)) // Sadece ilk (veya en son, sorguya göre)
                    {
                        gunlukOlcumler[tip] = deger;
                    }
                }
            }
        }

        displayText = $"Sabah: { (gunlukOlcumler.ContainsKey("Sabah") ? gunlukOlcumler["Sabah"].ToString() + " mg/dL" : "Girilmedi") }\n" +
                      $"Öğlen: { (gunlukOlcumler.ContainsKey("Öğle") ? gunlukOlcumler["Öğle"].ToString() + " mg/dL" : "Girilmedi") }\n" +
                      $"İkindi: { (gunlukOlcumler.ContainsKey("İkindi") ? gunlukOlcumler["İkindi"].ToString() + " mg/dL" : "Girilmedi") }\n" +
                      $"Akşam: { (gunlukOlcumler.ContainsKey("Akşam") ? gunlukOlcumler["Akşam"].ToString() + " mg/dL" : "Girilmedi") }\n" +
                      $"Gece: { (gunlukOlcumler.ContainsKey("Gece") ? gunlukOlcumler["Gece"].ToString() + " mg/dL" : "Girilmedi") }";
    }
    catch (Exception ex)
    {
        displayText = "Ölçüm verileri yüklenirken hata oluştu.";
        MessageBox.Show($"Ölçüm verileri yüklenirken hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    finally
    {
        lblDetay.Text = displayText;
    }}
    private void BtnHesaplaVeOner_Click(object sender, EventArgs e)
{
    // Önce güncel ölçümleri tekrar yükle (kullanıcı arada başka sekmede kayıt yapmış olabilir)
    GununKanSekeriOlcumleriniYukle();

    ComboBox cmbOgun = (ComboBox)this.Controls.Find("cmbDegerlendirmeOgunu", true).FirstOrDefault();
    Label lblOrtalamaSonuc = (Label)this.Controls.Find("lblHesaplananOrtalamaSonuc", true).FirstOrDefault();
    Label lblOneriSonuc = (Label)this.Controls.Find("lblInsulinOnerisiSonuc", true).FirstOrDefault();
    RichTextBox txtUyariMesajiSonuc = (RichTextBox)this.Controls.Find("txtUyariMesajiSonuc", true).FirstOrDefault();

    if (cmbOgun == null || lblOrtalamaSonuc == null || lblOneriSonuc == null || txtUyariMesajiSonuc == null)
    {
        MessageBox.Show("Arayüz elemanları bulunamadı.", "Kritik Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
    }

    lblOrtalamaSonuc.Text = "Ortalama Kan Şekeri: -";
    lblOneriSonuc.Text = "İnsülin Önerisi: -";
    txtUyariMesajiSonuc.Text = "Uyarılar: -";
    List<string> uyariMesajlariListesi = new List<string>();

    string secilenOgun = cmbOgun.SelectedItem.ToString();
    List<double> ortalamayaKatilacakDegerler = new List<double>();
    List<string> olcumTipleri = new List<string>();
    int toplamGecerliOlcumSayisiBuHesaplamaIcin = 0;

    // İlgili öğüne kadar olan ZAMANINDA ölçümleri al
    if (secilenOgun == "Sabah" || secilenOgun == "Öğlen" || secilenOgun == "İkindi" || secilenOgun == "Akşam" || secilenOgun == "Gece")
    {
        if (gunlukOlcumler.ContainsKey("Sabah")) { ortalamayaKatilacakDegerler.Add(gunlukOlcumler["Sabah"]); olcumTipleri.Add("Sabah"); }
        else if (secilenOgun != "Sabah") uyariMesajlariListesi.Add("Sabah ölçümü eksik! Ortalama hesaplanırken dikkate alınmadı.");
    }
    if (secilenOgun == "Öğlen" || secilenOgun == "İkindi" || secilenOgun == "Akşam" || secilenOgun == "Gece")
    {
        if (gunlukOlcumler.ContainsKey("Öğle")) { ortalamayaKatilacakDegerler.Add(gunlukOlcumler["Öğle"]); olcumTipleri.Add("Öğle"); }
        else if (secilenOgun != "Öğlen") uyariMesajlariListesi.Add("Öğlen ölçümü eksik! Ortalama hesaplanırken dikkate alınmadı.");
    }
    if (secilenOgun == "İkindi" || secilenOgun == "Akşam" || secilenOgun == "Gece")
    {
        if (gunlukOlcumler.ContainsKey("İkindi")) { ortalamayaKatilacakDegerler.Add(gunlukOlcumler["İkindi"]); olcumTipleri.Add("İkindi"); }
        else if (secilenOgun != "İkindi") uyariMesajlariListesi.Add("İkindi ölçümü eksik! Ortalama hesaplanırken dikkate alınmadı.");
    }
    if (secilenOgun == "Akşam" || secilenOgun == "Gece")
    {
        if (gunlukOlcumler.ContainsKey("Akşam")) { ortalamayaKatilacakDegerler.Add(gunlukOlcumler["Akşam"]); olcumTipleri.Add("Akşam"); }
        else if (secilenOgun != "Akşam") uyariMesajlariListesi.Add("Akşam ölçümü eksik! Ortalama hesaplanırken dikkate alınmadı.");
    }
    if (secilenOgun == "Gece")
    {
        if (gunlukOlcumler.ContainsKey("Gece")) { ortalamayaKatilacakDegerler.Add(gunlukOlcumler["Gece"]); olcumTipleri.Add("Gece"); }
        else uyariMesajlariListesi.Add("Gece ölçümü eksik! Ortalama hesaplanırken dikkate alınmadı.");
    }

    // Seçilen öğüne göre listeyi kırp
    List<double> sonListe = new List<double>();
    if (secilenOgun == "Sabah" && gunlukOlcumler.ContainsKey("Sabah")) sonListe.Add(gunlukOlcumler["Sabah"]);
    if (secilenOgun == "Öğlen") {
        if(gunlukOlcumler.ContainsKey("Sabah")) sonListe.Add(gunlukOlcumler["Sabah"]);
        if(gunlukOlcumler.ContainsKey("Öğle")) sonListe.Add(gunlukOlcumler["Öğle"]);
    }
    if (secilenOgun == "İkindi") {
        if(gunlukOlcumler.ContainsKey("Sabah")) sonListe.Add(gunlukOlcumler["Sabah"]);
        if(gunlukOlcumler.ContainsKey("Öğle")) sonListe.Add(gunlukOlcumler["Öğle"]);
        if(gunlukOlcumler.ContainsKey("İkindi")) sonListe.Add(gunlukOlcumler["İkindi"]);
    }
    if (secilenOgun == "Akşam") {
        if(gunlukOlcumler.ContainsKey("Sabah")) sonListe.Add(gunlukOlcumler["Sabah"]);
        if(gunlukOlcumler.ContainsKey("Öğle")) sonListe.Add(gunlukOlcumler["Öğle"]);
        if(gunlukOlcumler.ContainsKey("İkindi")) sonListe.Add(gunlukOlcumler["İkindi"]);
        if(gunlukOlcumler.ContainsKey("Akşam")) sonListe.Add(gunlukOlcumler["Akşam"]);
    }
     if (secilenOgun == "Gece") {
        if(gunlukOlcumler.ContainsKey("Sabah")) sonListe.Add(gunlukOlcumler["Sabah"]);
        if(gunlukOlcumler.ContainsKey("Öğle")) sonListe.Add(gunlukOlcumler["Öğle"]);
        if(gunlukOlcumler.ContainsKey("İkindi")) sonListe.Add(gunlukOlcumler["İkindi"]);
        if(gunlukOlcumler.ContainsKey("Akşam")) sonListe.Add(gunlukOlcumler["Akşam"]);
        if(gunlukOlcumler.ContainsKey("Gece")) sonListe.Add(gunlukOlcumler["Gece"]);
    }

    toplamGecerliOlcumSayisiBuHesaplamaIcin = sonListe.Count;

    if (toplamGecerliOlcumSayisiBuHesaplamaIcin == 0)
    {
        lblOrtalamaSonuc.Text = $"Ortalama ({secilenOgun}): Veri Yok";
        lblOneriSonuc.Text = "İnsülin Önerisi: Veri Yok";
        uyariMesajlariListesi.Add($"{secilenOgun} için ortalama hesaplanacak geçerli ölçüm bulunamadı.");
        txtUyariMesajiSonuc.Text = "Uyarılar:\n" + string.Join("\n", uyariMesajlariListesi);
        return;
    }

    double ortalamaKanSekeri = sonListe.Average();
    lblOrtalamaSonuc.Text = $"Ortalama ({secilenOgun}): {ortalamaKanSekeri:F2} mg/dL";

    string insulinOnerisiStr;
    double insulinOnerisiMl = 0;

    if (ortalamaKanSekeri < 70) { insulinOnerisiStr = "Yok (Hipoglisemi)"; insulinOnerisiMl = 0; }
    else if (ortalamaKanSekeri <= 110) { insulinOnerisiStr = "Yok (Normal)"; insulinOnerisiMl = 0; }
    else if (ortalamaKanSekeri <= 150) { insulinOnerisiStr = "1 ml (Orta Yüksek)"; insulinOnerisiMl = 1; }
    else if (ortalamaKanSekeri <= 200) { insulinOnerisiStr = "2 ml (Yüksek)"; insulinOnerisiMl = 2; }
    else { insulinOnerisiStr = "3 ml (Çok Yüksek)"; insulinOnerisiMl = 3; }
    lblOneriSonuc.Text = $"İnsülin Önerisi: {insulinOnerisiStr}";

    // Yetersiz Veri Uyarısı (Genel olarak o gün girilen ZAMANINDA ölçüm sayısına bakılır)
    if (gunlukOlcumler.Count <= 3)
    {
        uyariMesajlariListesi.Add("Yetersiz veri (bugün için toplamda 3 veya daha az zamanında ölçüm girildi)! Ortalama hesaplaması güvenilir değildir.");
    }

    if (uyariMesajlariListesi.Any())
    {
        txtUyariMesajiSonuc.Text = "Uyarılar:\n" + string.Join("\n", uyariMesajlariListesi);
    }
    else
    {
        txtUyariMesajiSonuc.Text = "Uyarılar: Yok";
    }

    try
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "INSERT INTO InsulinOnerileri (HastaID, Tarih, OrtalamaDeger, OnerilenDozML, HesaplananOgun) " +
                           "VALUES (@HastaID, @Tarih, @OrtalamaDeger, @OnerilenDoz, @HesaplananOgun)";  
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@HastaID", hastaID);
            cmd.Parameters.AddWithValue("@Tarih", DateTime.Now);
            cmd.Parameters.AddWithValue("@OrtalamaDeger", Math.Round(ortalamaKanSekeri, 2));
            cmd.Parameters.AddWithValue("@OnerilenDoz", insulinOnerisiMl);
            cmd.Parameters.AddWithValue("@HesaplananOgun", secilenOgun);
            cmd.ExecuteNonQuery();
        }
        MessageBox.Show($"{secilenOgun} için insülin önerisi başarıyla hesaplandı ve kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        LoadInsulinTakibi(); // DataGridView'i güncelle
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Veritabanına kaydetme sırasında hata: {ex.Message}", "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
        private void ProfilFotografiniYukle()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT ProfilResmi FROM Kullanicilar WHERE KullaniciID = @KullaniciID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@KullaniciID", hastaID);
                    object result = cmd.ExecuteScalar();

                    // Eski resmi temizle (varsa)
                    if (profilFotoBox.Image != null)
                    {
                        profilFotoBox.Image.Dispose();
                        profilFotoBox.Image = null;
                    }

                    if (result != null && result != DBNull.Value)
                    {
                        byte[] resimBytes = (byte[])result; // Byte dizisi olarak oku
                        using (MemoryStream ms = new MemoryStream(resimBytes))
                        {
                            profilFotoBox.Image = new Bitmap(Image.FromStream(ms)); // Byte dizisinden resim oluştur
                        }
                    }
                    else
                    {
                        profilFotoBox.Image = null; // Veritabanında kayıt yoksa PictureBox'ı temizle
                    }
                }
                catch (Exception ex)
                {
                    profilFotoBox.Image = null; // Hata oluşursa PictureBox'ı temizle
                }
            }
        }

        private void BtnFotoDegistir_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog.Title = "Profil Fotoğrafı Seç";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Seçilen dosyayı byte dizisine oku
                        byte[] resimBytes = File.ReadAllBytes(openFileDialog.FileName);

                        // Veritabanını güncelle - Byte dizisini kaydet
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            string query = "UPDATE Kullanicilar SET ProfilResmi = @ProfilResmi WHERE KullaniciID = @KullaniciID";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@ProfilResmi", resimBytes); // Byte dizisini parametre olarak ekle
                            cmd.Parameters.AddWithValue("@KullaniciID", hastaID);
                            cmd.ExecuteNonQuery();
                        }

                        // PictureBox'ı yeni resimle güncelle (dosya yolundan değil, byte dizisinden)
                        using (MemoryStream ms = new MemoryStream(resimBytes))
                        {
                             profilFotoBox.Image = new Bitmap(Image.FromStream(ms));
                        }

                        MessageBox.Show("Profil fotoğrafı başarıyla güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Fotoğraf yüklenirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void ProfilFotoBox_Click(object sender, EventArgs e)
        {
            if (profilFotoBox.Image != null)
            {
                Form fotoForm = new Form();
                fotoForm.Text = "Profil Fotoğrafı";
                fotoForm.Size = new Size(400, 400);
                fotoForm.StartPosition = FormStartPosition.CenterScreen;

                PictureBox buyukFoto = new PictureBox();
                buyukFoto.Dock = DockStyle.Fill;
                buyukFoto.SizeMode = PictureBoxSizeMode.Zoom;
                buyukFoto.Image = profilFotoBox.Image;

                fotoForm.Controls.Add(buyukFoto);
                fotoForm.ShowDialog();
            }
        }

        private void LoadData()
        {
            LoadKanSekeriTakibi();
            LoadDiyetTakibi();
            LoadEgzersizTakibi();
            LoadInsulinTakibi();
            GununKanSekeriOlcumleriniYukle();
            HesaplaVeGosterUygulamaOranlari(hastaID);
        }

        private void LoadKanSekeriTakibi()
{
    try
    {
        // dgvKanSekeri null kontrolü
        if (dgvKanSekeri == null)
        {
            MessageBox.Show("DataGridView referansı bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string query = "SELECT TarihSaat, Deger, ZamanindaMi, OlcumZamaniTipi FROM KanSekeriOlcumleri WHERE HastaID = @HastaID ORDER BY TarihSaat DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@HastaID", hastaID);

                DataTable dt = new DataTable();
                da.Fill(dt);
                
                // DataSource ataması öncesi mevcut veriyi temizle
                dgvKanSekeri.DataSource = null;
                dgvKanSekeri.DataSource = dt;
                
                // Sütun başlıklarını ayarla - null kontrolü ekle
                if (dgvKanSekeri.Columns["TarihSaat"] != null)
                    dgvKanSekeri.Columns["TarihSaat"].HeaderText = "Tarih/Saat";
                
                if (dgvKanSekeri.Columns["Deger"] != null)
                    dgvKanSekeri.Columns["Deger"].HeaderText = "Kan Şekeri";
                
                if (dgvKanSekeri.Columns["ZamanindaMi"] != null)
                    dgvKanSekeri.Columns["ZamanindaMi"].HeaderText = "Zamanında mı?";
                
                if (dgvKanSekeri.Columns["OlcumZamaniTipi"] != null)
                    dgvKanSekeri.Columns["OlcumZamaniTipi"].HeaderText = "Ölçüm Tipi";

                // Zaman dışı olanları kırmızı yapalım - iyileştirilmiş kontrol
                foreach (DataGridViewRow row in dgvKanSekeri.Rows)
                {
                    if (row?.Cells["ZamanindaMi"]?.Value != null && 
                        row.Cells["ZamanindaMi"].Value != DBNull.Value && 
                        !(bool)row.Cells["ZamanindaMi"].Value)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightPink;
                    }
                }

                // 📌 Ortalama Kan Şekeri Hesaplama ve Gösterme
                Label lblOrtalama = (Label)this.Controls.Find("lblOrtalamaKanSekeri", true).FirstOrDefault();
                if (lblOrtalama != null && dt.Rows.Count > 0)
                {
                    double toplam = 0;
                    int gecerliSayi = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        // DBNull kontrolü ekleyelim
                        if (row["Deger"] != DBNull.Value && row["Deger"] is int)
                        {
                            toplam += Convert.ToInt32(row["Deger"]);
                            gecerliSayi++;
                        }
                    }

                    if (gecerliSayi > 0)
                    {
                        double ortalama = toplam / gecerliSayi;
                        lblOrtalama.Text = $"Ortalama Kan Şekeri: {ortalama:F2} mg/dL";
                    }
                    else
                    {
                         lblOrtalama.Text = "Ortalama Kan Şekeri: Veri Yok";
                    }
                }
                 else if (lblOrtalama != null)
                 {
                     lblOrtalama.Text = "Ortalama Kan Şekeri: Veri Yok";
                 }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Veritabanı hatası: {sqlEx.Message}", "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Kan şekeri verileri yüklenirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
        private void LoadDiyetTakibi()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Tarih, DiyetTuru, CASE WHEN UygulandiMi = 1 THEN 'Evet' ELSE 'Hayır' END as Uygulandi FROM DiyetTakipleri WHERE HastaID = @HastaID ORDER BY Tarih DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@HastaID", hastaID);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                DataGridView dgv = (DataGridView)this.Controls.Find("dgvDiyet", true)[0];
                dgv.DataSource = dt;
            }
        }

        private void LoadEgzersizTakibi()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Tarih, EgzersizTuru, CASE WHEN YapildiMi = 1 THEN 'Evet' ELSE 'Hayır' END as Yapildi FROM EgzersizTakipleri WHERE HastaID = @HastaID ORDER BY Tarih DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@HastaID", hastaID);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                DataGridView dgv = (DataGridView)this.Controls.Find("dgvEgzersiz", true)[0];
                dgv.DataSource = dt;
            }
        }

        private void LoadInsulinTakibi(DateTime? filterDate = null)
{
    DataGridView dgvInsulinGrid = (DataGridView)this.Controls.Find("dgvInsulin", true).FirstOrDefault();
    if (dgvInsulinGrid == null) return;

    try
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT Tarih, HesaplananOgun, OrtalamaDeger, OnerilenDozML FROM InsulinOnerileri WHERE HastaID = @HastaID";
            
            // 📌 Tarih filtrelemesi ekle
            if (filterDate.HasValue)
            {
                query += " AND CAST(Tarih AS DATE) = CAST(@FilterDate AS DATE)";
            }

            query += " ORDER BY Tarih DESC";

            SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@HastaID", hastaID);
            
            if (filterDate.HasValue)
            {
                 adapter.SelectCommand.Parameters.AddWithValue("@FilterDate", filterDate.Value.Date);
            }

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dgvInsulinGrid.DataSource = dt; // DataSource'u doğrudan ata

            // Sütun başlıkları (DataTable'dan otomatik alır ama isterseniz özelleştirebilirsiniz)
            if (dt.Columns.Contains("HesaplananOgun") && dgvInsulinGrid.Columns["HesaplananOgun"] != null)
                dgvInsulinGrid.Columns["HesaplananOgun"].HeaderText = "Hes. Öğün";
            if (dt.Columns.Contains("OrtalamaDeger") && dgvInsulinGrid.Columns["OrtalamaDeger"] != null)
                dgvInsulinGrid.Columns["OrtalamaDeger"].HeaderText = "Ort. KŞ";
            if (dt.Columns.Contains("OnerilenDozML") && dgvInsulinGrid.Columns["OnerilenDozML"] != null)
                dgvInsulinGrid.Columns["OnerilenDozML"].HeaderText = "Öneri (ml)";
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"İnsülin takip verileri yüklenirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
private (bool zamanindaMi, string olcumTipi) OlcumSaatiniKontrolEt(DateTime now)

        {

            TimeSpan saat = now.TimeOfDay;


            if (saat >= TimeSpan.FromHours(7) && saat < TimeSpan.FromHours(8))

                return (true, "Sabah");

            if (saat >= TimeSpan.FromHours(12) && saat < TimeSpan.FromHours(13))

                return (true, "Öğle");

            if (saat >= TimeSpan.FromHours(15) && saat < TimeSpan.FromHours(16))

                return (true, "İkindi");

            if (saat >= TimeSpan.FromHours(18) && saat < TimeSpan.FromHours(19))

                return (true, "Akşam");

            if (saat >= TimeSpan.FromHours(22) && saat < TimeSpan.FromHours(23))

                return (true, "Gece");


            return (false, "Zaman Dışı");

        }
        private void BtnKanSekeriKaydet_Click(object sender, EventArgs e)
        {

            TextBox txtKanSekeri = (TextBox)this.Controls.Find("txtKanSekeri", true)[0];
            if (!int.TryParse(txtKanSekeri.Text, out int kanSekeri))
            {
                MessageBox.Show("Lütfen geçerli bir kan şekeri değeri giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int deger = int.Parse(txtKanSekeri.Text);
            if (deger < 30 || deger > 600)
            {
                MessageBox.Show("Kan şekeri değeri 30 ile 600 arasında olmalıdır.", "Geçersiz Değer", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime simdi = DateTime.Now;
            (bool zamanindaMi, string olcumTipi) = OlcumSaatiniKontrolEt(simdi);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO KanSekeriOlcumleri (HastaID, TarihSaat, Deger, ZamanindaMi, OlcumZamaniTipi) " +
                            "VALUES (@HastaID, @TarihSaat, @Deger, @ZamanindaMi, @OlcumZamaniTipi)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@HastaID", hastaID);
                cmd.Parameters.AddWithValue("@TarihSaat", simdi);
                cmd.Parameters.AddWithValue("@Deger", kanSekeri);
                cmd.Parameters.AddWithValue("@ZamanindaMi", zamanindaMi);
                cmd.Parameters.AddWithValue("@OlcumZamaniTipi", olcumTipi);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Kan şekeri değeri başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtKanSekeri.Clear();
            LoadKanSekeriTakibi();
            UyarilariOlustur(hastaID);  // Otomatik uyarı oluştur

            GununKanSekeriOlcumleriniYukle();
        }


        private void BtnDiyetUygulandi_Click(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)this.Controls.Find("dgvDiyet", true)[0];
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen işaretlemek istediğiniz diyet kaydını seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow row = dgv.SelectedRows[0];
            DateTime tarih = Convert.ToDateTime(row.Cells["Tarih"].Value);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE DiyetTakipleri SET UygulandiMi = 1 WHERE HastaID = @HastaID AND Tarih = @Tarih";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@HastaID", hastaID);
                cmd.Parameters.AddWithValue("@Tarih", tarih);
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Diyet uygulandı olarak işaretlendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadDiyetTakibi();
        }

        private void BtnEgzersizYapildi_Click(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)this.Controls.Find("dgvEgzersiz", true)[0];
            if (dgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen işaretlemek istediğiniz egzersiz kaydını seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow row = dgv.SelectedRows[0];
            DateTime tarih = Convert.ToDateTime(row.Cells["Tarih"].Value);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE EgzersizTakipleri SET YapildiMi = 1 WHERE HastaID = @HastaID AND Tarih = @Tarih";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@HastaID", hastaID);
                cmd.Parameters.AddWithValue("@Tarih", tarih);
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Egzersiz yapıldı olarak işaretlendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadEgzersizTakibi();
        }
        private void UyarilariOlustur(int hastaID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Önce eski uyarıları sil
                string silQuery = "DELETE FROM Uyarilar WHERE HastaID = @HastaID";
                using (SqlCommand silCmd = new SqlCommand(silQuery, conn))
                {
                    silCmd.Parameters.AddWithValue("@HastaID", hastaID);
                    silCmd.ExecuteNonQuery();
                }

                DateTime son7Gun = DateTime.Today.AddDays(-7);

                // 1️⃣ Kan şekeri ölçümlerine göre uyarılar
                string olcumQuery = @"SELECT TarihSaat, Deger FROM KanSekeriOlcumleri
                              WHERE HastaID = @HastaID AND TarihSaat >= @Tarih";
                using (SqlCommand cmd = new SqlCommand(olcumQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@HastaID", hastaID);
                    cmd.Parameters.AddWithValue("@Tarih", son7Gun);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<(DateTime tarih, TimeSpan saat, int deger)> olcumler = new List<(DateTime, TimeSpan, int)>();
                        while (reader.Read())
                        {
                            DateTime tarihSaat = reader.GetDateTime(0);
                            int deger = reader.GetInt32(1);
                            olcumler.Add((tarihSaat.Date, tarihSaat.TimeOfDay, deger));
                        }

                        reader.Close(); // ✅ Kapatmadan diğer query çalıştırılamaz

                        foreach (var (tarih, saat, deger) in olcumler)
                        {
                            string uyariTipi = "";
                            string mesaj = "";

                            if (deger < 70)
                            {
                                uyariTipi = "Acil Uyarı";
                                mesaj = "Hastanın kan şekeri seviyesi 70 mg/dL'nin altına düştü. Hipoglisemi riski! Hızlı müdahale gerekebilir.";
                            }
                            else if (deger <= 110)
                            {
                                uyariTipi = "Uyarı Yok";
                                mesaj = "Kan şekeri seviyesi normal aralıkta. Hiçbir işlem gerekmez.";
                            }
                            else if (deger <= 150)
                            {
                                uyariTipi = "Takip Uyarısı";
                                mesaj = "Hastanın kan şekeri 111-150 mg/dL arasında. Durum izlenmeli.";
                            }
                            else if (deger <= 200)
                            {
                                uyariTipi = "İzleme Uyarısı";
                                mesaj = "Hastanın kan şekeri 151-200 mg/dL arasında. Diyabet kontrolü gereklidir.";
                            }
                            else
                            {
                                uyariTipi = "Acil Müdahale Uyarısı";
                                mesaj = "Hastanın kan şekeri 200 mg/dL'nin üzerinde. Hiperglisemi durumu. Acil müdahale gerekebilir.";
                            }

                            string insert = @"INSERT INTO Uyarilar (HastaID, Tarih, Saat, UyariTipi, Mesaj)
                                      VALUES (@HastaID, @Tarih, @Saat, @UyariTipi, @Mesaj)";
                            using (SqlCommand insertCmd = new SqlCommand(insert, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@HastaID", hastaID);
                                insertCmd.Parameters.AddWithValue("@Tarih", tarih);
                                insertCmd.Parameters.AddWithValue("@Saat", saat);
                                insertCmd.Parameters.AddWithValue("@UyariTipi", uyariTipi);
                                insertCmd.Parameters.AddWithValue("@Mesaj", mesaj);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }

                // 2️⃣ Ölçüm eksikliği (hiç giriş yok)
                string eksikQuery = @"SELECT Tarih
                              FROM (SELECT TOP 7 DATEADD(DAY, -number, CAST(GETDATE() AS DATE)) AS Tarih
                                    FROM master..spt_values WHERE type = 'P') AS Gunler
                              WHERE NOT EXISTS (
                                  SELECT 1 FROM KanSekeriOlcumleri 
                                  WHERE HastaID = @HastaID AND CAST(TarihSaat AS DATE) = Gunler.Tarih
                              )";
                using (SqlCommand eksikCmd = new SqlCommand(eksikQuery, conn))
                {
                    eksikCmd.Parameters.AddWithValue("@HastaID", hastaID);
                    using (SqlDataReader eksikReader = eksikCmd.ExecuteReader())
                    {
                        List<DateTime> eksikTarihler = new List<DateTime>();
                        while (eksikReader.Read())
                        {
                            eksikTarihler.Add(eksikReader.GetDateTime(0));
                        }
                        eksikReader.Close();

                        foreach (var eksikTarih in eksikTarihler)
                        {
                            string insert = @"INSERT INTO Uyarilar (HastaID, Tarih, Saat, UyariTipi, Mesaj)
                                      VALUES (@HastaID, @Tarih, @Saat, @UyariTipi, @Mesaj)";
                            using (SqlCommand insertCmd = new SqlCommand(insert, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@HastaID", hastaID);
                                insertCmd.Parameters.AddWithValue("@Tarih", eksikTarih);
                                insertCmd.Parameters.AddWithValue("@Saat", new TimeSpan(0, 0, 0));
                                insertCmd.Parameters.AddWithValue("@UyariTipi", "Ölçüm Eksik Uyarısı");
                                insertCmd.Parameters.AddWithValue("@Mesaj", "Hasta gün boyunca kan şekeri ölçümü yapmamıştır. Acil takip önerilir.");
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }

                // 3️⃣ Ölçüm yetersizliği (<3 giriş varsa)
                string yetersizQuery = @"
            SELECT CAST(TarihSaat AS DATE) AS Tarih
            FROM KanSekeriOlcumleri
            WHERE HastaID = @HastaID AND TarihSaat >= @Tarih
            GROUP BY CAST(TarihSaat AS DATE)
            HAVING COUNT(*) < 3";

                using (SqlCommand yetersizCmd = new SqlCommand(yetersizQuery, conn))
                {
                    yetersizCmd.Parameters.AddWithValue("@HastaID", hastaID);
                    yetersizCmd.Parameters.AddWithValue("@Tarih", son7Gun);

                    using (SqlDataReader yetersizReader = yetersizCmd.ExecuteReader())
                    {
                        List<DateTime> yetersizTarihler = new List<DateTime>();
                        while (yetersizReader.Read())
                        {
                            yetersizTarihler.Add(yetersizReader.GetDateTime(0));
                        }
                        yetersizReader.Close();

                        foreach (var t in yetersizTarihler)
                        {
                            string insert = @"INSERT INTO Uyarilar (HastaID, Tarih, Saat, UyariTipi, Mesaj)
                                      VALUES (@HastaID, @Tarih, @Saat, @UyariTipi, @Mesaj)";
                            using (SqlCommand insertCmd = new SqlCommand(insert, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@HastaID", hastaID);
                                insertCmd.Parameters.AddWithValue("@Tarih", t);
                                insertCmd.Parameters.AddWithValue("@Saat", new TimeSpan(0, 0, 0));
                                insertCmd.Parameters.AddWithValue("@UyariTipi", "Ölçüm Yetersiz Uyarısı");
                                insertCmd.Parameters.AddWithValue("@Mesaj", "Hastanın günlük kan şekeri ölçüm sayısı yetersiz (<3). Durum izlenmelidir.");
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
        }
      



        private void BtnFilterInsulinByDate_Click(object sender, EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)this.Controls.Find("dtpInsulinFilter", true).FirstOrDefault();
            if (dtp != null)
            {
                LoadInsulinTakibi(dtp.Value); // Seçilen tarihi kullanarak veriyi yükle
            }
        }

        private void HesaplaVeGosterUygulamaOranlari(int hastaID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Diyet verisi
                string diyetQuery = @"SELECT COUNT(*) AS Toplam,
                                         SUM(CASE WHEN UygulandiMi = 1 THEN 1 ELSE 0 END) AS Uygulanan
                                  FROM DiyetTakipleri
                                  WHERE HastaID = @HastaID AND Tarih >= @Tarih";
                SqlCommand cmdDiyet = new SqlCommand(diyetQuery, conn);
                cmdDiyet.Parameters.AddWithValue("@HastaID", hastaID);
                cmdDiyet.Parameters.AddWithValue("@Tarih", DateTime.Today.AddDays(-7));

                int toplamDiyet = 0, uygulananDiyet = 0;
                using (SqlDataReader reader = cmdDiyet.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        toplamDiyet = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                        uygulananDiyet = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                    }
                }

                int diyetYuzde = (toplamDiyet > 0) ? (uygulananDiyet * 100 / toplamDiyet) : 0;
                progressBarDiyet.Value = diyetYuzde;
                lblDiyetYuzde.Text = $"Diyet Uyum: %{diyetYuzde}";

                // Egzersiz verisi
                string egzersizQuery = @"SELECT COUNT(*) AS Toplam,
                                            SUM(CASE WHEN YapildiMi = 1 THEN 1 ELSE 0 END) AS Yapilan
                                     FROM EgzersizTakipleri
                                     WHERE HastaID = @HastaID AND Tarih >= @Tarih";
                SqlCommand cmdEgzersiz = new SqlCommand(egzersizQuery, conn);
                cmdEgzersiz.Parameters.AddWithValue("@HastaID", hastaID);
                cmdEgzersiz.Parameters.AddWithValue("@Tarih", DateTime.Today.AddDays(-7));

                int toplamEgzersiz = 0, yapilanEgzersiz = 0;
                using (SqlDataReader reader = cmdEgzersiz.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        toplamEgzersiz = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                        yapilanEgzersiz = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                    }
                }

                int egzersizYuzde = (toplamEgzersiz > 0) ? (yapilanEgzersiz * 100 / toplamEgzersiz) : 0;
                progressBarEgzersiz.Value = egzersizYuzde;
                lblEgzersizYuzde.Text = $"Egzersiz Uyum: %{egzersizYuzde}";
            }
        }
    }
}
