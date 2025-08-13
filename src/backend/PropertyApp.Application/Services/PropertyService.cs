using MongoDB.Driver;
using PropertyApp.Application.DTOs;
using PropertyApp.Application.Interfaces;
using PropertyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyApp.Application.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;

        public PropertyService(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<PaginatedResponse<PropertyDTO>> GetPropertiesAsync(
            string? name,
            string? address,
            decimal? minPrice,
            decimal? maxPrice,
            int page,
            int pageSize)
        {
            var (propertiesWithImages, total) = await _propertyRepository.GetFilteredAsync(
                name, address, minPrice, maxPrice, page, pageSize);

            var items = propertiesWithImages.Select(p => new PropertyDTO
            {
                Id = p.Property.Id,
                Name = p.Property.Name,
                Address = p.Property.Address,
                Price = p.Property.Price,
                IdOwner = p.Property.IdOwner,
                Image = p.Image ?? string.Empty
            }).ToList();

            return new PaginatedResponse<PropertyDTO>
            {
                Items = items,
                Total = total,
                Page = page,
                PageSize = pageSize
            };
        }
        public async Task<PropertyDetailDTO?> GetPropertyByIdAsync(string id)
        {
            var result = await _propertyRepository.GetByIdAsync(id);
            if (result == null)
                return null;

            var (property, ownerName, image) = result.Value;

            return new PropertyDetailDTO
            {
                Id = property.Id,
                Name = property.Name,
                Address = property.Address,
                Price = property.Price,
                Year = property.Year,
                CodeInternal = property.CodeInternal,
                OwnerName = ownerName ?? string.Empty,
                MainImage = image ?? string.Empty
            };
        }
    }
}

