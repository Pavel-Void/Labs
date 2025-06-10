/// <file>
/// Содержит класс Animal, представляющий животное в зоомагазине.
/// </file>
using System;

namespace ZooshopApp
{
    /// <summary>
    /// Представляет животное в зоомагазине.
    /// </summary>
    public class Animal
    {
        /// <summary>Уникальный идентификатор животного.</summary>
        public int Id { get; }

        /// <summary>Вид животного (например, "Собака", "Кошка").</summary>
        public string Species { get; }

        /// <summary>Порода животного (например, "Овчарка", "Сфинкс").</summary>
        public string Breed { get; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="Animal"/>.
        /// </summary>
        /// <param name="id">Идентификатор животного.</param>
        /// <param name="species">Вид животного.</param>
        /// <param name="breed">Порода животного.</param>
        public Animal(int id, string species, string breed)
        {
            if (id < 0) throw new ArgumentOutOfRangeException(nameof(id));
            if (string.IsNullOrWhiteSpace(species)) throw new ArgumentException("Вид не может быть пустым.", nameof(species));
            if (string.IsNullOrWhiteSpace(breed)) throw new ArgumentException("Порода не может быть пустой.", nameof(breed));

            Id = id;
            Species = species;
            Breed = breed;
        }

        /// <summary>
        /// Возвращает строковое представление животного.
        /// </summary>
        public override string ToString() => $"{Id} | {Species} | {Breed}";

        /// <summary>
        /// Получить идентификатор животного.
        /// </summary>
        public int GetID() => Id;

        /// <summary>
        /// Получить вид животного.
        /// </summary>
        public string GetSpecies() => Species;

        /// <summary>
        /// Получить породу животного.
        /// </summary>
        public string GetBreed() => Breed;
    }
}