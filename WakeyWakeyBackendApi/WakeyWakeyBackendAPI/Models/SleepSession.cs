using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WakeyWakeyBackendAPI.Models
{
    [Table("SleepSessions")]
    public class SleepSession
    {
        [Key]
        [Required]
        public required int SessionId { get; set; }
        
        [Required]
        [ForeignKey(nameof(User))]
        public required int UserId { get; set; }
        
        [Required]
        public required DateTime StartTime { get; set; }
        
        [Required]
        public required DateTime EndTime { get; set; }
        
        [Required]
        public required int SleepScore { get; set; }
        
        public virtual required User User { get; set; }
    }
}
