using System;
interface gg
{
}

// Интерфейс для сравнения студентов
interface IS
{
    int CompareTo(object obj);
}

// Структура для студента (задание 1)
struct sfa : IS
{
    public string FullName;
    public int BirthYear;
    public string School;

    public sfa(string fullName, int birthYear, string school)
    {
        FullName = fullName;
        BirthYear = birthYear;
        School = school;
    }

    // Реализация интерфейса IStudentComparable
    public int CompareTo(object obj)
    {
        if (obj is sfa other)
        {
            return this.BirthYear.CompareTo(other.BirthYear);
        }
        throw new ArgumentException("Object is not a Student");
    }

    public void PrintInfo()
    {
        Console.WriteLine($"{FullName}, год рождения: {BirthYear}, школа: {School}");
    }
}

// Интерфейс для студента с оценками
interface IExaminable
{
    bool IsSessionPassed();
}

// Структура для студента с оценками (задание 2)
struct SW : IS, IExaminable
{
    public string FullName;
    public string GroupNumber;
    public int Exam1;
    public int Exam2;
    public int Exam3;

    public SW(string fullName, string groupNumber, int exam1, int exam2, int exam3)
    {
        FullName = fullName;
        GroupNumber = groupNumber;
        Exam1 = exam1;
        Exam2 = exam2;
        Exam3 = exam3;
    }

    // Реализация интерфейса IExaminable
    public bool IsSessionPassed()
    {
        return Exam1 >= 3 && Exam2 >= 3 && Exam3 >= 3;
    }

    // Реализация интерфейса IStudentComparable
    public int CompareTo(object obj)
    {
        if (obj is SW other)
        {
            return this.GroupNumber.CompareTo(other.GroupNumber);
        }
        throw new ArgumentException("Object is not a StudentWithGrades");
    }

    public void PrintInfo()
    {
        Console.WriteLine($"{FullName}, группа: {GroupNumber}, экзамены: {Exam1}, {Exam2}, {Exam3}");
    }
}

class Program
{
    // Метод для поиска студентов из заданной школы
    static sfa[] FindStudentsBySchool(sfa[] allStudents, string school)
    {
        // Сначала подсчитаем количество студентов из нужной школы
        int count = 0;
        for (int i = 0; i < allStudents.Length; i++)
        {
            if (allStudents[i].School.Equals(school, StringComparison.OrdinalIgnoreCase))
            {
                count++;
            }
        }

        // Создаем массив нужного размера
        sfa[] result = new sfa[count];
        int index = 0;

        // Заполняем массив
        for (int i = 0; i < allStudents.Length; i++)
        {
            if (allStudents[i].School.Equals(school, StringComparison.OrdinalIgnoreCase))
            {
                result[index] = allStudents[i];
                index++;
            }
        }

        return result;
    }

    // Метод для поиска студентов, успешно сдавших сессию
    static SW[] fs(SW[] allStudents)
    {
        // Подсчитываем количество успешных студентов
        int count = 0;
        for (int i = 0; i < allStudents.Length; i++)
        {
            if (allStudents[i].IsSessionPassed())
            {
                count++;
            }
        }

        // Создаем массив нужного размера
        SW[] result = new SW[count];
        int index = 0;

        // Заполняем массив
        for (int i = 0; i < allStudents.Length; i++)
        {
            if (allStudents[i].IsSessionPassed())
            {
                result[index] = allStudents[i];
                index++;
            }
        }

        return result;
    }

    // Метод для сортировки массива студентов (использует интерфейс IStudentComparable)
    static void sorty(IS[] students)
    {
        // Простая пузырьковая сортировка
        for (int i = 0; i < students.Length - 1; i++)
        {
            for (int j = 0; j < students.Length - 1 - i; j++)
            {
                if (students[j].CompareTo(students[j + 1]) > 0)
                {
                    // Меняем местами
                    IS temp = students[j];
                    students[j] = students[j + 1];
                    students[j + 1] = temp;
                }
            }
        }
    }

