using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Yemekhane
{
    public partial class Form_Yemek : Form
    {
        public Form_Yemek()
        {
            InitializeComponent();
        }
        static DataTable dt = null;
        static int durum = 0;
        private void Form_Yemek_Load(object sender, EventArgs e)
        {
            VerileriYukle();
        }


        //fonsiyonlar
        void VerileriYukle()
        {
            comboBox1.Items.Clear();
            dt = Yemek.GetAll();
            if (dt.Rows.Count <= 0)
                MessageBox.Show("HİÇ YEMEK EKLENMEMİŞ");
            foreach (DataRow item in dt.Rows)
            {
                comboBox1.Items.Add(item["AD"].ToString());
            }
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }
        bool KontrolEt()
        {
            int d = 1;
            if(txt_kalori.Text.Trim()==""||txt_yemekadi.Text.Trim()=="")
            {
                durum = durum * 0;
                MessageBox.Show("Boş Alan Bırakmayınız !!");
            }
             //sayi kontrolü yapılacak
            return Convert.ToBoolean(d);
        }
        //olaylar
        private void button1_Click(object sender, EventArgs e)
        {
            durum = 1;
            groupBox1.Enabled = true;
            txt_kalori.Text = txt_yemekadi.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            durum = 0;
            txt_kalori.Text = dt.Rows[comboBox1.SelectedIndex]["Kalori"].ToString();
            txt_yemekadi.Text = dt.Rows[comboBox1.SelectedIndex]["AD"].ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            durum = 2;
            groupBox1.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!KontrolEt())
                return;
            Yemek y = new Yemek() { AD = txt_yemekadi.Text.ToUpper(), Kalori = Convert.ToInt32(txt_kalori.Text) };

            if(durum==1)
            {
                Yemek.Ekle(y);
                MessageBox.Show("Eklendi:"+y.AD);

            }
            else if(durum==2)
            {
                Yemek.Guncelle(y,dt.Rows[comboBox1.SelectedIndex]["AD"].ToString());
                MessageBox.Show("Eklendi:" + y.AD);
            }
            VerileriYukle();
            groupBox1.Enabled = false;
            durum = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult sonuc = MessageBox.Show(comboBox1.SelectedItem.ToString() + "-Yemek Silinsin mi !", "Yemek Silme", MessageBoxButtons.YesNoCancel);
            if (sonuc == DialogResult.Yes)
            {
                Yemek y = new Yemek() { AD = txt_yemekadi.Text.ToUpper(), Kalori = Convert.ToInt32(txt_kalori.Text) };
                MessageBox.Show("  Yemek Silindi!");
                Yemek.Sil(y);
                txt_kalori.Text = txt_yemekadi.Text= "";
                groupBox1.Enabled = false;
                comboBox1.Text = "";
                VerileriYukle();
            }
        }
    }
}
