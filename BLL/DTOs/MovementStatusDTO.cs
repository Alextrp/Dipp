using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class MovementStatusDTO
    {
        public int MovementStatusId { get; set; }
        public DateTime StatusDate { get; set; }
        public string? StatusDescription { get; set; }
        public string? CurrentStation { get; set; }
    }
}
