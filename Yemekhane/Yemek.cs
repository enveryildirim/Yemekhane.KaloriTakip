using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Yemekhane
{
    public class Yemek
    {
        public string AD { get; set; }
        public int Kalori { get; set; }
        private static DataTable _dt_yemek = null;
        public static DataTable GetAll()
        {
            if (_dt_yemek == null)
            {
                _dt_yemek = new DataTable();
                _dt_yemek.TableName = "Yemek";

                DataColumn c_Plaka = new DataColumn("AD", typeof(string));
                DataColumn c_Aciklama = new DataColumn("Kalori", typeof(string));
               
                _dt_yemek.Columns.Add(c_Plaka);
                _dt_yemek.Columns.Add(c_Aciklama);
               
                _dt_yemek.PrimaryKey = new DataColumn[1] { _dt_yemek.Columns["AD"] };
                string path = Application.StartupPath + "\\veriler\\yemekler.xml";

                if (!File.Exists(path))
                    _dt_yemek.WriteXml(path);
                _dt_yemek.ReadXml(path);
            }
            return _dt_yemek;
        }

        public static void Ekle(Yemek a)
        {
            DataTable dt = GetAll();
            DataRow r = dt.NewRow();
            r["AD"] = a.AD;
            r["Kalori"] = a.Kalori;

            _dt_yemek.Rows.Add(r);
            _dt_yemek.AcceptChanges();
            string path = Application.StartupPath + "\\veriler\\yemekler.xml";
            _dt_yemek.WriteXml(path);
        }
        public static void Guncelle(Yemek a, string ad)
        {
            DataTable dt = GetAll();
            DataRow r = dt.Rows.Find(ad);
            r["AD"] = a.AD;
            r["Kalori"] = a.Kalori;

            _dt_yemek.AcceptChanges();
            string path = Application.StartupPath + "\\veriler\\yemekler.xml";
            _dt_yemek.WriteXml(path);

        }
        public static void Sil(Yemek a)
        {
            //string sorgu = "delete from Arac where Plaka=@p";
            //Dictionary<string, object> param = new Dictionary<string, object>();
            //param.Add("@p", a.Plaka);
            //DB.ExecuteCommand(sorgu, param);
            DataTable dt = GetAll();
            DataRow r = dt.Rows.Find(a.AD);
            dt.Rows.Remove(r);
            _dt_yemek.AcceptChanges();
            string path = Application.StartupPath + "\\veriler\\yemekler.xml";
            _dt_yemek.WriteXml(path);
        }
    }
}
