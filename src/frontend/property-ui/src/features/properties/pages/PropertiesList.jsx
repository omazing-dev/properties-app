import { useProperties } from "../hooks/useProperties";
import PropertyCard from "../components/PropertyCard";
import Pagination from "../components/Pagination";
import Filters from "../components/Filters";

export default function PropertiesList() {
  const { properties, loading, error, total, filters, setFilters } = useProperties({
    page: 1,
    pageSize: 6
  });

  if (loading) return <p>Cargando propiedades...</p>;
  if (error) return <p className="text-red-500">{error}</p>;

  return (
    <div className="space-y-6">

      <Filters filters={filters} setFilters={setFilters} />

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4 p-8">
        {properties.map((property) => (
          <PropertyCard key={property.id} property={property} />
        ))}
      </div>

      <Pagination
        currentPage={filters.page}
        totalPages={Math.ceil(total / filters.pageSize)}
        onPageChange={(page) => setFilters({ ...filters, page })}
      />
    </div>
  );
}
