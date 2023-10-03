using System;
using System.Threading;

class Program
{
    static int[] firstOctave = new int[] { 131, 139, 147, 156, 165, 175, 185, 196, 208, 220, 233, 247 };
    static int[] secondOctave = new int[] { 262, 277, 294, 311, 330, 349, 370, 392, 415, 440, 466, 494 };
    static int[] thirdOctave = new int[] { 523, 554, 587, 622, 659, 698, 740, 784, 831, 880, 932, 988 };
    static int currentOctave = 1;

    static void Main(string[] args)
    {
        Console.WriteLine("Press F1, F2, F3 to change octave");
        Console.WriteLine("Press ESC to exit");

        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
                else if (key.Key == ConsoleKey.F1)
                {
                    currentOctave = 1;
                    Console.WriteLine("Octave 1");
                }
                else if (key.Key == ConsoleKey.F2)
                {
                    currentOctave = 2;
                    Console.WriteLine("Octave 2");
                }
                else if (key.Key == ConsoleKey.F3)
                {
                    currentOctave = 3;
                    Console.WriteLine("Octave 3");
                }
                else
                {
                    int noteIndex = GetNoteIndex(key.Key);
                    if (noteIndex != -1)
                    {
                        int frequency = GetFrequency(noteIndex);
                        Console.Beep(frequency, 500);
                    }
                }
            }
        }
    }

    static int GetNoteIndex(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.Q:
                return 0;
            case ConsoleKey.W:
                return 1;
            case ConsoleKey.E:
                return 2;
            case ConsoleKey.R:
                return 3;
            case ConsoleKey.T:
                return 4;
            case ConsoleKey.Y:
                return 5;
            case ConsoleKey.U:
                return 6;
            case ConsoleKey.I:
                return 7;
            case ConsoleKey.O:
                return 8;
            case ConsoleKey.P:
                return 9;
            case ConsoleKey.Z:
                return 10;
            case ConsoleKey.X:
                return 11;
            default:
                return -1;
        }
    }

    static int GetFrequency(int noteIndex)
    {
        int[] octave = GetOctave(currentOctave);
        return octave[noteIndex];
    }

    static int[] GetOctave(int octaveNumber)
    {
        switch (octaveNumber)
        {
            case 1:
                return firstOctave;
            case 2:
                return secondOctave;
            case 3:
                return thirdOctave;
            default:
                return firstOctave;
        }
    }
}