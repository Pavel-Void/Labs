using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDelivery.Models;

/// <summary>
/// Модель автомобиля.
/// </summary>
public partial class Cars
{
    /// <summary>
    /// Идентификатор автомобиля.
    /// </summary>
    [Key]
    public int Carid { get; set; }
    /// <summary>
    /// Марка автомобиля.
    /// </summary>
    [Required]
    public string Make { get; set; } = null!;
    /// <summary>
    /// Модель автомобиля.
    /// </summary>
    [Required]
    public string Model { get; set; } = null!;
    /// <summary>
    /// Идентификатор комплектации.
    /// </summary>
    [Required]
    public int Complectationid { get; set; }
    /// <summary>
    /// Цена автомобиля.
    /// </summary>
    [Required]
    public int Price { get; set; }

    [ForeignKey("Complectationid")]
    [NotMapped]
    [ValidateNever]
    public virtual Complectations Complectations { get; set; }

    /// <summary>
    /// Заказы, связанные с автомобилем.
    /// </summary>
    public ICollection<Orders> Orders { get; set; } = new List<Orders>();
}
