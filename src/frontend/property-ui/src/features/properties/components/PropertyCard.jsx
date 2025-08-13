import { Link } from "react-router-dom";

export default function PropertyCard({ property }) {
  return (
    <Link to={`/properties/${property.id}`}>
      <div className="bg-white shadow-md rounded-lg overflow-hidden hover:shadow-lg transition-shadow">
        <img
          src={property.image}
          alt={property.name}
          className="w-full h-48 object-cover"
        />
        <div className="p-4">
          <h2 className="text-lg font-bold">{property.name}</h2>
          <p className="text-gray-600">{property.address}</p>
          <p className="text-green-600 font-semibold">
            ${property.price.toLocaleString()}
          </p>
        </div>
      </div>
    </Link>
  );
}
