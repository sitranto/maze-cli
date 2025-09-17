namespace Maze;

public class Map
{
    public char[,]? Grid;
    public required int GridWidth { get; set; }
    public required int GridHeight { get; set; }

    public void GenerateBinaryMaze()
    {
        var random = new Random();
        FillGrid();
        for (var y = 1; y < GridHeight - 1; y += 2)
        {
            for (var x = 1; x < GridWidth - 1; x += 2)
            {
                var canGoRight = (x < GridWidth - 2);
                var canGoDown = (y < GridHeight - 2);

                Grid![y, x] = ' ';

                if (canGoRight && canGoDown)
                {
                    if (random.Next(2) == 0)
                    {
                        Grid [y, x + 1] = ' ';
                    }
                    else
                    {
                        Grid [y + 1, x] = ' ';
                    }
                }
                else if (canGoRight)
                {
                    Grid [y, x + 1] = ' ';
                }
                else if (canGoDown)
                {
                    Grid [y + 1, x] = ' ';
                }
            }
        }
    }

    public void Print()
    {
        if (Grid == null)
        {
            throw new Exception("Grid not initialized");
        }

        for (var y = 0; y < GridHeight; y++)
        {
            for (var x = 0; x < GridWidth; x++)
            {
                Console.Write(Grid[y, x] + " ");
            }
            Console.WriteLine();
        }
    }

    private void FillGrid()
    {
        Grid = new char[GridHeight, GridWidth];
        for (var y = 0; y < GridHeight; y++)
        {
            for (var x = 0; x < GridWidth; x++)
            {
                Grid[y, x] = '▤';
            }
        }
    }
}
