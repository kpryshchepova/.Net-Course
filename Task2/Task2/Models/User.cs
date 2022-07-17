namespace Task2
{
    public class User
    {
        public string NameId { get; set; }
        public Score Score = new Score();
        public List<SavedData> results = new List<SavedData>();

        public void GetUserName(ILanguage language, int number)
        {
            NameId = new ReadUserName().ReadName(language, number);
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
