using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IKM.Models
{
    public class Racing
    {
        private int _id;
        private string _name;
        private int _driver_id;
        private int _car_id;
        private int _finished;

        [Key]
        public int Id { get { return _id; } set { _id = value; } }


        [Display(Name = "Название гонки")]
        [Required(ErrorMessage = "Введите название гонки")]
        public string Name { get { return _name; } set { _name = value; } }


        [Display(Name = "Водитель")]
        [Required(ErrorMessage = "Выберите водителя")]
        public int Driver_ID { get { return _driver_id; } set { _driver_id = value; } }


        [ForeignKey("Driver_ID")]
        public Driver Driver { get; set; }

        [Display(Name = "Машина")]
        [Required(ErrorMessage = "Выберите машину")]
        public int Car_ID { get { return _car_id; } set { _car_id = value; } }

        [ForeignKey("Car_ID")]
        public Car Car { get; set; }


        [Display(Name = "Место")]
        public int Finished { get { return _finished; } set { _finished = value; } }


        public Racing() { }

        public Racing(int id, string name, int driver_id, int car_id, int finished)
        {
            _id = id;
            _name = name;
            _driver_id = driver_id;
            _car_id = car_id;
            _finished = finished;
        }
    }
}