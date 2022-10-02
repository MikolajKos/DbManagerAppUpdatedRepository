using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbManagerApp.MVVM.Models
{
    public class MainModel
    {
        public string selectedFilePath;
        public string test = "lorem ipsum";

        public MainModel(string filePath = "")
        {
            this.selectedFilePath = filePath;
        }

        public string SearchData()
        {
            string result ="Lorem Ipsum";
            SqliteConnection conn = new SqliteConnection(@"DataSource=" + selectedFilePath);
            conn.Open();
            
            SqliteCommand command = new SqliteCommand();
            command.Connection = conn;
            command.CommandText = "SELECT Age FROM Users WHERE Id = 2;";
            using SqliteDataReader rdr = command.ExecuteReader();
            
            while (rdr.Read())
            {
                result = rdr.GetString(0);
            }
            return result;
        }
    }
}
