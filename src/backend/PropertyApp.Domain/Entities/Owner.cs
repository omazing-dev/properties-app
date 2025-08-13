using PropertyApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyApp.Domain.Entities
{
    public class Owner : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public DateTime Birthday { get; set; } 
    }
}
