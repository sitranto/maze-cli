namespace Maze;

public class GameCore
{
    private Map? map;
    private (int, int) playerPosition;
    private (int, int) exitPosition;

    public void MainMenu()
    {
        while (true)
        {
            Greetings();
            var pressed = Console.ReadKey();
            switch (pressed.Key)
            {
                case ConsoleKey.D1:
                {
                    Start();
                    break;
                }
                case ConsoleKey.D2:
                {
                    OpenSettings();
                    break;
                }
                case ConsoleKey.D3:
                {
                    return;
                }
            }
            Console.Clear();
        }
    }

    private void Start()
    {
        Console.Clear();
        map = new()
        {
            GridWidth = Settings.Width * 2 + 1,
            GridHeight = Settings.Height * 2 + 1
        };
        map.GenerateBinaryMaze();
        SetPlayer();
        SetExit();
        map.Print();
        Game();
    }

    private void Greetings()
    {
        Console.Write("Welcome to the Maze Game CLI!\nChoose an option:\n");
        Console.WriteLine("1. Start the game");
        Console.WriteLine("2. Open settings");
        Console.WriteLine("3. Exit");
    }

    private void Game()
    {
        while (true)
        {
            var pressed = Console.ReadKey();

            map!.Grid![playerPosition.Item1, playerPosition.Item2] = ' ';

            switch (pressed.Key)
            {
                case ConsoleKey.LeftArrow:
                {
                    if (playerPosition.Item2 - 1 >= 0 &&
                        map.Grid[playerPosition.Item1, playerPosition.Item2 - 1] != '▤')
                    {
                        playerPosition.Item2 -= 1;
                    }
                    break;
                }
                case ConsoleKey.RightArrow:
                {
                    if (playerPosition.Item2 - 1 <= map.GridWidth &&
                        map.Grid[playerPosition.Item1, playerPosition.Item2 + 1] != '▤')
                    {
                        playerPosition.Item2 += 1;
                    }
                    break;
                }
                case ConsoleKey.UpArrow:
                {
                    if (playerPosition.Item1 - 1 >= 0 &&
                        map.Grid[playerPosition.Item1 - 1, playerPosition.Item2] != '▤')
                    {
                        playerPosition.Item1 -= 1;
                    }
                    break;
                }
                case ConsoleKey.DownArrow:
                {
                    if (playerPosition.Item1 + 1 <= map.GridHeight &&
                        map.Grid[playerPosition.Item1 + 1, playerPosition.Item2] != '▤')
                    {
                        playerPosition.Item1 += 1;
                    }
                    break;
                }
            }

            RedrawMap();

            if (playerPosition == exitPosition)
            {
                return;
            }
        }
    }

    private void OpenSettings()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Settings:");
            Console.WriteLine("1. Maze width: " + Settings.Width);
            Console.WriteLine("2. Maze height: " + Settings.Height);
            Console.WriteLine("3. Exit");

            var pressed = Console.ReadKey();

            switch (pressed.Key)
            {
                case ConsoleKey.D1:
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("Enter value from 3 to 15 and press ENTER: ");
                        if (!short.TryParse(Console.ReadLine(), out var value) || value is > 15 or < 3)
                        {
                            continue;
                        }

                        Settings.Width = value;
                        break;
                    }
                    break;
                }
                case ConsoleKey.D2:
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("Enter value from 3 to 15 and press ENTER: ");
                        if (!short.TryParse(Console.ReadLine(), out var value) || value is > 15 or < 3)
                        {
                            continue;
                        }

                        Settings.Height = value;
                        break;
                    }
                    break;
                }
                case ConsoleKey.D3:
                {
                    return;
                }
            }
        }
    }

    private void SetPlayer()
    {
        playerPosition = (1, 0);
        map!.Grid![playerPosition.Item1, playerPosition.Item2] = 'P';
    }

    private void SetExit()
    {
        exitPosition = (map!.GridWidth - 2, map.GridHeight - 1);
        map.Grid![map.GridWidth - 2, map.GridHeight - 1] = 'E';
    }

    private void RedrawMap()
    {
        Console.Clear();
        map!.Grid![playerPosition.Item1, playerPosition.Item2] = 'P';
        map.Print();
    }

}
