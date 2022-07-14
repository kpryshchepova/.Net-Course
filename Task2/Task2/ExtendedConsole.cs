namespace Task2
{
    public class ExtendedConsole
    {
        User User1 { get; set; }
        User User2 { get; set; }
        ILanguage Language { get; set; }
        public ExtendedConsole(User user1, User user2, ILanguage language)
        {
            User1 = user1;
            User2 = user2;
            Language = language;
        }
        public string? ReadLine()
        {
            string? enteredString = Console.ReadLine();
            switch (enteredString)
            {
                case "/show-words":
                    Language.GetAllWords(User1.NameId);
                    if (User1.WordsInformation.WordList.Count == 0) { Language.NoData(); }
                    else  {
                        foreach (string word in User1.WordsInformation.WordList)
                        {
                            Console.WriteLine(word);
                        }
                    }
                    
                    Language.GetAllWords(User2.NameId);
                    if (User2.WordsInformation.WordList.Count == 0) { Language.NoData(); }
                    else
                    {
                        foreach (string word in User2.WordsInformation.WordList)
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
                    new DBService().GetAllScoreDataFromDB(Language);
                    return "";

                default: 
                    return enteredString;
            }

        }
    }
}
