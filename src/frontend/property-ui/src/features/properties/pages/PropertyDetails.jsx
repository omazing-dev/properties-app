import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { propertyService } from "../services/propertyService";

export default function PropertyDetails() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [property, setProperty] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchProperty = async () => {
      try {
        const data = await propertyService.getById(id);
        setProperty(data);
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    fetchProperty();
  }, [id]);

  if (loading) {
    return <div className="p-6 text-center">Cargando...</div>;
  }

  if (error) {
    return <div className="p-6 text-center text-red-500">Error: {error}</div>;
  }

  if (!property) {
    return <div className="p-6 text-center">Propiedad no encontrada</div>;
  }

  return (
    <div className="max-w-4xl mx-auto p-6">
      <button
        onClick={() => navigate("/properties")}
        className="mb-4 px-4 py-2 bg-gray-200 rounded hover:bg-gray-300 transition"
      >
        ← Atrás
      </button>

      <img
        src={property.mainImage}
        alt={property.name}
        className="w-full h-96 object-cover rounded-lg shadow-lg mb-6"
      />

      <h1 className="text-3xl font-bold mb-4">{property.name}</h1>

      <p className="text-gray-600 mb-2">
        <strong>Dirección:</strong> {property.address}
      </p>
      <p className="text-gray-600 mb-2">
        <strong>Precio:</strong> ${property.price.toLocaleString()}
      </p>
      <p className="text-gray-600 mb-2">
        <strong>Año:</strong> {property.year}
      </p>
      <p className="text-gray-600 mb-2">
        <strong>Código Interno:</strong> {property.codeInternal}
      </p>
      <p className="text-gray-600 mb-2">
        <strong>Propietario:</strong> {property.ownerName}
      </p>
    </div>
  );
}
