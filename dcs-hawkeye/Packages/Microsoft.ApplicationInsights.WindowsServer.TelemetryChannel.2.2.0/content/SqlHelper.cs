#region "    using    "
using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
#endregion

namespace DCS_Hawkeye_Server.DataLayer
{
    public class SqlHelper
    {
        private static string strConn;

        static SqlHelper()
        {
            strConn = ConfigurationManager.ConnectionStrings["SQLconnectionstring"].ConnectionString;
        }

        public static int ExecuteNonQuery(string sql, SqlParameter[] p)
        {
            SqlConnection cnn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            if (p != null)
            {
                for (int i = 0; i < p.Length; i++)
                {
                    cmd.Parameters.Add(p[i]);
                }
            }
            cnn.Open();
            int retval = cmd.ExecuteNonQuery();
            cnn.Close();
            return retval;
        }

        public static object ExecuteScalar(string sql)
        {
            SqlConnection cnn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            //for (int i = 0; i < p.Length; i++)
            //{
            //    cmd.Parameters.Add(p[i]);
            //}
            cnn.Open();
            object obj = cmd.ExecuteScalar();
            //return 0 on null
            if (obj.ToString() == string.Empty)
            {
                obj = 0;
            }
            cnn.Close();
            return obj;
        }

        public static object ExecuteScalarParam(string sql, SqlParameter[] p)
        {
            SqlConnection cnn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            for (int i = 0; i < p.Length; i++)
            {
                cmd.Parameters.Add(p[i]);
            }
            cnn.Open();
            object obj = cmd.ExecuteScalar();
            cnn.Close();
            return obj;
        }

        public static object TSQLExecuteScalarParam(string sql, SqlParameter[] p)
        {
            SqlConnection cnn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            if(p != null)
            {
                for (int i = 0; i < p.Length; i++)
                {
                    cmd.Parameters.Add(p[i]);
                }
            }
            cnn.Open();
            object obj = cmd.ExecuteScalar();
            cnn.Close();
            return obj;
        }

        public static DataSet GetDataSetSQL(string sql, SqlParameter[] p)
        {
            SqlConnection cnn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(sql, cnn);
            if (p != null)
            {
                for (int i = 0; i < p.Length; i++)
                {
                    cmd.Parameters.Add(p[i]);
                }
            }
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public static DataTable GetDataSetTSQL(string sql, SqlParameter[] p)
        {
            using (SqlConnection cnn = new SqlConnection(strConn))
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                SqlCommand cmd = new SqlCommand(sql, cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                if (p != null)
                {
                    for (int i = 0; i < p.Length; i++)
                    {
                        cmd.Parameters.Add(p[i]);
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
