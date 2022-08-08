namespace EmployeesApp.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public Employee Employee { get; set; }

        public Todo() { }

        public Todo(int id, string name, string description, bool isCompleted, Employee employee)
        {
            Id = id;
            Name = name;
            Description = description;
            IsCompleted = isCompleted;
            Employee = employee;
        }
    }
}
