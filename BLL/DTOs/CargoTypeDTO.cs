using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class CargoTypeDTO
    {
        public int CargoTypeId { get; set; }
        public string TypeName { get; set; } = null!;
        public string? Description { get; set; }
    }
}
