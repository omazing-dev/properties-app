import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import { BrowserRouter as Router, Routes, Route, Navigate } from "react-router-dom";
import PropertiesList from "./features/properties/pages/PropertiesList";
import PropertyDetail from "./features/properties/pages/PropertyDetails";

function App() {
  const [count, setCount] = useState(0)

  return (
     <Router>
      <div className="min-h-screen bg-gray-50">
        <Routes>
         
          <Route path="/" element={<Navigate to="/properties" />} />
          <Route path="/properties" element={<PropertiesList />} />
          <Route path="/properties/:id" element={<PropertyDetail />} />
        </Routes>
      </div>
    </Router>
  )
}

export default App
