using PropertyApp.Domain.Common;

namespace PropertyApp.Domain.Entities
{
    public class PropertyImage : BaseEntity
    {
        public string File { get; set; } = string.Empty;
        public bool Enabled { get; set; }

        public string IdProperty { get; set; } = string.Empty;

    }
}
