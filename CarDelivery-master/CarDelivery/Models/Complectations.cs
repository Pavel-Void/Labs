using System.ComponentModel.DataAnnotations;

namespace CarDelivery.Models;

/// <summary>
/// Модель комплектации.
/// </summary>
public partial class Complectations
{
    /// <summary>
    /// Идентификатор комплектации.
    /// </summary>
    [Key]
    public int Complectationid { get; set; }
    /// <summary>
    /// Название комплектации.
    /// </summary>
    [Required]
    public string Name { get; set; } = null!;
    /// <summary>
    /// Оборудование комплектации.
    /// </summary>
    [Required]
    public string Equipment { get; set; } = null!;
    /// <summary>
    /// Двигатель комплектации.
    /// </summary>
    [Required]
    public string Engine { get; set; } = null!;

    /// <summary>
    /// Автомобили, связанные с комплектацией.
    /// </summary>
    public ICollection<Cars> Cars { get; set; } = new List<Cars>();
}
