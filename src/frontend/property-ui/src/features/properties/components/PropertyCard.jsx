export default function PropertyCard({ property }) {
  return (
    <div className="border p-4 rounded-lg shadow-sm hover:shadow-md transition-shadow duration-200">
      <img
        src={property.image}
        alt={property.name}
        className="w-full h-48 object-cover rounded-md"
      />
      <h2 className="mt-2 text-lg font-bold">{property.name}</h2>
      <p className="text-gray-500">{property.address}</p>
      <p className="text-blue-600 font-semibold">${property.price}</p>
    </div>
  );
}
