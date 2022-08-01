using System;
using System.Collections.Generic;
using System.IO;


namespace Employees
{
    public class CsvFileService
    {

        public async IAsyncEnumerable<Employee> LoadFromCsvFileAsync(string path)
        {

            using (var streamReader = new StreamReader(path))
            {
                string? oneLine;
                while ((oneLine = await streamReader.ReadLineAsync()) != null)
                {
                    string[] values = oneLine.Split(';');
                    yield return new Employee(values[1], values[2], values[3], values[4], values[5], DateTime.Parse(values[0]));

                }
            }

        }

    }

}
