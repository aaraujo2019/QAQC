using Intercept_Intervals.UI;
using RN.RulesBussines;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnviosColombiaGold.Rol
{

    /// <summary>
    /// Formulario encargado de asignar los permisos a los controles por usuario dependiendo del rol
    /// </summary>
    public partial class ManagePermissions : Form
    {
        #region Variables
        public string Usuario { get; set; }
        public string IpLocal { get; set; }
        public string IpPublica { get; set; }
        public string SerialHDD { get; set; }
        public string NameForm { get; set; }
        private Dictionary<string, string> oldMenuToolTips =
          new Dictionary<string, string>();
        private Form workingForm;
        #endregion

        #region Constructor
        public ManagePermissions()
        {
            InitializeComponent();

            comboBox1.Items.Insert(0, "frm_Intercept_Intervals");
            comboBox1.Items.Insert(1, "frmPpal");

            if (string.IsNullOrEmpty(this.IpLocal))
                this.IpLocal = Adress_IP.Local();

            if (string.IsNullOrEmpty(this.IpPublica))
                this.IpPublica = Adress_IP.Publica();

            if (string.IsNullOrEmpty(this.SerialHDD))
                this.SerialHDD = Adress_IP.SerialNumberDisk();

            if (string.IsNullOrEmpty(this.Usuario))
                this.Usuario = Adress_IP.SerialNumberDisk();
        }
        public void ManagePermission(Form f, ToolTip toolTip1, ToolTip toolTip2)
        {
            PageControls.Items.Clear();
            workingForm = f;
            this.Text += " Por vista" + f.Name;
            ShowControls(f.Controls);
            PopulatePermissionTree();
        }

        public ManagePermissions(Form f, ToolTip toolTip1, ToolTip toolTip2)
        {
            InitializeComponent();
            workingForm = f;

            this.Text += " Por vista" + f.Name;
            ShowControls(f.Controls);
            PopulatePermissionTree();
        }
        #endregion

        #region Eventos

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    oldMenuToolTips.Clear();
                    frm_Intercept_Intervals oDB = new frm_Intercept_Intervals();
                    ManagePermission(oDB, null, null);

                    break;
                case 1:

                    oldMenuToolTips.Clear();
                    FrmPpal principal = new FrmPpal();
                    ManagePermission(principal, null, null);

                    break;

                default:
                    break;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var culturaCol = CultureInfo.GetCultureInfo("es-CO");
            DateTime dateReporte = Convert.ToDateTime(DateTime.Now, culturaCol);


            SqlConnection conn = LoadLog.GetConnection();

            SqlParameter param;
            foreach (String controlID in PageControls.SelectedItems)
            {
                foreach (DataRowView roleRow in PermissionRoles.SelectedItems)
                {

                    int roleID = Convert.ToInt32(roleRow["RoleID"]);
                    try
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = "spInsertNewControlToRole";
                        cmd.CommandType = CommandType.StoredProcedure;

                        param = cmd.Parameters.Add("@RoleID", SqlDbType.Int);
                        param.Value = roleID;
                        param.Direction = ParameterDirection.Input;

                        param = cmd.Parameters.Add("@PageName", SqlDbType.VarChar, 50);
                        param.Value = workingForm.Name.ToString();
                        param.Direction = ParameterDirection.Input;

                        param = cmd.Parameters.Add("@ControlID", SqlDbType.VarChar, 50);
                        param.Value = controlID;
                        param.Direction = ParameterDirection.Input;

                        param = cmd.Parameters.Add("@invisible", SqlDbType.Int);
                        param.Value = InVisible.Checked ? 1 : 0;
                        param.Direction = ParameterDirection.Input;

                        param = cmd.Parameters.Add("@disabled", SqlDbType.Int);
                        param.Value = Disabled.Checked ? 1 : 0;
                        param.Direction = ParameterDirection.Input;

                        int rowsInserted = cmd.ExecuteNonQuery();
                        if (rowsInserted > 0)

                            MessageBox.Show("Register Insert succesfull!");

                        if (string.IsNullOrEmpty(this.IpLocal))
                            this.IpLocal = Adress_IP.Local();

                        if (string.IsNullOrEmpty(this.IpPublica))
                            this.IpPublica = Adress_IP.Publica();

                        if (string.IsNullOrEmpty(this.SerialHDD))
                            this.SerialHDD = Adress_IP.SerialNumberDisk();

                        if (string.IsNullOrEmpty(this.Usuario))
                            this.Usuario = Adress_IP.SerialNumberDisk();

                        LoadLog.Register(dateReporte, this.Usuario, this.IpLocal, this.IpPublica, this.SerialHDD, Environment.MachineName, "Creación de permisos para el control", "ASignación de permisos al control");
   }
                    catch (Exception ex)
                    {
                        DisplayError(controlID, roleID, ex.Message);
                    }
                }
            }
            conn.Close();
            PopulatePermissionTree();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string controlID = string.Empty;
            int roleID = 0;
            try
            {
                var culturaCol = CultureInfo.GetCultureInfo("es-CO");
                DateTime dateReporte = Convert.ToDateTime(DateTime.Now, culturaCol);

                string[] node = PermissionTree.SelectedNode.FullPath.Split(':');
                string[] nameForm;
                if (node.Length > 1)
                {
                    if (!ByRoleRB.Checked)
                    {
                        nameForm = node[1].Split('-');

                        var roleId = this.controlSecurityDataSet1.Roles.Where(u => u.RoleName == nameForm[1].ToString()).Select(s => s.RoleID);

                        foreach (var item in roleId)
                        {
                            roleID = item;
                        }

                        string nameForms = nameForm[0].ToString();

                        nameForm = node[0].Split('\\');
                        controlID = nameForm[0].ToString();

                        object rowsInserted = LoadLog.update_Role(roleID, nameForms, controlID, InVisible.Checked ? 1 : 0, Disabled.Checked ? 1 : 0);


                        if (Convert.ToInt32(rowsInserted) > 0)

                            MessageBox.Show("Register Update succesfull!");

                        else
                            DisplayError(controlID, roleID, "Register Insert= " + rowsInserted.ToString());

                    }
                    else
                    {
                        nameForm = node[0].Split('-');
                        string[] nameForm1 = nameForm[0].ToString().Split('\\');
                        var roleId = this.controlSecurityDataSet1.Roles.Where(u => u.RoleName == nameForm1[0].ToString()).Select(s => s.RoleID);

                        foreach (var item in roleId)
                        {
                            roleID = item;
                        }

                        string nameForms = nameForm[0].ToString();

                        nameForm = node[1].Split('-');
                        nameForms = nameForm[0].ToString();
                        controlID = nameForm[1].ToString();

                        object rowsInserted = LoadLog.update_Role(roleID, nameForms, controlID, InVisible.Checked ? 1 : 0, Disabled.Checked ? 1 : 0);


                        if (Convert.ToInt32(rowsInserted) > 0)
                            DisplayError(controlID, roleID, "Register Insert= " + rowsInserted.ToString());

                        else
                            MessageBox.Show("Register Update succesfull!");
                    }


                    LoadLog.Register(dateReporte, this.Usuario, this.IpLocal, this.IpPublica, this.SerialHDD, Environment.MachineName, "Modificación de registro para asingar nuevos valores", "Asignación de permisos");


                }
            }
            catch (Exception ex)
            {
                DisplayError(controlID, roleID, ex.Message);
            }

            PopulatePermissionTree();
        }
        private void ManagePermissions_Load(object sender, EventArgs e)
        {
            this.rolesTableAdapter1.Fill(this.controlSecurityDataSet1.Roles);

        }

        private void PermissionTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (PermissionTree.SelectedNode != null)
            {
                string[] node = e.Node.FullPath.Split(':');

                if (node.Length > 1)
                {
                    string[] controller = node[2].Split(',');

                    if (controller[0].ToString().Trim().ToUpper().Contains("NO"))
                        InVisible.Checked = true;
                    else
                        InVisible.Checked = false;

                    if (controller[1].ToString().Trim().ToUpper().Contains("INACTIVO"))
                        Disabled.Checked = true;
                    else
                        Disabled.Checked = false;

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose(true);
        }

        private void ByControlRB_CheckedChanged(object sender, EventArgs e)
        {
            PopulatePermissionTree();
        }

        #endregion

        #region
        /// <summary>
        /// Control de errores
        /// </summary>
        /// <param name="controlID">Tipo de control</param>
        /// <param name="roleID">Tipo de rol</param>
        /// <param name="message">El mensaje que se muestra</param>
        private void DisplayError(string controlID, int roleID, string message)
        {
            MessageBox.Show("No se puede agregar control(" + controlID + ") y el rol (" + roleID + ")" + message,
                "No se puede agregar control al rol",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        /// <summary>
        /// Metodo encargado de consultar los permisos y organizarlos deacuerdo como se chekee la caja de chekeo
        /// </summary>
        private void PopulatePermissionTree()
        {
            string queryString = "select ctr .fkpage, controlID, Invisible, Disabled, RoleName " +
            "from ControlsToRoles ctr " +
            " join controls c on c.ControlID = ctr.FKControlID and c.Page = ctr.FKPage " +
            " join roles r on r.RoleID = ctr.FKRole where  ctr .fkpage ='"+ comboBox1.Text+"'";

            if (ByControlRB.Checked)

                queryString += " order by ControlID";

            else

                queryString += " order by RoleName";

            DataTable dt = null;
            try

            {
                dt = LoadLog.GetRoles(queryString);

            }
            catch (Exception e)
            {
                MessageBox.Show("Unable to recover permissions: " + e.Message,
                    "Error retrieving permissions",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

            PermissionTree.BeginUpdate();
            PermissionTree.Nodes.Clear();
            TreeNode parentNode = null;
            TreeNode subNode = null;

            string currentName = string.Empty;
            foreach (DataRow row in dt.Rows)
            {
                string subNodeText = String.Concat(" Formulario: ", row["fkpage"].ToString(), "-", (ByControlRB.Checked ? row["RoleName"].ToString() : row["ControlID"].ToString()));
                subNodeText += ":";
                subNodeText += Convert.ToInt32(row["Invisible"]) == 0 ? " visible " : " no visible ";
                subNodeText += ", ";
                subNodeText += Convert.ToInt32(row["Disabled"]) == 0 ? " Activo " : " Inactivo ";


                subNode = new TreeNode(subNodeText);
                string dataName = ByControlRB.Checked ? row["ControlID"].ToString() : row["RoleName"].ToString();
                if (currentName != dataName)
                {
                    parentNode = new TreeNode(dataName);
                    currentName = dataName;
                    PermissionTree.Nodes.Add(parentNode);
                }

                if (parentNode != null)

                    parentNode.Nodes.Add(subNode);

            }
            PermissionTree.EndUpdate();
        }

        /// <summary>
        /// Metodo encargado de mostrar el nombre de los controles de la ventana
        /// </summary>
        /// <param name="controlCollection"></param>
        private void ShowControls(Control.ControlCollection controlCollection)
        {
            foreach (Control c in controlCollection)
            {
                if (c.Controls.Count > 0)
                    ShowControls(c.Controls);

                if (c is MenuStrip)
                {
                    MenuStrip menuStrip = c as MenuStrip;
                    ShowToolStipItems(menuStrip.Items);
                }

                if (c is Button || c is ComboBox || c is TextBox ||
                    c is ListBox || c is DataGridView || c is RadioButton ||
                    c is RichTextBox || c is TabPage || c is TextBox || c is GroupBox)

                    PageControls.Items.Add(c.Name);
            }
        }
        /// <summary>
        /// Meotdo encargado de validar los items de los toolStrip
        /// </summary>
        /// <param name="toolStripItems">Control con la colecciòn de ToolsTrip</param>
        private void ShowToolStipItems(ToolStripItemCollection toolStripItems)
        {
            foreach (ToolStripMenuItem mi in toolStripItems)
            {
                oldMenuToolTips.Add(mi.Name, mi.ToolTipText);
                mi.ToolTipText = mi.Name;

                if (mi.DropDownItems.Count > 0)
                {
                    ShowToolStipItems(mi.DropDownItems);
                }

                PageControls.Items.Add(mi.Name);
            }
        }

        #endregion

        private void button4_Click(object sender, EventArgs e)
        {
            if (PermissionTree.SelectedNode != null)
            {
                string[] node = PermissionTree.SelectedNode.FullPath.Split('\\');
                string controlID = string.Empty;
                string page = string.Empty;

                if (node.Length > 1)
                {

                    controlID = node[0].ToString().Trim();
                    string[] separator = node[1].ToString().Trim().Split(':');
                    separator = separator[1].Split('-');
                    page = separator[0].ToString().Trim();
                }
                else
                {
                    MessageBox.Show("Select a relationship to eliminate");
                    return;
                }
                //userIDMess = userID;
                //roleIDMess = roleID;
                //var nameUser = this.controlSecurityDataSet1.Users.Where(u => u.Name == userID).Select(s => s.UserID);
                //// this.controlSecurityDataSet1.Roles
                //var roleId = this.controlSecurityDataSet1.Roles.Where(u => u.RoleName == roleID).Select(s => s.RoleID);

                //foreach (var item in nameUser)
                //{
                //    userID = item.ToString().Trim();
                //}
                //foreach (var item in roleId)
                //{
                //    roleID = item.ToString().Trim();
                //}
                try
                {
                    object rowsInserted = LoadLog.Delete_Control_Role("spDeleteControlsToRoles", page, controlID);


                    if (Convert.ToInt32(rowsInserted) > 0)
                    { MessageBox.Show("Register Delete succesfull"); PopulatePermissionTree(); }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error the Aliminate objeto", ex.Message);
                }
                //conn.Close();
                //FillUsersInRollsTree();
            }
            else
                MessageBox.Show("Select a relationship to eliminate");
        }
    }
}
