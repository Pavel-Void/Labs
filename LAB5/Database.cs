using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;


namespace ZooshopApp
{
    class Database
    {
        private string file;
        List<Animal> animals;
        List<Customer> customers;
        List<Sale> sales;

        public Database(string file, List<Animal> animals, List<Customer> customers, List<Sale> sales )
        {
            this.file = file;
            this.animals = animals;
            this.customers = customers;
            this.sales = sales;
        }

        public List<Animal> LoadAnimals()
        {
            Logger.Log("Загрузка данных из файла Excel.");

            var animals = new List<Animal>();
            using (var package = new ExcelPackage(new FileInfo(file)))
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


        public List<Customer> LoadCustomers()
        {
            var customers = new List<Customer>();
            using (var package = new ExcelPackage(new FileInfo(file)))
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

        public List<Sale> LoadSales()
        {
            var sales = new List<Sale>();
            using (var package = new ExcelPackage(new FileInfo(file)))
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

        public void ShowAnimals() =>
            animals.ForEach(animal => Console.WriteLine($"{animal}\n{new string('_', 50)}"));

        public void ShowCustomers() =>
            customers.ForEach(customer => Console.WriteLine($"{customer}\n{new string('_', 50)}"));

        public void ShowSales() =>
            sales.ForEach(sale => Console.WriteLine($"{sale}\n{new string('_', 50)}"));

        public void DeleteElement()
        {
            string k = "";
            Console.WriteLine(new string('_', 100));
            Console.Write("Выбери таблицу: 1 - Животные, 2 - Покупатели, 3 - Продажи: ");
            k = Console.ReadLine();

            Console.Write("Enter ID to delete: ");
            int id_to_delete;
            if (!int.TryParse(Console.ReadLine(), out id_to_delete))
            {
                Console.WriteLine("Неверный ID.");
                return;
            }

            switch (k)
            {
                case "1":
                    if (!animals.Any(a => a.GetID() == id_to_delete))
                    {
                        Console.WriteLine("Животное с таким ID не найдено.");
                        return;
                    }

                    var animalsToKeep = from animal in animals
                                        where animal.GetID() != id_to_delete
                                        select animal;
                    animals = animalsToKeep.ToList();

                    var salesToRemove = from sale in sales
                                        where sale.GetIdAnimals() == id_to_delete
                                        select sale.GetID();
                    var customersToKeep = from customer in customers
                                          where !salesToRemove.Contains(customer.GetID())
                                          select customer;
                    customers = customersToKeep.ToList();

                    var remainingSales = from sale in sales
                                         where sale.GetIdAnimals() != id_to_delete
                                         select sale;
                    sales = remainingSales.ToList();

                    Logger.Log($"Removed animal with id: {id_to_delete} and all connected sales");
                    break;

                case "2":
                    if (!customers.Any(c => c.GetID() == id_to_delete))
                    {
                        Console.WriteLine("Покупатель с таким ID не найден.");
                        return;
                    }

                    var filteredCustomers = from customer in customers
                                            where customer.GetID() != id_to_delete
                                            select customer;
                    customers = filteredCustomers.ToList();

                    var filteredSalesByCustomer = from sale in sales
                                                  where sale.GetIdCustomer() != id_to_delete
                                                  select sale;
                    sales = filteredSalesByCustomer.ToList();

                    Logger.Log($"Удалён покупатель с id: {id_to_delete} и информация об его покупках");
                    break;

                case "3":
                    if (!sales.Any(s => s.GetID() == id_to_delete))
                    {
                        Console.WriteLine("Продажа с таким ID не найдена.");
                        return;
                    }

                    var remainingSalesAfterDelete = from sale in sales
                                                    where sale.GetID() != id_to_delete
                                                    select sale;
                    sales = remainingSalesAfterDelete.ToList();

                    Logger.Log($"Удалена продажа с id: {id_to_delete}");
                    break;

                default:
                    Console.WriteLine("Неправильный номер листа");
                    Logger.Log("Неправильный номер листа");
                    break;
            }
        }



        public int Input()
        {
            Logger.Log("Начало ввода");
            int input;
            string line = Console.ReadLine();
            if (!int.TryParse(line, out input))
            {
                Console.Write("Value must be digit: ");
                Logger.Log("Wrong value");
                return Input();
            }
            if (input < 0)
            {
                Console.Write("Значение не может быть отрицательным: ");
                Logger.Log("Неверное значение");
                return Input();
            }
            Logger.Log("Конец ввода");
            return input;
        }

        public void EditElement()
        {
            string k = "";
            Console.WriteLine(new string('_', 100));
            Console.Write("Выберите таблицу: 1 - Животные, 2 - Покупатели, 3 - Продажи: ");
            k = Console.ReadLine();

            Console.Write("Введите id для изменений: ");
            int id_to_edit = Input();

            string new_species;
            string new_breed;
            string new_name;
            int new_age;
            string new_address;
            
            
            switch (k)
            {
                case "1":
                    if (!animals.Any(c => c.GetID() == id_to_edit))
                    {
                        Console.WriteLine($"Не могу найти ID: {id_to_edit} в таблице с животными");
                        return;
                    }

                    Console.Write("Введите новый вид и породу животного: ");
                    new_species = Console.ReadLine();
                    new_breed = Console.ReadLine();
                    animals[animals.FindIndex(c => c.GetID() == id_to_edit)] = new Animal(id_to_edit, new_species, new_breed);
                    Logger.Log($"У животного: id {id_to_edit}, новый вид {new_species} и новая порода {new_breed}");
                    break;

                case "2":
                    if (!customers.Any(c => c.GetID() == id_to_edit))
                    {
                        Console.WriteLine($"Не могу найти ID: {id_to_edit} в таблице покупателей");
                        return;
                    }

                    Console.Write("Введите новое имя, возраст и адрес: ");
                    new_name = Console.ReadLine();
                    new_age = Int32.Parse(Console.ReadLine());
                    new_address = Console.ReadLine();
                    customers[customers.FindIndex(c => c.GetID() == id_to_edit)] = new Customer(id_to_edit, new_name, new_age, new_address);
                    Logger.Log($"Покупатель: id {id_to_edit}, новое имя {new_name}, новый возраст {new_age}, новый адрес {new_address}");
                    break;

                case "3":
                    if (!sales.Any(s => s.GetID() == id_to_edit))
                    {
                        Console.WriteLine($"Не могу найти ID: {id_to_edit} в таблице продаж");
                        return;
                    }

                    Console.Write("Введите новый id животного, id покупателя, дату продажи (в формате ДД-ММ-ГГГГ) и цену: ");
                    int new_animal_id = Int32.Parse(Console.ReadLine());
                    int new_customer_id = Int32.Parse(Console.ReadLine());
                    DateTime new_date = DateTime.Parse(Console.ReadLine());
                    decimal new_price = Decimal.Parse(Console.ReadLine());

                    sales[sales.FindIndex(s => s.GetID() == id_to_edit)] = new Sale(id_to_edit, new_animal_id, new_customer_id, new_date, new_price);

                    Logger.Log($"Продажа: id {id_to_edit}, новый id животного {new_animal_id}, новый id покупателя {new_customer_id}, дата {new_date}, цена {new_price}");
                    break;

                default:
                    Console.WriteLine("Wrong sheet number");
                    Logger.Log("Wrong sheet number");
                    break;
            }
        }

        public void AddElement()
        {
            Console.WriteLine(new string('_', 100));
            Console.Write("Добавление записи в таблицу - Животные: ");

            string new_species;
            string new_breed;
            int new_animals_id = animals.Max(c => c.GetID()) + 1;

            Console.Write("Введите вид и породу животного: ");
            new_species = Console.ReadLine();
            new_breed = Console.ReadLine();


            Animal new_animal = new Animal(new_animals_id, new_species, new_breed);
            animals.Add(new_animal);
            Logger.Log($"Добавлено новое животное: id {new_animals_id}, вид {new_species}, порода {new_breed}");
        }

        public void ExecuteQueries()
        {
            Logger.Log("Выполнение запросов.");

            // Запрос 1: Определите, на какую сумму купили кошек породы «Сфинкс» в январе 2023 года.
            var totalSphynxInJanuary2023 = sales
                .Where(sale => animals.Any(animal => animal.GetID() == sale.GetIdAnimals() &&
                                                     animal.GetSpecies() == "Кошка" &&
                                                     animal.GetBreed() == "Сфинкс") &&
                                                     sale.GetDate().Year == 2023 &&
                                                     sale.GetDate().Month == 1)
                .Sum(sale => sale.GetPrice());

            Console.WriteLine($"Сумма покупок кошек породы \"Сфинкс\" в январе 2023 года: {totalSphynxInJanuary2023}");

            // Запрос 2: Какие породы кошек есть в зоомагазине.
            var catBreeds = animals
                .Where(animal => animal.GetSpecies() == "Кошка")
                .Select(animal => animal.GetBreed())
                .Distinct()
                .ToList();

            Console.WriteLine("Породы кошек в зоомагазине:");
            foreach (var breed in catBreeds)
            {
                Console.WriteLine(breed);
            }

            // Запрос 3: Какие виды животных были проданы покупателям из Каменска-Уральского.
            var animalsSoldToCustomersFromKamensk = sales
                .Where(sale => customers.Any(customer => customer.GetID() == sale.GetIdCustomer() &&
                                                         customer.GetAddress().Contains("Каменск-Уральский")))
                .Select(sale => animals.First(animal => animal.GetID() == sale.GetIdAnimals()).GetSpecies())
                .Distinct()
                .ToList();

            Console.WriteLine("Виды животных, проданные покупателям из Каменска-Уральского:");
            foreach (var species in animalsSoldToCustomersFromKamensk)
            {
                Console.WriteLine(species);
            }

            // Запрос 4: На какую сумму купили собак покупатели из Перми.
            var totalDogsSoldToPermCustomers = sales
                .Where(sale => animals.Any(animal => animal.GetID() == sale.GetIdAnimals() && animal.GetSpecies() == "Собака") &&
                               customers.Any(customer => customer.GetID() == sale.GetIdCustomer() && customer.GetAddress().Contains("Пермь")))
                .Sum(sale => sale.GetPrice());

            Console.WriteLine($"Сумма покупок собак покупателями из Перми: {totalDogsSoldToPermCustomers}");
        }
    }
}

