namespace Task2
{
    public class SaveAndRead
    {
        public void Transform()
        {
            Console.WriteLine("Data have transformed");
        }

        virtual public Task SaveDataAsync(List<SavedData> results)
        {
            Console.WriteLine("Data have saved");
            return null;
        }

        virtual public Task<List<SavedData>> ReadDataAsync()
        {
            Console.WriteLine("Data have read");
            return null;
        }
    }
}
