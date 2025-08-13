using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyApp.Application.DTOs
{
    public class PropertyDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string IdOwner { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}
