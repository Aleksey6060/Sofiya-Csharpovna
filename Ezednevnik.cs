using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static List<Note> notes = new List<Note>
    {
        new Note("Заметка 1", "Описание заметки 1", new DateTime(2023,10,6)),
        new Note("Заметка 2", "Описание заметки 2", new DateTime(2023,10,8)),
        new Note("Заметка 3", "Описание заметки 3", new DateTime(2023,10,13)),
    };

    static DateTime selectedDate = DateTime.Today;
    static List<Note> notesForSelectedDate;
    static int selectedIndex = 0;

    static void Main(string[] args)
    {
        Console.WriteLine("Ежедневник");
        Console.WriteLine("---------\n");

        ShowMenu();

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.Enter:
                    ShowNoteDetails();
                    break;
                case ConsoleKey.LeftArrow:
                    DecrementSelectedDate();
                    ShowMenu();
                    break;
                case ConsoleKey.RightArrow:
                    IncrementSelectedDate();
                    ShowMenu();
                    break;
                case ConsoleKey.UpArrow:
                    DecrementSelectedIndex();
                    ShowMenu();
                    break;
                case ConsoleKey.DownArrow:
                    IncrementSelectedIndex();
                    ShowMenu();
                    break;
                case ConsoleKey.Add:
                    AddNote();
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
            }
        }
    }

    static void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("Ежедневник - " + selectedDate.ToShortDateString());
        Console.WriteLine("-----------\n");

        notesForSelectedDate = notes.Where(n => n.Date.Date == selectedDate.Date).ToList();

        if (notesForSelectedDate.Count > 0)
        {
            Console.WriteLine("Заметки:\n");

            for (int i = 0; i < notesForSelectedDate.Count; i++)
            {
                Note note = notesForSelectedDate[i];
                string arrow = i == selectedIndex ? "->" : " ";
                Console.WriteLine(arrow + " " + (i + 1) + ". " + note.Title);
            }
        }
        else
        {
            Console.WriteLine("На выбранную дату нет заметок.");
        }

        Console.WriteLine("\nИспользуйте стрелки влево и вправо для выбора даты.");
        Console.WriteLine("Используйте стрелки вверх и вниз для выбора заметки.");
        Console.WriteLine("Нажмите Enter, чтобы открыть выбранную заметку.");
        Console.WriteLine("Нажмите \"+\" (плюс), чтобы добавить новую заметку.");
        Console.WriteLine("Нажмите Esc для выхода из программы.");
    }

    static void ShowNoteDetails()
    {
        Console.Clear();
        Console.WriteLine("Детали заметки (" + selectedDate.ToShortDateString() + "):\n");

        if (notesForSelectedDate.Count > 0)
        {
            Note selectedNote = notesForSelectedDate[selectedIndex];
            Console.WriteLine("Заголовок: " + selectedNote.Title);
            Console.WriteLine("Описание: " + selectedNote.Description);
            Console.WriteLine("--------------------");
        }
        else
        {
            Console.WriteLine("На выбранную дату нет заметок.");
        }

        Console.WriteLine("\nДля возврата к меню нажмите любую клавишу.");

        Console.ReadKey();
        ShowMenu();
    }

    static void IncrementSelectedDate()
    {
        selectedDate = selectedDate.AddDays(1);
        UpdateNotesForSelectedDate();
    }

    static void DecrementSelectedDate()
    {
        selectedDate = selectedDate.AddDays(-1);
        UpdateNotesForSelectedDate();
    }

    static void IncrementSelectedIndex()
    {
        selectedIndex++;
        if (selectedIndex >= notesForSelectedDate.Count)
        {
            selectedIndex = 0;
        }
    }

    static void DecrementSelectedIndex()
    {
        selectedIndex--;
        if (selectedIndex < 0)
        {
            selectedIndex = notesForSelectedDate.Count - 1;
        }
    }

    static void AddNote()
    {
        Console.Clear();
        Console.WriteLine("Добавление новой заметки\n");
        Console.WriteLine("Введите заголовок заметки:");
        string title = Console.ReadLine();

        Console.WriteLine("Введите описание заметки:");
        string description = Console.ReadLine();

        Note newNote = new Note(title, description, selectedDate);
        notes.Add(newNote);

        Console.WriteLine("\nЗаметка успешно добавлена!");
        Console.WriteLine("\nДля продолжения нажмите любую клавишу.");

        Console.ReadKey();
        ShowMenu();
    }

    static void UpdateNotesForSelectedDate()
    {
        notesForSelectedDate = notes.Where(n => n.Date.Date == selectedDate.Date).ToList();
        selectedIndex = 0;
    }
}

class Note
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }

    public Note(string title, string description, DateTime date)
    {
        Title = title;
        Description = description;
        Date = date;
    }
}