namespace Task2
{
    public interface ILanguage
    {
        string Name { get; }
        void WriteName(int number);
        void WriteMainWord(string name);
        void WriteNewWord(string name);
        void GetWinner(string name);
        void GetAllScore(string name);
        void GetAllWords(string name);
        void NoData();
    }

}
