using DbManagerApp.DatabaseOperations;
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
        #region Model variables

        public string selectedFilePath;
        public string comboBoxSelectedItem = "";
        public List<string> itemsSource = new List<string>();

        public SQLiteDataAdapter adapter;
        public SQLiteConnection conn;
        public DataTable dataTb;
        public SQLiteCommandBuilder commandBuilder;
        public DataSet dataS;

        #endregion


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

        

        //check if database path return value, if true load combobox items (database tables)
        public List<string> LoadComboBoxItems(string filePath)
        {
            try
            {
                MyDatabaseOperations dbOperations = new MyDatabaseOperations(filePath);
                dbOperations.ConnectToDatabase(out conn);
                
                //returns list of table names
                return dbOperations.ReadTableNames(conn);
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter valid database path.", "Wrong path", MessageBoxButton.OK, MessageBoxImage.Information);
                return itemsSource;
            }

        }

        //Search data depending on selected table in comboBox, by default selects table with table names 
        public DataTable SearchData(string filePath, string selectedTable)
        {
            //Creating MyDatabaseOperations object
            MyDatabaseOperations dbOperations = new MyDatabaseOperations(filePath);

            try
            {
                if (selectedTable == "" || selectedTable == null)
                {
                    MessageBox.Show("Please select table.", "Select Table", MessageBoxButton.OK, MessageBoxImage.Information);
                    return null;
                }
                else
                {
                    dbOperations.ConnectToDatabase(out conn);
                    dbOperations.SelectTableData(conn, out dataTb, selectedTable);

                    conn.Close();
                    return dataTb;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter valid database file path.");
                return dataTb;
            }
        }

        //Update Query
        public DataTable UpdateQuery()
        {
            try
            {
                //Will be moved to MyDatabaseOperations
                commandBuilder = new SQLiteCommandBuilder(adapter);
                adapter.Update(dataTb);
                MessageBox.Show("Updated successfully!", "Data Updated", MessageBoxButton.OK, MessageBoxImage.Information);

                return dataTb;
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return dataTb;
            }

        }
    }
}
