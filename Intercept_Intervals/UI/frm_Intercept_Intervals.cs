using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Globalization;
using RN.RulesBussines;

namespace Intercept_Intervals.UI
{
    public partial class frm_Intercept_Intervals : Form
    {
        DataTable generalData = new DataTable();
        public bool valueModificate = false;
        public bool valueFirstRecuperation = false;
        public bool containeRegister = false;

        public frm_Intercept_Intervals()
        {
            InitializeComponent(); FillCmb();
        }

        #region Propiedades - Variables

        public string Usuario { get; set; }
        public string IpLocal { get; set; }
        public string IpPublica { get; set; }
        public string SerialHDD { get; set; }
        #endregion

        public class matriz1
        {
            public decimal value { get; set; }
        }
        public class matriz2
        {
            public decimal value { get; set; }
        }
        public class matriz3
        {
            public decimal value { get; set; }
        }

        public class matrizResult
        {
            public decimal value { get; set; }
        }

        public class matrizTo
        {
            public string value { get; set; }
        }

        public class matrizMod
        {
            public string value { get; set; }
        }
        private void FillCmb()
        {
            try
            {
                if (string.IsNullOrEmpty(this.IpLocal))
                    this.IpLocal = Adress_IP.Local();

                if (string.IsNullOrEmpty(this.IpPublica))
                    this.IpPublica = Adress_IP.Publica();

                if (string.IsNullOrEmpty(this.SerialHDD))
                    this.SerialHDD = Adress_IP.SerialNumberDisk();

                if (string.IsNullOrEmpty(this.Usuario))
                    this.Usuario = Adress_IP.SerialNumberDisk();
                var culturaCol = CultureInfo.GetCultureInfo("es-CO");
                DateTime dateReporte = Convert.ToDateTime(DateTime.Now, culturaCol);


                LoadLog.Register(dateReporte, clsRf.sUser, IpLocal, IpPublica, SerialHDD, Environment.MachineName, "Search HoleID", "Consulta");
                this.cmbHoleID.DropDownStyle = ComboBoxStyle.DropDownList;
                this.cboSoruce.DropDownStyle = ComboBoxStyle.DropDownList;
                DataTable dtCollars = RN.GetDHCollars.getDHCollars("");
                DataRow drC = dtCollars.NewRow();
                drC[0] = "Select an option..";
                dtCollars.Rows.Add(drC);
                cmbHoleID.DisplayMember = "HoleID";
                cmbHoleID.ValueMember = "HoleID";
                cmbHoleID.DataSource = dtCollars;
                cmbHoleID.SelectedValue = "Select an option..";
                generalData = dtCollars;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            cmbHoleID.DisplayMember = "HoleID";
            cmbHoleID.ValueMember = "HoleID";
            cmbHoleID.DataSource = generalData;
            cmbHoleID.SelectedValue = "Select an option..";
            dataGridView4.Rows.Clear();
            txtBuscarSelloControl.Text = string.Empty;
            cboSoruce.SelectedIndex = 0;
            textBox2.Text = string.Empty;
            button2.Enabled = false;
        }

        private void cmbHoleID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cmbHoleID.Text))
            {
                DataTable dtCollars = RN.GetDHCollars.getDHHoleID(cmbHoleID.Text);

                dataGridView2.DataSource = dtCollars;
                dataGridView2.AutoResizeColumns();
                dataGridView2.Columns[16].Visible = false;
                containeRegister = false;

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (Convert.ToDecimal(row.Cells[8].Value) > 0 && Convert.ToDecimal(row.Cells[8].Value) <= 0.099M)
                        row.Cells[8].Style.BackColor = Color.Aqua;

                    if (Convert.ToDecimal(row.Cells[8].Value) > 0.1M && Convert.ToDecimal(row.Cells[8].Value) <= 0.299M)
                        row.Cells[8].Style.BackColor = Color.Lime;

                    if (Convert.ToDecimal(row.Cells[8].Value) > 0.3M && Convert.ToDecimal(row.Cells[8].Value) <= 0.499M)
                        row.Cells[8].Style.BackColor = Color.Yellow;

                    if (Convert.ToDecimal(row.Cells[8].Value) >= 0.5M && Convert.ToDecimal(row.Cells[8].Value) <= 0.999M)
                        row.Cells[8].Style.BackColor = Color.Red;

                    if (Convert.ToDecimal(row.Cells[8].Value) >= 1M)
                        row.Cells[8].Style.BackColor = Color.Magenta;

                    if (row.Cells[15].Value != null && !String.IsNullOrEmpty(row.Cells[15].Value.ToString()))
                    {
                        row.Cells[0].Value = true;
                        row.Cells[1].Value = row.Cells[15].Value.ToString().Trim();
                        containeRegister = true;
                    }
                }

