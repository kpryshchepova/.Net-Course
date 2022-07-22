using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Employees
{
    public class ApplicationViewModel : BasicViewModel
    {
        private ObservableCollection<Employee> _employees;
        private CSVFileService _csvFileService;
        private IRepository _employeesRepository;
        private ICommand _loadDataCommand;
        public BasicCommandAsync LoadFilteredDataCommand;
        private BasicCommandAsync _exportToXMLCommand;
        private BasicCommandAsync _exportToExcelCommand;

        public IEnumerable<Employee> EmployeesCollection => _employees;
        public ICommand LoadDataCommand => _loadDataCommand;


        public ApplicationViewModel(CSVFileService csvFileService)
        {
            _employees = new ObservableCollection<Employee>();
            _csvFileService = csvFileService;
            _loadDataCommand = new RelayCommandAsync(LoadData);
            _employeesRepository = new EmployeesRepository(new ApplicationDBContext());

        }

        private async Task LoadData()
        {
            try
            {
                var employeesdata = _csvFileService.LoadFromCSVFileAsync();
                await foreach (Employee employee in employeesdata)
                {
                    _employees.Add(employee);
                    await _employeesRepository.InsertAsync(employee);
                }
                     }
            catch (Exception ex)
            {
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

    }
}
