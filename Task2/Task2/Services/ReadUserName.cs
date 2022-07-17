namespace Task2
{
    public class ReadUserName
    {
        public string ReadName(ILanguage language, int number)
        {
            while (true)
            {
                language.WriteName(number);
                var NameId = Console.ReadLine();
                if (NameId.Length > 0 && new CheckData().CheckOnlyLetters(NameId, language)) return NameId;
            }
        }
    }
}
