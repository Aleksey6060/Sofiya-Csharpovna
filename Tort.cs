using System;                                 //для сохранения заказов на рабочем столе должен быть создан текстовый файл Order.txt
using System.Collections.Generic;
using System.IO;

namespace ConsoleCakeShop
{
    public class SubmenuItem
    {
        public string Description { get; set; }
        public decimal Price { get; set; }

        public SubmenuItem(string description)
        {
            Description = description;
        }

        public SubmenuItem(string description, decimal price)
        {
            Description = description;
            Price = price;
        }
    }

    public static class ArrowMenu
    {
        public static int ShowMenu(SubmenuItem[] submenuItems)
        {
            int selectedItemIndex = 0;
            bool menuActive = true;

            while (menuActive)
            {
                Console.Clear();
                Console.WriteLine("========== Меню ==========");

                for (int i = 0; i < submenuItems.Length; i++)
                {
                    SubmenuItem item = submenuItems[i];
                    string prefix = (i == selectedItemIndex) ? "-> " : "   ";
                    string itemText = item.Price != 0 ? $"{item.Description} - {item.Price:C}" : item.Description;
                    Console.WriteLine(prefix + itemText);
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedItemIndex = (selectedItemIndex - 1 + submenuItems.Length) % submenuItems.Length;
                        break;
                    case ConsoleKey.DownArrow:
                        selectedItemIndex = (selectedItemIndex + 1) % submenuItems.Length;
                        break;
                    case ConsoleKey.Enter:
                        Console.WriteLine();
                        menuActive = false;
                        break;
                    case ConsoleKey.Escape:
                        selectedItemIndex = -2;
                        menuActive = false;
                        break;
                    default:
                        break;
                }
            }

            return selectedItemIndex;
        }
    }

    class Program
    {
        static void SaveOrder(decimal totalPrice, List<string> ingredients)
        {
            string orderDetails = $"Сумма заказа: {totalPrice:C}{Environment.NewLine}" +
                                  $"Составляющие торта: {string.Join(", ", ingredients)}{Environment.NewLine}";

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "Order.txt");

            try
            {
                File.AppendAllText(filePath, orderDetails);
                Console.WriteLine($"Заказ сохранен в файле: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении заказа: {ex.Message}");
            }
        }

