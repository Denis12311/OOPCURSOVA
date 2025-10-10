using OOPCURSOVA.Data;
using OOPCURSOVA.Services;
using OOPCURSOVA.ViewModels;
using OOPCURSOVA.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OOPCURSOVA.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StudentView : Window
    {
        public StudentView()
        {
            InitializeComponent();
            DataContext = new StudentViewModel(
           new StudentService(new AppDbContext()),
           new GradeService(new AppDbContext()),
           new FilterService(),
                new ThemeService());
        }
    }
}