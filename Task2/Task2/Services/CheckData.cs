using System.Text.RegularExpressions;

namespace Task2
{
    public class CheckData
    {
        public bool CheckOnlyLetters(string wordToCheck, ILanguage language)
        {
            return Regex.IsMatch(wordToCheck, language.Name == "English"  ? @"^[a-zA-Z]+$" : @"^[а-яА-Я]+$");
        }
    }
}
