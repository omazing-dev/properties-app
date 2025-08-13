import { useState } from "react";

export default function Filters({ filters, setFilters }) {
  const [localFilters, setLocalFilters] = useState({
    name: filters.name || "",
    address: filters.address || "",
    minPrice: filters.minPrice || "",
    maxPrice: filters.maxPrice || ""
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setLocalFilters((prev) => ({
      ...prev,
      [name]: value
    }));
  };

  const handleApply = () => {
    setFilters({
      ...filters,
      page: 1, 
      ...localFilters
    });
  };

  const handleClear = () => {
    const cleared = { name: "", address: "", minPrice: "", maxPrice: "" };
    setLocalFilters(cleared);
    setFilters({ ...filters, page: 1, ...cleared });
  };

  return (
    <div className="p-4 bg-white rounded-lg shadow-md mb-6 flex flex-wrap gap-4 items-end">
      <div>
        <label className="block text-sm font-medium">Nombre propiedad</label>
        <input
          type="text"
          name="name"
          value={localFilters.name}
          onChange={handleChange}
          className="border rounded-md px-3 py-1 w-48"
          placeholder="Ej: Casa del Sol"
        />
      </div>

      <div>
        <label className="block text-sm font-medium">Dirección</label>
        <input
          type="text"
          name="address"
          value={localFilters.address}
          onChange={handleChange}
          className="border rounded-md px-3 py-1 w-48"
          placeholder="Ej: Calle 123"
        />
      </div>

      <div>
        <label className="block text-sm font-medium">Precio mín</label>
        <input
          type="number"
          name="minPrice"
          value={localFilters.minPrice}
          onChange={handleChange}
          className="border rounded-md px-3 py-1 w-28"
        />
      </div>

      <div>
        <label className="block text-sm font-medium">Precio máx</label>
        <input
          type="number"
          name="maxPrice"
          value={localFilters.maxPrice}
          onChange={handleChange}
          className="border rounded-md px-3 py-1 w-28"
        />
      </div>

      <button
        onClick={handleApply}
        className="bg-blue-600 text-white px-4 py-2 rounded-md hover:bg-blue-700"
      >
        Aplicar
      </button>

      <button
        onClick={handleClear}
        className="bg-gray-200 px-4 py-2 rounded-md hover:bg-gray-300"
      >
        Limpiar
      </button>
    </div>
  );
}
