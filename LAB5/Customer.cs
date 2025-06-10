/// <file>
/// Содержит класс Customer, представляющий покупателя в зоомагазине.
/// </file>
using System;

namespace ZooshopApp
{
    /// <summary>
    /// Представляет покупателя в зоомагазине.
    /// </summary>
    public class Customer
    {
        private int _id;
        private string _name;
        private int _age;
        private string _address;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Customer"/>.
        /// </summary>
        /// <param name="id">Идентификатор покупателя.</param>
        /// <param name="name">Имя покупателя.</param>
        /// <param name="age">Возраст покупателя.</param>
        /// <param name="address">Адрес покупателя.</param>
        public Customer(int id, string name, int age, string address)
        {
            if (id < 0) throw new ArgumentOutOfRangeException(nameof(id));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Имя не может быть пустым.", nameof(name));
            if (age < 0) throw new ArgumentOutOfRangeException(nameof(age));
            if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Адрес не может быть пустым.", nameof(address));

            _id = id;
            _name = name;
            _age = age;
            _address = address;
        }

        /// <summary>
        /// Получить идентификатор покупателя.
        /// </summary>
        public int GetID() => _id;

        /// <summary>
        /// Получить имя покупателя.
        /// </summary>
        public string GetName() => _name;

        /// <summary>
        /// Получить возраст покупателя.
        /// </summary>
        public int GetAge() => _age;

        /// <summary>
        /// Получить адрес покупателя.
        /// </summary>
        public string GetAddress() => _address;

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{_id} | {_name} | {_age} | {_address}";
        }
    }
}
