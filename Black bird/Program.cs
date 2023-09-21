using System;

class Calculator
{
    static void Main()
    {
        int choice;
        double num1, num2, result;
        while (true)
        {
            Console.WriteLine("Выберите операцию:");
            Console.WriteLine("1. Сложить два числа");
            Console.WriteLine("2. Вычесть первое из второго");
            Console.WriteLine("3. Перемножить два числа");
            Console.WriteLine("4. Разделить первое на второе");
            Console.WriteLine("5. Возвести в степень N первое число");
            Console.WriteLine("6. Найти квадратный корень из числа");
            Console.WriteLine("7. Найти 1 процент от числа");
            Console.WriteLine("8. Найти факториал из числа");
            Console.WriteLine("9. Выйти из программы");
            Console.Write("Введите номер операции: ");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Введите первое число: ");
                    num1 = double.Parse(Console.ReadLine());
                    Console.Write("Введите второе число: ");
                    num2 = double.Parse(Console.ReadLine());
                    result = num1 + num2;
                    Console.WriteLine("Результат: " + result);
                    break;
                case 2:
                    Console.Write("Введите первое число: ");
                    num1 = double.Parse(Console.ReadLine());
                    Console.Write("Введите второе число: ");
                    num2 = double.Parse(Console.ReadLine());
                    result = num2 - num1;
                    Console.WriteLine("Результат: " + result);
                    break;
                case 3:
                    Console.Write("Введите первое число: ");
                    num1 = double.Parse(Console.ReadLine());
                    Console.Write("Введите второе число: ");
                    num2 = double.Parse(Console.ReadLine());
                    result = num1 * num2;
                    Console.WriteLine("Результат: " + result);
                    break;
                case 4:
                    Console.Write("Введите первое число: ");
                    num1 = double.Parse(Console.ReadLine());
                    Console.Write("Введите второе число: ");
                    num2 = double.Parse(Console.ReadLine());
                    result = num1 / num2;
                    Console.WriteLine("Результат: " + result);
                    break;
                case 5:
                    Console.Write("Введите число: ");
                    num1 = double.Parse(Console.ReadLine());
                    Console.Write("Введите степень: ");
                    int power = int.Parse(Console.ReadLine());
                    result = Math.Pow(num1, power);
                    Console.WriteLine("Результат: " + result);
                    break;
                case 6:
                    Console.Write("Введите число: ");
                    num1 = double.Parse(Console.ReadLine());
                    result = Math.Sqrt(num1);
                    Console.WriteLine("Результат: " + result);
                    break;
                case 7:
                    Console.Write("Введите число: ");
                    num1 = double.Parse(Console.ReadLine());
                    result = num1 / 100;
                    Console.WriteLine("Результат: " + result);
                    break;
                case 8:
                    Console.Write("Введите число: ");
                    num1 = double.Parse(Console.ReadLine());
                    result = 1;
                    for (int i = 1; i <= num1; i++)
                    {
                        result *= i;
                    }
                    Console.WriteLine("Результат: " + result);
                    break;
                case 9:
                    Console.WriteLine("Программа завершена.");
                    return;
                default:
                    Console.WriteLine("Неверный выбор операции.");
                    break;
            }
            Console.WriteLine();
        }
    }
}