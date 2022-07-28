using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;

namespace Employees
{
    public class CsvFileService
    {

        public async IAsyncEnumerable<Employee> LoadFromCsvFileAsync(string path)
        {
            //StreamReader reader = null;
            const int BUFFER_SIZE = 1;
            //IAsyncEnumerable<Employee> buffer;
            IEnumerable<Employee> b = new ObservableCollection<Employee>();
            //long count = BUFFER_SIZE;

            using (var streamReader = new StreamReader(path))
            {
                string? oneLine;
                long counter = 0L;
                while ((oneLine = await streamReader.ReadLineAsync()) != null)
                {
                    counter++;
                    string[] values = oneLine.Split(';');
                    yield return new Employee(values[1], values[2], values[3], values[4], values[5], DateTime.Parse(values[0]));
                    //buffer.Add(new Employee(values[1], values[2], values[3], values[4], values[5], DateTime.Parse(values[0])));
                    Employee employee = new Employee(values[1], values[2], values[3], values[4], values[5], DateTime.Parse(values[0]).Date);
                    //buffer.Append(employee);
                    b.Append(employee);
                    //buffer.Append(new Employee(values[1], values[2], values[3], values[4], values[5], DateTime.Parse(values[0])));
                    //var u = await buffer.CountAsync();

                    if (counter % BUFFER_SIZE == 0)
                    {
                        //   yield return ProcessData(buffer);
                        //yield return (Employee)buffer;
                        //buffer = AsyncEnumerable.Empty<Employee>();
                        yield return (Employee)(IAsyncEnumerable<Employee>)b;
                        b = new ObservableCollection<Employee>();
                    }

                }
            }

        }

        //public async IAsyncEnumerable<DataTable> LoadFromCsvFileAsync()
        //{
        //    const int BUFFER_SIZE = 10;
        //    bool firstLineOfChunk = true;
        //    int chunkRowCount = 0;
        //    DataTable chunkDataTable = null;
        //    string columnData = null;
        //    bool firstLineOfFile = true;
        //    using (var sr = new StreamReader(FILE_PATH))
        //    {
        //        string line = null;
        //        //Read and display lines from the file until the end of the file is reached.                
        //        while ((line = sr.ReadLine()) != null)
        //        {
        //            //when reach first line it is column list need to create 
        //            //datatable based on that.
        //            if (firstLineOfFile)
        //            {
        //                columnData = line;
        //                firstLineOfFile = false;
        //                continue;
        //            }
        //            if (firstLineOfChunk)
        //            {
        //                firstLineOfChunk = false;
        //                chunkDataTable = CreateEmptyDataTable(columnData);
        //            }
        //            AddRow(chunkDataTable, line);
        //            chunkRowCount++;

        //            if (chunkRowCount == BUFFER_SIZE)
        //            {
        //                firstLineOfChunk = true;
        //                chunkRowCount = 0;
        //                yield return chunkDataTable;
        //                chunkDataTable = null;
        //            }
        //        }
        //    }
        //    //return last set of data which less then chunk size
        //    if (null != chunkDataTable)
        //        yield return chunkDataTable;
        //}

        //private DataTable CreateEmptyDataTable(string firstLine)
        //{
        //    IList<string> columnList = Split(firstLine);
        //    var dataTable = new DataTable("tblData");
        //    dataTable.Columns.AddRange(columnList.Select(v => new DataColumn(v)).ToArray());
        //    return dataTable;
        //}

        //private void AddRow(DataTable dataTable, string line)
        //{
        //    DataRow newRow = dataTable.NewRow();
        //    IList<string> fieldData = Split(line);
        //    for (int columnIndex = 0; columnIndex < dataTable.Columns.Count; columnIndex++)
        //    {
        //        newRow[columnIndex] = fieldData[columnIndex];
        //    }
        //    dataTable.Rows.Add(newRow);
        //}

        //private IList<string> Split(string input)
        //{
        //    //our csv file will be tab delimited
        //    var dataList = new List<string>();
        //    foreach (string column in input.Split('\t'))
        //    {
        //        dataList.Add(column);
        //    }
        //    return dataList;
        //}
    }

}
