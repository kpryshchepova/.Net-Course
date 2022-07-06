using System.Text.RegularExpressions;

class Task1
{
    static string MainWord;
    static string Player1;
    static string Player2;
    static List<string> Player1Words = new List<string>();
    static List<string> Player2Words = new List<string>();
    static int Language;
    static void Main()
    {
        Language = SelectLanguage();
        DefinePlayersNames();
        MainWord = GetMainWord();
        StartGame();
    }

    static int SelectLanguage()
    {
        while (true)
        {
            Console.WriteLine("\nPlease, select language:");
            Console.WriteLine("\n1. English");
            Console.WriteLine("\n2. Русский");
            
            string? enteredString = Console.ReadLine();
            if (enteredString.Length > 0 && Regex.IsMatch(enteredString, @"^[0-9]$") 
                && (int.Parse(enteredString) == 2 || int.Parse(enteredString) == 1)) {
                return int.Parse(enteredString);
            }
        }
    }

    static void DefinePlayersNames()
    {
        while (true)
        {
            Console.WriteLine(Language == 1 
                ? "\nPlease, write player 1 name (only letters):" 
                : "\nПожалуйста, введите имя игрока 1 (только буквы):");
            Player1 = Console.ReadLine();
            if (Player1.Length > 0 && CheckOnlyLetters(Player1)) break;
        }
        while (true)
        {
            Console.WriteLine(Language == 1
                ? "\nPlease, write player 2 name (only letters):"
                : "\nПожалуйста, введите имя игрока 2 (только буквы):");
            Player2 = Console.ReadLine();
            if (Player2.Length > 0 && CheckOnlyLetters(Player2)) break;
        }
        
    }

    static string GetMainWord()
    {
        while (true)
        {
            Console.WriteLine(Language == 1
                ? $"\n{Player1} enter the Main word (from 8 to 30 letters):"
                : $"\n{Player1} введите главное слово (от 8 до 30 букв):");
            string? enteredWord = Console.ReadLine();
            if (enteredWord.Length > 0 && CheckOnlyLetters(enteredWord) && enteredWord.Length >= 8 && enteredWord.Length <= 30) return enteredWord;
        }
    }

    static void StartGame()
    {
        int index = 1;
        string? word;
        for (; ; )
        {
            if (index % 2 != 0)
            {
                while (true)
                {
                    Console.WriteLine(Language == 1
                        ? $"\n{Player2} enter the word:"
                        : $"\n{Player2} введите слово:");
                    word = Console.ReadLine();
                    if (word.Length > 0) break;
                }

                if (CheckWord(word))
                {
                    Player2Words.Add(word);
                    index++;
                } else {
                    Console.WriteLine(Language == 1
                        ? $"\n!!!The winner is {Player1}!!!"
                        : $"\n!!!Победитель {Player1}!!!");
                    return;
                }
            } else {
                while (true)
                {
                    Console.WriteLine(Language == 1
                        ? $"\n{Player1} enter the word:"
                        : $"\n{Player1} введите слово:");
                    word = Console.ReadLine();
                    if (word.Length > 0) break;
                }

                if (CheckWord(word))
                {
                    Player1Words.Add(word);
                    index++;
                } else {
                    Console.WriteLine(Language == 1
                        ? $"\n!!!The winner is {Player2}!!!"
                        : $"\n!!!Победитель {Player2}!!!");
                    return;
                }
            }
        }
    }

    static bool CheckWord(string wordToCheck)
    {
        if (Player2Words.Exists(element => element == wordToCheck) || Player1Words.Exists(element => element == wordToCheck)) return false;
        foreach (char letter in wordToCheck)
        {
            if(!MainWord.Contains(letter)) return false;

            int lettersNumberInWordToCheck = wordToCheck.Count(c => c == letter);
            int lettersNumberInTheMainWord = MainWord.Count(c => c == letter);

            if (lettersNumberInWordToCheck > lettersNumberInTheMainWord) return false;
        }
        return true;
    }

    static bool CheckOnlyLetters(string wordToCheck)
    {
        return Regex.IsMatch(wordToCheck, Language == 1 ? @"^[a-zA-Z]+$" : @"^[а-яА-Я]+$");
    }

}
