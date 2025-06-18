namespace CarDelivery.Models;

/// <summary>
/// Модель пользователя.
/// </summary>
public partial class Users
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public int Userid { get; set; }
    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string Name { get; set; } = null!;
    /// <summary>
    /// Фамилия пользователя.
    /// </summary>
    public string Lastname { get; set; } = null!;
    /// <summary>
    /// Email пользователя.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Заказы пользователя.
    /// </summary>
    public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>();
}
