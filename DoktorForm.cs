using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Linq;
using System.Security.Cryptography;
using LiveCharts;
using LiveCharts.WinForms;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using LiveCharts.WinForms; // Grafik için

namespace diyabetapp
{
    public partial class DoktorForm : Form
    {
        private string connectionString = "Server=localhost\\SQLEXPRESS;Database=DiyabetDB;Trusted_Connection=True;Encrypt=False;";
        private int selectedHastaID = -1;
        private string profilResmiYolu = "";
        private int aktifDoktorID ;
       


        public DoktorForm(int doktorID)
        {
            InitializeComponent();
            // Hasta bilgi etiketlerinin konumunu sabitliyorum
            lblAdSoyadEtiket.Location = new Point(300, 30);
            lblAdSoyadDeger.Location = new Point(400, 30);
            lblEmailEtiket.Location = new Point(300, 60);
            lblEmailDeger.Location = new Point(400, 60);
            lblTcEtiket.Location = new Point(300, 90);
            lblTcDeger.Location = new Point(400, 90);
            lblCinsiyetEtiket.Location = new Point(300, 120);
            lblCinsiyetDeger.Location = new Point(400, 120);
            lblDogumEtiket.Location = new Point(300, 150);
            lblDogumDeger.Location = new Point(400, 150);
            aktifDoktorID = doktorID;
            HastalariGetir();
            this.ClientSize = new System.Drawing.Size(1394, 725);
            this.Controls.Add(tabControl1);
            this.Name = "DoktorForm";
            this.Text = "Doktor Ekranı";
        }

        private void HastalariGetir()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT KullaniciID, AdSoyad FROM Kullanicilar WHERE Rol = 'Hasta' AND EkleyenDoktorID = @aktifDoktorID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@aktifDoktorID", aktifDoktorID); // bu değişkenin yukarıda tanımlı olduğuna dikkat

                SqlDataReader reader = cmd.ExecuteReader();
                cmbHastaSec.Items.Clear();
                cmbUyariHastaSec.Items.Clear(); // <- bu eksikse tab 3 boş kalır

                while (reader.Read())
                {
                    ComboBoxItem item = new ComboBoxItem
                    {
                        Text = reader["AdSoyad"].ToString(),
                        Value = reader["KullaniciID"].ToString()
                    };

                    cmbHastaSec.Items.Add(item);
                    cmbUyariHastaSec.Items.Add(item); // <- tab3'teki ComboBox da dolacak
                }

