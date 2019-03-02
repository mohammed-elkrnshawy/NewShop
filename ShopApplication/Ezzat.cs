using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApplication
{
    public class Ezzat
    {
        static string constr = ConfigurationManager.ConnectionStrings["cnn1"].ConnectionString;


        public static DataSet GetDataSet(string stored_name, string table_name, params SqlParameter[] parr)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(stored_name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter item in parr)
            {
                cmd.Parameters.Add(item);
            }
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, table_name);
            return ds;
        }




        public static SqlDataReader GetDataReader(string stored_name, out SqlConnection conout, params SqlParameter[] parr)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(stored_name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter item in parr)
            {
                cmd.Parameters.Add(item);
            }
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            conout = con;
            return dr;
        }


        public static int ExecutedNoneQuery(string Query)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            int x = cmd.ExecuteNonQuery();
            con.Close();
            return x;

        }

        public static int ExecutedNoneQuery(string stored_name, params SqlParameter[] parr)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(stored_name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter item in parr)
            {
                cmd.Parameters.Add(item);
            }
            con.Open();
            int x = cmd.ExecuteNonQuery();
            con.Close();
            return x;
        }






        public static object ExecutedScalar(string stored_name, params SqlParameter[] parr)
        {
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand(stored_name, con);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter item in parr)
            {
                cmd.Parameters.Add(item);
            }
            con.Open();
            object x = cmd.ExecuteScalar();
            con.Close();
            return x;
        }






    }
}
