namespace Task2
{
    public class ReadWords
    {
        public async Task<string> ReadMainWord(ILanguage language, User currentPlayer, User player2, WordsInfo wordsInfoForUser1, WordsInfo wordsInfoForUser2)
        {
            while (true)
            {
                language.WriteMainWord(currentPlayer.NameId);
                string? enteredWord = await new ExtendedConsole(currentPlayer, player2, wordsInfoForUser1, wordsInfoForUser2, language).ReadLine();
                if (enteredWord.Length > 0
                    && new CheckData().CheckOnlyLetters(enteredWord, language)
                    && enteredWord.Length >= 8 && enteredWord.Length <= 30)
                {
                    return enteredWord;
                }
            }
        }

        public async Task<string> ReadNextWord(ILanguage language, User currentPlayer, User player2, WordsInfo wordsInfoForCurrentUser, WordsInfo wordsInfoForUser2)
        {
            while (true)
            {
                language.WriteNewWord(currentPlayer.NameId);
                string? word = await new ExtendedConsole(currentPlayer, player2, wordsInfoForCurrentUser, wordsInfoForUser2, language).ReadLine();
                if (word.Length > 0) return word;
            }
        }
    }
}
