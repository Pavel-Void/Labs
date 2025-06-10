/// <file>
/// Содержит класс Database для управления данными зоомагазина.
/// </file>
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ZooshopApp
{
    /// <summary>
    /// Предоставляет методы для управления и запросов к данным зоомагазина.
    /// </summary>
    public class Database
    {
        private string _file;
        private List<Animal> _animals;
        private List<Customer> _customers;
        private List<Sale> _sales;

        /// <summary>
        /// Initializes a new instance of the <see cref="Database"/> class.
        /// </summary>
        public Database(string file, List<Animal> animals, List<Customer> customers, List<Sale> sales)
        {
            _file = file;
            _animals = animals;
            _customers = customers;
            _sales = sales;
        }

        /// <summary>
        /// Загружает животных из Excel-файла.
        /// </summary>
        public List<Animal> LoadAnimals()
        {
            Logger.Log("Загрузка данных из файла Excel.");

            var animals = new List<Animal>();
            using (var package = new ExcelPackage(new FileInfo(_file)))
            {
                var worksheet = package.Workbook.Worksheets["Животные"];
                if (worksheet == null) return animals;

                int row = 2;
                while (worksheet.Cells[row, 1].Value != null)
                {
                    int id = int.Parse(worksheet.Cells[row, 1].Value.ToString());
                    string species = worksheet.Cells[row, 2].Value.ToString();
                    string breed = worksheet.Cells[row, 3].Value.ToString();
                    animals.Add(new Animal(id, species, breed));
                    Logger.Log($"Загружено животных: id {id}, species {species}, breed {breed}");
                    row++;
                }
                return animals;
            }
        }

        /// <summary>
        /// Загружает покупателей из Excel-файла.
        /// </summary>
        public List<Customer> LoadCustomers()
        {
            var customers = new List<Customer>();
            using (var package = new ExcelPackage(new FileInfo(_file)))
            {
                var worksheet = package.Workbook.Worksheets["Покупатели"];
                if (worksheet == null) return customers;

                int row = 2;
                while (worksheet.Cells[row, 1].Value != null)
                {
                    int id = int.Parse(worksheet.Cells[row, 1].Value.ToString());
                    string name = worksheet.Cells[row, 2].Value.ToString();
                    int age = int.Parse(worksheet.Cells[row, 3].Value.ToString());
                    string address = worksheet.Cells[row, 4].Value.ToString();
                    customers.Add(new Customer(id, name, age, address));
                    Logger.Log($"Добавлены покупатели: id {id}, name {name}, age {age}, address {address}");
                    row++;
                }
            }
            return customers;
        }

        /// <summary>
        /// Загружает продажи из Excel-файла.
        /// </summary>
        public List<Sale> LoadSales()
        {
            var sales = new List<Sale>();
            using (var package = new ExcelPackage(new FileInfo(_file)))
            {
                var worksheet = package.Workbook.Worksheets["Продажи"];
                if (worksheet == null) return sales;

                int row = 2;
                while (worksheet.Cells[row, 1].Value != null)
                {
                    int id = int.Parse(worksheet.Cells[row, 1].Value.ToString());
                    int id_animal = int.Parse(worksheet.Cells[row, 2].Value.ToString());
                    int id_customer = int.Parse(worksheet.Cells[row, 3].Value.ToString());
                    DateTime date = DateTime.Parse(worksheet.Cells[row, 4].Value.ToString());
                    decimal price = decimal.Parse(worksheet.Cells[row, 5].Value.ToString());
                    sales.Add(new Sale(id, id_animal, id_customer, date, price));
                    Logger.Log($"Добавлены продажи: id {id}, id_animal {id_animal}, id_customer {id_customer}, date {date}, price {price}");
                    row++;
                }
                return sales;
            }
        }

        /// <summary>
        /// Отображает всех животных.
        /// </summary>
        public void ShowAnimals() =>
            _animals.ForEach(animal => Console.WriteLine($"{animal}\n{new string('_', 50)}"));

        /// <summary>
        /// Отображает всех покупателей.
        /// </summary>
        public void ShowCustomers() =>
            _customers.ForEach(customer => Console.WriteLine($"{customer}\n{new string('_', 50)}"));

        /// <summary>
        /// Отображает все продажи.
        /// </summary>
        public void ShowSales() =>
            _sales.ForEach(sale => Console.WriteLine($"{sale}\n{new string('_', 50)}"));

        /// <summary>
        /// Удаляет элемент из базы данных на основе ввода пользователя.
        /// </summary>
        public void DeleteElement()
        {
            Console.WriteLine(new string('_', 100));
            Console.Write("Выбери таблицу: 1 - Животные, 2 - Покупатели, 3 - Продажи: ");
            string k = Console.ReadLine();

            Console.Write("Enter ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int idToDelete) || idToDelete < 0)
            {
                Console.WriteLine("Неверный ID.");
                return;
            }

            switch (k)
            {
                case "1":
                    if (!_animals.Any(a => a.GetID() == idToDelete))
                    {
                        Console.WriteLine("Животное с таким ID не найдено.");
                        return;
                    }

                    _animals = _animals.Where(animal => animal.GetID() != idToDelete).ToList();
                    _sales = _sales.Where(sale => sale.GetIdAnimals() != idToDelete).ToList();

                    Logger.Log($"Removed animal with id: {idToDelete} and all connected sales");
                    break;

                case "2":
                    if (!_customers.Any(c => c.GetID() == idToDelete))
                    {
                        Console.WriteLine("Покупатель с таким ID не найден.");
                        return;
                    }

                    _customers = _customers.Where(customer => customer.GetID() != idToDelete).ToList();
                    _sales = _sales.Where(sale => sale.GetIdCustomer() != idToDelete).ToList();

                    Logger.Log($"Удалён покупатель с id: {idToDelete} и информация об его покупках");
                    break;

                case "3":
                    if (!_sales.Any(s => s.GetID() == idToDelete))
                    {
                        Console.WriteLine("Продажа с таким ID не найдена.");
                        return;
                    }

                    _sales = _sales.Where(sale => sale.GetID() != idToDelete).ToList();

                    Logger.Log($"Удалена продажа с id: {idToDelete}");
                    break;

                default:
                    Console.WriteLine("Неправильный номер листа");
                    Logger.Log("Неправильный номер листа");
                    break;
            }
        }

        /// <summary>
        /// Считывает целое число с клавиатуры с проверкой.
        /// </summary>
        public int Input()
        {
            Logger.Log("Начало ввода");
            string line = Console.ReadLine();
            if (!int.TryParse(line, out int input) || input < 0)
            {
                Console.Write("Значение должно быть неотрицательным числом: ");
                Logger.Log("Неверное значение");
                return Input();
            }
            Logger.Log("Конец ввода");
            return input;
        }

        /// <summary>
        /// Редактирует элемент в базе данных на основе ввода пользователя.
        /// </summary>
        public void EditElement()
        {
            Console.WriteLine(new string('_', 100));
            Console.Write("Выберите таблицу: 1 - Животные, 2 - Покупатели, 3 - Продажи: ");
            string k = Console.ReadLine();

            Console.Write("Введите id для изменений: ");
            int idToEdit = Input();

            switch (k)
            {
                case "1":
                    if (!_animals.Any(c => c.GetID() == idToEdit))
                    {
                        Console.WriteLine($"Не могу найти ID: {idToEdit} в таблице с животными");
                        return;
                    }

                    Console.Write("Введите новый вид: ");
                    string newSpecies = Console.ReadLine();
                    Console.Write("Введите новую породу: ");
                    string newBreed = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(newSpecies) || string.IsNullOrWhiteSpace(newBreed))
                    {
                        Console.WriteLine("Вид и порода не могут быть пустыми.");
                        return;
                    }
                    _animals[_animals.FindIndex(c => c.GetID() == idToEdit)] = new Animal(idToEdit, newSpecies, newBreed);
                    Logger.Log($"У животного: id {idToEdit}, новый вид {newSpecies} и новая порода {newBreed}");
                    break;

                case "2":
                    if (!_customers.Any(c => c.GetID() == idToEdit))
                    {
                        Console.WriteLine($"Не могу найти ID: {idToEdit} в таблице покупателей");
                        return;
                    }

                    Console.Write("Введите новое имя: ");
                    string newName = Console.ReadLine();
                    Console.Write("Введите новый возраст: ");
                    if (!int.TryParse(Console.ReadLine(), out int newAge) || newAge < 0)
                    {
                        Console.WriteLine("Возраст должен быть неотрицательным числом.");
                        return;
                    }
                    Console.Write("Введите новый адрес: ");
                    string newAddress = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(newName) || string.IsNullOrWhiteSpace(newAddress))
                    {
                        Console.WriteLine("Имя и адрес не могут быть пустыми.");
                        return;
                    }
                    _customers[_customers.FindIndex(c => c.GetID() == idToEdit)] = new Customer(idToEdit, newName, newAge, newAddress);
                    Logger.Log($"Покупатель: id {idToEdit}, новое имя {newName}, новый возраст {newAge}, новый адрес {newAddress}");
                    break;

                case "3":
                    if (!_sales.Any(s => s.GetID() == idToEdit))
                    {
                        Console.WriteLine($"Не могу найти ID: {idToEdit} в таблице продаж");
                        return;
                    }

                    Console.Write("Введите новый id животного: ");
                    if (!int.TryParse(Console.ReadLine(), out int newAnimalId) || newAnimalId < 0)
                    {
                        Console.WriteLine("ID животного должен быть неотрицательным числом.");
                        return;
                    }
                    Console.Write("Введите новый id покупателя: ");
                    if (!int.TryParse(Console.ReadLine(), out int newCustomerId) || newCustomerId < 0)
                    {
                        Console.WriteLine("ID покупателя должен быть неотрицательным числом.");
                        return;
                    }
                    Console.Write("Введите дату продажи (в формате ДД-ММ-ГГГГ): ");
                    if (!DateTime.TryParse(Console.ReadLine(), out DateTime newDate))
                    {
                        Console.WriteLine("Некорректная дата.");
                        return;
                    }
                    Console.Write("Введите цену: ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal newPrice) || newPrice < 0)
                    {
                        Console.WriteLine("Цена должна быть неотрицательным числом.");
                        return;
                    }

                    _sales[_sales.FindIndex(s => s.GetID() == idToEdit)] = new Sale(idToEdit, newAnimalId, newCustomerId, newDate, newPrice);

                    Logger.Log($"Продажа: id {idToEdit}, новый id животного {newAnimalId}, новый id покупателя {newCustomerId}, дата {newDate}, цена {newPrice}");
                    break;

                default:
                    Console.WriteLine("Wrong sheet number");
                    Logger.Log("Wrong sheet number");
                    break;
            }
        }

        /// <summary>
        /// Добавляет новое животное в базу данных на основе ввода пользователя.
        /// </summary>
        public void AddElement()
        {
            Console.WriteLine(new string('_', 100));
            Console.Write("Добавление записи в таблицу - Животные: ");

            int newAnimalId = _animals.Any() ? _animals.Max(c => c.GetID()) + 1 : 1;

            Console.Write("Введите вид животного: ");
            string newSpecies = Console.ReadLine();
            Console.Write("Введите породу животного: ");
            string newBreed = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(newSpecies) || string.IsNullOrWhiteSpace(newBreed))
            {
                Console.WriteLine("Вид и порода не могут быть пустыми.");
                return;
            }

            Animal newAnimal = new Animal(newAnimalId, newSpecies, newBreed);
            _animals.Add(newAnimal);
            Logger.Log($"Добавлено новое животное: id {newAnimalId}, вид {newSpecies}, порода {newBreed}");
        }

        /// <summary>
        /// Выполняет предопределённые запросы и выводит результаты.
        /// </summary>
        public void ExecuteQueries()
        {
            Logger.Log("Выполнение запросов.");

            var totalSphynxInJanuary2023 = _sales
                .Where(sale => _animals.Any(animal => animal.GetID() == sale.GetIdAnimals() &&
                                                     animal.GetSpecies() == "Кошка" &&
                                                     animal.GetBreed() == "Сфинкс") &&
                                                     sale.GetDate().Year == 2023 &&
                                                     sale.GetDate().Month == 1)
                .Sum(sale => sale.GetPrice());

            Console.WriteLine($"Сумма покупок кошек породы \"Сфинкс\" в январе 2023 года: {totalSphynxInJanuary2023}");

            var catBreeds = _animals
                .Where(animal => animal.GetSpecies() == "Кошка")
                .Select(animal => animal.GetBreed())
                .Distinct()
                .ToList();

            Console.WriteLine("Породы кошек в зоомагазине:");
            foreach (var breed in catBreeds)
            {
                Console.WriteLine(breed);
            }

            var animalsSoldToCustomersFromKamensk = _sales
                .Where(sale => _customers.Any(customer => customer.GetID() == sale.GetIdCustomer() &&
                                                         customer.GetAddress().Contains("Каменск-Уральский")))
                .Select(sale => _animals.First(animal => animal.GetID() == sale.GetIdAnimals()).GetSpecies())
                .Distinct()
                .ToList();

            Console.WriteLine("Виды животных, проданные покупателям из Каменска-Уральского:");
            foreach (var species in animalsSoldToCustomersFromKamensk)
            {
                Console.WriteLine(species);
            }

            var totalDogsSoldToPermCustomers = _sales
                .Where(sale => _animals.Any(animal => animal.GetID() == sale.GetIdAnimals() && animal.GetSpecies() == "Собака") &&
                               _customers.Any(customer => customer.GetID() == sale.GetIdCustomer() && customer.GetAddress().Contains("Пермь")))
                .Sum(sale => sale.GetPrice());

            Console.WriteLine($"Сумма покупок собак покупателями из Перми: {totalDogsSoldToPermCustomers}");
        }

        /// <summary>
        /// Сохраняет изменения в Excel-файл.
        /// </summary>
        public void SaveChangesToFile()
        {
            Logger.Log("Сохранение изменений в файл Excel.");
            string outputFile = @"C:\Users\Pavel\repos\Labs\LAB5\save.xlsx";

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var animalsSheet = package.Workbook.Worksheets["Животные"] ?? package.Workbook.Worksheets.Add("Животные");
                animalsSheet.Cells.Clear();
                animalsSheet.Cells[1, 1].Value = "ID";
                animalsSheet.Cells[1, 2].Value = "Species";
                animalsSheet.Cells[1, 3].Value = "Breed";
                int animalRow = 2;
                foreach (var animal in _animals)
                {
                    animalsSheet.Cells[animalRow, 1].Value = animal.GetID();
                    animalsSheet.Cells[animalRow, 2].Value = animal.GetSpecies();
                    animalsSheet.Cells[animalRow, 3].Value = animal.GetBreed();
                    animalRow++;
                }

                var customersSheet = package.Workbook.Worksheets["Покупатели"] ?? package.Workbook.Worksheets.Add("Покупатели");
                customersSheet.Cells.Clear();
                customersSheet.Cells[1, 1].Value = "ID";
                customersSheet.Cells[1, 2].Value = "Name";
                customersSheet.Cells[1, 3].Value = "Age";
                customersSheet.Cells[1, 4].Value = "Address";
                int customerRow = 2;
                foreach (var customer in _customers)
                {
                    customersSheet.Cells[customerRow, 1].Value = customer.GetID();
                    customersSheet.Cells[customerRow, 2].Value = customer.GetName();
                    customersSheet.Cells[customerRow, 3].Value = customer.GetAge();
                    customersSheet.Cells[customerRow, 4].Value = customer.GetAddress();
                    customerRow++;
                }

                var salesSheet = package.Workbook.Worksheets["Продажи"] ?? package.Workbook.Worksheets.Add("Продажи");
                salesSheet.Cells.Clear();
                salesSheet.Cells[1, 1].Value = "ID";
                salesSheet.Cells[1, 2].Value = "Animal ID";
                salesSheet.Cells[1, 3].Value = "Customer ID";
                salesSheet.Cells[1, 4].Value = "Date";
                salesSheet.Cells[1, 5].Value = "Price";
                int salesRow = 2;
                foreach (var sale in _sales)
                {
                    salesSheet.Cells[salesRow, 1].Value = sale.GetID();
                    salesSheet.Cells[salesRow, 2].Value = sale.GetIdAnimals();
                    salesSheet.Cells[salesRow, 3].Value = sale.GetIdCustomer();
                    salesSheet.Cells[salesRow, 4].Value = sale.GetDate();
                    salesSheet.Cells[salesRow, 5].Value = sale.GetPrice();
                    salesRow++;
                }

                package.SaveAs(new FileInfo(outputFile));
                Logger.Log($"Изменения сохранены в файл: {outputFile}");
            }
        }
    }
}