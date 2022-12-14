using System.ComponentModel.DataAnnotations;
using System.Data;

namespace TransactionVisualizerWebsite.Models
{
    public class StockUpdateParameters
    {
        private static IsolationLevel isolationLevel = IsolationLevel.ReadCommitted;
        private static int maxAttempts = 1;
        private static int pauseBeforeUpdateInSeconds = 5;
        private static readonly int productId = 1;
        public enum ValidIsolationLevels
        {
            [Display(Name = "Read uncommitted")]
            ReadUncommitted = IsolationLevel.ReadUncommitted,

            [Display(Name = "Read committed")]
            ReadCommitted = IsolationLevel.ReadCommitted,

            [Display(Name = "Repeatable read")]
            RepeatableRead = IsolationLevel.RepeatableRead,

            Serializable = IsolationLevel.Serializable
        }
        [Required]
        [Display(Name = "Product Id")]
        public int ProductId => productId;
        [Required]
        [Display(Name = "Quantity to order")]
        [Range(1, int.MaxValue, ErrorMessage = "Must be greater than zero")]
        public int Quantity { get; set; } = 5;

        [Required]
        [Display(Name = "Max attempts")]
        [Range(1, int.MaxValue, ErrorMessage = "Must be greater than zero")]
        public int MaxAttempts { get => maxAttempts; set => maxAttempts = value; }
        [Required]
        [Display(Name = "Pause before update (sec.)")]
        [Range(0, int.MaxValue, ErrorMessage = "May not be negative")]
        public int PauseBeforeUpdateInSeconds { get => pauseBeforeUpdateInSeconds; set => pauseBeforeUpdateInSeconds = value; }
        [Required]
        [Display(Name = "Transaction isolation level")]
        public IsolationLevel IsolationLevel { get => isolationLevel; set => isolationLevel = value; } 
    }
}
