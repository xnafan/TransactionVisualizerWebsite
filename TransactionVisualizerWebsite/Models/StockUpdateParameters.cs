using System.ComponentModel.DataAnnotations;
using System.Data;

namespace TransactionVisualizerWebsite.Models
{
    public class StockUpdateParameters
    {
        
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
        [Display(Name ="Product Id")]
        public int ProductId { get; set; } = 1;

        [Required]
        [Display(Name = "Quantity to order")]
        [Range(1, int.MaxValue, ErrorMessage = "Must be greater than zero")]
        public int Quantity { get; set; } = 5;

        [Required]
        [Display(Name = "Pause before update (sec.)")]
        [Range(0, int.MaxValue, ErrorMessage = "May not be negative")]
        public int PauseBeforeUpdateInSeconds { get; set; } = 5;

        [Required]
        [Display(Name = "Transaction isolation level")]
        public IsolationLevel IsolationLevel { get; set; } = IsolationLevel.Chaos;
    }
}
