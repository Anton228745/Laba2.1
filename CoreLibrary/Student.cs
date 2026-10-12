using System;

namespace CoreLibrary;

/// Класс студента, который умеет сравнивать сам себя с другими студентами 
/// (реализует интерфейс IComparable).
public class Student : IComparable<Student>
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Student(string name, int age)
    {
        Name = name;
        Age = age;
    }

    // Сравнение экземпляров нашего класса (сортируем по возрасту)
    public int CompareTo(Student? other)
    {
        if (other == null) return 1; // Если объект null, он считается "меньше"
        return Age.CompareTo(other.Age);
    }

    // Переопределяем Equals, когда два студента равны
    public override bool Equals(object? obj)
    
    {
        if (obj is Student other)
        {
            return Name == other.Name && Age == other.Age;
        }
        return false;
    }

    // Если переопределяем Equals, обязательно нужно переопределить 
    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Age);
    }

    public override string ToString() => $"{Name} ({Age} лет)";
}