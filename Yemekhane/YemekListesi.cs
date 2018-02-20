using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Yemekhane
{
    public class YemekListesi
    {
        public string Tarih { get; set; }
        public string Ogun { get; set; }
        public string a1 { get; set; }
        public string a2 { get; set; }
        public string a3 { get; set; }
        public string a4 { get; set; }
        public string a5 { get; set; }
        public string a6 { get; set; }
        public string a7 { get; set; }
        public int ToplamKalori { get; set; }
        
        private static DataTable _dt_yemeklistesi;
        public static DataTable GetAll()
        {
            if (_dt_yemeklistesi == null)
            {
                _dt_yemeklistesi = new DataTable();
                _dt_yemeklistesi.TableName = "YemekListesi";

                DataColumn c_Plaka = new DataColumn("Tarih", typeof(string));
                DataColumn c_Aciklama = new DataColumn("Ogun", typeof(string));
                DataColumn c_kalori = new DataColumn("ToplamKalori", typeof(int));
                DataColumn c_1 = new DataColumn("a1", typeof(string));
                DataColumn c_2 = new DataColumn("a2", typeof(string));
                DataColumn c_3 = new DataColumn("a3", typeof(string));
                DataColumn c_4 = new DataColumn("a4", typeof(string));
                DataColumn c_5 = new DataColumn("a5", typeof(string));
                DataColumn c_6 = new DataColumn("a6", typeof(string));
                DataColumn c_7 = new DataColumn("a7", typeof(string));

                _dt_yemeklistesi.Columns.Add(c_Plaka);
                _dt_yemeklistesi.Columns.Add(c_Aciklama);
                _dt_yemeklistesi.Columns.Add(c_1);
                _dt_yemeklistesi.Columns.Add(c_2);
                _dt_yemeklistesi.Columns.Add(c_3);
                _dt_yemeklistesi.Columns.Add(c_4);
                _dt_yemeklistesi.Columns.Add(c_5);
                _dt_yemeklistesi.Columns.Add(c_6);
                _dt_yemeklistesi.Columns.Add(c_7);
                _dt_yemeklistesi.Columns.Add(c_kalori);

                _dt_yemeklistesi.PrimaryKey = new DataColumn[1] { _dt_yemeklistesi.Columns["Tarih"] };
                string path = Application.StartupPath + "\\veriler\\yemeklistesi.xml";

                if (!File.Exists(path))
                    _dt_yemeklistesi.WriteXml(path);
                _dt_yemeklistesi.ReadXml(path);
            }
            return _dt_yemeklistesi;
        }

        public static void Ekle(YemekListesi a)
        {
            DataRow ra = null;
            ra=_dt_yemeklistesi.Rows.Find(a.Tarih);
            if(ra!=null)
            {
                MessageBox.Show("O Tarihe Yemek eklenmiş");
                return;
            }
            DataTable dt = GetAll();
            DataRow r = dt.NewRow();
            r["Tarih"] = a.Tarih;
            r["Ogun"] = a.Ogun;
            r["a1"] = a.a1;
            r["a2"] = a.a2;
            r["a3"] = a.a3;
            r["a4"] = a.a4;
            r["a5"] = a.a5;
            r["a6"] = a.a6;
            r["a7"] = a.a7;
            r["ToplamKalori"] = a.ToplamKalori;
            _dt_yemeklistesi.Rows.Add(r);
            _dt_yemeklistesi.AcceptChanges();
            string path = Application.StartupPath + "\\veriler\\yemeklistesi.xml";
            _dt_yemeklistesi.WriteXml(path);
        }
        public static void Guncelle(YemekListesi a, string eskiplaka)
        {
            DataTable dt = GetAll();
            DataRow r = dt.Rows.Find(eskiplaka);
            r["Tarih"] = a.Tarih;
            r["Ogun"] = a.Ogun;
            r["a1"] = a.a1;
            r["a2"] = a.a2;
            r["a3"] = a.a3;
            r["a4"] = a.a4;
            r["a5"] = a.a5;
            r["a6"] = a.a6;
            r["a7"] = a.a7;
            r["ToplamKalori"] = a.ToplamKalori;
            _dt_yemeklistesi.AcceptChanges();
            string path = Application.StartupPath + "\\veriler\\yemeklistesi.xml";
            _dt_yemeklistesi.WriteXml(path);

        }
        public static void Sil(YemekListesi a)
        {
            DataTable dt = GetAll();
            DataRow r = dt.Rows.Find(a.Tarih);
            dt.Rows.Remove(r);
            _dt_yemeklistesi.AcceptChanges();
            string path = Application.StartupPath + "\\veriler\\yemeklistesi.xml";
            _dt_yemeklistesi.WriteXml(path);
        }

    }
}
