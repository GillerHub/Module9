using System;
using System.Collections;

namespace Module_9
{
    public class ReverseComparer : IComparer
    {
        public int Compare(Object x, Object y)
        {
            return (new CaseInsensitiveComparer()).Compare(y, x);
        }
    }

    class MyException : Exception
    {
        public MyException()
        { }
        public MyException(string message)
        : base(message)
        { }
    }
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Задан массив:");
            string[] LastNames = { "Напалов", "Заринин", "Вопчук", "Чужикин", "Теплов" };
            for (int i = 0; i < LastNames.Length; i++)
                Console.WriteLine("N[{0}] = {1}", i, LastNames[i]);

            NumberReader numberReader = new NumberReader();
            numberReader.NumberEnteredEvent += ShowNumber;

            try
            {
                numberReader.Read();
                if (numberReader.number == 1)
                {
                    Array.Sort(LastNames);
                    Console.WriteLine("Сортировка массива от А до Я:");
                    DisplayValues(LastNames);
                }
                else
                {
                    IComparer revComparer = new ReverseComparer();
                    Array.Sort(LastNames, revComparer);
                    Console.WriteLine("Сортировка массива от Я до А:");
                    DisplayValues(LastNames);
                }
            }

            catch (FormatException)
            {
                Console.WriteLine("Введено некорректное значение");
            }
            try
            {
                //Console.WriteLine("Блок try запущен");
                throw new MyException("Сообщение об ошибке");
            }
            catch (MyException ex)
            {
                Console.WriteLine("Сработало моё исключение");
                Console.WriteLine(ex.Message);
            }

        }

        static void ShowNumber(int number)
        {
            switch (number)
            {
                case 1:
                    Console.WriteLine("Введено значение 1");
                    break;
                case 2:
                    Console.WriteLine("Введено значение 2");
                    break;
            }
        }

        public static void DisplayValues(String[] arr)
        {
            for (int i = arr.GetLowerBound(0); i <= arr.GetUpperBound(0);
            i++)
            {
                Console.WriteLine("[{0}] : {1}", i, arr[i]);
            }
            Console.WriteLine();
        }

    }
    class NumberReader
    {
        public delegate void NumberEnteredDelegate(int number);
        public event NumberEnteredDelegate NumberEnteredEvent;
        public int number;
        public void Read()
        {
            Console.WriteLine();
            Console.WriteLine("Введите значение 1 или 2");

            number = Convert.ToInt32(Console.ReadLine());

            if (number != 1 && number != 2) throw new FormatException();

            NumberEntered(number);
        }

        protected virtual void NumberEntered(int number)
        {
            NumberEnteredEvent?.Invoke(number);
        }
    }
}