using System;

namespace LAB2
{
    public class DaughterClass : BaseClass
    {
        private string _secondText;

        public DaughterClass(string text, string secondText) : base(text)
        {
            if (string.IsNullOrWhiteSpace(secondText))
                throw new ArgumentNullException(nameof(secondText), "Второй текст не может быть нулевым или пустым");

            _secondText = secondText;
        }

        public int GetTextLength() => _text.Length;

        public string CombineText() => $"{_text} {_secondText}";

        public override void AddExclamations()
        {
            base.AddExclamations();
            _secondText = "!!!" + _secondText;
        }

        public override string ToString() => $"Text: {_text}, SecondText: {_secondText}";
    }
}