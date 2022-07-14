namespace Task2
{
    public class User
    {
        public string NameId { get; set; }
        public Score Score = new Score();
        public WordsInfo WordsInformation = new WordsInfo();
        public List<SavedData> results = new List<SavedData>();

        public void GetUserName(ILanguage language, int number)
        {
            while (true)
            {
                language.WriteName(number);
                NameId = Console.ReadLine();
                if (NameId.Length > 0 && new CheckData().CheckOnlyLetters(NameId, language)) break;
            }
        }

        public void SetPreviousResults(List<SavedData> results, User user2)
        {
            foreach (SavedData savedData in results)
            {
                if ((savedData.Name1 == NameId || savedData.Name1 == user2.NameId) 
                    && (savedData.Name2 == NameId || savedData.Name2 == user2.NameId))
                {
                    this.results.Add(savedData);
                }
                
            }

        }

    }
}
