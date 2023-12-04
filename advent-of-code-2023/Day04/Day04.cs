using advent_of_code_2023.aocBase;

namespace advent_of_code_2023.Day04
{
    public class Day04 : Base
    {
        protected override void Start(bool isTestInput)
        {
            Part1AndPart2(isTestInput, PartEnum.Part1);
            Part1AndPart2(isTestInput, PartEnum.Part2);
        }

        private void Part1AndPart2(bool isTestInput, PartEnum part)
        {
            String[] lines = ReadAllLines(isTestInput);

            List<int> cardsCount = new List<int>();
            int copyIndex = 1;
            int result = 0;

            foreach (string line in lines)
            {
                string[] cardContent = line.Split(": ");

                string[] numbers = cardContent[1].Split(" | ");

                string[] winningNumbersStringArray = numbers[0].Split(' ');
                string[] myNumbersStringArray = numbers[1].Split(' ');

                List<int> winningNumbers = new List<int>();
                foreach (string number in winningNumbersStringArray)
                {
                    if (string.IsNullOrWhiteSpace(number)) continue;
                    winningNumbers.Add(int.Parse(number));
                }

                List<int> myNumbers = new List<int>();
                foreach (string number in myNumbersStringArray)
                {
                    if (string.IsNullOrWhiteSpace(number)) continue;
                    myNumbers.Add(int.Parse(number));
                }

                IEnumerable<int> nums = winningNumbers.Intersect(myNumbers);

                if (part == PartEnum.Part1)
                {
                    int numberCount = nums.Count();

                    if (numberCount == 1) result = result + numberCount;
                    else if (numberCount > 1) result = result + (int)Math.Pow(2, numberCount - 1);
                }
                else if (part == PartEnum.Part2)
                {
                    if (cardsCount.Count < copyIndex) cardsCount.Add(1);
                    else cardsCount[copyIndex - 1]++;

                    int intersectCount = nums.Count();
                    int currentCardCopies = cardsCount[copyIndex - 1];

                    for (int cardNumber = copyIndex + 1; cardNumber <= copyIndex + intersectCount; cardNumber++)
                    {
                        if (cardNumber > cardsCount.Count) cardsCount.Add(currentCardCopies);
                        else cardsCount[cardNumber - 1] += currentCardCopies;
                    }

                    copyIndex++;
                }
            }

            if (part == PartEnum.Part2) result = cardsCount.Sum();
            
            Console.WriteLine(result);
        }
    }
}
