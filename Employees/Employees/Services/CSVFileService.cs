using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    public class CSVFileService
    {
        const string FILE_PATH = @"../../../EmployeesData.csv";
      
        public async IAsyncEnumerable<Employee> LoadFromCSVFileAsync()
        {
            //StreamReader reader = null;
            const int BUFFER_SIZE = 1000;
            IEnumerable<Employee> buffer = new ObservableCollection<Employee>();
            //long count = BUFFER_SIZE;
            
            using (var streamReader = new StreamReader(FILE_PATH))
            {
                string? oneLine;
                long counter = 0L;
                while ((oneLine = await streamReader.ReadLineAsync()) != null)
                {
                    counter++;
                    string[] values = oneLine.Split(';');
                    yield return new Employee(values[1], values[2], values[3], values[4], values[5], DateTime.Parse(values[0]));
                    //buffer.Append(new Employee(values[1], values[2], values[3], values[4], values[5], DateTime.Parse(values[0])));

                    //if (counter % BUFFER_SIZE == 0)
                    //{
                    //    yield return ProcessData(buffer);
                    //    yield return (Employee)buffer;
                    //    buffer = new ObservableCollection<Employee>();
                    //}

                }
            }

        }

        private IEnumerable<Employee> ProcessData(IEnumerable<Employee> buffer)
        {
            foreach (Employee item in buffer)
                yield return (Employee)item;
        }

    }
}
