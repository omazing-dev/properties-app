using PropertyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyApp.Application.Interfaces
{
    public interface IPropertyRepository
    {
        Task<(IEnumerable<(PropertyE Property, string? Image)>, int Total)> GetFilteredAsync(
            string? name,
            string? address,
            decimal? minPrice,
            decimal? maxPrice,
            int page,
            int pageSize);

        Task<(PropertyE Property, string? OwnerName, string? Image)?> GetByIdAsync(string id);
    }
}