                reader.Close();
            }
        }
        private void DoktorForm_Load(object sender, EventArgs e)
        {
            // cmbKanSekeri.Items.AddRange(new string[]
            // {
            // "Hipoglisemi (<70)",
            // "Normal (70–110)",
            // "Orta Yüksek (111–150)",
            // "Yüksek (151–200)",
            // "Hiperglisemi (>200)"
            // });

            // cmbBelirti.Items.Clear();

            // using (SqlConnection conn = new SqlConnection(connectionString))
            // {
            //     conn.Open();
            //     SqlCommand cmd = new SqlCommand("SELECT DISTINCT Belirtiler.BelirtiTuru FROM Belirtiler", conn);
            //     SqlDataReader reader = cmd.ExecuteReader();
            //     while (reader.Read())
            //     {
            //         cmbBelirti.Items.Add(reader.GetString(0));
            //     }
            // }
        }

        private void btnDiyetEkle_Click(object sender, EventArgs e)
        {
            if (selectedHastaID <= 0)
            {
                MessageBox.Show("Lütfen önce bir hasta seçin.");
                return;
            }

            string diyetTuru = cmbDiyet.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(diyetTuru))
            {
                MessageBox.Show("Lütfen bir diyet türü seçin.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO DiyetTakipleri (HastaID, Tarih, DiyetTuru, UygulandiMi) VALUES (@HastaID, @Tarih, @DiyetTuru, @UygulandiMi)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@HastaID", selectedHastaID);
                cmd.Parameters.AddWithValue("@Tarih", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@DiyetTuru", diyetTuru);
                cmd.Parameters.AddWithValue("@UygulandiMi", false); // başlangıçta uygulanmadı varsay
                cmd.ExecuteNonQuery();  // <-- EKLENMELİ

                MessageBox.Show("Diyet kaydı eklendi.");
                LoadDiyet(selectedHastaID);  // tabloyu güncelle
            }
        }


        private void btnEgzersizEkle_Click(object sender, EventArgs e)
        {
            if (selectedHastaID <= 0)
            {
                MessageBox.Show("Lütfen önce bir hasta seçin.");
                return;
            }

            string egzersizTuru = cmbEgzersiz.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(egzersizTuru))
            {
                MessageBox.Show("Lütfen bir egzersiz türü seçin.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO EgzersizTakipleri (HastaID, Tarih, EgzersizTuru, YapildiMi) VALUES (@HastaID, @Tarih, @EgzersizTuru, @YapildiMi)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@HastaID", selectedHastaID);
                cmd.Parameters.AddWithValue("@Tarih", DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@EgzersizTuru", egzersizTuru);
                cmd.Parameters.AddWithValue("@YapildiMi", false); // Başlangıçta yapılmadı olarak kaydet
                cmd.ExecuteNonQuery();  // <-- EKLENMELİ

                MessageBox.Show("Egzersiz kaydı eklendi.");
                LoadEgzersiz(selectedHastaID);  // tabloyu güncelle
            }
        }



        private void btnHastaEkle_Click(object sender, EventArgs e)
        {
            string adSoyad = txtAdSoyad.Text.Trim();
            string tcNo = txtTcNo.Text.Trim();
            string email = txtEmail.Text.Trim();
            string sifre = txtSifre.Text.Trim();
            string cinsiyet = cmbCinsiyet.SelectedItem?.ToString();
            DateTime dogumTarihi = dateDogumTarihi.Value;

            if (string.IsNullOrEmpty(adSoyad) || string.IsNullOrEmpty(tcNo) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(sifre) || string.IsNullOrEmpty(cinsiyet) || string.IsNullOrEmpty(profilResmiYolu))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!");
                return;
            }

            // ✅ TC No 11 haneli mi ve sadece rakamlardan mı oluşuyor?
            if (tcNo.Length != 11 || !tcNo.All(char.IsDigit))
            {
                MessageBox.Show("TC No 11 haneli olmalı ve sadece rakamlardan oluşmalıdır!", "Geçersiz TC", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string sifreHash = HashSHA256(sifre);
            byte[] resimBytes = File.ReadAllBytes(profilResmiYolu);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // ✅ Aynı TC daha önce kayıtlı mı kontrol et
                    string kontrolQuery = "SELECT COUNT(*) FROM Kullanicilar WHERE TcNo = @TcNo";
                    SqlCommand kontrolCmd = new SqlCommand(kontrolQuery, conn);
                    kontrolCmd.Parameters.AddWithValue("@TcNo", tcNo);
                    int tcVarMi = (int)kontrolCmd.ExecuteScalar();

                    if (tcVarMi > 0)
                    {
                        MessageBox.Show("Bu TC numarasıyla zaten bir hasta kayıtlı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // ✅ Kayıt işlemi
                    string query = "INSERT INTO Kullanicilar (AdSoyad, TcNo, Email, DogumTarihi, Cinsiyet, SifreHash, Rol, ProfilResmi, EkleyenDoktorID) " +
                    "VALUES (@AdSoyad, @TcNo, @Email, @DogumTarihi, @Cinsiyet, @SifreHash, 'Hasta', @ProfilResmi, @EkleyenDoktorID)";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@AdSoyad", adSoyad);
                        cmd.Parameters.AddWithValue("@TcNo", tcNo);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@DogumTarihi", dogumTarihi);
                        cmd.Parameters.AddWithValue("@Cinsiyet", cinsiyet);
                        cmd.Parameters.AddWithValue("@SifreHash", sifreHash);
                        cmd.Parameters.Add("@ProfilResmi", SqlDbType.VarBinary).Value = resimBytes;
                        cmd.Parameters.AddWithValue("@EkleyenDoktorID", aktifDoktorID);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Hasta başarıyla eklendi.");
                    HastalariGetir();

                    // 📧 E-posta gönder
                    SifreBilgisiGonder(email, tcNo, sifre);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
        private void btnResimSec_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    profilResmiYolu = ofd.FileName;
                    picProfilResmi.ImageLocation = profilResmiYolu;
                }
            }
        }
        private void SifreBilgisiGonder(string aliciEmail, string tcNo, string sifre)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("emirhansolak11@gmail.com");
                mail.To.Add(aliciEmail);
                mail.Subject = "Diyabet Sistemi Giriş Bilgileriniz";
                mail.Body = $"Merhaba,\n\nTC No: {tcNo}\nŞifre: {sifre}\n\nDiyabet Takip Sistemine giriş yapabilirsiniz.";

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("emirhansolak11@gmail.com", "scnurgcvyclufgnh");
                smtp.EnableSsl = true;

                smtp.Send(mail);
                MessageBox.Show("Bilgiler e-posta olarak gönderildi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mail gönderilemedi: " + ex.Message);
            }
        }



        private void btnHastaSec_Click(object sender, EventArgs e)
        {
            if (cmbHastaSec.SelectedItem != null)
            {
                ComboBoxItem selectedItem = (ComboBoxItem)cmbHastaSec.SelectedItem;
                selectedHastaID = Convert.ToInt32(selectedItem.Value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"SELECT AdSoyad, Email, TcNo, Cinsiyet, DogumTarihi, ProfilResmi 
                             FROM Kullanicilar 
                             WHERE KullaniciID = @id AND Rol = 'Hasta' AND EkleyenDoktorID = @doktorID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", selectedHastaID);
                    cmd.Parameters.AddWithValue("@doktorID", aktifDoktorID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Hasta bilgilerini göster
                        lblAdSoyadDeger.Text = reader["AdSoyad"].ToString();
                        lblEmailDeger.Text = reader["Email"].ToString();
                        lblTcDeger.Text = reader["TcNo"].ToString();
                        lblCinsiyetDeger.Text = reader["Cinsiyet"].ToString();
                        lblDogumDeger.Text = Convert.ToDateTime(reader["DogumTarihi"]).ToShortDateString();

                        // Etiketlerin konumlarını ayarla
                        lblAdSoyadEtiket.Location = new Point(300, 30);
                        lblAdSoyadDeger.Location = new Point(400, 30);

                        lblEmailEtiket.Location = new Point(300, 60);
                        lblEmailDeger.Location = new Point(400, 60);

                        lblTcEtiket.Location = new Point(300, 90);
                        lblTcDeger.Location = new Point(400, 90);

                        lblCinsiyetEtiket.Location = new Point(300, 120);
                        lblCinsiyetDeger.Location = new Point(400, 120);

                        lblDogumEtiket.Location = new Point(300, 150);
                        lblDogumDeger.Location = new Point(400, 150);

                        if (reader["ProfilResmi"] != DBNull.Value)
                        {
                            byte[] resimBytes = (byte[])reader["ProfilResmi"];
                            using (MemoryStream ms = new MemoryStream(resimBytes))
                            {
                                picHastaProfil.Image = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            picHastaProfil.Image = null;
                        }
                    }

                    reader.Close();
                }

                // Diyet, egzersiz ve insülin verilerini yükle
                LoadPatientData(selectedHastaID);
                HesaplaVeGosterUygulamaOranlari(selectedHastaID);
                OrtalamaKanSekeriGoster(selectedHastaID);
            }
            else
            {
                MessageBox.Show("Lütfen bir hasta seçin!");
            }

        }
        



        private void LoadPatientData(int hastaID)
        {
            LoadDiyet(hastaID);
            LoadEgzersiz(hastaID);
            LoadInsulin(hastaID);
        }
        private void OrtalamaKanSekeriGoster(int hastaID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT AVG(CAST(Deger AS FLOAT)) FROM KanSekeriOlcumleri
                         WHERE HastaID = @HastaID AND TarihSaat >= @Tarih";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@HastaID", hastaID);
                cmd.Parameters.AddWithValue("@Tarih", DateTime.Today.AddDays(-7));

                object result = cmd.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    double ortalama = Convert.ToDouble(result);
                    lblOrtalamaKan.Text = "Ortalama Kan Şekeri: " + Math.Round(ortalama, 1) + " mg/dL";
                }
                else
                {
                    lblOrtalamaKan.Text = "Ortalama Kan Şekeri: Veri yok";
                }
            }
        }

        private void LoadDiyet(int hastaID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Tarih, DiyetTuru, CASE WHEN UygulandiMi = 1 THEN 'Evet' ELSE 'Hayır' END as Uygulandi FROM DiyetTakipleri WHERE HastaID = @HastaID ORDER BY Tarih DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@HastaID", hastaID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvDiyet.DataSource = dt;
            }
        }

        private void LoadEgzersiz(int hastaID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Tarih, EgzersizTuru, CASE WHEN YapildiMi = 1 THEN 'Evet' ELSE 'Hayır' END as Yapildi FROM EgzersizTakipleri WHERE HastaID = @HastaID ORDER BY Tarih DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@HastaID", hastaID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvEgzersiz.DataSource = dt;
            }
        }

        private void LoadInsulin(int hastaID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Artık insülin önerileri değil, hastanın girdiği kan şekeri ölçümleri gösterilecek
                string query = "SELECT TarihSaat, Deger FROM KanSekeriOlcumleri WHERE HastaID = @HastaID ORDER BY TarihSaat DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@HastaID", hastaID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvInsulin.DataSource = dt;
                dgvInsulin.ReadOnly = true;
            }

            // dgvInsulin (artık kan şekeri tablosu) eklenmeden önce bir Label ekle
            Label lblKanSekeriBaslik = new Label();
            lblKanSekeriBaslik.Text = "Kan Şekeri Değerleri";
            lblKanSekeriBaslik.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblKanSekeriBaslik.Location = new Point(dgvInsulin.Location.X, dgvInsulin.Location.Y - 30);
            lblKanSekeriBaslik.AutoSize = true;
            this.Controls.Add(lblKanSekeriBaslik);
        }
        private string HashSHA256(string input)
        {
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
                return string.Concat(bytes.Select(b => b.ToString("x2")));
            }
        }
        public class ComboBoxItem
        {
            public string Text { get; set; }
            public string Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private void dateDogumTarihi_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbHastaSec_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click_1(object sender, EventArgs e)
        {

        }

        private void lblAdSoyadDeger_Click(object sender, EventArgs e)
        {

        }

        private void lvwUyarilar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void UyarilariGetir(int hastaID)
        {
            listViewUyarilar.Items.Clear();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Tarih, Saat, UyariTipi, Mesaj FROM Uyarilar WHERE HastaID = @HastaID ORDER BY Tarih DESC, Saat DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@HastaID", hastaID);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string tarih = Convert.ToDateTime(reader["Tarih"]).ToShortDateString();
                    string saat = reader["Saat"].ToString();
                    string tip = reader["UyariTipi"].ToString();
                    string mesaj = reader["Mesaj"].ToString();

                    ListViewItem item = new ListViewItem(tarih);
                    item.SubItems.Add(tip);
                    item.SubItems.Add(saat);
                    item.SubItems.Add(mesaj);

                    listViewUyarilar.Items.Add(item);
                }

                reader.Close();
            }
        }

        private void btnUyariGetir_Click(object sender, EventArgs e)
        {
            if (cmbUyariHastaSec.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir hasta seçin.");
                return;
            }

            ComboBoxItem secilenHasta = (ComboBoxItem)cmbUyariHastaSec.SelectedItem;
            int hastaID = Convert.ToInt32(secilenHasta.Value);

            listViewUyarilar.Items.Clear();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Tarih, Saat, UyariTipi, Mesaj FROM Uyarilar WHERE HastaID = @hastaID ORDER BY Tarih DESC, Saat DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@hastaID", hastaID);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(Convert.ToDateTime(reader["Tarih"]).ToShortDateString());
                    item.SubItems.Add(reader["UyariTipi"].ToString());
                    item.SubItems.Add(reader["Saat"].ToString());
                    item.SubItems.Add(reader["Mesaj"].ToString());

                    listViewUyarilar.Items.Add(item);
                }

                reader.Close();
            }
            UyarilariGetir(hastaID);
        }


        private void cmbDiyet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void btnOneriGetir_Click(object sender, EventArgs e)
        {
            if (selectedHastaID <= 0)
            {
                MessageBox.Show("Lütfen önce bir hasta seçin.");
                return;
            }

            if (cmbBelirtiler.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir belirti seçin.");
                return;
            }

            string belirti = cmbBelirtiler.SelectedItem.ToString();

            // Kan şekeri değerlerini al ve ortalamasını hesapla
            double ortalamaKanSekeri = 0;
            int olcumSayisi = 0;
            string kanSekeriKategori = "yok";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT AVG(Deger) FROM KanSekeriOlcumleri WHERE HastaID = @HastaID AND TarihSaat >= DATEADD(day, -7, GETDATE())";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@HastaID", selectedHastaID);

                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        ortalamaKanSekeri = Convert.ToDouble(result);
                        olcumSayisi = 1;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Veritabanı hatası (Kan Şekeri Okuma): " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Hata durumunda belki varsayılan öneriler gösterilebilir veya işlem durdurulabilir.
                // Örneğin, kanSekeriKategori'yi "Veri alınamadı" olarak ayarlayabilirsiniz.
                kanSekeriKategori = "Veri alınamadı (DB Hatası)";
                // Diğer öneri stringlerini de uygun şekilde ayarlayın veya temizleyin.
                // return; // Eğer işlem devam etmemeli ise
            }
            catch (Exception ex)
            {
                MessageBox.Show("Genel bir hata oluştu (Kan Şekeri Okuma): " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                kanSekeriKategori = "Veri alınamadı (Genel Hata)";
                // return;
            }

            string diyetOnerisi = "Öneri yok";
            string egzersizOnerisi = "Öneri yok";

            // Hipoglisemi
            if (ortalamaKanSekeri < 70)
            {
                kanSekeriKategori = "Düşük (Hipoglisemi): " + ortalamaKanSekeri.ToString("0.0") + " mg/dL";

                if (belirti == "Nöropati" || belirti == "Polifaji" || belirti == "Yorgunluk")
                {
                    diyetOnerisi = "Dengeli Beslenme";
                    egzersizOnerisi = "Yok";
                }
            }

            // Normal - Alt Düzey
            else if (ortalamaKanSekeri >= 70 && ortalamaKanSekeri <= 110)
            {
                kanSekeriKategori = "Normal (Alt Düzey): " + ortalamaKanSekeri.ToString("0.0") + " mg/dL";

                if (belirti == "Yorgunluk" || belirti == "Kilo Kaybı")
                {
                    diyetOnerisi = "Az Şekerli Diyet";
                    egzersizOnerisi = "Yürüyüş";
                }
                else if (belirti == "Polifaji" || belirti == "Polidipsi")
                {
                    diyetOnerisi = "Dengeli Beslenme";
                    egzersizOnerisi = "Yürüyüş";
                }
            }

            // Normal - Üst Düzey / Hafif Yüksek
            else if (ortalamaKanSekeri > 110 && ortalamaKanSekeri <= 180)
            {
                kanSekeriKategori = "Normal (Üst Düzey): " + ortalamaKanSekeri.ToString("0.0") + " mg/dL";

                if (belirti == "Bulanık Görme" || belirti == "Nöropati")
                {
                    diyetOnerisi = "Az Şekerli Diyet";
                    egzersizOnerisi = "Klinik Egzersiz";
                }
                else if (belirti == "Poliüri" || belirti == "Polidipsi")
                {
                    diyetOnerisi = "Şekersiz Diyet";
                    egzersizOnerisi = "Klinik Egzersiz";
                }
                else if (belirti == "Yorgunluk" || belirti == "Nöropati" || belirti == "Bulanık Görme")
                {
                    diyetOnerisi = "Az Şekerli Diyet";
                    egzersizOnerisi = "Yürüyüş";
                }
            }

            // Hiperglisemi
            else if (ortalamaKanSekeri >= 180)
            {
                kanSekeriKategori = "Çok Yüksek (Hiperglisemi): " + ortalamaKanSekeri.ToString("0.0") + " mg/dL";

                if (belirti == "Yaraların Yavaş İyileşmesi" && (belirti == "Polifaji" || belirti == "Polidipsi"))
                {
                    diyetOnerisi = "Şekersiz Diyet";
                    egzersizOnerisi = "Klinik Egzersiz";
                }
                else if (belirti == "Yaraların Yavaş İyileşmesi" || belirti == "Kilo Kaybı")
                {
                    diyetOnerisi = "Şekersiz Diyet";
                    egzersizOnerisi = "Yürüyüş";
                }
            }

            // Panelde göster
            pnlOneriler.Visible = true;
            lblKanSekeriAraligi.Text = "Kan Şekeri Aralığı: " + kanSekeriKategori;
            lblOnerilerDiyet.Text = "Diyet Önerisi: " + diyetOnerisi;
            lblOnerilerEgzersiz.Text = "Egzersiz Önerisi: " + egzersizOnerisi;

        }
        private void HesaplaVeGosterUygulamaOranlari(int hastaID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // ✅ Diyet verisi
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

                // ✅ Egzersiz verisi
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

        private void progressBar2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

       

    }
}