                if (!String.IsNullOrEmpty(txtBuscarSelloControl.Text))
                {
                    DataRow datarow;
                    datarow = dtCollars.NewRow(); //Con esto le indica que es una nueva fila.

                    datarow[0] = "";
                    datarow[1] = "";
                    datarow[2] = "";
                    datarow[3] = 0;
                    datarow[4] = 0;
                    datarow[5] = 0;
                    datarow[6] = 0;
                    datarow[7] = 0;
                    datarow[8] = 0;
                    datarow[9] = 0;
                    datarow[10] = 0;
                    datarow[11] = 0;
                    datarow[12] = 0;
                    datarow[13] = 0;
                    datarow[14] = 0;
                    dtCollars.Rows.Add(datarow);

                    if (dataGridView2.Rows.Count > 1)
                    {
                        if (containeRegister)
                        {
                            containeRegister = false;
                            dataGridView2.Columns[0].Visible = false;
                            checkBox1.Checked = false;
                            label6.Text = "Inactive";
                        }
                        DataGridViewCellEventArgs s = new DataGridViewCellEventArgs(0, dataGridView2.RowCount - 1);
                        dataGridView2_CellContentClick(sender, s);
                        valueFirstRecuperation = true;
                    }
                }
                else
                {
                    dataGridView2.Columns[0].Visible = true;
                    checkBox1.Checked = true;
                    containeRegister = true;
                    label6.Text = "Active";
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count > 1)
                    if (e.ColumnIndex == 0)
                    {
                        int indexCurrent = -1;

                        int countRegisterCheck = -1;
                        decimal Length_Grade = 0;
                        decimal au_ppm = 0, ag_ppm = 0;
                        string Vn_mod = string.Empty;

                        string jobno = string.Empty, dhid = string.Empty, from = string.Empty, to = string.Empty;
                        dataGridView4.Rows.Clear();
                        matriz1 matr1 = new matriz1();
                        List<matriz1> listmt1 = new List<matriz1>();

                        matriz2 matr2 = new matriz2();
                        List<matriz2> listmt2 = new List<matriz2>();

                        matriz3 matr3 = new matriz3();
                        List<matriz3> listmt3 = new List<matriz3>();

                        matrizTo matrTo = new matrizTo();
                        List<matrizTo> listmtTo = new List<matrizTo>();

                        matrizMod matrMod = new matrizMod();
                        List<matrizMod> listmtMod = new List<matrizMod>();

                        foreach (DataGridViewRow row in dataGridView2.Rows)
                        {
                            if (row.Index != dataGridView2.Rows.Count - 1)
                            {
                                indexCurrent++;

                                if (Convert.ToBoolean(dataGridView2.Rows[e.RowIndex].Cells[0].Value))
                                    valueModificate = false;
                                else
                                    valueModificate = true;

                                if (e.RowIndex == indexCurrent)
                                    row.Cells[0].Value = valueModificate;

                                if (Convert.ToBoolean(row.Cells[0].Value))
                                {
                                    if (row.Cells[1].Value == null || string.IsNullOrEmpty(row.Cells[1].Value.ToString()))
                                    {
                                        dataGridView2.EndEdit();
                                        MessageBox.Show("Value Vn_Mod is required");

                                        DataGridView dgv = (DataGridView)sender;

                                        dgv.Rows.OfType<DataGridViewRow>().ToList().
                                               ForEach(x => x.Cells[e.ColumnIndex].Value = false);
                                        dataGridView4.Rows.Clear();

                                        //DataGridView dgv = (DataGridView)sender;
                                        //      dataGridView2.EndEdit();

                                        //      dgv.Rows[indexCurrent].Cells[0].Value = false;
                                        //      dgv.Rows[indexCurrent].Cells[1].Value = string.Empty;

                                        button2.Enabled = false;
                                        return;
                                    }

                                    for (int i = 0; i < dataGridView2.ColumnCount; i++)
                                    {
                                        if (i != 8)
                                            row.Cells[i].Style.BackColor = Color.LightBlue;
                                    }

                                    if (Length_Grade == 0)
                                    {
                                        Length_Grade = Convert.ToDecimal(row.Cells[7].Value);
                                        jobno = row.Cells[3].Value.ToString();
                                        dhid = row.Cells[4].Value.ToString();
                                        from = row.Cells[5].Value.ToString();
                                        to = row.Cells[6].Value.ToString();
                                        matrTo.value = to;
                                        listmtTo.Add(matrTo);

                                        if (row.Cells[1].Value != null)
                                        {
                                            Vn_mod = row.Cells[1].Value.ToString();
                                            matrMod.value = Vn_mod;
                                            listmtMod.Add(matrMod);
                                        }

                                        if (row.Cells[8].Value != null)
                                            au_ppm = Convert.ToDecimal(row.Cells[8].Value);

                                        matr1.value = au_ppm;
                                        listmt1.Add(matr1);
                                        matr1 = new matriz1();

                                        if (row.Cells[9].Value != null && !string.IsNullOrEmpty(row.Cells[9].Value.ToString()))
                                            ag_ppm = Convert.ToDecimal(row.Cells[9].Value);

                                        matr2.value = ag_ppm;
                                        listmt2.Add(matr2);
                                        matr2 = new matriz2();

                                        matr3.value = Convert.ToDecimal(row.Cells[7].Value);
                                        listmt3.Add(matr3);
                                        matr3 = new matriz3();

                                        countRegisterCheck++;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            if (!listmtMod[listmtMod.Count - 1].value.Equals(row.Cells[1].Value.ToString()))
                                            {
                                                MessageBox.Show("The  final value 'vn_mod':" + listmtMod[listmtMod.Count - 1].value.ToString() + " does not match the actual initial value 'vn_mod':" + row.Cells[1].Value.ToString() + " in position number:" + (indexCurrent + 1).ToString());
                                                DataGridView dgv = (DataGridView)sender;
                                                dataGridView2.EndEdit();

                                                dgv.Rows[indexCurrent].Cells[0].Value = false;
                                                dgv.Rows[indexCurrent].Cells[1].Value = string.Empty;

                                                //dgv.Rows.OfType<DataGridViewRow>().ToList().
                                                //       ForEach(x => x.Cells[e.ColumnIndex].Value = true);
                                                //dataGridView4.Rows.Clear();
                                                button2.Enabled = false;
                                            }
                                            else
                                            if (listmtTo[listmtTo.Count - 1].value.Equals(row.Cells[5].Value.ToString()))
                                            {
                                                to = row.Cells[6].Value.ToString();
                                                Length_Grade = Length_Grade + Convert.ToDecimal(row.Cells[7].Value);

                                                matrTo.value = to;
                                                listmtTo.Add(matrTo);

                                                if (row.Cells[1].Value != null)
                                                    Vn_mod = row.Cells[1].Value.ToString();

                                                if (row.Cells[8].Value != null)
                                                    au_ppm = Convert.ToDecimal(row.Cells[8].Value);

                                                matr1.value = au_ppm;
                                                listmt1.Add(matr1);
                                                matr1 = new matriz1();

                                                if (row.Cells[9].Value != null && !string.IsNullOrEmpty(row.Cells[9].Value.ToString()))
                                                    ag_ppm = Convert.ToDecimal(row.Cells[9].Value);

                                                matr2.value = ag_ppm;
                                                listmt2.Add(matr2);
                                                matr2 = new matriz2();

                                                matr3.value = Convert.ToDecimal(row.Cells[7].Value);
                                                listmt3.Add(matr3);
                                                matr3 = new matriz3();

                                                countRegisterCheck++;
                                            }
                                            else
                                            {
                                                MessageBox.Show("The  final value 'TO':" + listmtTo[listmtTo.Count - 1].value.ToString() + " does not match the actual initial value 'from':" + row.Cells[5].Value.ToString() + " in position number:" + (indexCurrent + 1).ToString());
                                                DataGridView dgv = (DataGridView)sender;
                                                dataGridView2.EndEdit();

                                                dgv.Rows[indexCurrent].Cells[0].Value = false;
                                                dgv.Rows[indexCurrent].Cells[1].Value = string.Empty;

                                                //dgv.Rows.OfType<DataGridViewRow>().ToList().
                                                //       ForEach(x => x.Cells[e.ColumnIndex].Value = true);
                                                //dataGridView4.Rows.Clear();
                                                button2.Enabled = false;
                                                //return;
                                            }
                                        }
                                        catch
                                        {

                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < dataGridView2.ColumnCount; i++)
                                    {
                                        if (i != 8)
                                            row.Cells[i].Style.BackColor = new Color();
                                    }
                                    if (au_ppm > 0)
                                    {
                                        matrizResult mtrResult = new matrizResult();
                                        List<matrizResult> listmtrResult = new List<matrizResult>();

                                        for (int i = 0; i < listmt1.Count; i++)
                                        {
                                            mtrResult.value = listmt3[i].value * listmt1[i].value;
                                            listmtrResult.Add(mtrResult);
                                            mtrResult = new matrizResult();
                                        }

                                        au_ppm = 0;

                                        for (int i = 0; i < listmtrResult.Count; i++)
                                        {
                                            if (au_ppm == 0)
                                                au_ppm = listmtrResult[i].value;
                                            else
                                                au_ppm = au_ppm + listmtrResult[i].value;
                                        }
                                        mtrResult = new matrizResult();
                                        listmtrResult = new List<matrizResult>();

                                        for (int i = 0; i < listmt3.Count; i++)
                                        {
                                            mtrResult.value = listmt2[i].value * listmt3[i].value;
                                            listmtrResult.Add(mtrResult);
                                            mtrResult = new matrizResult();
                                        }

                                        ag_ppm = 0;

                                        for (int i = 0; i < listmtrResult.Count; i++)
                                        {
                                            if (ag_ppm == 0)
                                                ag_ppm = listmtrResult[i].value;
                                            else
                                                ag_ppm = ag_ppm + listmtrResult[i].value;
                                        }
                                        button2.Enabled = true;

                                        dataGridView4.Rows.Insert(dataGridView4.Rows.Count - 1, jobno, dhid, from, to, Length_Grade, (au_ppm / Length_Grade).ToString("##,0.000"), (ag_ppm / Length_Grade).ToString("##,0.000"), countRegisterCheck + 1, Vn_mod);
                                        dataGridView4.AutoResizeColumns();
                                        int valueDiferencial = (dataGridView4.Rows.Count) - indexCurrent;
                                        for (int i = 0; i < countRegisterCheck + 1; i++)
                                        {
                                            dataGridView4.Rows.Insert(dataGridView4.Rows.Count - 1, "", "", "", "", "", "", "", "");
                                        }
                                        countRegisterCheck = -1;
                                        to = string.Empty; jobno = string.Empty; dhid = string.Empty; from = string.Empty;
                                        ag_ppm = 0;
                                        au_ppm = 0;
                                        Length_Grade = 0;
                                    }
                                    else
                                        dataGridView4.Rows.Insert(dataGridView4.Rows.Count - 1, "", "", "", "", "", "", "", "");
                                }
                            }
                            else
                            {
                                int fila = row.Index;

                                CurrencyManager cm = (CurrencyManager)BindingContext[dataGridView2.DataSource];
                                cm.SuspendBinding();
                                dataGridView2.CurrentCell = null;
                                dataGridView2.Rows[fila - 1].Visible = false;
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            if (dataGridView4.RowCount > 1)
            {
                string cnStr = "Initial Catalog=GZC;Data Source=10.10.203.4; User ID=sa;Password=BdZandor123*;";
                try
                {
                    System.Data.SqlClient.SqlConnection objconexion = OpenConexion(cnStr);

                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        if (row.Cells[0].Value != null && Convert.ToBoolean(row.Cells[0].Value) && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                        {
                            if (row.Cells[1].Value == null)
                            {
                                MessageBox.Show("Select all value Column Vn_mod this select");
                                return;
                            }
                        }
                    }

                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        if (row.Cells[0].Value != null && Convert.ToBoolean(row.Cells[0].Value) && !String.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                        {
                            cmd.CommandText = "UPDATE [GZC].[dbo].[DH_Samples]   SET      Vn_mod = '" + row.Cells[1].Value.ToString() + "' WHERE SKDHSamples =" + Convert.ToInt32(row.Cells[16].Value) + " and HoleID ='" + row.Cells[4].Value.ToString() + "'";
                            //cmd = new System.Data.SqlClient.SqlCommand(cmd.CommandText, objconexion);
                            //cmd.ExecuteNonQuery();
                            LoadLog.alterdataBase(cmd.CommandText);
                            LoadLog.Register(DateTime.Now, clsRf.sUser, IpLocal, IpPublica, SerialHDD, Environment.MachineName, cmd.CommandText, "update");

                        }
                        else
                        {
                            if (row.Cells[15].Value != null && !String.IsNullOrEmpty(row.Cells[15].Value.ToString()))
                            {

                                cmd.CommandText = "UPDATE [GZC].[dbo].[DH_Samples]   SET      Vn_mod = Null WHERE SKDHSamples =" + Convert.ToInt32(row.Cells[16].Value) + " and HoleID ='" + row.Cells[4].Value.ToString() + "'";
                                //cmd = new System.Data.SqlClient.SqlCommand(cmd.CommandText, objconexion);
                                //cmd.ExecuteNonQuery();
                                LoadLog.alterdataBase(cmd.CommandText);
                                LoadLog.Register(DateTime.Now, clsRf.sUser, IpLocal, IpPublica, SerialHDD, Environment.MachineName, cmd.CommandText, "update");

                            }
                        }
                    }
                    var culturaCol = CultureInfo.GetCultureInfo("es-CO");
                    DateTime dateReporte = Convert.ToDateTime(DateTime.Now, culturaCol);

                    foreach (DataGridViewRow row in dataGridView4.Rows)
                    {

                        if (row.Cells[1].Value != null && !String.IsNullOrEmpty(row.Cells[1].Value.ToString()))
                        {

                            cmd.CommandText = String.Format("SELECT ToV FROM DH_IntercepInterval  WHERE HoleID = @HoleID and FromV= @FromV");
                            cmd = new System.Data.SqlClient.SqlCommand(cmd.CommandText, objconexion);

                            cmd.Parameters.AddWithValue("@HoleID ", row.Cells[1].Value.ToString());
                            cmd.Parameters.AddWithValue("@FromV", Convert.ToDecimal(row.Cells[2].Value));
                            object valueTo = cmd.ExecuteScalar();

                            //LoadLog.alterdataBase(cmd.CommandText);
                            LoadLog.Register(dateReporte, clsRf.sUser, IpLocal, IpPublica, SerialHDD, Environment.MachineName, cmd.CommandText, "Search");


                            if (valueTo != null && valueTo.ToString().Length > 0)
                            {
                                string valueDescrioption = string.Empty;
                                if (!string.IsNullOrEmpty(textBox2.Text.Trim()))
                                {
                                    valueDescrioption = string.Concat(textBox2.Text, " Con intervale inicial de ", Convert.ToDecimal(row.Cells[2].Value), " hasta ", valueTo);
                                    cmd.CommandText = "update GZC.dbo.DH_IntercepInterval set Tov=" + Convert.ToDecimal(row.Cells[3].Value) + ",Length_Grade=" + Convert.ToDecimal(row.Cells[4].Value) + ", Au_Grade=" + Convert.ToDecimal(row.Cells[5].Value) + ",Ag_Grade= " + Convert.ToDecimal(row.Cells[6].Value) + ",Comments='" + valueDescrioption + "',TotalRegister=" + Convert.ToInt32(row.Cells[7].Value) + " , Date_Event ='" + dateReporte.ToString() + "' where HoleID ='" + row.Cells[1].Value.ToString() + "' and Fromv =" + Convert.ToDecimal(row.Cells[2].Value);
                                    //cmd = new System.Data.SqlClient.SqlCommand(cmd.CommandText, objconexion);
                                    //cmd.ExecuteNonQuery();
                                    LoadLog.alterdataBase(cmd.CommandText);
                                    LoadLog.Register(dateReporte, clsRf.sUser, IpLocal, IpPublica, SerialHDD, Environment.MachineName, cmd.CommandText, "Update");

                                }
                                else
                                {
                                    MessageBox.Show("Comment is required for the rank update");
                                    return;
                                }
                            }
                            else
                            {
                                cmd.CommandText = "INSERT INTO GZC.dbo.DH_IntercepInterval(HoleID,Fromv,Tov,Length_Grade,Au_Grade,Ag_Grade,Comments,TotalRegister,Vn_mod,Date_Event)VALUES(" + "'" + row.Cells[1].Value.ToString() + "'," + Convert.ToDecimal(row.Cells[2].Value) + " ," + Convert.ToDecimal(row.Cells[3].Value) + "," + Convert.ToDecimal(row.Cells[4].Value) + " ," + Convert.ToDecimal(row.Cells[5].Value) + "," + Convert.ToDecimal(row.Cells[6].Value) + ",'" + textBox2.Text + "'," + Convert.ToInt32(row.Cells[7].Value) + ",'" + row.Cells[8].Value.ToString() + "','" + dateReporte + "')";
                                //cmd = new System.Data.SqlClient.SqlCommand(cmd.CommandText, objconexion);
                                //cmd.ExecuteNonQuery();
                                LoadLog.alterdataBase(cmd.CommandText);
                                LoadLog.Register(dateReporte, clsRf.sUser, IpLocal, IpPublica, SerialHDD, Environment.MachineName, cmd.CommandText, "Update");

                            }
                        }
                    }

                    Clear();
                    MessageBox.Show("Save full");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message);

                }
            }
        }

        public SqlConnection OpenConexion(string cnnString)
        {
            SqlConnection Conn = new SqlConnection();
            Conn.ConnectionString = cnnString;
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
                MessageBox.Show("Error al Abrir Conexion " + Ex.Errors);
            }
            return Conn;
        }

