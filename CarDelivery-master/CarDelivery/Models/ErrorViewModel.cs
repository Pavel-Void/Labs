namespace CarDelivery.Models
{
    /// <summary>
    /// Модель представления ошибки.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Идентификатор запроса.
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// Показывать ли идентификатор запроса.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
