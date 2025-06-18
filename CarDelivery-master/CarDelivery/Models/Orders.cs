using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CarDelivery.Models;

/// <summary>
/// Модель заказа.
/// </summary>
public partial class Orders
{
    /// <summary>
    /// Идентификатор заказа.
    /// </summary>
    public int Orderid { get; set; }
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public int Userid { get; set; }
    /// <summary>
    /// Идентификатор автомобиля.
    /// </summary>
    public int Carid { get; set; }
    /// <summary>
    /// Количество.
    /// </summary>
    public int Quantity { get; set; }

    [ValidateNever]
    public virtual Cars Cars { get; set; } = null!;

    [ValidateNever]
    public virtual Users Users { get; set; } = null!;
}
