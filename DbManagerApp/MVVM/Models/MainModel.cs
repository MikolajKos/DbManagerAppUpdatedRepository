using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DbManagerApp.MVVM.Models
{
    public class MainModel
    {
        public string selectedFilePath;
        public string test = "lorem ipsum";
        public DataTable dataTb;

        public List<string> LoadComboBoxItems(string filePath = "")
        {
            List<string> itemsSource;
            SQLiteConnection conn = new SQLiteConnection(@"DataSource=" + filePath);
            conn.Open();

            SQLiteCommand command = new SQLiteCommand();
            command.Connection = conn;
            command.CommandText = "SELECT name FROM sqlite_sequence";

            conn.Close();

            return itemsSource;
        }

        public DataTable SearchData(string filePath = "")
        {   
            try
            {
                SQLiteConnection conn = new SQLiteConnection(@"DataSource=" + filePath);
                conn.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT * FROM Users;", conn);
                DataTable dataTb = new DataTable();
                adapter.Fill(dataTb);

                conn.Close();
                return dataTb;
            }
            catch (Exception e)
            {
                MessageBox.Show("Please enter valid database file path.");
                return null;
            }
        }
    }
}
