using System.Collections.Generic;
using System.Text;

namespace WpfApp_Calculator
{
    public static class CalculatorHelper
    {
        public static string CalculateSameCommands(List<long> numbers, StringBuilder commands)
        {
            long sum = 0;
            sum += numbers[0];

            for (int i = 0; i < commands.Length; i++)
            {
                if (commands[i] == '+')
                    sum += numbers[i + 1];
                else if (commands[i] == '-')
                    sum -= numbers[i + 1];
                else if (commands[i] == '*')
                    sum *= numbers[i + 1];
                else
                    sum /= numbers[i + 1];
            }

            return sum.ToString();
        }

        public static void CalculateDivideAndMultiply(ref List<long> numbers, ref StringBuilder commands)
        {

            for (int i = 0; i < commands.Length; i++)
            {
                if (commands[i] == '/')
                {
                    numbers[i] /= numbers[i + 1];
                    numbers[i + 1] = numbers[i];
                    numbers[i] = 1;
                    commands[i] = '*';
                }
            }

            for (int i = 0; i < commands.Length; i++)
            {
                if (commands[i] == '*' && numbers[i + 1] != 1)
                {
                    numbers[i] *= numbers[i + 1];
                    numbers[i + 1] = 1;
                    i = 0;
                }
            }

        }

    }

}
