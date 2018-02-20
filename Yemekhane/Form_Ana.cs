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
    public partial class Form_Ana : Form
    {
        public Form_Ana()
        {
            InitializeComponent();
        }
        static DataTable dt = null;
        void VerileriYukle()
        {
            dt = YemekListesi.GetAll();
            dataGridView1.DataSource = dt;
        }
        public static void GridNumaralandir(DataGridView dataGridView)
        {
            if (dataGridView != null)
            {
                for (int count = 0; (count <= (dataGridView.Rows.Count - 1)); count++)
                {
                    string sayi = (count + 1).ToString().PadLeft(dataGridView.Rows.Count.ToString().Length, '0');
                    dataGridView.Rows[count].HeaderCell.Value = sayi;
                }
            }
        }
        private void Form_Ana_Load(object sender, EventArgs e)
        {
            VerileriYukle();
        }
        static YemekListesi yliste=null;
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count < 1)
                return;
            string rid = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            DataRow r = null;
            foreach (DataRow item in dt.Rows)
            {
                if (item["Tarih"].ToString() == rid)
                {
                    r = item;
                    break;
                }
            }

            yliste = new YemekListesi
            {
                Tarih = r["Tarih"].ToString(),
                Ogun = r["Ogun"].ToString(),
                a1 = r["a1"].ToString(),
                a2 = r["a2"].ToString(),
                a3 = r["a3"].ToString(),
                a4 = r["a4"].ToString(),
                a5 = r["a5"].ToString(),
                a6 = r["a6"].ToString(),
                a7 = r["a7"].ToString(),
                ToplamKalori = Convert.ToInt32(r["ToplamKalori"].ToString())
            };

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.yemek = null;
            frm.ShowDialog();
            VerileriYukle();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(yliste==null)
                MessageBox.Show("Bir Kayıt Seçin");
             
            else
            {
                Form1 frm = new Form1();
                frm.yemek = yliste;
                frm.ShowDialog();
                VerileriYukle();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

          
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
            //Bitmap bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
            Bitmap bm = new Bitmap(800,100);
            dataGridView1.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));
            e.Graphics.DrawImage(bm, 0, 0);
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count<1)
                return ;
             DialogResult ra = MessageBox.Show("Yemek Silinsin mi", "Yemek Silme", MessageBoxButtons.YesNoCancel);
             if (ra == DialogResult.Yes)
             {
                 DataRow r=dt.Rows[dataGridView1.SelectedRows[0].Index];
                 YemekListesi.Sil(new YemekListesi() { Tarih = r["Tarih"].ToString() });
                 MessageBox.Show("Silindi");
             }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form_Yemek frmy = new Form_Yemek();
            frmy.ShowDialog();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridNumaralandir(dataGridView1);
        }
        
    }
}
