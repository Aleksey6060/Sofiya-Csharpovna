using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;

public class Figure
{
    public string Name { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
}

public class TextEditor
{
    public void OpenAndConvert(string filePath)
    {
        bool exitProgram = false;

        do
        {
            string[] lines = File.ReadAllLines(filePath);
            List<Figure> figures = new List<Figure>();

            for (int i = 0; i < lines.Length; i += 3)
            {
                Figure figure = new Figure
                {
                    Name = lines[i],
                    Width = double.Parse(lines[i + 1]),
                    Height = double.Parse(lines[i + 2])
                };

                figures.Add(figure);
            }

            foreach (Figure figure in figures)
            {
                Console.WriteLine($"Name: {figure.Name}, Width: {figure.Width}, Height: {figure.Height}");
            }

            Console.WriteLine("Выберите формат для экспорта данных. Введите 'json', 'xml' или 'txt':");
            string exportFormat = Console.ReadLine();

            if (exportFormat.ToLower() == "json")
            {
                ExportToJson(figures);
            }
            else if (exportFormat.ToLower() == "xml")
            {
                ExportToXml(figures);
            }
            else if (exportFormat.ToLower() == "txt")
            {
                ExportToTxt(figures);
            }
            else
            {
                Console.WriteLine("Некорректный формат для экспорта данных.");
            }

            Console.WriteLine("Нажмите Escape для выхода из программы или любую другую клавишу для открытия нового файла.");
            exitProgram = Console.ReadKey().Key == ConsoleKey.Escape;

        } while (!exitProgram);
    }

    private void ExportToJson(List<Figure> figures)
    {
        string jsonData = JsonConvert.SerializeObject(figures, Newtonsoft.Json.Formatting.Indented);

        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string newFilePath = Path.Combine(desktopPath, "output.json");

        File.WriteAllText(newFilePath, jsonData);

        Console.WriteLine($"Данные успешно экспортированы в JSON. Файл сохранен на рабочем столе с именем 'output.json'.");
    }

    private void ExportToXml(List<Figure> figures)
    {
        XDocument xmlDocument = new XDocument(new XElement("Figures"));

        foreach (Figure figure in figures)
        {
            XElement figureElement = new XElement("Figure",
                new XElement("Name", figure.Name),
                new XElement("Width", figure.Width),
                new XElement("Height", figure.Height));

            xmlDocument.Root.Add(figureElement);
        }

        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string newFilePath = Path.Combine(desktopPath, "output.xml");

        xmlDocument.Save(newFilePath);

        Console.WriteLine($"Данные успешно экспортированы в XML. Файл сохранен на рабочем столе с именем 'output.xml'.");
    }

    private void ExportToTxt(List<Figure> figures)
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string newFilePath = Path.Combine(desktopPath, "output.txt");

        using (StreamWriter writer = new StreamWriter(newFilePath))
        {
            foreach (Figure figure in figures)
            {
                writer.WriteLine(figure.Name);
                writer.WriteLine(figure.Width);
                writer.WriteLine(figure.Height);
            }
        }

        Console.WriteLine($"Данные успешно экспортированы в TXT. Файл сохранен на рабочем столе с именем 'output.txt'.");
    }
}

public class Program
{
    public static void Main()
    {
        TextEditor editor = new TextEditor();

        Console.WriteLine("Введите путь к файлу:");
        string filePath = Console.ReadLine();

        editor.OpenAndConvert(filePath);

        Console.ReadLine();
    }
}