using Microsoft.Win32;

namespace Employees
{
    public class SaveFileDialogService : IFileDialog
    {
        public string FilePath { get; set; }

        public bool OpenDialogWindow()
        {
            SaveFileDialog openFileDialog = new SaveFileDialog();
            if (openFileDialog.ShowDialog() != true) return false;
            FilePath = openFileDialog.FileName;
            return true;
        }

    }
}
