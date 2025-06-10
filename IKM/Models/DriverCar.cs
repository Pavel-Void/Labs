using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IKM.Models
{
    public class DriverCar
    {
        [Key]
        [Column("Driver_ID")]
        public int Driver_ID { get; set; }

        [Key]
        [Column("Car_ID")]
        public int Car_ID { get; set; }


        [ForeignKey("Driver_ID")]
        public Driver Driver { get; set; }

        [ForeignKey("Car_ID")]
        public Car Car { get; set; }

        public DriverCar() { }

        public DriverCar(int driverId, int carId)
        {
            Driver_ID = driverId;
            Car_ID = carId;
        }
    }
}