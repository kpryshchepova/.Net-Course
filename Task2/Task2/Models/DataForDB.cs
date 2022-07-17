namespace Task2
{
    public class DataForDB
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Player2 { get; set; }
        public int AllScore { get; set; }
        public string MainWord { get; set; }

        public DataForDB() {}
        public DataForDB(User user1, User user2, int score, string mainWord)
        {
            Name = user1.NameId;
            Player2 = user2.NameId;
            MainWord = mainWord;
            AllScore = score;
        }

    }
}
