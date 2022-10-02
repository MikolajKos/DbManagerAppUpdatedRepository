using DbManagerApp.Core;
using DbManagerApp.MVVM.Models;
using DbManagerApp.MVVM.Models.DataModels;
using System;
using System.Collections.Generic;
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

        public string testProp
        {
            get
            {
                return model.test;
            }
            set
            {
                model.test = value;
                onPropertyChanged(nameof(testProp));
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
                        model.test = model.SearchData();
                        onPropertyChanged(nameof(SearchClick), nameof(testProp));
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
