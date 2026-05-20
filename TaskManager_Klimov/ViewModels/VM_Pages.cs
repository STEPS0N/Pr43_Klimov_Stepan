using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TaskManager_Klimov.Classes;
using TaskManager_Klimov.Context;
using TaskManager_Klimov.Models;

namespace TaskManager_Klimov.ViewModels
{
    public class VM_Pages : Notification
    {
        public VM_Tasks vm_tasks = new VM_Tasks();

        public VM_Pages()
        {
            MainWindow.init.frame.Navigate(new View.Main(vm_tasks));
        }

        public RealyCommand OnClose
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    MainWindow.init.Close();
                });
            }
        }

        public string SearchText
        {
            get => vm_tasks.SearchText;
            set
            {
                vm_tasks.SearchText = value;
                OnPropertyChanged("SearchText");
            }
        }
    }
}
