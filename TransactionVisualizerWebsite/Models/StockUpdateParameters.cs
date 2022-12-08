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

            Snapshot = IsolationLevel.Snapshot, 

            Serializable = IsolationLevel.Serializable
        }
        [Required]
        [Display(Name ="Product Id")]
        public int ProductId { get; set; } = 1;

        [Required]
        [Display(Name = "Change in inventory")]
        public int StockChangeAmount { get; set; } = 5;

        [Required]
        [Display(Name = "Pause between read and update (in seconds)")]
        public int PauseBeforeUpdateInSeconds { get; set; } = 5;

        [Required]
        [Display(Name = "Transaction isolation level")]
        public IsolationLevel IsolationLevel { get; set; } = IsolationLevel.Chaos;
    }
}
