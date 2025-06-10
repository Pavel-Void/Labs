using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IKM.Models
{
    public class DriverProperty
    {
        private int _id;
        private int _driverId;
        private string _propertyType;
        private string _address;
        private float _area;
        private decimal _estimatedValue;
        private bool _isMortgaged;
        private short _ownershipPercentage;
        private int _buildingYear;
        private string _propertyId;
        private byte[]? _additionalInfo;

        [Key]
        public int Id { get { return _id; } set { _id = value; } }

        [Required]
        [ForeignKey("Driver")]
        [Display(Name = "Id Водителя")]
        public int Driver_ID { get { return _driverId; } set { _driverId = value; } }

        [Display(Name = "Тип недвижимости")]
        [Required(ErrorMessage = "Укажите тип недвижимости")]
        [MaxLength(50)]
        public string PropertyType { get { return _propertyType; } set { _propertyType = value; } }

        [Display(Name = "Адрес недвижимости")]
        [Required(ErrorMessage = "Укажите адрес недвижимости")]
        public string Address { get { return _address; } set { _address = value; } }

        [Display(Name = "Площадь недвижимости")]
        [Required(ErrorMessage = "Укажите площадь недвижимости")]
        public float Area { get { return _area; } set { _area = value; } }

        [Display(Name = "Оценочная стоимость")]
        [Required(ErrorMessage = "Укажите оценочную стоимость")]
        public decimal EstimatedValue { get { return _estimatedValue; } set { _estimatedValue = value; } }

        [Display(Name = "В ипотеке?")]
        [Required]
        public bool IsMortgaged { get { return _isMortgaged; } set { _isMortgaged = value; } }

        [Display(Name = "Процент владения")]
        [Required(ErrorMessage = "Укажите процент владения")]
        public short OwnershipPercentage { get { return _ownershipPercentage; } set { _ownershipPercentage = value; } }

        [Display(Name = "Год постройки")]
        [Required(ErrorMessage = "Укажите год постройки")]
        public int BuildingYear { get { return _buildingYear; } set { _buildingYear = value; } }

        [Display(Name = "Уникальный идентификатор недвижимости")]
        [Required(ErrorMessage = "Укажите уникальный идентификатор недвижимости")]
        [MaxLength(15)]
        public string PropertyID { get { return _propertyId; } set { _propertyId = value; } }

        [Display(Name = "Дополнительная информация")]
        public byte[]? AdditionalInfo { get { return _additionalInfo; } set { _additionalInfo = value; } }

        public Driver? Driver { get; set; }

        public DriverProperty() { }

        public DriverProperty(int id, int driverId, string propertyType, string address, float area, decimal estimatedValue,
            bool isMortgaged, short ownershipPercentage, int buildingYear, string propertyId, byte[]? additionalInfo)
        {
            _id = id;
            _driverId = driverId;
            _propertyType = propertyType;
            _address = address;
            _area = area;
            _estimatedValue = estimatedValue;
            _isMortgaged = isMortgaged;
            _ownershipPercentage = ownershipPercentage;
            _buildingYear = buildingYear;
            _propertyId = propertyId;
            _additionalInfo = additionalInfo;
        }
    }
}