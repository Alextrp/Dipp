using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class RequestDTO
    {
        public int RequestId { get; set; }
        public string CargoName { get; set; } = null!;
        public string RouteName { get; set; } = null!;
        public DateTime RequestDate { get; set; }
        public decimal? Cost { get; set; }
        public string? PaymentStatus { get; set; }

        public string RequestStatus { get; set; }
    }
}
