using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Employees
{
    public class XmlFileService
    {
        public void ExportToXmlFile(ObservableCollection<Employee> employeesList, string path)
        {
            XDocument document = new XDocument();
            XElement root = new XElement("TestProgram");
            foreach (Employee employee in employeesList)
            {
                XElement record = new XElement("Record");
                record.Add(new XAttribute("id", employee.Id));
                record.Add(new XElement("FirstName", employee.FirstName));
                record.Add(new XElement("LastName", employee.LastName));
                record.Add(new XElement("Patronymic", employee.Patronymic));
                record.Add(new XElement("City", employee.City));
                record.Add(new XElement("Country", employee.Country));
                record.Add(new XElement("Birthday", employee.Birthday));
                root.Add(record);
            }

            document.Add(root);
            document.Save(path);
        }
    }
}
