using System;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace diyabetapp
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer slideTimer;
        private int slideOffset = -300;

        public Form1()
        {
            InitializeComponent();
            InitializeAnimations();
        }

        private void InitializeAnimations()
        {
            // Panel1'i başlangıçta gizle
            panel1.Left = slideOffset;
            
            // Fade-in efekti için form opacity'si
            this.Opacity = 0;
            
            // Slide timer'ı ayarla
            slideTimer = new System.Windows.Forms.Timer();
            slideTimer.Interval = 10;
            slideTimer.Tick += SlideTimer_Tick;
            
            // Form yüklendiğinde animasyonları başlat
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Fade-in animasyonu
            System.Windows.Forms.Timer fadeTimer = new System.Windows.Forms.Timer();
            fadeTimer.Interval = 10;
            fadeTimer.Tick += (s, args) =>
            {
                this.Opacity += 0.05;
                if (this.Opacity >= 1)
                {
                    fadeTimer.Stop();
                    fadeTimer.Dispose();
                    // Fade-in tamamlandıktan sonra slide animasyonunu başlat
                    slideTimer.Start();
                }
            };
            fadeTimer.Start();
        }

        private void SlideTimer_Tick(object sender, EventArgs e)
        {
            if (panel1.Left < 0)
            {
                panel1.Left += 10;
                if (panel1.Left >= 0)
                {
                    panel1.Left = 0;
                    slideTimer.Stop();
                    
                    // Login panel'in fade-in animasyonu
                    panel2.Visible = true;
                    panel2.BackColor = Color.FromArgb(0, Color.White);
                    System.Windows.Forms.Timer loginFadeTimer = new System.Windows.Forms.Timer();
                    loginFadeTimer.Interval = 10;
                    int opacity = 0;
                    loginFadeTimer.Tick += (s, args) =>
                    {
                        opacity += 5;
                        if (opacity >= 255)
                        {
                            opacity = 255;
                            loginFadeTimer.Stop();
                            loginFadeTimer.Dispose();
                        }
                        panel2.BackColor = Color.FromArgb(opacity, Color.White);
                    };
                    loginFadeTimer.Start();
                }
            }
        }

        private void buttonGiris_Click(object sender, EventArgs e)
{
    string tcNo = textBoxTcNo.Text.Trim();
    string sifre = textBoxSifre.Text.Trim();

    if (string.IsNullOrEmpty(tcNo) || string.IsNullOrEmpty(sifre))
    {
        MessageBox.Show("TC ve şifre boş olamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }

    string hashedPassword = HashSHA256(sifre);
    string connectionString = "Server=localhost\\SQLEXPRESS;Database=DiyabetDB;Trusted_Connection=True;Encrypt=False;";

    using (SqlConnection conn = new SqlConnection(connectionString))
    {
        conn.Open();
        string query = "SELECT KullaniciID, Rol FROM Kullanicilar WHERE TcNo = @tc AND SifreHash = @sifre";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@tc", tcNo);
            cmd.Parameters.AddWithValue("@sifre", hashedPassword);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                int kullaniciID = Convert.ToInt32(reader["KullaniciID"]);
                string role = reader["Rol"].ToString();

                // Fade-out animasyonu
                System.Windows.Forms.Timer fadeOutTimer = new System.Windows.Forms.Timer();
                fadeOutTimer.Interval = 10;
                fadeOutTimer.Tick += (s, args) =>
                {
                    this.Opacity -= 0.05;
                    if (this.Opacity <= 0)
                    {
                        fadeOutTimer.Stop();
                        fadeOutTimer.Dispose();
                        this.Hide();

                        if (role == "Doktor")
                        {
                            DoktorForm doktorForm = new DoktorForm(kullaniciID); // burada artık doktorID yerine kullaniciID gönderiliyor
                            doktorForm.ShowDialog();
                        }
                        else if (role == "Hasta")
                        {
                            HastaForm hastaForm = new HastaForm(tcNo); // Hasta formu için tc yeterli gibi görünüyordu
                            hastaForm.ShowDialog();
                        }

                        this.Opacity = 0;
                        this.Show();
                        System.Windows.Forms.Timer fadeInTimer = new System.Windows.Forms.Timer();
                        fadeInTimer.Interval = 10;
                        fadeInTimer.Tick += (s2, args2) =>
                        {
                            this.Opacity += 0.05;
                            if (this.Opacity >= 1)
                            {
                                fadeInTimer.Stop();
                                fadeInTimer.Dispose();
                            }
                        };
                        fadeInTimer.Start();
                    }
                };
                fadeOutTimer.Start();
            }
            else
            {
                MessageBox.Show("TC veya şifre hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


        private string HashSHA256(string input)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
                return string.Concat(bytes.Select(b => b.ToString("x2")));
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
    }
}
