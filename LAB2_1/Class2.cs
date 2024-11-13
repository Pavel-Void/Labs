using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB2
{
    // Дочерний класс
    public class DaughterClass : BaseClass
    {
        private string SecondText;

        // Конструктор с параметрами для инициализации всех полей
        public DaughterClass(string text, string SecondText) : base(text)
        {
            this.SecondText = SecondText;
        }

        // Метод для получения длины text
        public int GetTextLength()
        {
            return text.Length;
        }

        // Метод объединения text и SecondText
        public string CombineText()
        {
            return text + " " + SecondText;
        }

        // Перегрузка ToString для вывода всех полей
        public override string ToString()
        {
            return $"Text: {text}, SecondText: {SecondText}";
        }
    }
}
