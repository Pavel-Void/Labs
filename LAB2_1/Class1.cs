using System;

namespace LAB2
{
    public class BaseClass
    {
        protected string _text;

        public BaseClass(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException(nameof(text), "Текст не может быть нулевым или пустым");

            _text = text;
        }

        public BaseClass(BaseClass other) : this(other?._text ?? throw new ArgumentNullException(nameof(other)))
        {
        }

        public virtual void AddExclamations() => _text = "!!!" + _text;

        public override string ToString() => $"Text: {_text}";
    }
}