        static void Main(string[] args)
        {
            string cakeShape = string.Empty;
            string cakeSize = string.Empty;
            string cakeFlavor = string.Empty;
            string cakeFloor = string.Empty;
            string cakeFrosting = string.Empty;
            string cakeDecoration = string.Empty;
            decimal totalPrice = 0;
            List<string> ingredients = new List<string>();

            SubmenuItem[] mainMenuItems = {
                new SubmenuItem("Форма торта"),
                new SubmenuItem("Размер торта"),
                new SubmenuItem("Вкус торта"),
                new SubmenuItem("Количество этажей"),
                new SubmenuItem("Глазурь торта"),
                new SubmenuItem("Декор торта"),
                new SubmenuItem("Сохранить заказ") // Добавляем пункт "Сохранить заказ" в меню
            };

            bool exitProgram = false;

            while (!exitProgram)
            {
                int mainSelectedMenuItemIndex = ArrowMenu.ShowMenu(mainMenuItems);

                if (mainSelectedMenuItemIndex == -2)
                {
                    Console.WriteLine("Выход в главное меню...");
                    Console.WriteLine();
                    continue;
                }
                else if (mainSelectedMenuItemIndex == -1)
                {
                    Console.WriteLine("Выход из программы...");
                    Console.ReadLine();
                    return;
                }

                SubmenuItem mainSelectedMenuItem = mainMenuItems[mainSelectedMenuItemIndex];

                Console.WriteLine($"Вы выбрали пункт: {mainSelectedMenuItem.Description}");
                Console.WriteLine();

                if (mainSelectedMenuItem.Description == "Форма торта")
                {
                    SubmenuItem[] shapeMenuItems = {
                        new SubmenuItem("Круглая форма", 10),
                        new SubmenuItem("Прямоугольная форма", 15),
                        new SubmenuItem("Сердце", 20)
                    };

                    int shapeSelectedMenuItemIndex = ArrowMenu.ShowMenu(shapeMenuItems);

                    if (shapeSelectedMenuItemIndex == -2)
                    {
                        Console.WriteLine("Выход в главное меню...");
                        Console.WriteLine();
                        continue;
                    }
                    else if (shapeSelectedMenuItemIndex == -1)
                    {
                        Console.WriteLine("Выход из программы...");
                        Console.ReadLine();
                        return;
                    }

                    SubmenuItem shapeMenuItem = shapeMenuItems[shapeSelectedMenuItemIndex];
                    cakeShape = shapeMenuItem.Description;
                    Console.WriteLine($"Вы выбрали форму: {cakeShape}");
                }
                else if (mainSelectedMenuItem.Description == "Размер торта")
                {
                    SubmenuItem[] sizeMenuItems = {
                        new SubmenuItem("Маленький", 5),
                        new SubmenuItem("Средний", 10),
                        new SubmenuItem("Большой", 15)
                    };

                    int sizeSelectedMenuItemIndex = ArrowMenu.ShowMenu(sizeMenuItems);

                    if (sizeSelectedMenuItemIndex == -2)
                    {
                        Console.WriteLine("Выход в главное меню...");
                        Console.WriteLine();
                        continue;
                    }
                    else if (sizeSelectedMenuItemIndex == -1)
                    {
                        Console.WriteLine("Выход из программы...");
                        Console.ReadLine();
                        return;
                    }

                    SubmenuItem sizeMenuItem = sizeMenuItems[sizeSelectedMenuItemIndex];
                    cakeSize = sizeMenuItem.Description;
                    Console.WriteLine($"Вы выбрали размер: {cakeSize}");
                }
                else if (mainSelectedMenuItem.Description == "Вкус торта")
                {
                    SubmenuItem[] flavorMenuItems = {
                        new SubmenuItem("Шоколадный", 5),
                        new SubmenuItem("Ванильный", 5),
                        new SubmenuItem("Красный бархат", 10),
                        new SubmenuItem("Фруктовый", 8)
                    };

                    int flavorSelectedMenuItemIndex = ArrowMenu.ShowMenu(flavorMenuItems);

                    if (flavorSelectedMenuItemIndex == -2)
                    {
                        Console.WriteLine("Выход в главное меню...");
                        Console.WriteLine();
                        continue;
                    }
                    else if (flavorSelectedMenuItemIndex == -1)
                    {
                        Console.WriteLine("Выход из программы...");
                        Console.ReadLine();
                        return;
                    }

                    SubmenuItem flavorMenuItem = flavorMenuItems[flavorSelectedMenuItemIndex];
                    cakeFlavor = flavorMenuItem.Description;
                    Console.WriteLine($"Вы выбрали вкус: {cakeFlavor}");
                    ingredients.Add(cakeFlavor);
                    totalPrice += flavorMenuItem.Price;
                }
                else if (mainSelectedMenuItem.Description == "Количество этажей")
                {
                    SubmenuItem[] difficultyMenuItems = {
                        new SubmenuItem("1 этаж", 2),
                        new SubmenuItem("2 этажа", 5),
                        new SubmenuItem("3 этажа", 10)
    };

                    int difficultySelectedMenuItemIndex = ArrowMenu.ShowMenu(difficultyMenuItems);

                    if (difficultySelectedMenuItemIndex == -2)
                    {
                        Console.WriteLine("Выход в главное меню...");
                        Console.WriteLine();
                        continue;
                    }
                    else if (difficultySelectedMenuItemIndex == -1)
                    {
                        Console.WriteLine("Выход из программы...");
                        Console.ReadLine();
                        return;
                    }

                    SubmenuItem difficultyMenuItem = difficultyMenuItems[difficultySelectedMenuItemIndex];
                    string difficultyLevel = difficultyMenuItem.Description;
                    Console.WriteLine($"Вы выбрали: {difficultyLevel}");
                    ingredients.Add(difficultyLevel);
                    totalPrice += difficultyMenuItem.Price;
                }
                else if (mainSelectedMenuItem.Description == "Глазурь торта")
                {
                    SubmenuItem[] frostingMenuItems = {
                        new SubmenuItem("Шоколадная", 2),
                        new SubmenuItem("Сливочно-сахарная", 2),
                        new SubmenuItem("Крем-сыр", 3),
                        new SubmenuItem("Фруктовая", 2)
    };

                    int frostingSelectedMenuItemIndex = ArrowMenu.ShowMenu(frostingMenuItems);

                    if (frostingSelectedMenuItemIndex == -2)
                    {
                        Console.WriteLine("Выход в главное меню...");
                        Console.WriteLine();
                        continue;
                    }
                    else if (frostingSelectedMenuItemIndex == -1)
                    {
                        Console.WriteLine("Выход из программы...");
                        Console.ReadLine();
                        return;
                    }

                    SubmenuItem frostingMenuItem = frostingMenuItems[frostingSelectedMenuItemIndex];
                    cakeFrosting = frostingMenuItem.Description;
                    Console.WriteLine($"Вы выбрали глазурь: {cakeFrosting}");
                    ingredients.Add(cakeFrosting);
                    totalPrice += frostingMenuItem.Price;
                }
                else if (mainSelectedMenuItem.Description == "Декор торта")
                {
                    SubmenuItem[] decorationMenuItems = {
                        new SubmenuItem("Цветные конфети", 1),
                        new SubmenuItem("Фруктовые ягоды", 2),
                        new SubmenuItem("Шоколадные украшения", 3),
                        new SubmenuItem("Марципановые фигурки", 4)
    };

                    int decorationSelectedMenuItemIndex = ArrowMenu.ShowMenu(decorationMenuItems);

                    if (decorationSelectedMenuItemIndex == -2)
                    {
                        Console.WriteLine("Выход в главное меню...");
                        Console.WriteLine();
                        continue;
                    }
                    else if (decorationSelectedMenuItemIndex == -1)
                    {
                        Console.WriteLine("Выход из программы...");
                        Console.ReadLine();
                        return;
                    }

                    SubmenuItem decorationMenuItem = decorationMenuItems[decorationSelectedMenuItemIndex];
                    cakeDecoration = decorationMenuItem.Description;
                    Console.WriteLine($"Вы выбрали декор: {cakeDecoration}");
                    ingredients.Add(cakeDecoration);
                    totalPrice += decorationMenuItem.Price;
                }
                else if (mainSelectedMenuItem.Description == "Сохранить заказ")
                {
                    if (totalPrice == 0)
                    {
                        Console.WriteLine("Невозможно сохранить заказ без выбранных опций.");
                        Console.WriteLine("Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        continue;
                    }

                    SaveOrder(totalPrice, ingredients);
                    Console.WriteLine("Заказ сохранен!");
                    Console.WriteLine("Нажмите любую клавишу для продолжения или 'Q' для выхода из программы...");
                    string continueInput = Console.ReadLine();
                    if (continueInput.ToUpper() == "Q")
                        break;

                   
                    totalPrice = 0;
                    ingredients.Clear();
                    Console.Clear();
                }
            }
        }
    }
}