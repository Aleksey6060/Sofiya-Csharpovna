using System;

class Program
{
    static void Main()
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Выберите программу:");
            Console.WriteLine("1. Игра \"Угадай число\"");
            Console.WriteLine("2. Таблица умножения");
            Console.WriteLine("3. Вывод делителей числа");
            Console.WriteLine("0. Выход");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    PlayGuessNumberGame();
                    break;
                case "2":
                    PrintMultiplicationTable();
                    break;
                case "3":
                    PrintDivisors();
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                    break;
            }

            Console.WriteLine();
        }

        Console.WriteLine("До свидания!");
    }

    static void PlayGuessNumberGame()
    {
        Random random = new Random();
        int targetNumber = random.Next(101);

        Console.WriteLine("Добро пожаловать в игру \"Угадай число\"!");
        Console.WriteLine("Я загадал число от 0 до 100. Попробуйте угадать.");

        int attempts = 0;
        bool guessed = false;

        while (!guessed)
        {
            Console.Write("Ваше число: ");
            string input = Console.ReadLine();
            int guess;

            if (int.TryParse(input, out guess))
            {
                attempts++;

                if (guess < targetNumber)
                {
                    Console.WriteLine("Загаданное число больше.");
                }
                else if (guess > targetNumber)
                {
                    Console.WriteLine("Загаданное число меньше.");
                }
                else
                {
                    Console.WriteLine($"Вы угадали число за {attempts} попыток. Поздравляю!");
                    guessed = true;
                }
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Попробуйте снова.");
            }
        }
    }

    static void PrintMultiplicationTable()
    {
        int[,] table = new int[10, 10];

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                table[i, j] = (i + 1) * (j + 1);
            }
        }

        Console.WriteLine("Таблица умножения:");
        Console.WriteLine();

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Console.Write($"{table[i, j],4}");
            }
            Console.WriteLine();
        }
    }

    static void PrintDivisors()
    {
        Console.Write("Введите число: ");
        string input = Console.ReadLine();
        int number;

        if (int.TryParse(input, out number))
        {
            Console.WriteLine($"Делители числа {number}:");

            for (int i = 1; i <= number; i++)
            {
                if (number % i == 0)
                {
                    Console.WriteLine(i);
                }
            }
        }
        else
        {
            Console.WriteLine("Некорректный ввод. Попробуйте снова.");
        }
    }
}