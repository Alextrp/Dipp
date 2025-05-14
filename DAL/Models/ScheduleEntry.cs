using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ScheduleEntry
    {
        [Key]
        public int ScheduleEntryId { get; set; }

        [Required]
        public int RequestId { get; set; }

        [Required]
        public int StationId { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        [Required]
        public TimeSpan Downtime { get; set; } // Время простоя на станции

        [ForeignKey("RequestId")]
        public virtual Request Request { get; set; } = null!;

        [ForeignKey("StationId")]
        public virtual Station Station { get; set; } = null!;
    }

}
