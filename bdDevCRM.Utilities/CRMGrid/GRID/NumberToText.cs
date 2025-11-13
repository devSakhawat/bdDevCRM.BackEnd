using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Entities.CRMGrid.GRID
{
    public class NumberToText
    {
        /// <summary>
        /// Translate Amount(Number) to words 
        /// </summary>
        /// <param name="number">Number</param>
        /// <returns>Translated Word</returns>
        public static string GetWordOfNumber(long number)
        {
            try
            {
                var wordNumber = new StringBuilder();
                string[] tens;
                string[] ones;
                var powers = GetWords(out tens, out ones);

                if (number == 0 || number < 0)
                {
                    return "0";
                }
                if (number < 0)
                {
                    wordNumber.Append("Negative ");
                    number = -number;
                }

                var groupedNumber = GroupedNumber(number);
                for (int i = 3; i >= 0; i--)
                {
                    long group = groupedNumber[i];
                    GetWordForValue(@group, wordNumber, ones, i, powers, tens);
                }

                return wordNumber.ToString().Trim();
            }
            catch (Exception)
            {
                throw new Exception("Error in Creating Text ! Try again");
            }
        }

        private static void GetWordForValue(long @group, StringBuilder wordNumber, string[] ones, int i, string[] powers,
            string[] tens)
        {
            try
            {
                if (@group >= 100)
                {
                    wordNumber.Append(ones[@group/100 - 1] + " Hundred ");
                    @group %= 100;

                    if (@group == 0 && i > 0)
                        wordNumber.Append(powers[i - 1]);
                }

                if (@group >= 20)
                {
                    if (@group%10 != 0)
                        wordNumber.Append(tens[@group/10 - 2] + " " + ones[@group%10 - 1] + " ");
                    else
                        wordNumber.Append(tens[@group/10 - 2] + " ");
                }
                else if (@group > 0)
                    wordNumber.Append(ones[@group - 1] + " ");

                if (@group != 0 && i > 0)
                    wordNumber.Append(powers[i - 1]);
            }
            catch (Exception)
            {
                throw new Exception("Error, Getting Text of Number!");
            }
        }

        private static long[] GroupedNumber(long number)
        {
            try
            {
                var groupedNumber = new long[] {0, 0, 0, 0};
                int groupIndex = 0;

                while (number > 0)
                {
                    groupedNumber[groupIndex++] = number%1000;
                    number /= 1000;
                }
                return groupedNumber;
            }
            catch (Exception)
            {
                throw new Exception("Error, Grouping Number!");
            }
        }

        private static string[] GetWords(out string[] tens, out string[] ones)
        {
            try
            {
                var powers = new string[] {"Thousand ", "Million ", "Billion "};
                tens = new string[] {"Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"};
                ones = new string[]
                {
                    "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
                    "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen",
                    "Nineteen"
                };
                return powers;
            }
            catch (Exception ex)
            {
                throw new Exception("Error, Gettings words!");
            }
        }
    }
}