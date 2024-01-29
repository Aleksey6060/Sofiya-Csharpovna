using System;
using System.Collections.Generic;
using System.Threading;

// Enum for the snake's direction
enum Direction
{
    Up,
    Down,
    Left,
    Right
}

// Enum for the game area borders
enum Border
{
    MaxRight = 80,
    MaxBottom = 24
}

class SnakeGame
{
    private bool isRunning;
    private Thread inputThread;
    private Thread renderThread;
    private List<int[]> snake;
    private Direction currentDirection;
    private bool isFoodEaten;
    private int[] foodPosition;
    private Random random;

    public void Start()
    {
        isRunning = true;
        snake = new List<int[]>
        {
            new int[] { 40, 12 }  // starting position of the snake's head
        };
        currentDirection = Direction.Right;
        isFoodEaten = true;
        random = new Random();

        inputThread = new Thread(ReadInput);
        inputThread.Start();

        renderThread = new Thread(Render);
        renderThread.Start();

        while (isRunning)
        {
            Update();

            Thread.Sleep(100);
        }
    }

    public void Stop()
    {
        isRunning = false;
        inputThread.Join();
        renderThread.Join();
        Console.Clear();
    }

    private void Update()
    {
        if (isFoodEaten)
        {
            GenerateFood();
            isFoodEaten = false;
        }
        else
        {
            snake.RemoveAt(snake.Count - 1);  // remove the last segment (tail)
        }

        int[] newHead = GetNextHeadPosition();
        snake.Insert(0, newHead);  // insert the new head at the beginning of the list

        if (IsCollision(newHead))
        {
            Stop();
            Console.WriteLine("Game Over");
        }

        if (foodPosition[0] == newHead[0] && foodPosition[1] == newHead[1])
        {
            isFoodEaten = true;
        }
    }

    private void Render()
    {
        while (isRunning)
        {
            Console.Clear();

            foreach (int[] segment in snake)
            {
                Console.SetCursorPosition(segment[0], segment[1]);
                Console.Write("#");
            }

            Console.SetCursorPosition(foodPosition[0], foodPosition[1]);
            Console.Write("*");

            Thread.Sleep(50);
        }
    }

    private void ReadInput()
    {
        while (isRunning)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        currentDirection = Direction.Up;
                        break;
                    case ConsoleKey.DownArrow:
                        currentDirection = Direction.Down;
                        break;
                    case ConsoleKey.LeftArrow:
                        currentDirection = Direction.Left;
                        break;
                    case ConsoleKey.RightArrow:
                        currentDirection = Direction.Right;
                        break;
                }
            }
        }
    }

    private int[] GetNextHeadPosition()
    {
        int[] head = snake[0];
        int[] newHead = new int[] { head[0], head[1] };

        switch (currentDirection)
        {
            case Direction.Up:
                newHead[1]--;
                break;
            case Direction.Down:
                newHead[1]++;
                break;
            case Direction.Left:
                newHead[0]--;
                break;
            case Direction.Right:
                newHead[0]++;
                break;
        }

        // Wrap around the borders
        if (newHead[0] < 0)
        {
            newHead[0] = (int)Border.MaxRight;
        }
        else if (newHead[0] > (int)Border.MaxRight)
        {
            newHead[0] = 0;
        }

        if (newHead[1] < 0)
        {
            newHead[1] = (int)Border.MaxBottom;
        }
        else if (newHead[1] > (int)Border.MaxBottom)
        {
            newHead[1] = 0;
        }

        return newHead;
    }

    private bool IsCollision(int[] position)
    {
        for (int i = 1; i < snake.Count; i++)
        {
            if (snake[i][0] == position[0] && snake[i][1] == position[1])
            {
                return true;
            }
        }

        return false;
    }

    private void GenerateFood()
    {
        int x = random.Next(0, (int)Border.MaxRight + 1);
        int y = random.Next(0, (int)Border.MaxBottom + 1);

        foodPosition = new int[] { x, y };
    }
}

class Program
{
    static void Main(string[] args)
    {
        SnakeGame game = new SnakeGame();
        game.Start();
    }
}