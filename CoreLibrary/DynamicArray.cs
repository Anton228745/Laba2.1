using System;
using System.Collections;
using System.Collections.Generic;

namespace CoreLibrary;

/// <summary>
/// Собственный динамический массив (аналог List<T> из .NET), 
/// написанный на базе обычного массива без использования готовых коллекций.
/// </summary>
/// <typeparam name="T">Тип элементов, которые будут храниться в массиве.</typeparam>
public class DynamicArray<T> : IEnumerable<T>
{
    private T[] _array;      // Внутренний массив, где физически лежат элементы
    private int _length;     // Реальное количество элементов в массиве

    // b. Конструктор по умолчанию: создает массив начальной емкостью 8 элементов
    public DynamicArray()
    {
        _array = new T[8];
        _length = 0;
    }

    // c. Конструктор с заданной емкостью (capacity)
    public DynamicArray(int capacity)
    {
        if (capacity < 0) 
            throw new ArgumentOutOfRangeException(nameof(capacity), "Емкость не может быть отрицательной.");
        
        _array = new T[capacity];
        _length = 0;
    }

    // d. Конструктор, принимающий любую готовую коллекцию (например, другой список или массив)
    public DynamicArray(IEnumerable<T> collection)
    {
        if (collection == null) 
            throw new ArgumentNullException(nameof(collection));
        
        // Считаем, сколько элементов в переданной коллекции
        int count = 0;
        foreach (var item in collection) count++;

        // Создаем внутренний массив размером с коллекцию (но не меньше 8)
        _array = new T[Math.Max(count, 8)];
        
        // Перекладываем элементы в наш динамический массив
        foreach (var item in collection)
        {
            _array[_length++] = item;
        }
    }

    // i. Свойство Length — возвращает реальное количество элементов
    public int Length => _length;

    // j. Свойство Capacity — возвращает общую вместимость внутреннего массива (память "на вырост")
    public int Capacity => _array.Length;

    // e. Метод Add: добавляет элемент в самый конец
    public void Add(T item)
    {
        // Если свободное место закончилось — удваиваем емкость массива
        if (_length >= _array.Length)
        {
            int newCapacity = _array.Length == 0 ? 8 : _array.Length * 2;
            Array.Resize(ref _array, newCapacity);
        }

        _array[_length] = item;
        _length++;
    }

    // f. Метод AddRange: добавляет целую коллекцию в конец
    public void AddRange(IEnumerable<T> collection)
    {
        if (collection == null) 
            throw new ArgumentNullException(nameof(collection));

        // Считаем количество элементов в добавляемой коллекции
        int addCount = 0;
        foreach (var _ in collection) addCount++;

        if (addCount == 0) return;

        // Важное условие из методички: расширяем массив ровно ОДИН раз, 
        // вне зависимости от того, сколько элементов добавляем
        if (_length + addCount > _array.Length)
        {
            int newCapacity = Math.Max(_array.Length * 2, _length + addCount);
            Array.Resize(ref _array, newCapacity);
        }

        // Копируем элементы
        foreach (var item in collection)
        {
            _array[_length++] = item;
        }
    }

    // g. Метод Remove: удаляет первое вхождение элемента
    public bool Remove(T item)
    {
        // Ищем индекс элемента во внутренней памяти
        int index = Array.IndexOf(_array, item, 0, _length);
        if (index < 0) return false; // Элемент не найден

        // Сдвигаем все элементы после найденного на одну позицию влево ("затираем" дырку)
        for (int i = index; i < _length - 1; i++)
        {
            _array[i] = _array[i + 1];
        }

        // Очищаем последний элемент и уменьшаем счетчик длины
        _array[_length - 1] = default!;
        _length--;

        // По требованию методички: реальная емкость (_array.Length) НЕ уменьшается при удалении
        return true;
    }

    // h. Метод Insert: вставка элемента на конкретную позицию по индексу
    public bool Insert(int index, T item)
    {
        // Проверяем, не вышли ли за границы допустимых индексов
        if (index < 0 || index > _length) 
            throw new ArgumentOutOfRangeException(nameof(index), "Индекс находится за пределами массива.");

        // Если не хватает памяти — расширяем массив
        if (_length >= _array.Length)
        {
            int newCapacity = _array.Length == 0 ? 8 : _array.Length * 2;
            Array.Resize(ref _array, newCapacity);
        }

        // Сдвигаем элементы вправо от места вставки
        for (int i = _length; i > index; i--)
        {
            _array[i] = _array[i - 1];
        }

        _array[index] = item;
        _length++;
        return true;
    }

    // l. Индексатор: позволяет обращаться к элементам с проверкой границ
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= _length) 
                throw new ArgumentOutOfRangeException(nameof(index), "Индекс за пределами массива.");
            return _array[index];
        }
        set
        {
            if (index < 0 || index >= _length) 
                throw new ArgumentOutOfRangeException(nameof(index), "Индекс за пределами массива.");
            _array[index] = value;
        }
    }

    //  позволяет перебирать наш массив в цикле foreach
    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _length; i++)
        {
            yield return _array[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}