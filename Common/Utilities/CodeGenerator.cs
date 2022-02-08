using System;

namespace Common.Utilities
{
    public static class CodeGenerator
    {
        public static string Generate(int digit = 4)
        {
            string min = "1";
            string max = "10";
            for (int i = 0; i < digit - 1; i++)
            {
                min = min + "0";
                max = max + "0";
            }
            return new Random().Next(int.Parse(min), int.Parse(max)).ToString();
        }
    }
}
