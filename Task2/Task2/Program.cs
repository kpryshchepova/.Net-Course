using System.Text.RegularExpressions;

namespace Task2
{
    class Task2 {

        static ILanguage Language { get; set; }
        static async Task Main()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                FileService fileService = new FileService();
                List<SavedData> results = await fileService.ReadDataAsync();

                while (true)
                {
                    Console.WriteLine("\nPlease, select language:");
                    Console.WriteLine("\n1. English");
                    Console.WriteLine("\n2. Русский");

                    string? enteredString = Console.ReadLine();
                    if (enteredString.Length > 0 && Regex.IsMatch(enteredString, @"^[0-9]$")
                        && (int.Parse(enteredString) == 2 || int.Parse(enteredString) == 1))
                    {
                        Language = int.Parse(enteredString) == 1 ? new English() : new Russian();
                        break;

                    }
                }
            
                User user1 = new User();
                User user2 = new User();

                user1.GetUserName(Language, 1);
                user2.GetUserName(Language, 2);

                user1.SetPreviousResults(results, user2);
                user2.SetPreviousResults(results, user1);

                user1.WordsInformation.GetMainWord(Language, user1, user2);
                user2.WordsInformation.MainWord = user1.WordsInformation.MainWord;
            
                int index = 1;
                string? word;
                while (true)
                {
                    if (index % 2 != 0)
                    {
                        if (!user2.WordsInformation.GetNextWord(Language, user2, user1)) break; else index++;
                    }
                    else
                    {
                        if (!user1.WordsInformation.GetNextWord(Language, user1, user2)) break; else index++;
                    }
                }

                SavedData currentResult = new SavedData(user1.NameId, user2.NameId, user1.Score.Value, user2.Score.Value);
                results.Add(currentResult);
                await fileService.SaveDataAsync(results);

                int allScoreForUser1 = 0;
                int allScoreForUser2 = 0;

                foreach (var result in results)
                {
                    if (result.Name1 == user1.NameId) allScoreForUser1 += result.Score1;
                    if (result.Name2 == user1.NameId) allScoreForUser1 += result.Score2;
                    if (result.Name1 == user2.NameId) allScoreForUser2 += result.Score1;
                    if (result.Name2 == user2.NameId) allScoreForUser2 += result.Score2;
                }
                DataForDB dataForDBForUser1 = new DataForDB(user1, user2, allScoreForUser1);
                DataForDB dataForDBForUser2 = new DataForDB(user2, user1, allScoreForUser2);
            

                db.GameInfo.Add(dataForDBForUser1);
                db.GameInfo.Add(dataForDBForUser2);

                await db.SaveChangesAsync();
            }

        }
    }
    
}

