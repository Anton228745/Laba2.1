using System;
using System.Collections.Generic;

namespace CoreLibrary;

public class CollectionsTasks
{

    /// 1. Подсчет частоты слов в тексте. 
    /// Разделителями считаются пробел и точка. Регистр букв игнорируется.
    public static Dictionary<string, int> GetWordFrequency(string text)
    {
        // Словарь для хранения пары: "слово" -> "сколько раз встретилось"
        // гарантирует, что "Hello" и "hello" — это одно и то же слово
        var frequencyMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        if (string.IsNullOrWhiteSpace(text)) return frequencyMap;

        // Режем текст на кусочки по пробелу и точке
        char[] separators = { ' ', '.' };
        string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

        foreach (var word in words)
        {
            string cleanWord = word.Trim();
            if (string.IsNullOrEmpty(cleanWord)) continue;

            // Если слово уже есть в словаре — увеличиваем счетчик на 1, если нет — добавляем с единицей
            if (frequencyMap.ContainsKey(cleanWord))
            {
                frequencyMap[cleanWord]++;
            }
            else
            {
                frequencyMap[cleanWord] = 1;
            }
        }

        return frequencyMap;
    }

    /// 3. Демонстрация работы с разными стандартными коллекциями .NET (Список, Словарь, Очередь, Стек).
    public static void DemonstrateStandardCollections()
    {
        // 1. Список — упорядоченная коллекция с доступом по индексу
        List<string> shoppingList = new List<string> { "Молоко", "Хлеб" };
        shoppingList.Add("Сыр");

        // 2. Словарь — хранит пары "Ключ - Значение" для быстрого поиска
        Dictionary<int, string> userRoles = new Dictionary<int, string>
        {
            { 1, "Администратор" },
            { 2, "Пользователь" }
        };

        // 3. Очередь — принцип FIFO (First In, First Out — первый зашел, первый вышел, как в кассу)
        Queue<string> printQueue = new Queue<string>();
        printQueue.Enqueue("Документ_1.pdf");
        printQueue.Enqueue("Документ_2.pdf");
        string currentPrint = printQueue.Dequeue(); // Заберет "Документ_1.pdf"

        // 4. Стек — принцип LIFO (Last In, First Out — последним пришел, первым вышел, как стопка тарелок)
        Stack<string> browserHistory = new Stack<string>();
        browserHistory.Push("Google.com");
        browserHistory.Push("GitHub.com");
        string backPage = browserHistory.Pop(); // Вернет "GitHub.com"
    }
}