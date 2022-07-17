namespace Task2
{
    public class Russian : ILanguage
    {
        public string Name { get { return "Русский"; } }
        public void WriteName(int number)
        {
            Console.WriteLine($"\nПожалуйста, введите имя для игрока {number}:");
        }

        public void WriteMainWord(string name)
        {
            Console.WriteLine($"\n{name} пожалуйста, введите главное слово:");
        }

        public void WriteNewWord(string name)
        {
            Console.WriteLine($"\n{name}, введите слово");
        }

        public void GetWinner(string name)
        {
            Console.WriteLine($"\nПобедитель {name}");
        }

        public void GetAllScore(string name)
        {
            Console.WriteLine($"\nОбщий счет для {name}:");
        }

        public void GetAllWords(string name)
        {
            Console.WriteLine($"\nВсе слова для {name}:");
        }

        public void NoData()
        {
            Console.WriteLine($"\nНет данных");
        }
    }
}
