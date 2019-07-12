using Entities.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace EnviosColombiaGold
{
    public partial class frmSearchId : Form
    {
        private clsLabSubmit oLabS = new clsLabSubmit();
        public int cargarConsulta { get; set; }


        #region Delegados
        public delegate void PasarDatoSeleccionado(string Dato);
        public event PasarDatoSeleccionado Pasado;
        #endregion
        public frmSearchId()
        {
            InitializeComponent();

        }
        public frmSearchId(int cargarConsulta)
        {
            InitializeComponent();
            this.cargarConsulta = cargarConsulta;

        }


        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }

        public static string GetSHA1(String texto)
        {
            try
            {
                SHA1 sha1 = SHA1CryptoServiceProvider.Create();
                Byte[] textOriginal = ASCIIEncoding.Default.GetBytes(texto);
                Byte[] hash = sha1.ComputeHash(textOriginal);
                StringBuilder cadena = new StringBuilder();
                foreach (byte i in hash)
                {
                    cadena.AppendFormat("{0:x2}", i);
                }
                return cadena.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void pbtnChange_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridViewConsulta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxbConsulta.Text) && this.cargarConsulta == 0)
            {

                var ent_dtCmbShipm = LoadLabSubmit1("3", TxbConsulta.Text);
                this.dataGridViewConsulta.AutoGenerateColumns = true;
                DataTable dt = ConvertToDataTable(ent_dtCmbShipm);
                //dataGridViewConsulta.DataSource = ent_dtCmbShipm.Select(x => new { Preparation = x.Ssumit }).ToList();
                var valueDisplay = ent_dtCmbShipm.Select(x => new { Preparation = x.Ssumit }).ToList();
                dataGridViewConsulta.DataSource = valueDisplay;
                //timer1.Stop();

            }
            if (this.cargarConsulta > 0)
            {
                clsRf oRf = new clsRf();
                var ent_dtCmbShipm = oRf.getLabResult("3", TxbConsulta.Text);
                this.dataGridViewConsulta.AutoGenerateColumns = true;
                dataGridViewConsulta.DataSource = ent_dtCmbShipm;
            }
            //progressBar1.Visible = false;

        }
                     
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }

                table.Rows.Add(row);
            }
            return table;

        }

        private IList<Ent_Prefix> LoadLabSubmit1(string _sOpcion, string _SSubmit)
        {
            try
            {
                oLabS.sOpcion = _sOpcion.ToString();
                oLabS.sSubmit = _SSubmit.ToString();
                var dtLabS = oLabS.getLabSubmitEntities();

                return dtLabS;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void dataGridViewConsulta_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int Fill = dataGridViewConsulta.CurrentCell.RowIndex;
            Pasado(dataGridViewConsulta[0, Fill].Value.ToString());
            Close();
        }

        private void TxbConsulta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                buttonBuscar_Click(null, null);
            }
        }
    }
}
