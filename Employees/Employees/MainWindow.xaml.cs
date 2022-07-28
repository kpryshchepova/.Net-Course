using System.Windows;

namespace Employees
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ApplicationViewModel(
                new CsvFileService(), 
                new ApplicationDbContext(), 
                new ExcelFileService(), 
                new XmlFileService(), 
                new SaveFileDialogService(),
                new OpenFileDialogService()
            );

        }
    }
}
