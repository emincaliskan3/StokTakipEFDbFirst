﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StokTakipEFDbFirst
{
    public partial class KullaniciYonetimi : Form
    {
        public KullaniciYonetimi()
        {
            InitializeComponent();
        }
        StokTakipEntities context = new StokTakipEntities();
        private void KullaniciYonetimi_Load(object sender, EventArgs e)
        {
            dgvKullanicilar.DataSource = context.Kullanicilar.ToList();
        }
        void Temizle()
        {
            txtAd.Text = string.Empty;
            txtSoyad.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtKullaniciAdi.Text = string.Empty;
            txtSifre.Text = string.Empty;
            txtTelefon.Text = string.Empty;

            btnEkle.Enabled = true;
            btnGuncelle.Enabled = false;
            btnSil.Enabled = false;
        }
            private void btnEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text) || string.IsNullOrWhiteSpace(txtSifre.Text))
            {
                MessageBox.Show("Kullanıcı Adı ve Şifre Boş Bırakılamaz!");
                return;
            }
            var kullanici = new Kullanicilar()
            {
                Email = txtEmail.Text,
                Name = txtAd.Text,
                Surname = txtSoyad.Text,
                Password = txtSifre.Text,
                Username = txtKullaniciAdi.Text,
                Phone = txtTelefon.Text,
                UserGuid = Guid.NewGuid() 
            };
            context.Kullanicilar.Add(kullanici);
            var sonuc = context.SaveChanges();
            if (sonuc > 0)
            {
                dgvKullanicilar.DataSource = context.Kullanicilar.ToList();
                MessageBox.Show("Kayıt Başarılı!");
            }
        }

        private void dgvKullanicilar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = (int)dgvKullanicilar.CurrentRow.Cells[0].Value;
            var kullanici = context.Kullanicilar.Find(id);

            txtAd.Text = kullanici.Name;
            txtSoyad.Text = kullanici.Surname;
            txtTelefon.Text = kullanici.Phone;
            txtEmail.Text = kullanici.Email;
            txtKullaniciAdi.Text = kullanici.Username;
            txtSifre.Text = kullanici.Password;

            btnEkle.Enabled = false;
            btnGuncelle.Enabled = true;
            btnSil.Enabled = true;
            btnGeri.Enabled = true;
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text) || string.IsNullOrWhiteSpace(txtSifre.Text))
            {
                MessageBox.Show("Kullanıcı Adı ve Şifre Boş Bırakılamaz!");
                return;
            }
            var kullanici = new Kullanicilar()
            {
                Id = (int)dgvKullanicilar.CurrentRow.Cells[0].Value,
                Email = txtEmail.Text,
                Name = txtAd.Text,
                Surname = txtSoyad.Text,
                Password = txtSifre.Text,
                Username = txtKullaniciAdi.Text,
                Phone = txtTelefon.Text,
                UserGuid = Guid.NewGuid() 
            };

            context.Kullanicilar.AddOrUpdate(kullanici);
            var sonuc = context.SaveChanges();
            if (sonuc > 0)
            {
                dgvKullanicilar.DataSource = context.Kullanicilar.ToList();
                MessageBox.Show("Kayıt Başarılı!");
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            var kullanici = context.Kullanicilar.Find((int)dgvKullanicilar.CurrentRow.Cells[0].Value);

            context.Kullanicilar.Remove(kullanici);

            var sonuc = context.SaveChanges();
            if (sonuc > 0)
            {
                dgvKullanicilar.DataSource = context.Kullanicilar.ToList();
                MessageBox.Show("Kayıt Silme Başarılı!");
            }
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            Temizle();
            btnGeri.Enabled = false;
        }
    }
}
