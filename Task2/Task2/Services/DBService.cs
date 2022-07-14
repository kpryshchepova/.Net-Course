namespace Task2
{
    public class DBService
    {
        public void GetAllScoreDataFromDB(ILanguage language)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.GameInfo.ToList();
                if (result.Count == 0) language.NoData(); 
                else
                    foreach (var item in result) Console.WriteLine($"{item.Name} - {item.AllScore}");
            }
        }
        
    }
}
