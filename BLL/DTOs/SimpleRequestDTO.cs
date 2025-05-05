using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class SimpleRequestDTO
    {
        public int RequestId { get; set; }
        public int UserId { get; set; }
        public int FromStationId { get; set; }
        public int ToStationId { get; set; }
        public int CargoTypeId { get; set; }
        public decimal WeightKg { get; set; }
        public decimal PreliminaryCost { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }

}
