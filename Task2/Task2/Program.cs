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

                WordsInfo wordsInfoForUser1 = new WordsInfo();
                WordsInfo wordsInfoForUser2 = new WordsInfo();

                await wordsInfoForUser1.GetMainWord(Language, user1, user2, wordsInfoForUser1, wordsInfoForUser2);
                wordsInfoForUser2.MainWord = wordsInfoForUser1.MainWord;
            
                int index = 1;
                bool isNextWordCorrect;
                while (true)
                {
                    if (index % 2 != 0)
                    {
                        isNextWordCorrect = await wordsInfoForUser2.GetNextWord(Language, user2, user1, wordsInfoForUser2, wordsInfoForUser1);
                        if (!isNextWordCorrect) break; else index++;
                    }
                    else
                    {
                        isNextWordCorrect = await wordsInfoForUser1.GetNextWord(Language, user1, user2, wordsInfoForUser1, wordsInfoForUser2);
                        if (!isNextWordCorrect) break; else index++;
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
                DataForDB dataForDBForUser1 = new DataForDB(user1, user2, allScoreForUser1, wordsInfoForUser1.MainWord);
                DataForDB dataForDBForUser2 = new DataForDB(user2, user1, allScoreForUser2, wordsInfoForUser2.MainWord);


                await db.GameInfo.AddAsync(dataForDBForUser1);
                await db.GameInfo.AddAsync(dataForDBForUser2);

                await db.SaveChangesAsync();
            }

        }
    }
    
}

