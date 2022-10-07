using Microsoft.Win32;
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

        public SQLiteDataAdapter adapter;


        //Load FileDialog, user can choose database path
        public string SelectDatabaseFilePath()
        {
            OpenFileDialog fileDialog = new();

            fileDialog.Multiselect = false;
            fileDialog.Title = "Please select your database file.";
            fileDialog.Filter = "Access files (*.db)|*.db";
            fileDialog.DefaultExt = ".db";
            fileDialog.ShowDialog();
            selectedFilePath = fileDialog.FileName;

            return selectedFilePath;
        }

        //loads ListView content

        //check if database path return value, if true load combobox items (database tables)
        public List<string> LoadComboBoxItems(string filePath)
        {
            try
            {
                string connectionString = $@"DataSource={filePath}";
                SQLiteConnection conn = new SQLiteConnection(connectionString);
                conn.Open();
                adapter = new SQLiteDataAdapter($"SELECT name FROM sqlite_sequence;", conn);
                DataTable dataTb = new DataTable();
                adapter.Fill(dataTb);

                for (int i = 0; i < dataTb.Rows.Count; i++)
                {
                    itemsSource.Add(dataTb.Rows[i]["name"].ToString());
                }

                conn.Close();
                return itemsSource;
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter valid database path.", "Wrong path", MessageBoxButton.OK, MessageBoxImage.Information);
                
                return itemsSource;
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
                    adapter = new SQLiteDataAdapter($"SELECT * FROM {selectedTable};", conn);
                    DataTable dataTb = new DataTable();
                    adapter.Fill(dataTb);

                    conn.Close();
                    return dataTb;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter valid database file path.");
                return null;
            }
        }
    }
}
