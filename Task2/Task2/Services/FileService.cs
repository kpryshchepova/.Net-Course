using System.Text.Json;
namespace Task2
{
    public class FileService : SaveAndRead
    {
        override public async Task SaveDataAsync(List<SavedData> results)
        {
            using (FileStream fs = new FileStream(@"../../../result.json", FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync<List<SavedData>>(fs, results);
                Console.WriteLine("Data has been saved to file");
            }
        }

        public async Task<List<SavedData>> ReadDataAsync()
        {
            using (FileStream fs = new FileStream(@"../../../result.json", FileMode.OpenOrCreate))
            {
                List<SavedData> results = await JsonSerializer.DeserializeAsync<List<SavedData>>(fs);

                return results;
            }
        }

    }
}
