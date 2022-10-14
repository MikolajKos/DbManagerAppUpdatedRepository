using DbManagerApp.Core;
using DbManagerApp.MVVM.Models;
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
        //Create MainModel object
        MainModel model = new MainModel();

        public string selectedPathProp 
        {                                                              
            get                                                        
            {
                return model.selectedFilePath;           
            }                                                           
            set                                                        
            {
                model.selectedFilePath = value;                        
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
                        model.selectedFilePath = model.SelectDatabaseFilePath();
                        
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
                        model.dataTb = model.SearchData(model.selectedFilePath, model.comboBoxSelectedItem);
                        onPropertyChanged(nameof(SearchClick), nameof(selectedPathProp), nameof(DataGridSource), nameof(TbComboBoxSource), nameof(CbSelectedItem));
                    },
                    (object o) =>
                    {
                        return true;
                    });
                return searchClick;
            }
        }



        private ICommand loadTable = null;

        public ICommand LoadTable
        {
            get
            {
                if (loadTable == null) loadTable = new RelayCommand(
                    (object o) =>
                    {
                        //Loads table names to ComboBox
                        model.itemsSource = model.LoadComboBoxItems(model.selectedFilePath);
                        onPropertyChanged(nameof(LoadTable), nameof(selectedPathProp), nameof(DataGridSource), nameof(TbComboBoxSource), nameof(CbSelectedItem));
                    },
                    (object o) =>
                    {
                        return true;
                    });
                return loadTable;
            }
        }



        private ICommand updateCommand = null;

        public ICommand UpdateCommand
        {
            get
            {
                if (updateCommand == null) updateCommand = new RelayCommand(
                    (object o) =>
                    {
                        model.dataTb = model.UpdateQuery();
                        onPropertyChanged(nameof(UpdateCommand), nameof(DataGridSource));
                    },
                    (object o) =>
                    {
                        if (model.dataTb == null)
                            return false;
                        return true;
                    });
                return updateCommand;
            }
        }
    }
}
