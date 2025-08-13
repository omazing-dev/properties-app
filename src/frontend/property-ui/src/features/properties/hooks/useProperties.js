import { useEffect, useState } from "react";
import { propertyService } from "../services/propertyService";

export const useProperties = (initialFilters) => {
  const [properties, setProperties] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [total, setTotal] = useState(0);

  const [filters, setFilters] = useState(initialFilters);

  const fetchProperties = async () => {
    setLoading(true);
    setError(null);

    try {
      // 🔹 Filtramos los undefined, null o strings vacíos
      const cleanedFilters = Object.fromEntries(
        Object.entries(filters).filter(([_, v]) => v !== undefined && v !== null && v !== "")
      );

      console.log("⚙️ Filtros usados en fetch:", cleanedFilters);

      const data = await propertyService.getAll(cleanedFilters);

      setProperties(data.items);
      setTotal(data.total);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchProperties();
  }, [filters]);

  return {
    properties,
    loading,
    error,
    total,
    filters,
    setFilters,
    refetch: fetchProperties
  };
};
