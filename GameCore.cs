namespace Maze;

public class GameCore
{
    private readonly Map map;
    private (int, int) playerPosition = (1, 0);
    private (int, int) exitPosition;

    public int Width { get; set; } = 6;
    public int Height { get; set; } = 6;

    public GameCore()
    {
        map = new Map()
        {
            GridWidth = Width * 2 + 1,
            GridHeight = Height * 2 + 1
        };
    }

    public void MainMenu()
    {
        Console.Write("Welcome to the Maze Game CLI!\nChoose an option:\n");
        Console.WriteLine("1. Start the game");
        Console.WriteLine("2. Open settings");
        Console.WriteLine("3. Exit");
        var pressed = Console.ReadKey();
        Console.Clear();
        switch (pressed.Key)
        {
            case ConsoleKey.D1:
            {
                Start();
                break;
            }
            case ConsoleKey.D2:
            {
                break;
            }
            case ConsoleKey.D3:
            {
                return;
            }
        }
    }

    private void Start()
    {
        map.GenerateBinaryMaze();
        SetPlayer();
        SetExit();
        map.Print();
        Game();
    }

    private void Game()
    {
        while (true)
        {
            var pressed = Console.ReadKey();

            map.Grid![playerPosition.Item1, playerPosition.Item2] = ' ';

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

    private void SetPlayer()
    {
        map.Grid![playerPosition.Item1, playerPosition.Item2] = 'P';
    }

    private void SetExit()
    {
        exitPosition = (map.GridWidth - 2, map.GridHeight - 1);
        map.Grid![map.GridWidth - 2, map.GridHeight - 1] = 'E';
    }

    private void RedrawMap()
    {
        Console.Clear();
        map.Grid![playerPosition.Item1, playerPosition.Item2] = 'P';
        map.Print();
    }

}
