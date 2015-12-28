using System.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace DatabaseSQLtoMSSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            // ms sql connection string 
            string mssqlConnection = "Server =server_address; Database =DBName; Uid =UserName; Pwd =Password";
            SqlConnection mssqlConn = new SqlConnection(mssqlConnection);
            mssqlConn.Open();
            // mysql connectioin string
            string ConnectionString = "Server =server_address; Database =DBName; Uid =UserName; Pwd =Password";
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            conn.Open();

            // go get the information from the mySql table
            MySqlCommand myCommand = new MySqlCommand("select * from AllRelVar", conn);

            // make a data table to hold the info
            DataTable table = new DataTable();
            // load the query results into the table
            table.Load(myCommand.ExecuteReader());

            // copy the table to its new home in the ms sql table
            using (SqlBulkCopy bulk = new SqlBulkCopy(mssqlConn))
            {
                // tell it which table to copy to
                bulk.DestinationTableName = "ReligionsAll";
                // copy the info
                bulk.WriteToServer(table);
            }
            // close both connections
            mssqlConn.Close();
            conn.Close();
        }
    }
}
