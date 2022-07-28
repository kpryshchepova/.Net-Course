using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System.Windows;

namespace Employees
{
    public class ApplicationViewModel : BasicViewModel
    {
        private ObservableCollection<Employee> _employees;
        private CsvFileService _csvFileService;
        private ExcelFileService _excelFileService;
        private XmlFileService _xmlFileService;
        private SaveFileDialogService _saveFileDialogService;
        private OpenFileDialogService _openFileDialogService;
        private ApplicationDbContext _applicationDbContext;
        private IRepository _employeesRepository;
        private ICommand _loadDataFromCsvCommand;
        private ICommand _loadDataFromDbCommand;
        private RelayCommand _loadFilteredDataCommand;
        private RelayCommand _exportToXmlCommand;
        private RelayCommand _exportToExcelCommand;

        public IEnumerable<Employee> EmployeesCollection => _employees;
        public ICommand LoadDataFromCsvCommand => _loadDataFromCsvCommand;
        public ICommand LoadDataFromDbCommand => _loadDataFromDbCommand;

        public ApplicationViewModel(
            CsvFileService csvFileService, 
            ApplicationDbContext applicationDBContext,
            ExcelFileService excelFileService, 
            XmlFileService xmlFileService,
            SaveFileDialogService saveFileDialogService,
            OpenFileDialogService openFileDialogService)
        {
            IsDataLoaded = false;
            _saveFileDialogService = saveFileDialogService;
            _openFileDialogService = openFileDialogService;
            _excelFileService = excelFileService;
            _xmlFileService = xmlFileService;
            _applicationDbContext = applicationDBContext;
            _csvFileService = csvFileService;
            _loadDataFromCsvCommand = new RelayCommandAsync(LoadDataFromCsvFile);
            _loadDataFromDbCommand = new RelayCommandAsync(LoadDataFromDb);
            _employeesRepository = new EmployeesRepository(new ApplicationDbContext());
            _employees = new ObservableCollection<Employee>();

        }

        public RelayCommand LoadFilteredDataCommand
        {
            get
            {
                return _loadFilteredDataCommand ?? (_loadFilteredDataCommand = new RelayCommand(obj =>
                    {
                        const int pageSize = 100;
                        var allEmployees = _applicationDbContext.EmployeesData.ToList();
                        var totalRecords = allEmployees.Count;
                        var numberOfPages = Math.Ceiling((double)totalRecords / pageSize);
                        var employeesFilteredData = allEmployees.Where(employee => 
                            (string.IsNullOrEmpty(_firstNameFilter) || employee.FirstName.ToLower().Equals(_firstNameFilter.ToLower()))
                            && (string.IsNullOrEmpty(_lastNameFilter) || employee.LastName.ToLower().Equals(_lastNameFilter.ToLower()))
                            && (string.IsNullOrEmpty(_patronymicFilter) || employee.Patronymic.ToLower().Equals(_patronymicFilter.ToLower()))
                            && (string.IsNullOrEmpty(_cityFilter) || employee.City.ToLower().Equals(_cityFilter.ToLower()))
                            && (string.IsNullOrEmpty(_countryFilter) || employee.Country.ToLower().Equals(_countryFilter.ToLower()))
                            && (employee.Birthday.Date == _birthdayFilter.Date)).OrderBy(employee => employee.FirstName).
                            Skip((int)((numberOfPages - 1) * pageSize)).
                            Take(pageSize);

                        _employees.Clear();

                        foreach (Employee employee in employeesFilteredData)
                        {
                            _employees.Add(employee);
                        }
                    })
                );
            }
        }

        public RelayCommand ExportToXmlFileCommand
        {
            get
            {
                return _exportToXmlCommand ?? (_exportToXmlCommand = new RelayCommand(obj =>
                {
                    try
                    {
                        if (_saveFileDialogService.OpenDialogWindow())
                        {
                            _xmlFileService.ExportToXmlFile(_employees, $"{_saveFileDialogService.FilePath}.xml");
                            MessageBox.Show("Файл сохранен");
                        }
                        
                    } 
                    catch (Exception ex) 
                    {
                        MessageBox.Show("Произошла ошибка при загрузке данных!");
                        Console.WriteLine(ex);
                    }
                    
                }));
            }
        }

        public RelayCommand ExportToExcelFileCommand
        {
            get
            {
                return _exportToExcelCommand ?? (_exportToExcelCommand = new RelayCommand(obj =>
                {
                    try
                    {
                        if (_saveFileDialogService.OpenDialogWindow())
                        {
                            var dataTable = _excelFileService.ConvertToDataTable(_employees.ToList());
                            _excelFileService.ExportToExcelFile(dataTable, $"{_saveFileDialogService.FilePath}.xlsx");
                            MessageBox.Show("Файл сохранен");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла ошибка при загрузке данных!");
                        Console.WriteLine(ex);
                    }

                }));
            }
        }

        private async Task LoadDataFromDb()
        {
            try
            {
                _employees.Clear();
                var employeesList = await _employeesRepository.GetEmployeesAsync();
                foreach (Employee employee in employeesList)
                {
                    _employees.Add(employee);
                }

                IsDataLoaded = true;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке данных!");
                Console.WriteLine(ex);
            }
        }

        private async Task LoadDataFromCsvFile()
        {
            try
            {
                if (_openFileDialogService.OpenDialogWindow())
                {
                    _employees.Clear();
                    var employeesdata = _csvFileService.LoadFromCsvFileAsync(_openFileDialogService.FilePath);
                    await _employeesRepository.InsertAsync(employeesdata);
                    await foreach (Employee employee in employeesdata)
                    {
                        _employees.Add(employee);

                    }
                    IsDataLoaded = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при обработке файла!");
                Console.WriteLine(ex);
            }
        }

        private string _firstNameFilter;
        public string FirstNameFilter
        {
            get => _firstNameFilter;
            set
            {
                _firstNameFilter = value;
                OnPropertyChanged(nameof(FirstNameFilter));
            }
        }

        private string _lastNameFilter;
        public string LastNameFilter
        {
            get => _lastNameFilter;
            set
            {
                _lastNameFilter = value;
                OnPropertyChanged(nameof(LastNameFilter));
            }
        }

        private string _patronymicFilter;
        public string PatronymicFilter
        {
            get => _patronymicFilter;
            set
            {
                _patronymicFilter = value;
                OnPropertyChanged(nameof(PatronymicFilter));
            }
        }

        private string _cityFilter;
        public string CityFilter
        {
            get => _cityFilter;
            set
            {
                _cityFilter = value;
                OnPropertyChanged(nameof(CityFilter));
            }
        }

        private string _countryFilter;
        public string CountryFilter
        {
            get => _countryFilter;
            set
            {
                _countryFilter = value;
                OnPropertyChanged(nameof(CountryFilter));
            }
        }

        private DateTime _birthdayFilter;
        public DateTime BirthdayFilter
        {
            get => _birthdayFilter;
            set
            {
                _birthdayFilter = value;
                OnPropertyChanged(nameof(BirthdayFilter));
            }
        }

        private bool _isDataLoaded;
        public bool IsDataLoaded
        {
            get => _isDataLoaded;
            set
            {
                _isDataLoaded = value;
                OnPropertyChanged(nameof(IsDataLoaded));
            }
        }

    }
}
