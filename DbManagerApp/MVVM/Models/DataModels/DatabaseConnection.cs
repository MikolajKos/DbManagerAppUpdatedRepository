using Microsoft.Data.Sqlite;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DbManagerApp.MVVM.Models.DataModels
{
    public class DatabaseConnection
    {
        public string filePath;

        public string SelectDatabaseFilePath()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Multiselect = false;
            fileDialog.Title = "Please select your database file.";
            fileDialog.Filter = "Access files (*.db)|*.db";
            fileDialog.DefaultExt = ".db";
            fileDialog.ShowDialog();
            filePath = fileDialog.FileName;

            return filePath;
        }

        
        
    }
}
