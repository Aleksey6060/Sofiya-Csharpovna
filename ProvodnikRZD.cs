using System;
using System.Diagnostics;
using System.IO;

namespace DiskExplorer
{
    class Program
    {
        static void Main(string[] args)
        {
            ExploreDrive(DriveInfo.GetDrives());
        }

        static void ExploreDrive(DriveInfo[] drives)
        {
            int selectedDriveIndex = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите диск:");

                for (int i = 0; i < drives.Length; i++)
                {
                    Console.Write(selectedDriveIndex == i ? "-> " : "   ");

                    if (i == selectedDriveIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(drives[i].Name);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(drives[i].Name);
                    }
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow && selectedDriveIndex > 0)
                {
                    selectedDriveIndex--;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow && selectedDriveIndex < drives.Length - 1)
                {
                    selectedDriveIndex++;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    DriveInfo selectedDrive = drives[selectedDriveIndex];
                    ExploreFolder(selectedDrive.RootDirectory);
                    break;
                }
            }
        }

        static void ExploreFolder(DirectoryInfo folder)
        {
            string[] directories = null;
            string[] files = null;
            int visibleItemsCount = Console.WindowHeight - 5;
            int selectedFolderIndex = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Содержимое папки {folder.FullName}:");

                directories = Directory.GetDirectories(folder.FullName);
                files = Directory.GetFiles(folder.FullName);

                int startIndex = Math.Max(0, selectedFolderIndex - (visibleItemsCount - 1));
                int endIndex = Math.Min(directories.Length + files.Length - 1, startIndex + visibleItemsCount - 1);

                for (int i = startIndex; i <= endIndex; i++)
                {
                    if (i < directories.Length)
                    {
                        Console.WriteLine(i == selectedFolderIndex ? "-> " + Path.GetFileName(directories[i]) : "   " + Path.GetFileName(directories[i]));
                    }
                    else
                    {
                        int fileIndex = i - directories.Length;
                        Console.WriteLine(fileIndex == selectedFolderIndex ? "-> " + Path.GetFileName(files[fileIndex]) : "   " + Path.GetFileName(files[fileIndex]));
                    }
                }

                Console.SetCursorPosition(Console.WindowWidth - 35, 0);
                Console.WriteLine("Доступные команды:");
                Console.SetCursorPosition(Console.WindowWidth - 35, 1);
                Console.WriteLine("A - Создать новую папку");
                Console.SetCursorPosition(Console.WindowWidth - 35, 2);
                Console.WriteLine("D - Удалить папку");
                Console.SetCursorPosition(Console.WindowWidth - 35, 3);
                Console.WriteLine("F - Создать новый файл");
                Console.SetCursorPosition(Console.WindowWidth - 35, 4);
                Console.WriteLine("R - Удалить файл");
                Console.SetCursorPosition(Console.WindowWidth - 35, 5);
                Console.WriteLine("Escape - Перейти обратно в папку");


                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow && selectedFolderIndex > 0)
                {
                    selectedFolderIndex--;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow && selectedFolderIndex < directories.Length + files.Length - 1)
                {
                    selectedFolderIndex++;
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (selectedFolderIndex < directories.Length)
                    {
                        string selectedPath = directories[selectedFolderIndex];
                        DirectoryInfo selectedFolder = new DirectoryInfo(selectedPath);

                        if (selectedFolder.Exists)
                        {
                            ExploreFolder(selectedFolder);
                        }
                    }
                    else
                    {
                        int fileIndex = selectedFolderIndex - directories.Length;
                        string selectedFilePath = files[fileIndex];
                        Process.Start(selectedFilePath);
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (folder.Parent != null)
                    {
                        folder = folder.Parent;
                        selectedFolderIndex = 0;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.A)
                {
                    Console.WriteLine("Введите имя новой папки: ");
                    string newDirectoryName = Console.ReadLine();

                    string newDirectoryPath = Path.Combine(folder.FullName, newDirectoryName);

                    if (!Directory.Exists(newDirectoryPath))
                    {
                        Directory.CreateDirectory(newDirectoryPath);
                        Console.WriteLine("Папка успешно создана!");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Папка с таким именем уже существует!");
                        Console.ReadKey();
                    }
                }
                else if (keyInfo.Key == ConsoleKey.D)
                {
                    if (selectedFolderIndex < directories.Length)
                    {
                        string selectedPath = directories[selectedFolderIndex];
                        DirectoryInfo selectedFolder = new DirectoryInfo(selectedPath);

                        if (selectedFolder.Exists)
                        {
                            Console.WriteLine($"Вы уверены, что хотите удалить папку {selectedFolder.Name}? (Y/N)");

                            ConsoleKeyInfo confirmation = Console.ReadKey();
                            if (confirmation.KeyChar == 'Y' || confirmation.KeyChar == 'y')
                            {
                                Directory.Delete(selectedPath, true);
                                Console.WriteLine("Папка успешно удалена!");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("Удаление папки отменено.");
                                Console.ReadKey();
                            }
                        }
                    }
                }
                else if (keyInfo.Key == ConsoleKey.F)
                {
                    Console.WriteLine("Введите имя нового файла: ");
                    string newFileName = Console.ReadLine();

                    string newFilePath = Path.Combine(folder.FullName, newFileName);

                    if (!File.Exists(newFilePath))
                    {
                        File.Create(newFilePath).Close();
                        Console.WriteLine("Файл успешно создан!");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Файл с таким именем уже существует!");
                        Console.ReadKey();
                    }
                }
                else if (keyInfo.Key == ConsoleKey.R)
                {
                    if (selectedFolderIndex >= directories.Length && selectedFolderIndex < directories.Length + files.Length)
                    {
                        int fileIndex = selectedFolderIndex - directories.Length;
                        string selectedFilePath = files[fileIndex];
                        FileInfo selectedFile = new FileInfo(selectedFilePath);

                        if (selectedFile.Exists)
                        {
                            Console.WriteLine($"Вы уверены, что хотите удалить файл {selectedFile.Name}? (Y/N)");

                            ConsoleKeyInfo confirmation = Console.ReadKey();
                            if (confirmation.KeyChar == 'Y' || confirmation.KeyChar == 'y')
                            {
                                File.Delete(selectedFilePath);
                                Console.WriteLine("Файл успешно удален!");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("Удаление файла отменено.");
                                Console.ReadKey();
                            }
                        }
                    }
                }
            }
        }
    }
}