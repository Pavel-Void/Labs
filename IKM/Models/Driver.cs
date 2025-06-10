using System.ComponentModel.DataAnnotations;

namespace IKM.Models
{
    public class Driver
    {
        private int _id;
        private string _name;
        private int _age;

        [Key]
        public int Id { get { return _id; } set { _id = value; } }

        [Display(Name = "Имя водителя")]
        [Required(ErrorMessage = "Напишите имя водителя")]
        public string Name { get { return _name; } set { _name = value; } }


        [Display(Name = "Возраст водителя")]
        [Required(ErrorMessage = "Напишите возраст водителя")]
        public int Age { get { return _age; } set { _age = value; } }


        public Driver() { }

        public Driver(int id, string name, int age)
        {
            _id = id;
            _name = name;
            _age = age;
        }
    }
}
