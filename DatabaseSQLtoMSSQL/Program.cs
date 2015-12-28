using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DatabaseSQLtoMSSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            string mssqlConnection = "Server ="server address"; Database ="database name"; Uid ="user name"; Pwd ="password"";
            SqlConnection mssqlConn = new SqlConnection(mssqlConnection);
            mssqlConn.Open();

            string ConnectionString = "Server ="server address"; Database ="database name"; Uid ="user name"; Pwd ="password"";
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            conn.Open();
            MySqlCommand myCommand = new MySqlCommand("select * from AllRelVar", conn);
            //MySqlDataReader myReader = myCommand.ExecuteReader();
                    //while (myReader.Read())
                    //{
                        DataTable table = new DataTable();
                        table.Load(myCommand.ExecuteReader());
                        //int id = Convert.ToInt32(myReader["id"]);
                        //int relid = Convert.ToInt32(myReader["Rel_ID"]);
                        //string rel = myReader["Religion"].ToString();
                        //SqlCommand mssqlCommand = new SqlCommand("INSERT INTO ReligionsAll VALUES(@id, @relID, @rel)", mssqlConn);
                        //mssqlCommand.Parameters.AddWithValue("@id",id);
                        //mssqlCommand.Parameters.AddWithValue("@relID",relid);
                        //mssqlCommand.Parameters.AddWithValue("@rel",rel);
                        //mssqlCommand.ExecuteNonQuery();
                        //Console.WriteLine(id);
                   // }
                        //using (mssqlConn)
                        using (SqlBulkCopy bulk = new SqlBulkCopy(mssqlConn))
                        {
                            bulk.DestinationTableName = "ReligionsAll";
                            bulk.WriteToServer(table);
                        }
                        Console.WriteLine(table.ToString());
                    mssqlConn.Close();
                    conn.Close();
                //SqlDataAdapter adp = new SqlDataAdapter("select * from Religions", ConnectionString);
                //DataSet ds = new DataSet();
                //try { adp.Fill(ds, "ReligionsTable"); }
                //catch (Exception ex) {
                //    Console.Out.WriteLine(ex);
                //}
                //Console.Out.WriteLine(ds.ToString());
        }
    }
}
