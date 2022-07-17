using System.Text.RegularExpressions;

namespace Task2
{
    public class CheckData
    {
        public bool CheckOnlyLetters(string wordToCheck, ILanguage language)
        {
            return Regex.IsMatch(wordToCheck, language.Name == "English"  ? @"^[a-zA-Z]+$" : @"^[а-яА-Я]+$");
        }

        public bool CheckWord(string wordToCheck, WordsInfo wordsInfoForCurrentUser, WordsInfo wordsInfoForUser2, string mainWord)
        {
            if (wordsInfoForCurrentUser.WordList.Exists(element => element == wordToCheck)
                || wordsInfoForUser2.WordList.Exists(element => element == wordToCheck))
                return false;
            foreach (char letter in wordToCheck)
            {
                if (!mainWord.Contains(letter)) return false;

                int lettersNumberInWordToCheck = wordToCheck.Count(c => c == letter);
                int lettersNumberInTheMainWord = mainWord.Count(c => c == letter);

                if (lettersNumberInWordToCheck > lettersNumberInTheMainWord) return false;
            }
            return true;
        }
    }

}
