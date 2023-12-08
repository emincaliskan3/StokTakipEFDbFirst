using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StokTakipEFDbFirst
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        StokTakipEntities context = new StokTakipEntities();

        private void Form1_Load(object sender, EventArgs e)
        {
            dgvUrunler.DataSource = context.Urunler.ToList();
        }
        void Temizle()
        {
            txtStok.Text = string.Empty;
            txtUrunAciklamasi.Text = string.Empty;
            txtUrunAdi.Text = string.Empty;
            txtUrunFiyati.Text = string.Empty;
            btnEkle.Enabled = true;
            btnGuncelle.Enabled = false;
            btnSil.Enabled = false;
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUrunAdi.Text))
            {
                MessageBox.Show("Ürün Adı Boş Bırakılamaz!");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtUrunFiyati.Text))
            {
                MessageBox.Show("Ürün Fiyatı Boş Bırakılamaz!");
                return;
            }
            try
            {
                context.Urunler.Add(new Urunler
                {
                    Name = txtUrunAdi.Text,
                    Description = txtUrunAciklamasi.Text,
                    Price = Convert.ToDecimal(txtUrunFiyati.Text),
                    Stock = int.Parse(txtStok.Text)
                });
                var sonuc = context.SaveChanges(); 
                if (sonuc > 0)
                {
                    Temizle();
                    dgvUrunler.DataSource = context.Urunler.ToList();
                    MessageBox.Show("Kayıt Başarılı!");
                }
                else
                    MessageBox.Show("Kayıt Başarısız!");
            }
            catch (Exception hata)
            {
                
                MessageBox.Show("Hata Oluştu! Geçersiz Değer Girdiniz!");
            }
        }

        private void dgvUrunler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnEkle.Enabled = false;
                btnGuncelle.Enabled = true;
                btnSil.Enabled = true;
                btnGeri.Enabled = true;

                int urunId = Convert.ToInt32(dgvUrunler.CurrentRow.Cells[0].Value);
                var urun = context.Urunler.Find(urunId); 
                txtUrunAdi.Text = urun.Name;
                txtUrunAciklamasi.Text = urun.Description;
                txtUrunFiyati.Text = urun.Price.ToString();
                txtStok.Text = urun.Stock.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Hata Oluştu!");
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            int urunId = Convert.ToInt32(dgvUrunler.CurrentRow.Cells[0].Value);
            var urun = context.Urunler.Find(urunId);
            urun.Name = txtUrunAdi.Text;
            urun.Description = txtUrunAciklamasi.Text;
            urun.Stock = int.Parse(txtStok.Text);
            urun.Price = Convert.ToDecimal(txtUrunFiyati.Text);
            var sonuc = context.SaveChanges();
            if (sonuc > 0)
            {
                Temizle();
                dgvUrunler.DataSource = context.Urunler.ToList();
                MessageBox.Show("Kayıt Başarılı!");
            }
            else
            {
                MessageBox.Show("Kayıt Başarısız!");
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                int urunId = Convert.ToInt32(dgvUrunler.CurrentRow.Cells[0].Value);
                var urun = context.Urunler.Find(urunId);
                context.Urunler.Remove(urun); 
                var sonuc = context.SaveChanges();
                if (sonuc > 0)
                {
                    Temizle();
                    dgvUrunler.DataSource = context.Urunler.ToList();
                    MessageBox.Show("Kayıt Silindi!");
                }
                else
                {
                    MessageBox.Show("Kayıt Silinemedi!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Hata Oluştu!");
            }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            dgvUrunler.DataSource = context.Urunler.Where(u => u.Name.Contains(txtAra.Text)).ToList();
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            dgvUrunler.DataSource = context.Urunler.Where(u => u.Name.Contains(txtAra.Text) || u.Description.Contains(txtAra.Text)).ToList();
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            Temizle();
            btnGeri.Enabled = false;

        }
    }
}
