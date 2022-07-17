namespace Task2
{
    public class DBService
    {
        public async Task<List<DataForDB>> GetAllScoreDataFromDBAsync(ILanguage language)
        {
            var result = await new DataAccessService().LoadDataAsync(language);
            return result;

        }
        
    }
}
