using advent_of_code_2023.aocBase;

namespace advent_of_code_2023.Day02
{
    public class Day02 : Base
    {
        protected override void Start(bool isTestInput)
        {
            Part1(isTestInput);
            Part2(isTestInput);
        }

        private void Part1(bool isTestInput)
        {
            String[] lines = ReadAllLines(isTestInput);

            int possibleGameIDsSum = 0;
            foreach (string line in lines)
            {
                //Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
                String[] content = line.Split(":");

                String gameID = content[0].Split(" ")[1];
                String[] gameDetails = content[1].Split(";");

                int maxRedCubes = 12;
                int maxGreenCubes = 13;
                int maxBlueCubes = 14;

                bool possible = true;

                foreach (string gameDetail in gameDetails)
                {
                    String[] typeDetails = gameDetail.Split(",");

                    foreach (string typeDetail in typeDetails)
                    {
                        String[] typeDetailInfo = typeDetail.TrimStart(' ').Split(' ');
                        int number = int.Parse(typeDetailInfo[0]);
                        if (typeDetailInfo[1].Equals("red") && maxRedCubes - number < 0) possible = false;
                        else if (typeDetailInfo[1].Equals("green") && maxGreenCubes - number < 0) possible = false;
                        else if (typeDetailInfo[1].Equals("blue") && maxBlueCubes - number < 0) possible = false;

                        if (possible == false) break;
                    }
                    if (possible == false) break;
                }
                if (possible == false) continue;

                possibleGameIDsSum += int.Parse(gameID);
            }
            Console.WriteLine(possibleGameIDsSum);
        }

        private void Part2(bool isTestInput)
        {
            String[] lines = ReadAllLines(isTestInput);

            int possiblePower = 0;
            foreach (string line in lines)
            {
                //Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
                String[] content = line.Split(":");

                String gameID = content[0].Split(" ")[1];
                String[] gameDetails = content[1].Split(";");

                int power = 1;
                int redCubesMAX = -1;
                int greenCubesMAX = -1;
                int blueCubesMAX = -1;

                foreach (string gameDetail in gameDetails)
                {
                    String[] typeDetails = gameDetail.Split(",");

                    foreach (string typeDetail in typeDetails)
                    {
                        String[] typeDetailInfo = typeDetail.TrimStart(' ').Split(' ');
                        int number = int.Parse(typeDetailInfo[0]);
                        if (typeDetailInfo[1].Equals("red") && number > redCubesMAX) redCubesMAX = number;
                        else if (typeDetailInfo[1].Equals("green") && number > greenCubesMAX) greenCubesMAX = number;
                        else if (typeDetailInfo[1].Equals("blue") && number > blueCubesMAX) blueCubesMAX = number;
                    }
                }
                power = redCubesMAX * greenCubesMAX * blueCubesMAX;

                possiblePower += power;
            }
            Console.WriteLine(possiblePower);
        }
    }
}