    static void Main()
    {
        Console.WriteLine("=== ЗАДАНИЕ 1: Список студентов по школам ===\n");

        // Создаем массив студентов
        sfa[] sd = new sfa[6];
        sd[0] = new sfa("Иванов Иван Иванович", 2000, "Школа №15");
        sd[1] = new sfa("Петров Петр Петрович", 2001, "Школа №10");
        sd[2] = new sfa("Сидорова Анна Сергеевна", 1999, "Школа №15");
        sd[3] = new sfa("Козлов Алексей Владимирович", 2002, "Школа №5");
        sd[4] = new sfa("Николаева Мария Дмитриевна", 2000, "Школа №10");
        sd[5] = new sfa("Федоров Дмитрий Игоревич", 2001, "Школа №15");

        // Выводим всех студентов
        Console.WriteLine("Все студенты:");
        foreach (sfa student in sd)
        {
            student.PrintInfo();
        }

        // Запрашиваем школу для поиска
        Console.Write("\nВведите название школы для поиска: ");
        string searchSchool = Console.ReadLine();

        // Находим студентов из заданной школы
        sfa[] sS = FindStudentsBySchool(sd, searchSchool);

        // Сортируем по году рождения (используя интерфейс)
        IS[] CS = new IS[sS.Length];
        Array.Copy(sS, CS, sS.Length);
        sorty(CS);

        // Копируем обратно в массив Student[]
        Array.Copy(CS, sS, sS.Length);

        // Выводим результат
        Console.WriteLine($"\nСтуденты, окончившие {searchSchool} (отсортированы по году рождения):");
        if (sS.Length > 0)
        {
            foreach (sfa student in sS)
            {
                student.PrintInfo();
            }
        }
        else
        {
            Console.WriteLine("Студенты из указанной школы не найдены.");
        }

        Console.WriteLine("\n" + new string('=', 60));
        Console.WriteLine("=== ЗАДАНИЕ 2: Список студентов по успеваемости ===\n");

        // Создаем массив студентов с оценками
        SW[] sf = new SW[6];
        sf[0] = new SW("Иванов Иван Иванович", "ГРУППА-101", 4, 5, 3);
        sf[1] = new SW("Петров Петр Петрович", "ГРУППА-102", 3, 3, 4);
        sf[2] = new SW("Сидорова Анна Сергеевна", "ГРУППА-101", 5, 5, 5);
        sf[3] = new SW("Козлов Алексей Владимирович", "ГРУППА-103", 2, 3, 4);
        sf[4] = new SW("Николаева Мария Дмитриевна", "ГРУППА-102", 4, 4, 3);
        sf[5] = new SW("Федоров Дмитрий Игоревич", "ГРУППА-101", 3, 2, 3);

        // Выводим всех студентов с оценками
        Console.WriteLine("Все студенты с оценками:");
        foreach (SW student in sf)
        {
            student.PrintInfo();
        }

        // Находим студентов, успешно сдавших сессию (используя интерфейс IExaminable)
        SW[] gs = fs(sf);

        // Сортируем по номеру группы (используя интерфейс)
        IS[] cS = new IS[gs.Length];
        Array.Copy(gs, cS, gs.Length);
        sorty(cS);
        Array.Copy(cS, gs, gs.Length);

        // Выводим результат
        Console.WriteLine("\nСтуденты, успешно сдавшие сессию (отсортированы по номеру группы):");
        if (gs.Length > 0)
        {
            foreach (SW student in gs)
            {
                student.PrintInfo();
            }
        }
        else
        {
            Console.WriteLine("Нет студентов, успешно сдавших сессию.");
        }

        // Демонстрация работы интерфейсов
        Console.WriteLine("\n" + new string('=', 40));
        Console.WriteLine("Демонстрация работы интерфейсов:");

        if (sd.Length >= 2)
        {
            Console.WriteLine($"\nСравнение студентов по году рождения:");
            Console.WriteLine($"{sd[0].FullName} ({sd[0].BirthYear})");
            Console.WriteLine($"{sd[1].FullName} ({sd[1].BirthYear})");

            int cs = sd[0].CompareTo(sd[1]);
            if (cs < 0)
                Console.WriteLine("Первый студент родился раньше второго");
            else if (cs > 0)
                Console.WriteLine("Первый студент родился позже второго");
            else
                Console.WriteLine("Студенты одного года рождения");
        }

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}