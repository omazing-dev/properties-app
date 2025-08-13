using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyApp.Application.DTOs
{
    public class PropertyDetailDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Year { get; set; }
        public string CodeInternal { get; set; } = string.Empty;

        public string OwnerName { get; set; } = string.Empty;
        public string? MainImage { get; set; }
    }
}
