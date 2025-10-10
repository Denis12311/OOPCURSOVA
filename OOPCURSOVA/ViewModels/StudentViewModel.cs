using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Input;
using OOPCURSOVA.Models;
using OOPCURSOVA.Services;
using OOPCURSOVA.Utility;

namespace OOPCURSOVA.ViewModels
{
    internal class StudentViewModel: BaseViewModel
    {
        private readonly StudentService _studentService;
        private readonly GradeService _gradeService;
        private readonly FilterService _filterService;
        private readonly ThemeService _themeService;

        public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();
        public ObservableCollection<Grade> Grades { get; set; } = new ObservableCollection<Grade>();

        private Student _selectedStudent;
        public Student SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                OnPropertyChanged(nameof(SelectedStudent));
                LoadGrades();
                UpdateAverageGrade();
            }
        }

        private Grade _selectedGrade;
        public Grade SelectedGrade
        {
            get => _selectedGrade;
            set { _selectedGrade = value; OnPropertyChanged(nameof(SelectedGrade)); }
        }

        private double _averageGrade;
        public double AverageGrade
        {
            get => _averageGrade;
            set { _averageGrade = value; OnPropertyChanged(nameof(AverageGrade)); }
        }

        private string _filterText;
        public string FilterText
        {
            get => _filterText;
            set { _filterText = value; OnPropertyChanged(nameof(FilterText)); }
        }

        private string _filterGroup;
        public string FilterGroup
        {
            get => _filterGroup;
            set { _filterGroup = value; OnPropertyChanged(nameof(FilterGroup)); }
        }

        private string _minAverageText;
        public string MinAverageText
        {
            get => _minAverageText;
            set
            {
                _minAverageText = value;
                OnPropertyChanged(nameof(MinAverageText));


                if (double.TryParse(_minAverageText, NumberStyles.Any, CultureInfo.InvariantCulture, out double val))
                    MinAverage = val;
                else
                    MinAverage = null;
            }
        }

        private double? MinAverage { get; set; }


        public ICommand AddStudentCommand { get; }
        public ICommand UpdateStudentCommand { get; }
        public ICommand DeleteStudentCommand { get; }
        public ICommand FilterCommand { get; }

        public ICommand AddGradeCommand { get; }
        public ICommand UpdateGradeCommand { get; }
        public ICommand DeleteGradeCommand { get; }

        public ICommand ToggleThemeCommand { get; }

        public StudentViewModel(StudentService studentService, GradeService gradeService, FilterService filterService,ThemeService themeService)
        {
            _studentService = studentService;
            _gradeService = gradeService;
            _filterService = filterService;
            _themeService = themeService;

            LoadStudents();

            AddStudentCommand = new RelayCommand2(AddStudent);
            UpdateStudentCommand = new RelayCommand2(UpdateStudent, () => SelectedStudent != null);
            DeleteStudentCommand = new RelayCommand2(DeleteStudent, () => SelectedStudent != null);

            FilterCommand = new RelayCommand<string>(FilterStudents);

            AddGradeCommand = new RelayCommand2(AddGrade, () => SelectedStudent != null);
            UpdateGradeCommand = new RelayCommand2(UpdateGrade, () => SelectedGrade != null);
            DeleteGradeCommand = new RelayCommand2(DeleteGrade, () => SelectedGrade != null);

            ToggleThemeCommand = new RelayCommand2(() => _themeService.ToggleTheme());
        }

        private void LoadStudents()
        {
            Students.Clear();
            foreach (var s in _studentService.GetAllStudent()) 
                Students.Add(s);
        }

        private void LoadGrades()
        {
            Grades.Clear();
            if (SelectedStudent != null)
            {
                foreach (var g in _gradeService.GetGradesByStudentId(SelectedStudent.Id))
                    Grades.Add(g);
            }
        }

        private void UpdateAverageGrade()
        {
            if (SelectedStudent != null)
                AverageGrade = _gradeService.GetAverageGrade(SelectedStudent.Id);
            else
                AverageGrade = 0.0;
        }

        private void AddStudent()
        {
            var student = new Student { FirstName = "Новый", LastName = "Студент", Group = "A1" };
            _studentService.AddStudent(student);
            Students.Add(student);
        }

        private void UpdateStudent()
        {
            if (SelectedStudent != null)
            {
                _studentService.UpdateStudents(SelectedStudent);
                LoadStudents();
            }
        }

        private void DeleteStudent()
        {
            if (SelectedStudent != null)
            {
                _studentService.DeleteStudent(SelectedStudent.Id);
                Students.Remove(SelectedStudent);
                Grades.Clear();
                AverageGrade = 0.0;
            }
        }

        private void FilterStudents(string _)
        {
            var allStudents = _studentService.GetAllStudent();
            var filtered = _filterService.Filter(
                allStudents,
                name: FilterText,
                group: FilterGroup,
                minAverage: MinAverage
            );

            Students.Clear();
            foreach (var s in filtered)
                Students.Add(s);
        }



        private void AddGrade()
        {
            if (SelectedStudent == null) return;

            var grade = new Grade { StudentId = SelectedStudent.Id, Subject = "Новый предмет", Score = 0 };
            _gradeService.AddGrade(grade);
            LoadGrades();
            UpdateAverageGrade();
        }

        private void UpdateGrade()
        {
            if (SelectedGrade == null) return;

            _gradeService.UpgradeGrade(SelectedGrade);
            LoadGrades();
            UpdateAverageGrade();
        }

        private void DeleteGrade()
        {
            if (SelectedGrade == null) return;

            _gradeService.DeleteGrade(SelectedGrade.Id);
            LoadGrades();
            UpdateAverageGrade();
        }


    }
}
