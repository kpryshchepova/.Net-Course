namespace Task2
{
    public class English : ILanguage
    {
        public string Name { get { return "English"; } }
        public void WriteName(int number)
        {
            Console.WriteLine($"\nPlease, write name for player {number}:");
        }

        public void WriteMainWord(string name)
        {
            Console.WriteLine($"\n{name} please, write main word:");
        }

        public void WriteNewWord(string name)
        {
            Console.WriteLine($"\n{name}, write a word");
        }

        public void GetWinner(string name)
        {
            Console.WriteLine($"\nThe winner is {name}");
        }
        public void GetAllScore(string name)
        {
            Console.WriteLine($"\nAll score for {name}:");
        }

        public void GetAllWords(string name)
        {
            Console.WriteLine($"\nAll words for {name}:");
        }

        public void NoData()
        {
            Console.WriteLine($"\nNo data");
        }
    }
}
