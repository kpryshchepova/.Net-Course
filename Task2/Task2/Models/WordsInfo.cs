namespace Task2
{
    public class WordsInfo
    {
        public string MainWord { get; set; }
        public List<string> WordList = new List<string> { };

        public async Task GetMainWord(ILanguage language, User currentPlayer, User player2, WordsInfo wordsInfoForUser1, WordsInfo wordsInfoForUser2)
        {
            MainWord = await new ReadWords().ReadMainWord(language, currentPlayer, player2, wordsInfoForUser1, wordsInfoForUser2);
        }

        public async Task<bool> GetNextWord(ILanguage language, User currentPlayer, User player2, WordsInfo wordsInfoForCurrentUser, WordsInfo wordsInfoForUser2)
        {
            string? word = await new ReadWords().ReadNextWord(language, currentPlayer, player2, wordsInfoForCurrentUser, wordsInfoForUser2);

            if (new CheckData().CheckWord(word, wordsInfoForCurrentUser, wordsInfoForUser2, MainWord))
            {
                wordsInfoForCurrentUser.WordList.Add(word);
                currentPlayer.Score.UpdateScore();
                return true;
            } else {
                language.GetWinner(player2.NameId);
                return false;
            }
        }

    }
}
