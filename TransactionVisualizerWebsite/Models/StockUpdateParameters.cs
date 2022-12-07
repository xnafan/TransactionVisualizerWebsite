using System.ComponentModel.DataAnnotations;
using System.Data;

namespace TransactionVisualizerWebsite.Models
{
    public class StockUpdateParameters
    {
        
        public enum ValidIsolationLevels
        {
            ReadUncommitted = IsolationLevel.ReadUncommitted, 
            ReadCommitted = IsolationLevel.ReadCommitted, 
            RepeatableRead = IsolationLevel.RepeatableRead, 
            Snapshot = IsolationLevel.Snapshot, 
            Serializable = IsolationLevel.Serializable
        }
        [Required]
        public int ProductId { get; set; } = 1;
        [Required]
        public int StockChangeAmount { get; set; } = 5;
        [Required]
        public int PauseBeforeUpdateInSeconds { get; set; } = 5;
        [Required]
        public IsolationLevel IsolationLevel { get; set; } = IsolationLevel.Chaos;
    }
}
