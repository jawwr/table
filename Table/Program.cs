using System;
 
namespace Table
{
    class Program
    {
        static void Main(string[] args)
        {
            //защита от дурака
            int n = 0;
            do
            {
                try
                { 
                    Console.WriteLine("Введите количество строк");
                    n = int.Parse(Console.ReadLine());
                    if (n <= 0)
                        throw new Exception("Количево строк не допустимо");
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    n = int.Parse(Console.ReadLine());
                }
                
            } while (true);
            
            //защита от дурака
            int m = 0;
            do
            {
                try
                { 
                    Console.WriteLine("Введите количество столбцов");
                    m = int.Parse(Console.ReadLine());
                    if (m <= 0)
                        throw new Exception("Количево столбцов не допустимо");
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    m = int.Parse(Console.ReadLine());
                }
            } while (true);
            
            int[] value = GenerateRandomValue(n, m);//генерируется массив рандомных чисел
            int width = GetMaxWidth(value);//генерируется максимальная ширина ячейки в зависимости от максимального количества символов от сгенерированных чисел

            foreach (var str in GenerateTable(m,n,value,width))
            {
                Console.WriteLine(str);//отрисовка таблицы
            }
            
            Console.Read();
        }

        static int[] GenerateRandomValue(int n, int m)//генератор рандомных чисел
        {
            Random random = new Random();
            int[] value = new int[n * m];
            for (int i = 0; i < (n * m); i++)
            {
                value[i] = random.Next(-100, 100);
            }

            return value;
        }
        
        static int GetMaxWidth(int[] value)//получение максимальной ширины таблицы
        {
            int width = 0;
            for (int i = 0; i < value.Length; i++)
            {
                if (value[i].ToString().Length > width)
                    width = (value[i].ToString().Length);
            }

            return width;
        }
 
        static string PrintSymbols(char symbol, int width)//генерация строчных символов для таблицы
        {
            string result = string.Empty;
            for (int i = 0; i < width; i++) 
                result += symbol;
            return result;
        }
 
        static string PrintValue(int word, int width)//генерация значения в ячейке
        {
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

        static string[] GenerateTable(int width, int height, int[] values, int widthmax)//генерация самой таблицы
        {
            string[] strings = new string[(height) + 2 + (height - 1)];
            
            strings[0] = "╔";//генерация открывающей строки
            for (int i = 0; i < width; i++)
                strings[0] += i != width - 1 ? $"{PrintSymbols('═', widthmax)}╦" : $"{PrintSymbols('═', widthmax)}";
            strings[0] += "╗";
            
            int c = 0;
            for (int i = 1; i <= height + (height - 1); i++)
            {
                if (i % 2 == 0)//генерация граней ячеек
                {
                    strings[i] = "╠";
                    for(int j = 0; j < width; j++)
                        strings[i] += j != width - 1 ? $"{PrintSymbols('═', widthmax)}╬" : $"{PrintSymbols('═', widthmax)}";
                    strings[i] += "╣";
                }
                else
                {
                    //генерация самой ячейки со значением
                    strings[i] = "║";
                    for (int j = 0; j < width; j++)
                    {
                        strings[i] += j != width - 1 ?$"{PrintValue(values[c],widthmax)}║" : $"{PrintValue(values[c],widthmax)}";
                        c++;
                    }
                    strings[i] += "║";
                }
            }
            
            strings[strings.Length - 1] = "╚";//генерация закрывающей строки
            for (int i = 0; i < width; i++)
                strings[strings.Length - 1] += i != width - 1 ? $"{PrintSymbols('═', widthmax)}╩" : $"{PrintSymbols('═', widthmax)}";
            strings[strings.Length - 1] += "╝";
            
            return strings;
        }
    }
}