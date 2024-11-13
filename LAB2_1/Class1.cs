using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB2
{
    public class BaseClass
    {
        protected string text;

        // Конструктор с параметром
        public BaseClass(string text)
        {
            this.text = text;
        }

        // Конструктор копирования
        public BaseClass(BaseClass other)
        {
            text = other.text;
        }

        // Метод добавления трех восклицательных знаков
        public void AddExclamations()
        {
            text = "!!!" + text;
        }

        // Перегрузка ToString
        public override string ToString()
        {
            return $"Text: {text}";
        }
    }
}
