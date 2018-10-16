using RN.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RN
{
    public class Conexion
    {
        public static List<DataConection> getConection()
        {
            string cadena = "";
            string key = "";
            List<DataConection> conections = new List<DataConection>();
            int Line = 0;
            try
            {

                var cn = ConfigurationManager.ConnectionStrings;
                foreach (var item in cn)
                {
                    if (((System.Configuration.ConfigurationElement)item).LockItem)
                    {
                        DataConection conection = new DataConection();
                        key = ((System.Configuration.ConnectionStringSettings)item).Name;
                        if (!key.Contains("LocalSqlServer"))
                        {
                            if (!key.Contains("SqlProvider"))
                            {
                                conection.Description = key;
                                conections.Add(conection);
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            return conections;
        }

        public static string[] getConectionArray()
        {
            string cadena = "";
            string key = "";
            List<DataConection> conections = new List<DataConection>();

            int Line = 0;
            try
            {
                System.IO.StreamReader File = new System.IO.StreamReader(@"C:\WINDOWS\ConnServerNP.ini");
                while ((cadena = File.ReadLine()) != null)
                {
                    DataConection conection = new DataConection();
                    if (cadena.Contains("Key"))
                    {
                        int index = cadena.IndexOf('=');
                        key = cadena.Substring(index + 1, cadena.Length - index - 1).Trim();
                        conection.Description = key;
                        conections.Add(conection);
                    }
                    Line++;
                }
                File.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            return null;
        }


        public static SqlConnection OpenConexion()
        {

            SqlConnection Conn = new SqlConnection();
            string cadena = "";
            string servidor = "";
            string dbase = "";
            string usuario = "";
            string pws = "";
            string clave = "";
            int Linea = 0;

            #region Lectura de INI

            try
            {
                bool isConnection = false;

                var cn = ConfigurationManager.ConnectionStrings;
                foreach (var item in cn)
                {
                    if (((System.Configuration.ConfigurationElement)item).LockItem)
                    {
                        string[] val = ((System.Configuration.ConnectionStringSettings)item).ConnectionString.Split(';');

                        servidor = val[0].Substring(12, 15).Trim();
                        dbase = val[1].Substring(16, 3).Trim();
                        usuario = val[2].Substring(8, 13).Trim();
                        pws = Clave.DesEncriptar(Convert.FromBase64String(val[3].Substring(9, 32).Trim()));

                        break;
                    }
                }
            }
            catch (Exception ErrorConexion)
            {
                MessageBox.Show(ErrorConexion.Message);
            }

            Conn.ConnectionString = "Server=" + servidor + ";Database=" + dbase + ";User Id=" + usuario + ";Password=" + pws + ";";

            try
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
                else
                    Conn.Open();

                return Conn;

            }
            catch (SqlException Ex)
            {
                // MessageBox.Show("Error de comunicacion:  " + Ex.Errors, "Informe de la aplicacion.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #endregion


            return Conn;

        }
    }
}