        private void txtBuscarSelloControl_Leave(object sender, EventArgs e)
        {
            DataTable dtCollars = generalData;

            if (txtBuscarSelloControl.Text.Length > 1 && !String.IsNullOrEmpty(cboSoruce.Text))
            {
                txtBuscarSelloControl.Text = txtBuscarSelloControl.Text.Replace("\r\n", "");
                string _sqlWhere = "HoleID  LIKE '%" + txtBuscarSelloControl.Text + "%' and Source='" + cboSoruce.Text + "'";

                try
                {
                    if (dtCollars.Select(_sqlWhere).Count() > 0)
                    {

                        DataTable _newDataTable = dtCollars.Select(_sqlWhere).CopyToDataTable();
                        if (_newDataTable.Rows.Count == 1)
                        {

                            cmbHoleID.DisplayMember = "HoleID";
                            cmbHoleID.ValueMember = "HoleID";
                            cmbHoleID.DataSource = _newDataTable;
                            cmbHoleID.SelectedValue = "Select an option..";

                            if (!valueFirstRecuperation)
                            {
                                dataGridView4.Rows.Clear();
                                valueFirstRecuperation = false;

                            }
                        }
                        else
                        {
                            if (_newDataTable.Rows.Count == 0)
                            {
                                cmbHoleID.DisplayMember = "HoleID";
                                cmbHoleID.ValueMember = "HoleID";
                                cmbHoleID.DataSource = generalData;
                                cmbHoleID.SelectedValue = "Select an option..";

                                MessageBox.Show("No record matches");
                            }
                            else
                            {
                                cmbHoleID.DisplayMember = "HoleID";
                                cmbHoleID.ValueMember = "HoleID";
                                cmbHoleID.DataSource = generalData;
                                cmbHoleID.SelectedValue = "Select an option..";

                                MessageBox.Show("Enter only the precise record");
                            }
                        }
                    }
                    else
                    {
                        cmbHoleID.DisplayMember = "HoleID";
                        cmbHoleID.ValueMember = "HoleID";
                        cmbHoleID.DataSource = generalData;
                        cmbHoleID.SelectedValue = "Select an option..";
                        MessageBox.Show("No record matches");
                    }
                }
                catch (Exception ex)
                {
                }

            }
            else
            {
                if (string.IsNullOrEmpty(cboSoruce.Text))
                    MessageBox.Show("Select to Source");
            }
        }



