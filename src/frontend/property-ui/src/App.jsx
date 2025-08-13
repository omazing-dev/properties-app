import { BrowserRouter as Router, Routes, Route, Navigate } from "react-router-dom";
import PropertiesList from "./features/properties/pages/PropertiesList";
import PropertyDetails from "./features/properties/pages/PropertyDetails"; 

function App() {
  return (
    <Router>
      <div className="min-h-screen bg-gray-50">
        <Routes>
          <Route path="/" element={<Navigate to="/properties" />} />
          <Route path="/properties" element={<PropertiesList />} />
          <Route path="/properties/:id" element={<PropertyDetails />} /> 
        </Routes>
      </div>
    </Router>
  );
}

export default App;
