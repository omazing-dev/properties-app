using PropertyApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyApp.Domain.Entities
{
    public class PropertyTrace : BaseEntity
    {
        public DateTime DateSale { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public decimal Tax { get; set; }

        public string IdProperty { get; set; } = string.Empty;
    }
}
