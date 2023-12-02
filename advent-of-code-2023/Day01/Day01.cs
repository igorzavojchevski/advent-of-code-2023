using advent_of_code_2023.aocBase;
using System.Text;

namespace advent_of_code_2023.Day01
{
    public class Day01 : Base
    {
        protected override void Start(bool isTestInput)
        {
            Part1(isTestInput);
            Part2(isTestInput);
        }

        private void Part1(bool isTestInput = false)
        {
            String[] lines = ReadAllLines(isTestInput);

            int sum = 0;

            foreach (string line in lines)
            {
                int i = 0;
                int j = line.Length - 1;
                while (i <= j)
                {
                    if (char.IsDigit(line[i]) && char.IsDigit(line[j]))
                    {
                        sum += int.Parse($"{line[i]}{line[j]}");
                        break;
                    }

                    if (!char.IsDigit(line[i])) i++;
                    if (!char.IsDigit(line[j])) j--;
                }
            }

            Console.WriteLine(sum);
        }

        private void Part2(bool isTestInput = false)
        {
            String[] lines = ReadAllLines(isTestInput);

            char[] repeating = new char[] { 'o', 't', 'f', 's', 'e', 'n' };
            //oneight

            Stack<int> stack = new();
            int sum = 0;
            foreach (string line in lines)
            {
                int i = 0;

                while (i < line.Length)
                {
                    int iterator = i;
                    if (line[i].Equals('o') && i + 2 < line.Length && line.Substring(i, 3).Equals("one"))
                    {
                        stack.Push(1);
                        i = GetNewIndex(repeating, line, i, 3);
                    }
                    else if (line[i].Equals('t'))
                    {
                        if (i + 2 < line.Length && line.Substring(i, 3).Equals("two"))
                        {
                            stack.Push(2);
                            i = GetNewIndex(repeating, line, i, 3);
                        }
                        else if (i + 4 < line.Length && line.Substring(i, 5).Equals("three"))
                        {
                            stack.Push(3);
                            i = GetNewIndex(repeating, line, i, 5);
                        }
                    }
                    else if (line[i].Equals('f'))
                    {
                        if (i + 3 < line.Length && line.Substring(i, 4).Equals("four"))
                        {
                            stack.Push(4);
                            i = GetNewIndex(repeating, line, i, 4);
                        }
                        else if (i + 3 < line.Length && line.Substring(i, 4).Equals("five"))
                        {
                            stack.Push(5);
                            i = GetNewIndex(repeating, line, i, 4);
                        }
                    }
                    else if (line[i].Equals('s'))
                    {
                        if (i + 2 < line.Length && line.Substring(i, 3).Equals("six"))
                        {
                            stack.Push(6);
                            i = GetNewIndex(repeating, line, i, 3);
                        }
                        else if (i + 4 < line.Length && line.Substring(i, 5).Equals("seven"))
                        {
                            stack.Push(7);
                            i = GetNewIndex(repeating, line, i, 5);
                        }
                    }
                    else if (line[i].Equals('e'))
                    {
                        if (i + 4 < line.Length && line.Substring(i, 5).Equals("eight"))
                        {
                            stack.Push(8);
                            i = GetNewIndex(repeating, line, i, 5);
                        }
                    }
                    else if (line[i].Equals('n'))
                    {
                        if (i + 3 < line.Length && line.Substring(i, 4).Equals("nine"))
                        {
                            stack.Push(9);
                            i = GetNewIndex(repeating, line, i, 4);
                        }
                    }
                    else if (char.IsDigit(line[i]))
                    {
                        stack.Push(int.Parse(line[i].ToString()));
                    }

                    if (iterator == i) i++;
                }

                StringBuilder sb = new();
                while (stack.Count > 0)
                    sb.Append(stack.Pop());

                string wordNumber = sb.ToString();

                wordNumber = wordNumber.Length >= 2 ? $"{wordNumber[^1]}{wordNumber[0]}" : $"{wordNumber}{wordNumber}";

                sum = sum + int.Parse(wordNumber);
            }

            Console.WriteLine(sum);
        }

        private int GetNewIndex(char[] repeating, string line, int i, int potentialIndexSpaces)
        {
            i += potentialIndexSpaces;
            if (i - 1 < line.Length && repeating.Contains(line[i - 1])) i--;
            return i;
        }
    }
}
