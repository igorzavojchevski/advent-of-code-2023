using advent_of_code_2023.aocBase;
using System.Text;

namespace advent_of_code_2023.Day03
{
    public class Day03 : Base
    {
        protected override void Start(bool isTestInput)
        {
            Part1AndPart2(isTestInput, PartEnum.Part1);
            Part1AndPart2(isTestInput, PartEnum.Part2);
        }

        private void Part1AndPart2(bool isTestInput, PartEnum part)
        {
            String[] lines = ReadAllLines(isTestInput);

            char[][] grid = new char[lines.Length][];
            HashSet<char> symbols = new() { '+', '#', '*', '$', '.' };

            for (int i = 0; i < lines.Length; i++)
                grid[i] = lines[i].ToCharArray();

            int m = grid.Length;
            int n = grid[0].Length;

            int[][] directions = new int[][]
            {
                new int[] { 0, 1 },
                new int[] { 0, -1 },
                new int[] { 1, 0 },
                new int[] { -1, 0 },
                new int[] { 1, -1 },
                new int[] { 1, 1 },
                new int[] { -1, -1 },
                new int[] { -1, 1 },
            };

            int[][] numberDirections = new int[][]
            {
                new int[] { 0, 1 },
                new int[] { 0, -1 },
            };

            int result = 0;

            for (int r = 0; r < m; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    result = result + DFS(part, grid, r, c, directions, numberDirections, symbols);
                }
            }

            Console.WriteLine(result);
        }

        private int DFS(PartEnum part, char[][] grid, int r, int c, int[][] directions, int[][] numberDirections, HashSet<char> symbols)
        {
            int m = grid.Length;
            int n = grid[0].Length;

            bool[,] visited = new bool[m, n];

            int result = part == PartEnum.Part1 ? 0 : 1; 
            int adjacentNumberCounter = 0;

            Stack<(int, int)> stack = new Stack<(int, int)>();
            stack.Push((r, c));

            while (stack.Count != 0)
            {
                var (row, column) = stack.Pop();

                if (visited[row, column]) continue;

                visited[row, column] = true;

                char currentChar = grid[row][column];

                if (char.IsDigit(currentChar) || currentChar == '.') continue;

                foreach (int[] dir in directions)
                {
                    int newRow = row + dir[0];
                    int newColumn = column + dir[1];

                    if (visited[newRow, newColumn]) continue;

                    if (newRow < 0 || newColumn < 0 || newRow >= m || newColumn >= n || symbols.Contains(grid[newRow][newColumn]))
                        continue;

                    if (part == PartEnum.Part1) result = result + BuildNumber(grid, newRow, newColumn, numberDirections, symbols, visited);
                    else if (part == PartEnum.Part2)
                    {
                        adjacentNumberCounter++;
                        result = result * BuildNumber(grid, newRow, newColumn, numberDirections, symbols, visited);
                    }

                    stack.Push((newRow, newColumn));
                }
            }

            if (part == PartEnum.Part2 && adjacentNumberCounter < 2) result = 0;

            return result;
        }

        private int BuildNumber(char[][] grid, int newRow, int newColumn, int[][] numberDirections, HashSet<char> symbols, bool[,] visited)
        {
            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue((newRow, newColumn));
            StringBuilder sb = new StringBuilder();
            sb.Append(grid[newRow][newColumn]);

            while (queue.Count != 0)
            {
                var (row, column) = queue.Dequeue();

                visited[row, column] = true;

                foreach (int[] dir in numberDirections)
                {
                    int newColumnNumber = column + dir[1];

                    if (newColumnNumber < 0 || newColumnNumber >= grid[newColumn].Length || !char.IsDigit(grid[newRow][newColumnNumber]) || symbols.Contains(grid[newRow][newColumnNumber]))
                        continue;

                    if (visited[row, newColumnNumber]) continue;

                    if (newColumnNumber < column) sb.Insert(0, grid[newRow][newColumnNumber]);
                    else if (newColumnNumber > column) sb.Append(grid[newRow][newColumnNumber]);

                    queue.Enqueue((row, newColumnNumber));
                }
            }

            return int.Parse(sb.ToString());
        }
    }
}
