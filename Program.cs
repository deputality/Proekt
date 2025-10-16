using System;
using System.Collections.Generic;

// Класс Student для первого задания
class Student : IComparable<Student>, IComparer<Student>
{
    public string FullName { get; set; }
    public int BirthYear { get; set; }
    public string School { get; set; }

    public Student(string fullName, int birthYear, string school)
    {
        FullName = fullName;
        BirthYear = birthYear;
        School = school;
    }

    // Реализация IComparable - сравнение по году рождения
    public int CompareTo(Student other)
    {
        return this.BirthYear.CompareTo(other.BirthYear);
    }

    // Реализация IComparer - сравнение по году рождения
    public int Compare(Student x, Student y)
    {
        return x.BirthYear.CompareTo(y.BirthYear);
    }

    // Перегрузка операций отношения
    public static bool operator >(Student s1, Student s2) => s1.BirthYear > s2.BirthYear;
    public static bool operator <(Student s1, Student s2) => s1.BirthYear < s2.BirthYear;
    public static bool operator >=(Student s1, Student s2) => s1.BirthYear >= s2.BirthYear;
    public static bool operator <=(Student s1, Student s2) => s1.BirthYear <= s2.BirthYear;

    public override string ToString()
    {
        return $"{FullName}, год рождения: {BirthYear}, школа: {School}";
    }
}

// Класс StudentWithGrades для второго задания
class StudentWithGrades : IComparable<StudentWithGrades>, IComparer<StudentWithGrades>
{
    public string FullName { get; set; }
    public string GroupNumber { get; set; }
    public int Exam1 { get; set; }
    public int Exam2 { get; set; }
    public int Exam3 { get; set; }

    public StudentWithGrades(string fullName, string groupNumber, int exam1, int exam2, int exam3)
    {
        FullName = fullName;
        GroupNumber = groupNumber;
        Exam1 = exam1;
        Exam2 = exam2;
        Exam3 = exam3;
    }

    // Проверка успешной сдачи сессии (все экзамены сданы на 3 и выше)
    public bool IsSessionPassed()
    {
        return Exam1 >= 3 && Exam2 >= 3 && Exam3 >= 3;
    }

    // Реализация IComparable - сравнение по номеру группы
    public int CompareTo(StudentWithGrades other)
    {
        return this.GroupNumber.CompareTo(other.GroupNumber);
    }

    // Реализация IComparer - сравнение по номеру группы
    public int Compare(StudentWithGrades x, StudentWithGrades y)
    {
        return x.GroupNumber.CompareTo(y.GroupNumber);
    }

    // Перегрузка операций отношения
    public static bool operator >(StudentWithGrades s1, StudentWithGrades s2) =>
        s1.GroupNumber.CompareTo(s2.GroupNumber) > 0;
    public static bool operator <(StudentWithGrades s1, StudentWithGrades s2) =>
        s1.GroupNumber.CompareTo(s2.GroupNumber) < 0;
    public static bool operator >=(StudentWithGrades s1, StudentWithGrades s2) =>
        s1.GroupNumber.CompareTo(s2.GroupNumber) >= 0;
    public static bool operator <=(StudentWithGrades s1, StudentWithGrades s2) =>
        s1.GroupNumber.CompareTo(s2.GroupNumber) <= 0;

    public override string ToString()
    {
        return $"{FullName}, группа: {GroupNumber}, экзамены: {Exam1}, {Exam2}, {Exam3}";
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== ЗАДАНИЕ 1: Список студентов по школам ===\n");

        // Создаем коллекцию студентов
        List<Student> students = new List<Student>
        {
            new Student("Иванов Иван Иванович", 2000, "Школа №15"),
            new Student("Петров Петр Петрович", 2001, "Школа №10"),
            new Student("Сидорова Анна Сергеевна", 1999, "Школа №15"),
            new Student("Козлов Алексей Владимирович", 2002, "Школа №5"),
            new Student("Николаева Мария Дмитриевна", 2000, "Школа №10"),
            new Student("Федоров Дмитрий Игоревич", 2001, "Школа №15")
        };

        // Выводим всех студентов
        Console.WriteLine("Все студенты:");
        foreach (var student in students)
        {
            Console.WriteLine(student);
        }

        // Запрашиваем школу для поиска
        Console.Write("\nВведите название школы для поиска: ");
        string searchSchool = Console.ReadLine();

        // Создаем новый список студентов из заданной школы
        List<Student> studentsFromSchool = new List<Student>();
        foreach (var student in students)
        {
            if (student.School.Equals(searchSchool, StringComparison.OrdinalIgnoreCase))
            {
                studentsFromSchool.Add(student);
            }
        }

        // Сортируем по году рождения
        studentsFromSchool.Sort();

        // Выводим результат
        Console.WriteLine($"\nСтуденты, окончившие {searchSchool} (отсортированы по году рождения):");
        if (studentsFromSchool.Count > 0)
        {
            foreach (var student in studentsFromSchool)
            {
                Console.WriteLine(student);
            }
        }
        else
        {
            Console.WriteLine("Студенты из указанной школы не найдены.");
        }

        Console.WriteLine("\n" + new string('=', 50) + "\n");
        Console.WriteLine("=== ЗАДАНИЕ 2: Список студентов по успеваемости ===\n");

        // Создаем коллекцию студентов с оценками
        List<StudentWithGrades> studentsWithGrades = new List<StudentWithGrades>
        {
            new StudentWithGrades("Иванов Иван Иванович", "ГРУППА-101", 4, 5, 3),
            new StudentWithGrades("Петров Петр Петрович", "ГРУППА-102", 3, 3, 4),
            new StudentWithGrades("Сидорова Анна Сергеевна", "ГРУППА-101", 5, 5, 5),
            new StudentWithGrades("Козлов Алексей Владимирович", "ГРУППА-103", 2, 3, 4),
            new StudentWithGrades("Николаева Мария Дмитриевна", "ГРУППА-102", 4, 4, 3),
            new StudentWithGrades("Федоров Дмитрий Игоревич", "ГРУППА-101", 3, 2, 3)
        };

        // Выводим всех студентов с оценками
        Console.WriteLine("Все студенты с оценками:");
        foreach (var student in studentsWithGrades)
        {
            Console.WriteLine(student);
        }

        // Создаем список студентов, успешно сдавших сессию
        List<StudentWithGrades> successfulStudents = new List<StudentWithGrades>();
        foreach (var student in studentsWithGrades)
        {
            if (student.IsSessionPassed())
            {
                successfulStudents.Add(student);
            }
        }

        // Сортируем по номеру группы
        successfulStudents.Sort();

        // Выводим результат
        Console.WriteLine("\nСтуденты, успешно сдавшие сессию (отсортированы по номеру группы):");
        if (successfulStudents.Count > 0)
        {
            foreach (var student in successfulStudents)
            {
                Console.WriteLine(student);
            }
        }
        else
        {
            Console.WriteLine("Нет студентов, успешно сдавших сессию.");
        }

        // Демонстрация работы перегруженных операторов
        Console.WriteLine("\n" + new string('=', 50));
        Console.WriteLine("Демонстрация работы перегруженных операторов:");

        if (students.Count >= 2)
        {
            Student s1 = students[0];
            Student s2 = students[1];
            Console.WriteLine($"\nСравнение студентов:");
            Console.WriteLine($"{s1.FullName} ({s1.BirthYear}) > {s2.FullName} ({s2.BirthYear}): {s1 > s2}");
            Console.WriteLine($"{s1.FullName} ({s1.BirthYear}) < {s2.FullName} ({s2.BirthYear}): {s1 < s2}");
        }

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}