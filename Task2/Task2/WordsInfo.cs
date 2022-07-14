namespace Task2
{
    public class WordsInfo
    {
        public string MainWord { get; set; }
        public List<string> WordList = new List<string> { };

        public void GetMainWord(ILanguage language, User currentPlayer, User player2)
        {
            while (true)
            {
                language.WriteMainWord(currentPlayer.NameId);
                string? enteredWord = new ExtendedConsole(currentPlayer, player2, language).ReadLine();
                if (enteredWord.Length > 0
                    && new CheckData().CheckOnlyLetters(enteredWord, language) 
                    && enteredWord.Length >= 8 && enteredWord.Length <= 30)
                {
                    MainWord = enteredWord;
                    break;
                }
            }
        }

        public bool GetNextWord(ILanguage language, User currentPlayer, User player2)
        {
            string? word;

            while (true)
            {
                language.WriteNewWord(currentPlayer.NameId);
                word = new ExtendedConsole(currentPlayer, player2, language).ReadLine();
                if (word.Length > 0) break;
            }

            if (CheckWord(word, currentPlayer, player2))
            {
                currentPlayer.WordsInformation.WordList.Add(word);
                currentPlayer.Score.UpdateScore();
                return true;
            } else {
                language.GetWinner(player2.NameId);
                return false;
            }
        }

        private bool CheckWord(string wordToCheck, User currentPlayer, User player2)
        {
            if (currentPlayer.WordsInformation.WordList.Exists(element => element == wordToCheck) 
                || player2.WordsInformation.WordList.Exists(element => element == wordToCheck)) 
                return false;
            foreach (char letter in wordToCheck)
            {
                if (!MainWord.Contains(letter)) return false;

                int lettersNumberInWordToCheck = wordToCheck.Count(c => c == letter);
                int lettersNumberInTheMainWord = MainWord.Count(c => c == letter);

                if (lettersNumberInWordToCheck > lettersNumberInTheMainWord) return false;
            }
            return true;
        }
    }
}
