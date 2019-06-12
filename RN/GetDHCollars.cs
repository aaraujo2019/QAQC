using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RN
{
 public  static class GetDHCollars
    {
        public static DataTable getDHCollars(string sHoleID)
        {
            try
            {

                DataSet dtDHCollars = new DataSet();
                SqlParameter[] arr = GetParameters(0);
                //arr[0].ParameterName = "@HoleID";
                //arr[0].Value = sHoleID;
                dtDHCollars = ExecuteDataset("usp_DH_HoleID_LIST", arr, CommandType.StoredProcedure);
                return dtDHCollars.Tables[0];

            }
            catch (Exception eX)
            {
                throw new Exception("Error in DHcollars: " + eX.Message);
            }
        }
        public static DataTable getDHHoleID(string sHoleID)
        {
            try
            {

                DataSet dtDHCollars = new DataSet();
                SqlParameter[] arr = GetParameters(1);
                arr[0].ParameterName = "@HoleID";
                arr[0].Value = sHoleID;
                dtDHCollars = ExecuteDataset("usp_DH_Hole_List_Filter", arr, CommandType.StoredProcedure);
                return dtDHCollars.Tables[0];

            }
            catch (Exception eX)
            {
                throw new Exception("Error in DHcollars: " + eX.Message);
            }
        }

        public static SqlParameter[] GetParameters(int cantidad)
        {
            List<SqlParameter> arr = new List<SqlParameter>();
            int contador;
            for (contador = 0; contador < cantidad; contador++)
            {
                arr.Add(new SqlParameter());
            }
            return arr.ToArray();
        }

        public static DataSet ExecuteDataset(string query, SqlParameter[] arr, CommandType tipo)
        {
            SqlConnection con = Conexion.OpenConexion();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = query;
            cmd.CommandType = tipo;
            SetParameters(cmd, arr);
            SqlDataAdapter adap = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adap.Fill(ds);
            adap.Dispose();
            con.Close();
            return ds;
        }
        private static void SetParameters(SqlCommand cmd, SqlParameter[] arr)
        {
            if (arr != null)
            {
                foreach (SqlParameter obj in arr)
                    cmd.Parameters.Add(obj);
            }
        }
    }


}
