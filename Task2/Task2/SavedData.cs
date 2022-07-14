namespace Task2
{
    public class SavedData
    {
        public string Name1 { get; set; }
        public int Score1 { get; set; }
        public string Name2 { get; set; }
        public int Score2 { get; set; }

        public SavedData(string name1, string name2, int score1, int score2)
        {
            Name1 = name1;
            Name2 = name2;
            Score1 = score1;
            Score2 = score2;
        }
    }
}
