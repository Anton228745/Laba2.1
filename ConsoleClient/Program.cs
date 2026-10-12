using System;
using CoreLibrary;

class Program
{
    static void Main()
    {
        // Настраиваем консоль для нормального отображения кириллицы
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== ТЕМА: КОЛЛЕКЦИИ И СРАВНЕНИЕ ОБЪЕКТОВ ===\n");

        // ТЕСТ 1: Частота слов 
        Console.WriteLine("--- 1. Тест: Подсчет частоты слов ---");
        string text = "Hello world. Csharp is cool. Hello Csharp!";
        var wordCounts = CollectionsTasks.GetWordFrequency(text);
        
        foreach (var pair in wordCounts)
        {
            Console.WriteLine($"Слово '{pair.Key}': встречается {pair.Value} раз(а)");
        }
        Console.WriteLine();

        //  ТЕСТ 2:
        Console.WriteLine("--- 2. Тест: Собственный DynamicArray<T> ---");
        var myArray = new DynamicArray<int>();
        
        myArray.Add(10);
        myArray.Add(20);
        myArray.AddRange(new[] { 30, 40, 50 }); // Добавляем коллекцию за один раз

        Console.WriteLine($"Элементы массива (Length: {myArray.Length}, Capacity: {myArray.Capacity}):");
        foreach (var item in myArray) // Проверяем работу yield-итератора
        {
            Console.Write($"{item} ");
        }
        Console.WriteLine();

        myArray.Insert(1, 15); // Вставим число 15 под индексом 1
        myArray.Remove(30);    // Удалим число 30
        
        Console.WriteLine($"После Insert(1, 15) и Remove(30): {string.Join(", ", myArray)}");
        Console.WriteLine();

        // ТЕСТ 3: Сравнение объектов 
        Console.WriteLine("--- 3. Тест: Сравнение студентов (IComparable) ---");
        var student1 = new Student("Антон", 20);
        var student2 = new Student("Дмитрий", 22);

        int comparison = student1.CompareTo(student2);
        if (comparison < 0)
            Console.WriteLine($"{student1} младше, чем {student2}");
        else if (comparison > 0)
            Console.WriteLine($"{student1} старше, чем {student2}");
        else
            Console.WriteLine("Они ровесники");

        Console.WriteLine("\nВсе тесты успешно выполнены!");
    }
}