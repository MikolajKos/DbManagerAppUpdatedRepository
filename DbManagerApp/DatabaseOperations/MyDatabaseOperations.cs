using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbManagerApp.DatabaseOperations
{
    public class MyDatabaseOperations
    {
        public string filePath;

        public MyDatabaseOperations(string path)
        {
            this.filePath = path;
        }

        //Connect to database
        public void ConnectToDatabase(out SQLiteConnection dbConnection)
        {
            string connectionString = $@"DataSource={filePath}";
            dbConnection = new SQLiteConnection(connectionString);
            dbConnection.Open();
        }
    
        //Fills tableList with table names
        public List<string> ReadTableNames(SQLiteConnection conn)
        {
            List<string> tableList = new List<string>();
            DataTable dataTb = new DataTable();
            FillDataTable(conn, out dataTb);

            for (int i = 0; i < dataTb.Rows.Count; i++)
            {
                tableList.Add(dataTb.Rows[i]["name"].ToString());
            }
            conn.Close();

            return tableList;
        }

        //Fills DataTable with table names, this method is only using by ReadTableNames method
        private void FillDataTable(SQLiteConnection conn, out DataTable dataTable)
        {
            dataTable = new DataTable();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter($"SELECT name FROM sqlite_sequence;", conn);

            adapter.Fill(dataTable);
        }

        //Selects table using table name
        public void SelectTableData(SQLiteConnection conn, out DataTable dataTable, string selectedTable)
        {
            SQLiteDataAdapter adapter = new SQLiteDataAdapter($"SELECT * FROM {selectedTable};", conn);
            dataTable = new DataTable();
            adapter.Fill(dataTable);
        }
    }
}
