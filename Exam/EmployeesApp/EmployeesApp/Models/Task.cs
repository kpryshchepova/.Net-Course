namespace EmployeesApp.Models
{
    public record class Task(int Id, Employee Employee, string Name, string Description, bool IsCompleted);
}
