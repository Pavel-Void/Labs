/// <file>
/// Содержит класс Sale, представляющий сделку (продажу) в зоомагазине.
/// </file>
using System;

namespace ZooshopApp
{
    /// <summary>
    /// Представляет сделку (продажу) в зоомагазине.
    /// </summary>
    public class Sale
    {
        private int _id;
        private int _animalId;
        private int _customerId;
        private DateTime _date;
        private decimal _price;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Sale"/>.
        /// </summary>
        /// <param name="id">Идентификатор продажи.</param>
        /// <param name="animalId">Идентификатор животного.</param>
        /// <param name="customerId">Идентификатор покупателя.</param>
        /// <param name="date">Дата продажи.</param>
        /// <param name="price">Цена продажи.</param>
        public Sale(int id, int animalId, int customerId, DateTime date, decimal price)
        {
            if (id < 0) throw new ArgumentOutOfRangeException(nameof(id));
            if (animalId < 0) throw new ArgumentOutOfRangeException(nameof(animalId));
            if (customerId < 0) throw new ArgumentOutOfRangeException(nameof(customerId));
            if (price < 0) throw new ArgumentOutOfRangeException(nameof(price));

            _id = id;
            _animalId = animalId;
            _customerId = customerId;
            _date = date;
            _price = price;
        }

        /// <summary>
        /// Получить идентификатор продажи.
        /// </summary>
        public int GetID() => _id;

        /// <summary>
        /// Получить идентификатор животного.
        /// </summary>
        public int GetIdAnimals() => _animalId;

        /// <summary>
        /// Получить идентификатор покупателя.
        /// </summary>
        public int GetIdCustomer() => _customerId;

        /// <summary>
        /// Получить дату продажи.
        /// </summary>
        public DateTime GetDate() => _date;

        /// <summary>
        /// Получить цену продажи.
        /// </summary>
        public decimal GetPrice() => _price;

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{_id} | {_animalId} | {_customerId} | {_date} | {_price}";
        }
    }
}
