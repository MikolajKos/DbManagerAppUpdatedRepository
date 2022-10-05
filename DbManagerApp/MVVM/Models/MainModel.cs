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
        public string comboBoxSelectedItem = "";
        public DataTable dataTb;
        public List<string> itemsSource = new List<string>();

        

        //check if database path return value, if true load combobox items
        public List<string> LoadComboBoxItems(string filePath = "")
        {
            try
            {
                SQLiteConnection conn = new SQLiteConnection(@"DataSource=" + filePath);
                conn.Open();

                string commandText = "SELECT name FROM sqlite_sequence";
                SQLiteCommand command = new SQLiteCommand(commandText, conn);
                SQLiteDataReader reader = command.ExecuteReader();
                int columnCount = reader.FieldCount;
                
                while (reader.Read())
                {
                    try
                    {
                        itemsSource.Add(reader.GetString(0));
                    }
                    catch
                    {
                        MessageBox.Show("Please enter valid database path.", "Wrong path", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;
                    }
                }

                conn.Close();

                return itemsSource;
            }
            catch (Exception e)
            {
                MessageBox.Show("Please enter valid database path.", "Wrong path", MessageBoxButton.OK, MessageBoxImage.Information);

                return null;
            }

        }

         
        public DataTable SearchData(string filePath = "", string selectedTable = "")
        {   
            try
            {
                if (selectedTable == "" || selectedTable == null)
                {
                    MessageBox.Show("Please select table.", "Select Table", MessageBoxButton.OK, MessageBoxImage.Information);
                    return null;
                }
                else
                {
                    SQLiteConnection conn = new SQLiteConnection(@"DataSource=" + filePath);
                    conn.Open();
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter($"SELECT * FROM {selectedTable};", conn);
                    DataTable dataTb = new DataTable();
                    adapter.Fill(dataTb);

                    conn.Close();
                    return dataTb;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Please enter valid database file path.");
                return null;
            }
        }
    }
}
