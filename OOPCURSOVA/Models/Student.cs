using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPCURSOVA.Models
{
    public class Student: INotifyPropertyChanged
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _group; 
        private ObservableCollection<Grade> _grades=new ObservableCollection<Grade>();

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }

        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(nameof(FirstName)); }
        }

        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(nameof(LastName)); }
        }

        public string Group
        {
            get => _group;
            set { _group = value;OnPropertyChanged(nameof(Group)); }
        }
        public ObservableCollection<Grade> Grades
        {
            get => _grades;
            set { _grades = value; OnPropertyChanged(nameof(Grades)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
