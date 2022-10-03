using DbManagerApp.Core;
using DbManagerApp.MVVM.Models;
using DbManagerApp.MVVM.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DbManagerApp.MVVM.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        MainModel model = new MainModel();
        DatabaseConnection dataModel = new DatabaseConnection();

        public string selectedPathProp 
        {                                                              
            get                                                        
            {
                return dataModel.filePath;           
            }                                                           
            set                                                        
            {
                dataModel.filePath = value;                        
                onPropertyChanged(nameof(selectedPathProp));           
            }
        }

        public DataTable DataGridSource 
        {
            get
            {
                return model.dataTb;
            }
            set
            {
                model.dataTb = value;
                onPropertyChanged(nameof(DataGridSource));
            }
        }

        public List<string> TbComboBoxSource 
        {
            get
            {
                return model.itemsSource;
            }
            set
            {
                model.itemsSource = value;
                onPropertyChanged(nameof(TbComboBoxSource));
            }
        }

        public string CbSelectedItem 
        {
            get
            {
                return model.comboBoxSelectedItem;
            }
            set
            {
                model.comboBoxSelectedItem = value;
                onPropertyChanged(nameof(CbSelectedItem));
            }
        }

        private ICommand selectFilePath = null;

        public ICommand SelectFilePath
        {
            get
            {
                if (selectFilePath == null) selectFilePath = new RelayCommand(
                    (object o) =>
                    {
                        dataModel.filePath = dataModel.SelectDatabaseFilePath();
                        onPropertyChanged(nameof(SelectFilePath), nameof(selectedPathProp));
                    },
                    (object o) =>
                    {
                        return true;
                    });
                return selectFilePath;
            }
        }

        private ICommand searchClick = null;

        public ICommand SearchClick
        {
            get
            {
                if (searchClick == null) searchClick = new RelayCommand(
                    (object o) =>
                    {
                        //Loads table to DataGrid
                        model.dataTb = model.SearchData(selectedPathProp, model.comboBoxSelectedItem);

                        //Loads table names to ComboBox
                        model.itemsSource = model.LoadComboBoxItems(selectedPathProp);
                        onPropertyChanged(nameof(SearchClick), nameof(selectedPathProp), nameof(DataGridSource), nameof(TbComboBoxSource), nameof(CbSelectedItem));
                    },
                    (object o) =>
                    {
                            return true;
                    });
                return searchClick;
            }
        }
    }
}
