using Microsoft.EntityFrameworkCore;

namespace Task2
{
    public class DataAccessService
    {
        public async Task<List<DataForDB>> LoadDataAsync(ILanguage language)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return await db.GameInfo.ToListAsync();
            }
        }
    }
}
