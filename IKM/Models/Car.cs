using System.ComponentModel.DataAnnotations;

namespace IKM.Models
{
    public class Car
    {
        private int _id;
        private string _name;
        private string _name_model;

        [Key]
        public int Id { get { return _id; } set { _id = value; } }

        [Display(Name = "Фирма машины")]
        [Required(ErrorMessage = "Напишите фирму машины")]
        public string Name { get { return _name; } set { _name = value; } }


        [Display(Name = "Модель машины")]
        [Required(ErrorMessage = "Напишите модель машины")]
        public string Name_Model { get { return _name_model; } set { _name_model = value; } }


        public Car() { }

        public Car(int id, string name, string name_model)
        {
            _id = id;
            _name = name;
            _name_model = name_model;
        }
    }
}
