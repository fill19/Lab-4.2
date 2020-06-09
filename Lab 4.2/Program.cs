using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_4._2
{
    class InvalidBookDataException : Exception
    {
        public InvalidBookDataException(string message) : base(message) { }
    }
    class Book
    {
        public string Author;
        public string Title;
        public string Publisher;
        public uint Year;
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Read data
            Console.WriteLine("Введіть дані про книги: автор|назва|видавництво|рік:");
            List<Book> books = new List<Book>();
            while (true)
            {
                string data = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(data)) break;

                string[] parsedData = data.Split('|');
                if (parsedData.Length != 4)
                    throw new InvalidBookDataException($"Неправильний формат вводу: не вистачає стовпчиків");
                uint year;
                if (!uint.TryParse(parsedData[3], out year))
                    throw new InvalidBookDataException($"Неправильний формат вводу: неправильно вказаний рік");

                Book book = new Book()
                {
                    Author = parsedData[0],
                    Title = parsedData[1],
                    Publisher = parsedData[2],
                    Year = year,
                };
                books.Add(book);
            }

            // Sort data
            books = books.OrderBy(book => book.Author).ToList();

            // Print data
            Console.WriteLine();
            Console.WriteLine($"{"Автор",10} {"Назва",10} {"Видавництво",10} {"Рік",10}");
            foreach (Book book in books)
            {
                Console.WriteLine($"{book.Author,10} {book.Title,10} {book.Publisher,10} {book.Year,10}");
            }
            Console.WriteLine();

            // Print how many books from each publisher are there
            Dictionary<string, uint> countByPublisher = new Dictionary<string, uint>();
            foreach (Book book in books)
            {
                if (!countByPublisher.ContainsKey(book.Publisher)) countByPublisher[book.Publisher] = 0;
                countByPublisher[book.Publisher]++;
            }

            foreach (KeyValuePair<string, uint> entry in countByPublisher)
            {
                if (entry.Value == 1)
                {
                    Console.WriteLine($"Тут є  {entry.Value} книг від видавництва {entry.Key} ");
                }
                else
                {
                    Console.WriteLine($"Тут є  {entry.Value} книг від видавництва {entry.Key}");
                }
            }

            Console.ReadKey();
        }
    }
}
