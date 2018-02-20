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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public YemekListesi yemek { get; set; }
        static DataTable dt = null;

        void VerileriYukle()
        {

            comboBox_ogun.SelectedIndex = 0;
            dt = Yemek.GetAll();
          
            if(dt.Rows.Count <= 0)
            {
                //MessageBox.Show("HİÇ YEMEK EKLENMEMİŞ");
                Form_Yemek f = new Form_Yemek();
                f.Show();
                this.Close();
            }
            foreach (DataRow item in dt.Rows)
            {
                comboBox1.Items.Add(item["AD"].ToString());
                comboBox2.Items.Add(item["AD"].ToString());
                comboBox3.Items.Add(item["AD"].ToString());
                comboBox4.Items.Add(item["AD"].ToString());
                comboBox5.Items.Add(item["AD"].ToString());
                comboBox6.Items.Add(item["AD"].ToString());
                comboBox7.Items.Add(item["AD"].ToString());
                //
                comboBox1.AutoCompleteCustomSource.Add(item["AD"].ToString());
                comboBox2.AutoCompleteCustomSource.Add(item["AD"].ToString());
                comboBox3.AutoCompleteCustomSource.Add(item["AD"].ToString());
                comboBox4.AutoCompleteCustomSource.Add(item["AD"].ToString());
                comboBox5.AutoCompleteCustomSource.Add(item["AD"].ToString());
                comboBox6.AutoCompleteCustomSource.Add(item["AD"].ToString());
                comboBox7.AutoCompleteCustomSource.Add(item["AD"].ToString());
            }

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
                comboBox3.SelectedIndex = 0;
                comboBox4.SelectedIndex = 0;
                comboBox5.SelectedIndex = 0;
                comboBox6.SelectedIndex = 0;
                comboBox7.SelectedIndex = 0;
                
            }

            if(yemek!=null)
            {
                dateTimePicker1.Value = Convert.ToDateTime(yemek.Tarih);
                comboBox_ogun.SelectedItem = yemek.Ogun;
                comboBox1.Text = yemek.a1;
                comboBox2.Text = yemek.a2;
                comboBox3.Text = yemek.a3;
                comboBox4.Text = yemek.a4;
                comboBox5.Text = yemek.a5;
                comboBox6.Text = yemek.a6;
                comboBox7.Text = yemek.a7;
                lbl_kalori.Text = yemek.ToplamKalori.ToString();
            }
        }
        void KaloriHesapla()
        {
            int tutar = 0;
            if(comboBox1.SelectedIndex!=0)
             tutar +=  Convert.ToInt32(dt.Rows[comboBox1.SelectedIndex-1]["Kalori"].ToString());
            if (comboBox2.SelectedIndex != 0)
                tutar +=  Convert.ToInt32(dt.Rows[comboBox2.SelectedIndex-1]["Kalori"].ToString());
            if (comboBox3.SelectedIndex != 0)
            tutar +=  Convert.ToInt32(dt.Rows[comboBox3.SelectedIndex-1]["Kalori"].ToString());
            if (comboBox4.SelectedIndex != 0)
             tutar +=  Convert.ToInt32(dt.Rows[comboBox4.SelectedIndex-1]["Kalori"].ToString());
            if (comboBox5.SelectedIndex != 0)
             tutar +=  Convert.ToInt32(dt.Rows[comboBox5.SelectedIndex-1]["Kalori"].ToString());
            if (comboBox6.SelectedIndex != 0)
             tutar +=  Convert.ToInt32(dt.Rows[comboBox6.SelectedIndex-1]["Kalori"].ToString());
            if (comboBox7.SelectedIndex != 0)
             tutar +=  Convert.ToInt32(dt.Rows[comboBox7.SelectedIndex-1]["Kalori"].ToString());
          
            lbl_kalori.Text = tutar.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            YemekListesi y = new YemekListesi() 
            {
                Tarih=dateTimePicker1.Value.ToLongDateString(),
                Ogun=comboBox_ogun.SelectedItem.ToString(),
                a1 = comboBox1.SelectedItem.ToString(),
                a2 = comboBox2.SelectedItem.ToString(),
                a3 = comboBox3.SelectedItem.ToString(),
                a4 = comboBox4.SelectedItem.ToString(),
                a5 = comboBox5.SelectedItem.ToString(),
                a6 = comboBox6.SelectedItem.ToString(),
                a7 = comboBox7.SelectedItem.ToString(),
                ToplamKalori=Convert.ToInt32(lbl_kalori.Text)
            };
            if (yemek==null)
            {
                YemekListesi.Ekle(y);
                MessageBox.Show("Eklendi");
            }
            else if(yemek!=null)
            {
                YemekListesi.Guncelle(y,yemek.Tarih);
                MessageBox.Show("Güncelle");
            }

            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            VerileriYukle();
            dd = 1;
            KaloriHesapla();
        }
         int dd = 0;
        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(dd==1)
                KaloriHesapla();
        }
    }
}
