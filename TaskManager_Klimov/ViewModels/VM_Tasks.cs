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
    public class VM_Tasks : Notification
    {
        public TasksContext tasksContext = new TasksContext();
        private ObservableCollection<Tasks> allTasks;

        public ObservableCollection<Tasks> Tasks { get; set; }

        public VM_Tasks()
        {
            allTasks = new ObservableCollection<Tasks>(tasksContext.Tasks.OrderBy(x => x.Done));
            Tasks = new ObservableCollection<Tasks>(allTasks);
        }

        public RealyCommand OnAddTask
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    Tasks NewTask = new Tasks()
                    {
                        DateExecute = DateTime.Now
                    };
                    Tasks.Add(NewTask);
                    tasksContext.Tasks.Add(NewTask);
                    tasksContext.SaveChanges();
                });
            }
        }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                if (searchText == value) return;
                searchText = value;
                OnPropertyChanged("SearchText");

                Tasks.Clear();

                if (string.IsNullOrEmpty(searchText))
                {
                    foreach (var item in allTasks)
                    {
                        Tasks.Add(item);
                    }
                }
                else
                {
                    var filtered = allTasks.Where(x =>
                        !string.IsNullOrEmpty(x.Name) &&
                        x.Name.Contains(searchText, System.StringComparison.OrdinalIgnoreCase));

                    foreach (var item in filtered)
                    {
                        Tasks.Add(item);
                    }
                }
            }
        }
    }
}