        private void txtBuscarSelloControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)(Keys.Enter))
            {
                DataTable dtCollars = generalData;

                if (txtBuscarSelloControl.Text.Length > 1 && !String.IsNullOrEmpty(cboSoruce.Text))
                {
                    txtBuscarSelloControl.Text = txtBuscarSelloControl.Text.Replace("\r\n", "");
                    string _sqlWhere = "HoleID  LIKE '%" + txtBuscarSelloControl.Text + "%'and Source='" + cboSoruce.Text + "'";

                    try
                    {
                        if (dtCollars.Select(_sqlWhere).Count() > 0)
                        {
                            DataTable _newDataTable = dtCollars.Select(_sqlWhere).CopyToDataTable();
                            if (_newDataTable.Rows.Count == 1)
                            {
                                cmbHoleID.DisplayMember = "HoleID";
                                cmbHoleID.ValueMember = "HoleID";
                                cmbHoleID.DataSource = _newDataTable;
                                cmbHoleID.SelectedValue = "Select an option..";
                                dataGridView4.Rows.Clear();
                            }
                            else
                            {
                                if (_newDataTable.Rows.Count == 0)
                                {
                                    cmbHoleID.DisplayMember = "HoleID";
                                    cmbHoleID.ValueMember = "HoleID";
                                    cmbHoleID.DataSource = generalData;
                                    cmbHoleID.SelectedValue = "Select an option..";

                                    MessageBox.Show("No record matches");
                                }
                                else
                                {
                                    cmbHoleID.DisplayMember = "HoleID";
                                    cmbHoleID.ValueMember = "HoleID";
                                    cmbHoleID.DataSource = generalData;
                                    cmbHoleID.SelectedValue = "Select an option..";

                                    MessageBox.Show("Enter only the precise record");
                                }
                            }
                        }
                        else
                        {
                            cmbHoleID.DisplayMember = "HoleID";
                            cmbHoleID.ValueMember = "HoleID";
                            cmbHoleID.DataSource = generalData;
                            cmbHoleID.SelectedValue = "Select an option..";
                            MessageBox.Show("No record matches");
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(cboSoruce.Text))
                     MessageBox.Show("Select to Source");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            exportIntercept(cboSoruce.Text);
            var culturaCol = CultureInfo.GetCultureInfo("es-CO");
            DateTime dateReporte = Convert.ToDateTime(DateTime.Now, culturaCol);


            LoadLog.Register(dateReporte, clsRf.sUser, IpLocal, IpPublica, SerialHDD, Environment.MachineName, "Generate Report", "Report");

        }

        private void exportIntercept(string HoldId)
        {
            try
            {
                //QCReport
                Excel.Application oXL;
                Excel._Workbook oWB;
                Excel._Worksheet oSheet;

                oXL = new Excel.Application();
                string pathArchive = string.Concat(Application.StartupPath, "\\GCG_Intercept_Intervals.xls");
                oWB = oXL.Workbooks.Open(pathArchive, 0, true, 5,
                Type.Missing, Type.Missing, false, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, null, null);

                oSheet = (Excel._Worksheet)oWB.Sheets[1];

                if (cboSoruce.Text.Contains("GEX"))
                    oSheet.Cells[4, 7] = "Pozos Exploración";
                else
                    oSheet.Cells[4, 7] = "Pozos Geología Minas";
                
                if (dataGridView2.Rows.Count > 0)
                {
                    int iInicial = 9;
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        if (i != dataGridView2.Rows.Count - 2)
                        {
                            if (dataGridView2.Rows[i].Cells[0].Value != null)
                                if (Convert.ToBoolean(dataGridView2.Rows[i].Cells[0].Value))
                                {
                                    oSheet.Cells[iInicial, 1] = "*".ToString();//Hole
                                    var columnHeadingsRange1 = oSheet.Range[
                                       oSheet.Cells[iInicial, 1],
                                       oSheet.Cells[iInicial, 9]];

                                    columnHeadingsRange1.Interior.Color = Color.LightBlue;

                                    var columnHeadingsRange2 = oSheet.Range[
                                      oSheet.Cells[iInicial, 11],
                                      oSheet.Cells[iInicial, 11]];

                                    columnHeadingsRange2.Interior.Color = Color.LightBlue;

                                }
                            oSheet.Cells[iInicial, 2] = dataGridView2.Rows[i].Cells[3].Value.ToString();
                            oSheet.Cells[iInicial, 3] = dataGridView2.Rows[i].Cells[4].Value.ToString();
                            oSheet.Cells[iInicial, 4] = dataGridView2.Rows[i].Cells[5].Value.ToString();
                            oSheet.Cells[iInicial, 5] = dataGridView2.Rows[i].Cells[6].Value.ToString();
                            oSheet.Cells[iInicial, 6] = dataGridView2.Rows[i].Cells[7].Value.ToString();
                            oSheet.Cells[iInicial, 7] = dataGridView2.Rows[i].Cells[10].Value.ToString();
                            oSheet.Cells[iInicial, 8] = dataGridView2.Rows[i].Cells[11].Value.ToString();
                            oSheet.Cells[iInicial, 9] = dataGridView2.Rows[i].Cells[15].Value.ToString();

                            var columnHeadingsRange = oSheet.Range[
     oSheet.Cells[iInicial, 10],
     oSheet.Cells[iInicial, 10]];

                            oSheet.Cells[iInicial, 10] = dataGridView2.Rows[i].Cells[8].Value.ToString();

                            if (Convert.ToDecimal(dataGridView2.Rows[i].Cells[8].Value) > 0 && Convert.ToDecimal(dataGridView2.Rows[i].Cells[8].Value) <= 0.099M)
                                columnHeadingsRange.Interior.Color = Color.Aqua;

                            if (Convert.ToDecimal(dataGridView2.Rows[i].Cells[8].Value) > 0.1M && Convert.ToDecimal(dataGridView2.Rows[i].Cells[8].Value) <= 0.299M)
                                columnHeadingsRange.Interior.Color = Color.Lime;

                            if (Convert.ToDecimal(dataGridView2.Rows[i].Cells[8].Value) > 0.3M && Convert.ToDecimal(dataGridView2.Rows[i].Cells[8].Value) <= 0.499M)
                                columnHeadingsRange.Interior.Color = Color.Yellow;


                            if (Convert.ToDecimal(dataGridView2.Rows[i].Cells[8].Value) >= 0.5M && Convert.ToDecimal(dataGridView2.Rows[i].Cells[8].Value) <= 0.999M)
                                columnHeadingsRange.Interior.Color = Color.Red;

                            if (Convert.ToDecimal(dataGridView2.Rows[i].Cells[8].Value) >= 1M)
                                columnHeadingsRange.Interior.Color = Color.Magenta;

                            oSheet.Cells[iInicial, 11] = dataGridView2.Rows[i].Cells[9].Value.ToString();
                            oSheet.Cells[iInicial, 12] = dataGridView4.Rows[i].Cells[4].Value.ToString();
                            oSheet.Cells[iInicial, 13] = dataGridView4.Rows[i].Cells[5].Value.ToString();
                            oSheet.Cells[iInicial, 14] = dataGridView4.Rows[i].Cells[6].Value.ToString();
                            iInicial++;
                        }
                        else
                            break;
                    }
                }
                oXL.Visible = true;
                oXL.UserControl = true;
}
            catch (Exception ex)
            {
                if (ex.Message == "No se puede encontrar la tabla 0.")
                {
                    MessageBox.Show("No hay datos que mostrar");
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!containeRegister)
            {
                dataGridView2.Columns[0].Visible = true;
                checkBox1.Checked = true;
                containeRegister = true;
                label6.Text = "Active";
            }
            else
            {
                dataGridView2.Columns[0].Visible = false;
                checkBox1.Checked = false;
                containeRegister = false;
                label6.Text = "Inactive";
            }

        }
    }
}
