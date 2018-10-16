using EnviosColombiaGold.CommonUI;
using EnviosColombiaGold.Rol.DataSet;
using RN;
using RN.RulesBussines;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnviosColombiaGold.Rol
{
    /// <summary>
    /// Formulario encargado de crear los roles y usuarioen el sistema para ser asginados entre si, 
    /// </summary>
    public partial class ManageRoles : Form
    {
        #region Variables
        DataRow rows;
        #endregion 

        #region Constructor
        public ManageRoles()
        {
            InitializeComponent();
            FillUsersInRollsTree();
        }
        #endregion

        #region Eventos
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            var user = DirectorioActivo.ExisteUsuarioAD(GlobalValue.Dominio, NewUserName.Text);

            if (user != null)
            {
                ControlSecurityDataSet.UsersRow newUsersRow;
                newUsersRow = controlSecurityDataSet1.Users.NewUsersRow();
                newUsersRow.Name = NewUserName.Text.Trim();
                newUsersRow.Identification = txbIdentificacion.Text.Trim();
                NewUserName.Text = string.Empty;
                txbIdentificacion.Text = string.Empty;
                this.controlSecurityDataSet1.Users.Rows.Add(newUsersRow);
                this.usersTableAdapter1.Update(this.controlSecurityDataSet1.Users);
                AppUsersListBox.SelectedIndex = -1;
            }
            else
                MessageBox.Show("The user does not exist in the active directory");
        }

        private void botonEliminarUsuario_Click(object sender, EventArgs e)
        {
            rows = this.controlSecurityDataSet1.Users.Where(r => r.Name == AppUsersListBox.Text).FirstOrDefault();
            var vass = this.controlSecurityDataSet1.Users.Where(r => r.Name == AppUsersListBox.Text).FirstOrDefault();

            int value = this.usersTableAdapter1.Delete(rows[0].ToString().Trim(), Convert.ToInt32(rows[1]), rows[2].ToString(), rows[3].ToString());

            DataRowAction act = new DataRowAction();
            ControlSecurityDataSet.UsersRow rw = this.controlSecurityDataSet1.Users[6];
            rw.Delete();
            vass.Delete();
            ControlSecurityDataSet.UsersRowChangeEvent es = new ControlSecurityDataSet.UsersRowChangeEvent(rw, act);
            controlSecurityDataSet1.Users.Rows.Remove(rows);
            controlSecurityDataSet1.Users.AcceptChanges();
            //object val;
            //this.controlSecurityDataSet1.Users.UsersRowDeleted += Users_UsersRowDeleted(null, es);

            this.usersTableAdapter1.Update(this.controlSecurityDataSet1.Users);

            AppUsersListBox.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newName = string.Empty;
            newName = NewRoleName.Text;
            NewRoleName.Text = string.Empty; // clear the control

            ControlSecurityDataSet.RolesRow newRolesRow;
            newRolesRow = controlSecurityDataSet1.Roles.NewRolesRow();
            newRolesRow.RoleName = newName;
            this.controlSecurityDataSet1.Roles.Rows.Add(newRolesRow);

            try
            {
                this.rolesTableAdapter1.Update(this.controlSecurityDataSet1.Roles);
            }
            catch (Exception ex)
            {
                this.controlSecurityDataSet1.Roles.Rows.Remove(newRolesRow);
                MessageBox.Show("You can not add the role" + newName + ex.Message,
                "Can not add the role!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            RolesListBox.SelectedIndex = -1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow rows = this.controlSecurityDataSet1.Roles.Where(r => r.RoleName == RolesListBox.Text).FirstOrDefault();
               
                object rowsInserted = LoadLog.Delete_Role("spDeleteRole", 0, Convert.ToInt32(rows[1]), false);

                if (Convert.ToInt32(rowsInserted) > 0)
                {
                    MessageBox.Show("Role Delete Sucesfull", "Message");
                }


                this.controlSecurityDataSet1.Roles.Where(r => r.RoleName == RolesListBox.Text).FirstOrDefault().Delete();

                controlSecurityDataSet1.Roles.Rows.Remove(rows);
                this.rolesTableAdapter1.Update(this.controlSecurityDataSet1.Roles);
                RolesListBox.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("FK_UsersToRoles_Roles"))
                    DisplayMessage("FK_UsersToRoles_Roles");
                else
                    MessageBox.Show( ex.Message,"Can not delete the role!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void AddUsersToRole_Click(object sender, EventArgs e)
        {
            //SqlConnection conn = Conexion.OpenConexion();

            //SqlParameter param;

            foreach (DataRowView userRow in AppUsersListBox.SelectedItems)
            {
                foreach (DataRowView roleRow in RolesListBox.SelectedItems)
                {
                    int userID = Convert.ToInt32(userRow["UserID"]);
                    int roleID = Convert.ToInt32(roleRow["RoleID"]);
                    try
                    {
                        object rowsInserted = LoadLog.Delete_Role("spInsertNewUserInRole", userID, roleID, true);

                        if (Convert.ToInt32(rowsInserted) > 0)
                            MessageBox.Show("Relationship created successfully");
                    }
                    catch (Exception ex)
                    {
                        DisplayError(userID, roleID, ex.Message);
                    }

                }
            }
            //conn.Close();
            FillUsersInRollsTree();
        }


        private void botonEliminarRelUsuRol_Click(object sender, EventArgs e)
        {
            //SqlConnection conn = Conexion.OpenConexion();
            //SqlParameter param;


            if (UsersInRoles.SelectedNode != null)
            {
                string[] node = UsersInRoles.SelectedNode.FullPath.Split('\\');
                string userID = string.Empty;
                string roleID = string.Empty;

                string userIDMess = string.Empty;
                string roleIDMess = string.Empty;

                if (node.Length > 1)
                {
                    if (rbName.Checked)
                    {
                        userID = node[0].ToString().Trim();

                        roleID = node[1].ToString().Trim();
                    }
                    else
                    {
                        userID = node[1].ToString().Trim();

                        roleID = node[0].ToString().Trim();
                    }
                }
                else
                {
                    MessageBox.Show("Select a relationship to eliminate");
                    return;
                }
                userIDMess = userID;
                roleIDMess = roleID;
                var nameUser = this.controlSecurityDataSet1.Users.Where(u => u.Name == userID).Select(s => s.UserID);
                // this.controlSecurityDataSet1.Roles
                var roleId = this.controlSecurityDataSet1.Roles.Where(u => u.RoleName == roleID).Select(s => s.RoleID);

                foreach (var item in nameUser)
                {
                    userID = item.ToString().Trim();
                }
                foreach (var item in roleId)
                {
                    roleID = item.ToString().Trim();
                }
                try
                {
                 
                    object rowsInserted = LoadLog.Delete_Role("spDeleteUserInRole", Convert.ToInt32(userID), Convert.ToInt32(roleID), true);


                    if (Convert.ToInt32(rowsInserted) > 0)
                        DisplayConfirmation(userIDMess, roleIDMess);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error the Aliminate objeto", ex.Message);
                }
                //conn.Close();
                FillUsersInRollsTree();
            }
            else
                MessageBox.Show("Select a relationship to eliminate");
        }
        private void rbName_Click(object sender, EventArgs e)
        {
            FillUsersInRollsTree();
        }

        private void rbRole_Click(object sender, EventArgs e)
        {
            FillUsersInRollsTree();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.Close();
            this.Dispose(true);
        }
        private void ManageRoles_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'controlSecurityDataSet1.Roles' Puede moverla o quitarla según sea necesario.
            this.rolesTableAdapter1.Fill(this.controlSecurityDataSet1.Roles);
            // TODO: esta línea de código carga datos en la tabla 'controlSecurityDataSet1.Users' Puede moverla o quitarla según sea necesario.
            this.usersTableAdapter1.Fill(this.controlSecurityDataSet1.Users);

        }
        #endregion

        #region Metodos
        /// <summary>
        /// Metodo encargado de recuperar roles por usuario o usuario por roles 
        /// </summary>
        private void FillUsersInRollsTree()
        {
            string queryString = "select u.Name, r.RoleName from userstoRoles utr " +
            " join users u on u.userID = utr.FKUserID " +
            " join Roles r on r.roleID = utr.FKRoleID ";

            if (rbName.Checked)
            {
                queryString += "order by Name";
            }
            else
            {
                queryString += "order by RoleName";
            }

            UsersInRoles.BeginUpdate();
            UsersInRoles.Nodes.Clear();
            TreeNode parentNode = null;
            TreeNode subNode = null;

            DataTable dt = LoadLog.GetRoles(queryString);
            string currentName = string.Empty;
            foreach (DataRow row in dt.Rows)
            {
                if (rbName.Checked)
                {
                    subNode = new TreeNode(row["roleName"].ToString());
                    if (currentName != row["Name"].ToString())
                    {
                        parentNode = new TreeNode(row["Name"].ToString());
                        currentName = row["Name"].ToString();
                        UsersInRoles.Nodes.Add(parentNode);
                    }
                }
                else
                {
                    subNode = new TreeNode(row["Name"].ToString());
                    if (currentName != row["RoleName"].ToString())
                    {
                        parentNode = new TreeNode(row["RoleName"].ToString());
                        currentName = row["RoleName"].ToString();
                        UsersInRoles.Nodes.Add(parentNode);
                    }

                }

                if (parentNode != null)
                {
                    parentNode.Nodes.Add(subNode);
                }
            }
            UsersInRoles.EndUpdate();
        }
        private void DisplayError(int userID, int roleID, string message)
        {
            MessageBox.Show("Not add user  (" + userID + ") the rol (" + roleID + ")" + message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void DisplayConfirmation(string userID, string roleID)
        {
            MessageBox.Show("Objeto delete (" + userID + ") the rol (" + roleID + ") The relation in DataBase", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void DisplayMessage(string message)
        {
            MessageBox.Show("Not delete Rol for" + message,
                "role associated with user",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        #endregion

    }
}
