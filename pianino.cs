using System;

namespace SimplePiano
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press the corresponding key to play a note:");
            Console.WriteLine("A - C, S - D, D - E, F - F, G - G, H - A, J - B, K - C#, L - D#, Z - F#, X - G#, С - Bb");
            Console.WriteLine("Press F4-F5 to switch octaves");

            int octave = 4; // начинаем с четвертой октавы

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.A:
                            PlaySound(261, octave, 500);
                            break;
                        case ConsoleKey.S:
                            PlaySound(293, octave, 500);
                            break;
                        case ConsoleKey.D:
                            PlaySound(329, octave, 500);
                            break;
                        case ConsoleKey.F:
                            PlaySound(349, octave, 500);
                            break;
                        case ConsoleKey.G:
                            PlaySound(392, octave, 500);
                            break;
                        case ConsoleKey.H:
                            PlaySound(440, octave, 500);
                            break;
                        case ConsoleKey.J:
                            PlaySound(493, octave, 500);
                            break;
                        case ConsoleKey.K:
                            PlaySound(277, octave, 500);
                            break;
                        case ConsoleKey.L:
                            PlaySound(311, octave, 500);
                            break;
                        case ConsoleKey.Z:
                            PlaySound(370, octave, 500);
                            break;
                        case ConsoleKey.X:
                            PlaySound(415, octave, 500);
                            break;
                        case ConsoleKey.C:
                            PlaySound(462, octave, 500);
                            break;
                        case ConsoleKey.F4:
                            octave = 4; // переход к четвертой октаве
                            Console.WriteLine("Octave: " + octave);
                            break;
                        case ConsoleKey.F5:
                            octave = 5; // переход к пятой октаве
                            Console.WriteLine("Octave: " + octave);
                            break;
                    }
                }
            }
        }

        static void PlaySound(int frequency, int octave, int duration)
        {
            int freq = frequency * (int)Math.Pow(2, octave - 4); // вычисляем частоту звука в зависимости от октавы
            Console.Beep(freq, duration);
        }
    }
}