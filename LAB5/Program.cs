using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;

namespace ZooshopApp
{
    class Program
    {
        private static string file = @"C:\Users\Pavel\repos\Labs\LAB5\LR5-var2.xlsx";
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в приложение 'Зоомагазин'!");
            Logger.Initialize();

            List<Animal> animals = new List<Animal>();
            List<Customer> customers = new List<Customer>();
            List<Sale> sales = new List<Sale>();

            Database database = new Database(file, animals, customers, sales);
            Logger.Log("Создание новой базы данных");

            animals = database.LoadAnimals();
            customers = database.LoadCustomers();
            sales = database.LoadSales();
            database = new Database(file, animals, customers, sales);
            Logger.Log("Значение импортированно из эксел файла в базу данных");

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1. Просмотр базы данных о животных");
                Console.WriteLine("2. Просмотр базы данных о покупателях");
                Console.WriteLine("3. Просмотр базы данных о продажах");
                Console.WriteLine("4. Удаление элемента");
                Console.WriteLine("5. Корректировка элемента");
                Console.WriteLine("6. Добавление элемента");
                Console.WriteLine("7. Выполнение запросов");
                Console.WriteLine("8. Выход");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        database.ShowAnimals();
                        break;
                    case "2":
                        database.ShowCustomers();
                        break;
                    case "3":
                        database.ShowSales();
                        break;
                    case "4":
                        database.DeleteElement();
                        break;
                    case "5":
                        database.EditElement();
                        break;
                    case "6":
                        database.AddElement();
                        break;
                    case "7":
                        database.ExecuteQueries();
                        break;
                    case "8":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }

            Console.WriteLine("Программа завершена.");
        }
    }
}
