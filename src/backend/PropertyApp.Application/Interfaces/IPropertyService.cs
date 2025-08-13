using PropertyApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyApp.Application.Interfaces
{
    public interface IPropertyService
    {
        Task<PaginatedResponse<PropertyDTO>> GetPropertiesAsync(
           string? name,
           string? address,
           decimal? minPrice,
           decimal? maxPrice,
           int page,
           int pageSize);

        Task<PropertyDetailDTO?> GetPropertyByIdAsync(string id);

    }
}
