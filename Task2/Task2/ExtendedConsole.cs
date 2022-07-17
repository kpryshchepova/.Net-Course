namespace Task2
{
    public class ExtendedConsole
    {
        User User1 { get; set; }
        User User2 { get; set; }
        WordsInfo WordsInfoForUser1 { get; set; }
        WordsInfo WordsInfoForUser2 { get; set; }
        ILanguage Language { get; set; }
        public ExtendedConsole(User user1, User user2, WordsInfo wordsInfoForUser1, WordsInfo wordsInfoForUser2, ILanguage language)
        {
            User1 = user1;
            User2 = user2;
            Language = language;
            WordsInfoForUser1 = wordsInfoForUser1;
            WordsInfoForUser2 = wordsInfoForUser2;
        }
        public async Task<string?> ReadLine()
        {
            string? enteredString = Console.ReadLine();
            switch (enteredString)
            {
                case "/show-words":
                    Language.GetAllWords(User1.NameId);
                    if (WordsInfoForUser1.WordList.Count == 0) { Language.NoData(); }
                    else  {
                        foreach (string word in WordsInfoForUser1.WordList)
                        {
                            Console.WriteLine(word);
                        }
                    }
                    
                    Language.GetAllWords(User2.NameId);
                    if (WordsInfoForUser2.WordList.Count == 0) { Language.NoData(); }
                    else
                    {
                        foreach (string word in WordsInfoForUser2.WordList)
                        {
                            Console.WriteLine(word);
                        }
                    }

                    return "";

                case "/score":
                    var result1 = 0;
                    foreach (SavedData score in User1.results)
                    {
                        if (score.Name1 == User1.NameId) result1 += score.Score1;
                        if (score.Name2 == User1.NameId) result1 += score.Score2;
                    }
                    Language.GetAllScore(User1.NameId);
                    Console.WriteLine(result1);

                    var result2 = 0;
                    foreach (SavedData score in User2.results)
                    {
                        if (score.Name1 == User2.NameId) result2 += score.Score1;
                        if (score.Name2 == User2.NameId) result2 += score.Score2;
                    }
                    Language.GetAllScore(User2.NameId);
                    Console.WriteLine(result2);
                    return "";

                case "/total-score":
                    var result = await new DBService().GetAllScoreDataFromDBAsync(Language);

                    if (result.Count == 0) Language.NoData();
                    else
                    {
                        int count = 0;
                        foreach (DataForDB record in result.FindAll(res => res.Name == User1.NameId))
                        {
                            count += record.AllScore;
                        }
                        Console.WriteLine($"{User1.NameId} - {count}");

                        count = 0;
                        foreach (DataForDB record in result.FindAll(res => res.Name == User2.NameId))
                        {
                            count += record.AllScore;
                        }
                        Console.WriteLine($"{User2.NameId} - {count}");
                    }
                        
                    return "";

                default:
                    return enteredString;
            }

        }
    }
}
