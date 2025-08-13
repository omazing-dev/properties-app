import { Link } from "react-router-dom";

export default function PropertyCard({ property }) {
  return (
    <Link
      to={`/properties/${property.id}`}
      className="block bg-white rounded-lg shadow hover:shadow-lg transition overflow-hidden"
    >
      <img
        src={property.mainImage || "https://via.placeholder.com/400x300?text=Sin+Imagen"}
        alt={property.name}
        className="w-full h-48 object-cover"
      />
      <div className="p-4">
        <h2 className="text-lg font-semibold mb-2">{property.name}</h2>
        <p className="text-gray-600 text-sm mb-2">{property.address}</p>
        <p className="text-gray-800 font-bold">
          ${property.price?.toLocaleString()}
        </p>
      </div>
    </Link>
  );
}
