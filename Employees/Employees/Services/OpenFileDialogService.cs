using Microsoft.Win32;

namespace Employees
{
    public class OpenFileDialogService : IFileDialog
    {
        public string FilePath { get; set; }

        public bool OpenDialogWindow()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() != true) return false;
            FilePath = openFileDialog.FileName;
            return true;
        }
    }
}
