namespace Employees
{
    public interface IFileDialog
    {
        string FilePath { get; set; }
        bool OpenDialogWindow();
    }
}
