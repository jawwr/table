using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
 
namespace Table
{
    class Program
    {
 
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int m = int.Parse(Console.ReadLine());
            int[] value = GenerateRandomValue(n, m);
            int width = GetMaxWidth(value);

            foreach (var str in GenerateTable(m,n,value,width))
            {
                Console.WriteLine(str);
            }
            
            Console.Read();
        }

        static int[] GenerateRandomValue(int n, int m)
        {
            Random random = new Random();
            int[] value = new int[n * m];
            for (int i = 0; i < (n * m); i++)
            {
                value[i] = random.Next(-100, 100);
            }

            return value;
        }
        
        static int GetMaxWidth(int[] value)
        {
            int width = 0;
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i].ToString().Length > width)
                    width = (value[i].ToString().Length);
            }

            return width;
        }
 
        static string PrintSymbols(char symbol, int width) {
            string result = string.Empty;
            for (int i = 0; i < width; i++) 
                result += symbol;
            return result;
        }
 
        static string PrintValue(int word, int width) {
            int count = width - word.ToString().Length;
            string str = string.Empty;
            if (count == 1)
                str = $"{word} ";
            else if (count == 2)
                str = $"{word}  ";
            else
                str = $"{word}";
            return str;
        }

        static string[] GenerateTable(int width, int height, int[] values, int widthmax)
        {
            string[] strings = new string[(height) + 2 + (height - 1)];
            strings[0] = "╔";
            for (int i = 0; i < width; i++)
                strings[0] += i != width - 1 ? $"{PrintSymbols('═', widthmax)}╦" : $"{PrintSymbols('═', widthmax)}";
            strings[0] += "╗";
            int c = 0;
            for (int i = 1; i <= height + (height - 1); i++)
            {
                if (i % 2 == 0)
                {
                    strings[i] = "╠";
                    for(int j = 0; j < width; j++)
                        strings[i] += j != width - 1 ? $"{PrintSymbols('═', widthmax)}╬" : $"{PrintSymbols('═', widthmax)}";
                    strings[i] += "╣";
                }
                else
                {
                    strings[i] = "║";
                    for (int j = 0; j < width; j++)
                    {
                        strings[i] += j != width - 1 ?$"{PrintValue(values[c],widthmax)}║" : $"{PrintValue(values[c],widthmax)}";
                        c++;
                    }
                    strings[i] += "║";
                }
            }
            strings[strings.Length - 1] = "╚";
            for (int i = 0; i < width; i++)
                strings[strings.Length - 1] += i != width - 1 ? $"{PrintSymbols('═', widthmax)}╩" : $"{PrintSymbols('═', widthmax)}";
            strings[strings.Length - 1] += "╝";
            return strings;
        }
    }
}